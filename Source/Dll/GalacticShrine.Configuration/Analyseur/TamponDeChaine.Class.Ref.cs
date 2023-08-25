/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System.Collections.Generic;
using System.IO;
using GalacticShrine.Configuration.Properties;

namespace GalacticShrine.Configuration.Analyseur {

  public sealed class TamponDeChaine {

    TextReader SourceDeDonnees;
    List<char> Tampon;
    Plage IndicesDesTampons;

    public struct Plage {

      int Demarrages, Tailles;

      public int Demarrage {

        get => Demarrages;
        set => Demarrages = value < 0 ? 0 : value;
      }

      public int Taille {

        get => Tailles;
        set => Tailles = value < 0 ? 0 : value;
      }

      public int Fin => Tailles <= 0 ? 0 : Demarrages + (Tailles - 1);

      public bool EstVide => Tailles == 0;

      public void Reinitialiser() {

        Demarrage = 0;
        Taille = 0;
      }

      public static Plage DeL_IndexA_LaTaille(int Demarrage, int Taille) {
        
        if (Demarrage < 0 || Taille <= 0)
          return new Plage();

        return new Plage { Demarrage = Demarrage, Taille = Taille };
      }

      public static Plage AvecIndex(int Demarrage, int Fin) {
        
        if (Demarrage < 0 || Fin < 0 || Fin - Demarrage < 0) 
          return new Plage();
        
        return new Plage { Demarrage = Demarrage, Taille = Fin - Demarrage + 1 };
      }

      public override string ToString() => string.Format(Resources.TamponDeChaineToString, Demarrage, Fin, Taille);
    }
    
    readonly static int CapaciteParDefaut = 256;
    
    public TamponDeChaine() : this(capacity: CapaciteParDefaut) { }

    public TamponDeChaine(int capacity) => Tampon = new List<char>(capacity: capacity);

    internal TamponDeChaine(List<char> Tampons, Plage IndiceDesTampons) {

      Tampon = Tampons;
      IndicesDesTampons = IndiceDesTampons;
    }

    public int Compter => IndicesDesTampons.Taille;

    public bool EstVide => IndicesDesTampons.EstVide;

    public bool EstUnEspaceBlanc {

      get {

        int IndexDeDemarrage = IndicesDesTampons.Demarrage;
        while(IndexDeDemarrage <= IndicesDesTampons.Fin && char.IsWhiteSpace(c: Tampon[index: IndexDeDemarrage])) { IndexDeDemarrage++; }
        return IndexDeDemarrage > IndicesDesTampons.Fin;
      }
    }

    public char this[int idx] => Tampon[index: idx + IndicesDesTampons.Demarrage];

    public TamponDeChaine ChangementsDansLesRejets() {
      
      IndicesDesTampons = Plage.DeL_IndexA_LaTaille(Demarrage: 0, Taille: Tampon.Count);
      return this;
    }

    public Plage TrouverSousChaine(string SousChaine, int IndexDeDemarrage = 0) {
      
      int LongueurDeLaSousChaine = SousChaine.Length;
      
      if (LongueurDeLaSousChaine <= 0 || Compter < LongueurDeLaSousChaine) {
        
        return new Plage();
      }
      
      IndexDeDemarrage += IndicesDesTampons.Demarrage;

      // Rechercher le premier caractère de la sous-chaîne
      for(int PremierCaractereIdx = IndexDeDemarrage; PremierCaractereIdx <= IndicesDesTampons.Fin; ++PremierCaractereIdx) {
        
        if (Tampon[index: PremierCaractereIdx] != SousChaine[index: 0])
          continue;

        // Échec maintenant si la sous-chaîne ne peut pas tenir compte de la taille des et de l'index de début de recherche
        if (PremierCaractereIdx + LongueurDeLaSousChaine - 1 > IndicesDesTampons.Fin) 
          return new Plage();
        
        bool EstLaNonConcordanceDesChaines = false;
        // Vérifier si la Sous-Chaîne correspond au Démarrage à l'index

        for(int currentIdx = 0; currentIdx < LongueurDeLaSousChaine; ++currentIdx) {
          
          if (Tampon[index: PremierCaractereIdx + currentIdx] != SousChaine[index: currentIdx]) {
            
            EstLaNonConcordanceDesChaines = true;
            break;
          }
        }
        
        if (EstLaNonConcordanceDesChaines)
          continue;
        
        return Plage.DeL_IndexA_LaTaille(Demarrage: PremierCaractereIdx - IndicesDesTampons.Demarrage, Taille: LongueurDeLaSousChaine);
      }
      return new Plage();
    }
    
    public bool LigneDeLecture() {
      
      if (SourceDeDonnees == null)
        return false;
      
      Tampon.Clear();
      int c = SourceDeDonnees.Read();
      
      // Read until new line ('\n') or EOF (-1)
      while (c != '\n' && c != -1) {
        
        if (c != '\r') {

          Tampon.Add(item: (char)c);
        }
        
        c = SourceDeDonnees.Read();
      }
      
      IndicesDesTampons = Plage.DeL_IndexA_LaTaille(Demarrage: 0, Taille: Tampon.Count);
      
      return Tampon.Count > 0 || c != -1;
    }
    
    public void Reinitialiser(TextReader DonneesSource) {
      
      SourceDeDonnees = DonneesSource;
      IndicesDesTampons.Reinitialiser();
      Tampon.Clear();
    }

    public void Redimensionner(Plage Plage) => Redimensionner(Plage.Demarrage, Plage.Taille);

    public void Redimensionner(int NouvelleTaille) => Redimensionner(0, NouvelleTaille);

    public void Redimensionner(int IndexDeDemarrage, int Taille) {
      
      if (IndexDeDemarrage < 0 || Taille < 0)
        return;
      
      var IndexDeDemarrageInterne = IndicesDesTampons.Demarrage + IndexDeDemarrage;
      var IndexDeFinInterne = IndexDeDemarrageInterne + Taille - 1;
      
      if (IndexDeFinInterne > IndicesDesTampons.Fin)
        return;
      
      IndicesDesTampons.Demarrage = IndexDeDemarrageInterne;
      IndicesDesTampons.Taille = Taille;
    }

    public void RedimensionnerEntreLesIndex(int IndexDeDemarrage, int IndexDeFin) => Redimensionner(IndexDeDemarrage: IndexDeDemarrage, Taille: IndexDeFin - IndexDeDemarrage + 1);

    public TamponDeChaine SousChaine(Plage Plage) {

      var Copie = AvalezCopie();
      Copie.Redimensionner(Plage: Plage);
      return Copie;
    }

    public TamponDeChaine AvalezCopie() => new(Tampons: Tampon, IndiceDesTampons: IndicesDesTampons);

    public void DemarrageGarniture() {

      if (EstVide)
        return;

      int IndexDeDemarrage = IndicesDesTampons.Demarrage;
      while(IndexDeDemarrage <= IndicesDesTampons.Fin && char.IsWhiteSpace(c: Tampon[index: IndexDeDemarrage])) {
        
        IndexDeDemarrage++;
      }

      // Nous devons faire une copie de cette valeur car IndicesDesTampons.Fin est une propriété calculée,
      // donc elle changera si nous modifions IndicesDesTampons.Demarrage ou IndicesDesTampons.Taille
      int IndexDeFin = IndicesDesTampons.Fin;
      
      IndicesDesTampons.Demarrage = IndexDeDemarrage;
      IndicesDesTampons.Taille = IndexDeFin - IndexDeDemarrage + 1;
    }

    public void FinDeGarniture() {

      if (EstVide)
        return;
      
      int IndexDeFin = IndicesDesTampons.Fin;
      while(IndexDeFin >= IndicesDesTampons.Demarrage && char.IsWhiteSpace(c: Tampon[index: IndexDeFin])) {

        IndexDeFin--;
      }
      
      IndicesDesTampons.Taille = IndexDeFin - IndicesDesTampons.Demarrage + 1;
    }
    
    public void Garniture() {

      FinDeGarniture();
      DemarrageGarniture();
    }
    
    public bool DemarrageAvec(string str) {
      
      if (string.IsNullOrEmpty(value: str))
        return false;
      
      if (EstVide)
        return false;
      
      int Index = 0;
      int IndexTampon = IndicesDesTampons.Demarrage;
      
      for (; Index < str.Length; ++Index, ++IndexTampon) {

        if (str[index: Index] != Tampon[index: IndexTampon])
          return false;
      }
      
      return true;
    }

    public override string ToString() => new(value: Tampon.ToArray(), startIndex: IndicesDesTampons.Demarrage, length: IndicesDesTampons.Taille);

    public string ToString(Plage Plage) {
      
      if (Plage.EstVide || Plage.Demarrage < 0 || Plage.Taille > IndicesDesTampons.Taille || Plage.Demarrage + IndicesDesTampons.Demarrage > IndicesDesTampons.Fin) {
        
        return string.Empty;
      }
      
      return new string(
        value: Tampon.ToArray(), 
        startIndex: IndicesDesTampons.Demarrage + Plage.Demarrage, 
        length: Plage.Taille
      );
    }
  }
}
