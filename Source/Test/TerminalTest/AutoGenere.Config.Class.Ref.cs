/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.IO;
using System.Reflection;
using GalacticShrine.Outils;
using static GalacticShrine.DossierReference;
using static GalacticShrine.FichierReference;

namespace GalacticShrine.Test.Terminal {

  internal class AutoGenere {

    public AutoGenere(string Extention = "ini") {

      DateTime date = DateTime.Now;

      string Chemins = Path.Combine(path1: Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().ObtenirLemplacementDorigine()), path2: "Config");

      string Nom = Path.Combine(path1: Chemins, path2: $"General.{Extention}");

      try {
        
        // !Directory.Exists("Config")
        if(!VerifieSiExiste(Localisation: new DossierReference(Chemins: Chemins))) {

          Creer(Localisation: new DossierReference(Chemins: "Config"));
        }

        // Check if file already exists. If yes, delete it.  //File.Exists(Nom)   
        if(!VerifieSiExiste(Localisation: new FichierReference(Chemins: Nom))) {

          // Create a new file     
          using(StreamWriter Fichier = Creer(Nom: Nom)) {

            Fichier.WriteLine(format: "; Copyright © 2018 - {0}, Galactic-Shrine - Tous droits réservés.", date.ToString("yyyy"));
            Fichier.WriteLine(format: ";");
            Fichier.WriteLine(format: "; Fichier Auto-généré le: {0} à {1}", arg0: date.ToString("dddd d MMMM yyyy"), arg1: date.ToString("HH:mm K UTC"));
            Fichier.WriteLine(format: "");
            Fichier.WriteLine(format: "[Terminal]");
            Fichier.WriteLine(format: "");
            Fichier.WriteLine(format: "DefaultTemplate = Sombre");
          }
        }
      }
      catch(Exception Ex) {

        Console.WriteLine(value: Ex.ToString());
      }
    }
  }
}
