/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using GalacticShrine.Configuration.Analyseur;
using GalacticShrine.Configuration.Configuration;
using GalacticShrine.Enumeration.Configuration;
using GalacticShrine.Exceptions.Configuration;
using GalacticShrine.Configuration.Properties;
using GalacticShrine.Modele.Configuration.Ini;
using static GalacticShrine.Configuration.Analyseur.TamponDeChaine;
using static GalacticShrine.FichierReference;

namespace GalacticShrine.Configuration {

  public class Ini {

    /**
    * <summary>
    *   [FR] Schéma qui définit la structure du fichier ini à analyser.<br/>
    *   [EN] Schema that defines the structure of the ini file to be analyzed.
    * </summary>
    **/
    public SchemaIni Schema { get; protected set; }

    public virtual AnalyseurIni Configuration { get; protected set; }

    /**
     * <summary>
     *   [FR] Liste temporaire de commentaires<br/>
     *   [EN] Temp list of comments
     * </summary>
     **/
    List<string> ListeDeCommentairesActuelleTemporaire;

    uint NumeroDeLaLigneEnCours;

    /**
     * <summary>
     *   [FR] Variable temporaire avec le nom de la section en cours de traitement<br/>
     *   [EN] Temporary variable with the name of the section being processed
     * </summary>
     **/
    string NomDeLaSectionActuelle;

    /**
     * <summary>
     *   [FR] Tampon utilisé pour contenir la ligne en cours de traitement.<br/>
     *        Permet d'éviter d'allouer une nouvelle chaîne<br/>
     *   [EN] Buffer used to hold the line currently being processed.
     *        Avoids the need to allocate a new string.
     * </summary>
     **/
    readonly TamponDeChaine Tampon = new(capacity: 256);

    /**
     * <summary>
     *   [FR] Contient une liste des exceptions capturées lors de l'analyse syntaxique.<br/>
     *   [EN] Contains a list of exceptions caught during parsing.
     * </summary>
     **/
    private readonly List<Exception> ErreurExceptions;

    /**
     * <summary>
     *   [FR] Vrai(True) si l'opération d'analyse syntaxique rencontre un problème<br/>
     *   [EN] True is the parsing operation encounters a problem
     * </summary>
     **/
    public bool A_DesErreurs => ErreurExceptions.Count > 0;

    /**
     * <summary>
     *   [FR] Renvoie une liste des erreurs trouvées lors de l'analyse du fichier de configuration.<br/>
     *   [EN] Returns a list of errors found when analyzing the configuration file.
     * </summary>
     * <remarks>
     *   [FR] Si l'option de configuration ThrowExceptionOnError est fausse,<br/>
     *        il peut contenir un élément pour chaque problème rencontré lors de l'analyse;<br/>
     *        sinon, il ne contiendra que la même exception que celle qui a été soulevée.<br/>
     *   [EN] If the ThrowExceptionOnError configuration option is false,<br/>
     *        it can contain one item for each problem encountered during analysis;<br/>
     *        otherwise, it will only contain the same exception as the one raised.
     * </remarks>
     **/
    public ReadOnlyCollection<Exception> Erreur => ErreurExceptions.AsReadOnly();

    /**
     * <summary>
     *   [FR] Liste temporaire de commentaires<br/>
     *   [EN] Temp list of comments
     * </summary>
     **/
    public List<string> ListeDeCommentairesActuelleTemp {

      get {

        if(ListeDeCommentairesActuelleTemporaire == null) {

          ListeDeCommentairesActuelleTemporaire = new List<string>();
        }

        return ListeDeCommentairesActuelleTemporaire;
      }

      internal set => ListeDeCommentairesActuelleTemporaire = value;
    }

    /**
     * <summary>
     *   [FR] Constructeur<br/>
     *   [EN] Constructor
     * </summary>
     **/
    public Ini() {
    
      Schema           = new SchemaIni();
      Configuration    = new AnalyseurIni();
      ErreurExceptions = new List<Exception>();
    }

    /**
     * <summary>
     *   [FR] Analyse une chaîne contenant des données ini valides.<br/>
     *   [EN] Parses a string containing valid ini data.
     * </summary>
     * <param name="ChaineIni">
     *   [FR] une chaîne de données au format INI.<br/>
     *   [EN] a data string in INI format.
     * </param>
     **/
    public DonneesIni Analyse(string ChaineIni) {

       return Analyse(LecteurDeTexte: new StringReader(ChaineIni));
    }

    /**
     * <summary>
     *   [FR] Analyse une chaîne contenant des données ini valides.<br/>
     *   [EN] Parses a string containing valid ini data.
     * </summary>
     * <param name="LecteurDeTexte">
     *   [FR] Lecteur de texte pour la chaîne source contenant des données ini.<br/>
     *   [EN] Text reader for the source string containing the ini data.
     * </param>
     * <returns>
     *   [FR] Une instance <see cref="GalacticShrine.Configuration.DonneesIni"/> contenant les données lues à partir de la source.<br/>
     *   [EN] An <see cref="GalacticShrine.Configuration.DonneesIni"/> instance containing the data read from the source.
     * </returns>
     * <exception cref="AnalyseException">
     *   [FR] Lancé si les données n'ont pas pu être analysées<br/>
     *   [EN] Launched if data could not be analyzed
     * </exception>
     **/
    public DonneesIni Analyse(TextReader LecteurDeTexte) {

      DonneesIni DonneesIni = Configuration.InsensibleA_LaCasse ? new DonneesIniInsensibleA_LaCasse(SchemaIniInstence: Schema) : new DonneesIni(SchemaIniInstance: Schema);

      Analyse(LecteurDeTexte: LecteurDeTexte, DonneesIni: ref DonneesIni);

      return DonneesIni;
    }

    /**
     * <summary>
     *   [FR] Analyse une chaîne contenant des données ini valides.<br/>
     *   [EN] Parses a string containing valid ini data.
     * </summary>
     * <param name="LecteurDeTexte">
     *   [FR] Lecteur de texte pour la chaîne source contenant des données ini.<br/>
     *   [EN] Text reader for the source string containing the ini data.
     * </param>
     * <returns>
     *   [FR] Une instance <see cref="GalacticShrine.Configuration.DonneesIni"/> contenant les données lues à partir de la source.<br/>
     *   [EN] An <see cref="GalacticShrine.Configuration.DonneesIni"/> instance containing the data read from the source.
     * </returns>
     * <exception cref="AnalyseException">
     *   [FR] Lancé si les données n'ont pas pu être analysées<br/>
     *   [EN] Launched if data could not be analyzed
     * </exception>
     **/
    public void Analyse(TextReader LecteurDeTexte, ref DonneesIni DonneesIni) {

      DonneesIni.Effacer();

      DonneesIni.Schema = Schema.CloneEnProfondeur();

      ErreurExceptions.Clear();

      if(Configuration.AnalyseDesCommentaires) {

        ListeDeCommentairesActuelleTemp.Clear();
      }

      NomDeLaSectionActuelle = null;
      Tampon.Reinitialiser(DonneesSource: LecteurDeTexte);
      NumeroDeLaLigneEnCours = 0;

      while(Tampon.LigneDeLecture()) {

        NumeroDeLaLigneEnCours++;

        try {

          LigneDeProcessus(LigneActuelle: Tampon, DonneesIni: DonneesIni);
        }
        catch(Exception ex) {

          ErreurExceptions.Add(item: ex);
          if(Configuration.LancerDesExceptionsEnCasDerreur) {

            throw;
          }
        }
      }
      
      try {

        /**
         * [FR] Commentaires orphelins, assignés à la dernière section/valeur clé
         * [EN] Orphan comments, assign to last section/key value
         **/
        if(Configuration.AnalyseDesCommentaires && ListeDeCommentairesActuelleTemp.Count > 0) {

          if(DonneesIni.Sections.Compter > 0) {

            /**
             * [FR] Vérifier s'il y a effectivement des sections dans le fichier
             * [EN] Check if there are sections in the file
             **/
            DonneesIni.Sections.RechercheParNom(NomDeLaSection: NomDeLaSectionActuelle).Commentaire.AddRange(collection: ListeDeCommentairesActuelleTemp);
          }
          else if(DonneesIni.ProprieteGlobales.Compter > 0) {

            /**
             * [FR] Pas de sections, mettre le commentaire dans la dernière paire clé-valeur mais seulement si le fichier ini contient au moins une paire clé-valeur.
             * [EN] No sections, put the comment in the last key-value pair, but only if the ini file contains at least one key-value pair.
             **/
            DonneesIni.ProprieteGlobales.ObtenirLeDernier().Commentaire.AddRange(collection: ListeDeCommentairesActuelleTemp);
          }

          ListeDeCommentairesActuelleTemp.Clear();
        }
      }
      catch(Exception ex) {

        ErreurExceptions.Add(ex);
        if(Configuration.LancerDesExceptionsEnCasDerreur) {

          throw;
        }
      }
      
      if(A_DesErreurs) {

        DonneesIni.Effacer();
      }
    }

    /**
     * <summary>
     *   [FR] Ouvre un fichier, qui contient une chaîne de données au format INI.<br/>
     *   [EN] Opens a file containing a data string in INI format.
     * </summary>
     * <param name="FichierA_Ouvrir">
     *   [FR] Le fichier à ouvrir, qui contient une chaîne de données au format INI.<br/>
     *   [EN] The file to be opened, which contains a data string in INI format.
     * </param>
     **/
    public DonneesIni Ouvrir(string FichierA_Ouvrir) {

      if(!VerifieSiExiste(Localisation: new FichierReference(Chemins: FichierA_Ouvrir)))
        throw new ArgumentException(Resources.FichierNonTrouve);

      string ChaineIni = LireTousLeTexte(Localisation: new FichierReference(Chemins: FichierA_Ouvrir));

      return Analyse(LecteurDeTexte: new StringReader(ChaineIni));
    }

    protected virtual bool CommentaireDeProcessus(TamponDeChaine LigneActuelle) {

      /**
       * [FR] La ligne est moyenne lorsqu'elle est arrivée ici, il suffit donc de vérifier si les premiers caractères sont ceux des commentaires
       * [EN] Line is medium when it arrives here, so just check if the first characters are those of the comments
       **/
      var LigneActuelleCoupee = LigneActuelle.AvalezCopie();
      LigneActuelleCoupee.DemarrageGarniture();

      if(!LigneActuelleCoupee.DemarrageAvec(Schema.AttributionDuCommentaire)) {

        return false;
      }

      if(!Configuration.AnalyseDesCommentaires) {

        return true;
      }

      LigneActuelleCoupee.FinDeGarniture();

      var GammeCommentaire = LigneActuelleCoupee.TrouverSousChaine(SousChaine: Schema.AttributionDuCommentaire);
      /**
       * [FR] Extraire la plage de la chaîne qui contient le commentaire mais pas le délimiteur de commentaire
       * [EN] Extract the range of the string that contains the comment but not the comment delimiter
       **/
      var Gammes = Plage.DeL_IndexA_LaTaille(
        Demarrage: GammeCommentaire.Demarrage + Schema.AttributionDuCommentaire.Length, 
        Taille: LigneActuelleCoupee.Compter - Schema.AttributionDuCommentaire.Length
      );

      var Commentaire = LigneActuelleCoupee.SousChaine(Plage: Gammes);
      if(Configuration.HabillageCommentaires) {

        Commentaire.Garniture();
      }

      ListeDeCommentairesActuelleTemp.Add(item: Commentaire.ToString());

      return true;
    }

    /**
     * <summary>
     *   [FR] Traite une chaîne de caractères contenant une section ini.<br/>
     *   [EN] Processes a string containing an ini section.
     * </summary>
     * <param name="LigneActuelle">
     *   [FR] La chaîne à traiter.<br/>
     *   [EN] The string to be processed.
     * </param>
     **/
    protected virtual bool SectionDeProcessus(TamponDeChaine LigneActuelle, DonneesIni Donnees) {

      if(LigneActuelle.Compter <= 0)
        return false;

      var DebutDeLaPlageDeSection = LigneActuelle.TrouverSousChaine(SousChaine: Schema.DebutDeSection);

      if(DebutDeLaPlageDeSection.EstVide)
        return false;

      var FinDeLaPlageDeSection = LigneActuelle.TrouverSousChaine(SousChaine: Schema.FinDeSection, IndexDeDemarrage: DebutDeLaPlageDeSection.Taille);
      if(FinDeLaPlageDeSection.EstVide) {

        if(Configuration.SauterLesLignesInvalides)
          return false;

        throw new AnalyseException(
          Message: string.Format(
            format: Resources.MessageExceptionErreurPasDeValeurDeSectionFermante, 
            arg0: nameof(Configuration), 
            arg1: nameof(Configuration.SauterLesLignesInvalides)
          ), 
          NumeroDeLigne: NumeroDeLaLigneEnCours, 
          ContenuDeLigne: LigneActuelle.ChangementsDansLesRejets().ToString()
        );
      }

      LigneActuelle.RedimensionnerEntreLesIndex(
        IndexDeDemarrage: DebutDeLaPlageDeSection.Demarrage + Schema.DebutDeSection.Length, 
        IndexDeFin: FinDeLaPlageDeSection.Fin - Schema.FinDeSection.Length
      );

      if(Configuration.HabillageSections) {

        LigneActuelle.Garniture();
      }

      var NomDeLaSection = LigneActuelle.ToString();

      /**
       * [FR] Sauvegarde temporaire du nom de la section.
       * [EN] Temporally save section name.
       **/
      NomDeLaSectionActuelle = NomDeLaSection;

      /**
       * [FR] Vérifie si la section existe déjà
       * [EN] Checks if the section already exists
       **/
      if(!Configuration.AutoriserLesSectionsDupliquees) {

        if(Donnees.Sections.Contient(NomDeLaSection: NomDeLaSection)) {

          if(Configuration.SauterLesLignesInvalides)
            return false;
         
          throw new AnalyseException(
            Message: string.Format(
              format: Resources.MessageExceptionErreurSectionDupliqueeAvecLeNom, 
              arg0: NomDeLaSection, 
              arg1: nameof(Configuration), 
              arg2: nameof(Configuration.SauterLesLignesInvalides)
            ), 
            NumeroDeLigne: NumeroDeLaLigneEnCours, 
            ContenuDeLigne: LigneActuelle.ChangementsDansLesRejets().ToString()
          );
        }
      }

      /**
       * [FR] Si la section n'existe pas, ajoutez-la aux données ini.
       * [EN] If the section doesn't exist, add it to the ini data.
       **/
      Donnees.Sections.Ajouter(NomDeLaSection: NomDeLaSection);

      /**
       * [FR] Sauvegarder les commentaires lus jusqu'à présent et les affecter à cette section.
       * [EN] Save comments read so far and assign them to this section.
       **/
      if(Configuration.AnalyseDesCommentaires) {

        Donnees.Sections.RechercheParNom(NomDeLaSection: NomDeLaSection)
          .Commentaire.AddRange(collection: ListeDeCommentairesActuelleTemp);
        ListeDeCommentairesActuelleTemp.Clear();
      }

      return true;
    }

    protected virtual bool ProprieteDeProcessus(TamponDeChaine LigneActuelle, DonneesIni Donnees) {

      if(LigneActuelle.Compter <= 0)
        return false;

      var IndexD_AffectationDesPropriete = LigneActuelle.TrouverSousChaine(SousChaine: Schema.AttributionDePropriete);

      if(IndexD_AffectationDesPropriete.EstVide)
        return false;

      var PlageDeCle          = Plage.AvecIndex(Demarrage: 0, Fin: IndexD_AffectationDesPropriete.Demarrage - 1);
      var ValeurIndexDeDepart = IndexD_AffectationDesPropriete.Fin + 1;
      var ValeurTaille        = LigneActuelle.Compter - IndexD_AffectationDesPropriete.Fin - 1;
      var PlageDeValeur       = Plage.DeL_IndexA_LaTaille(Demarrage: ValeurIndexDeDepart, Taille: ValeurTaille);
      var Cle                 = LigneActuelle.SousChaine(Plage: PlageDeCle);
      var Valeur              = LigneActuelle.SousChaine(Plage: PlageDeValeur);

      if(Configuration.HabillagePropriete) {

        Cle.Garniture();
        Valeur.Garniture();
      }

      if(Cle.EstVide) {

        if(Configuration.SauterLesLignesInvalides)
          return false;

        throw new AnalyseException(
          Message: string.Format(
            format: Resources.MessageExceptionErreurProprietesSansCle, 
            arg0: nameof(Configuration),
            arg1: nameof(Configuration.SauterLesLignesInvalides)
          ), 
          NumeroDeLigne: NumeroDeLaLigneEnCours, 
          ContenuDeLigne: LigneActuelle.ChangementsDansLesRejets().ToString()
        );
      }

      /**
       * [FR] Vérifier si nous n'avons pas encore lu de sections.
       * [EN] Check if we haven't read any sections yet.
       **/
      if(string.IsNullOrEmpty(value: NomDeLaSectionActuelle)) {

        if(!Configuration.AutoriserLesClesSansSection) {

          throw new AnalyseException(
            Message: string.Format(
              format: Resources.MessageExceptionErreurProprietesHorsSection, 
              arg0: nameof(Configuration), 
              arg1: nameof(Configuration.AutoriserLesClesSansSection)
            ), 
            NumeroDeLigne: NumeroDeLaLigneEnCours, 
            ContenuDeLigne: LigneActuelle.ChangementsDansLesRejets().ToString()
          );
        }

        AjouterUneCleA_LaCollectionDeValeursDeCle(
          Cle: Cle.ToString(), 
          Valeur: Valeur.ToString(), 
          CollectionDeDonneesDeCles: Donnees.ProprieteGlobales, 
          NomDeLaSection: "global"
        );
      }
      else {

        AjouterUneCleA_LaCollectionDeValeursDeCle(
          Cle: Cle.ToString(), 
          Valeur: Valeur.ToString(), 
          CollectionDeDonneesDeCles: Donnees.Sections.RechercheParNom(NomDeLaSectionActuelle).Proprietes, 
          NomDeLaSection: NomDeLaSectionActuelle
        );
      }

      return true;
    }

    /**
     * <summary>
     *   [FR] Méthode abstraite qui décide de ce qu'il faut faire dans le cas où nous essayons d'ajouter une clé dupliquée à une section.<br/>
     *   [EN] Abstract method that decides what to do if we try to add a duplicate key to a section.
     * </summary>
     **/
    void ManipulerLesClesDupliqueesDansLaCollection(string Cle, string Valeur, ProprieteCollection CollectionDeDonneesDeCles, string NomDeLaSection) {

      switch(Configuration.ComportementDesProprietesDupliquees) {

        case ComportementDesProprietesDupliquees.DesactiverEtArretAvecErreur:
             
          throw new AnalyseException(
            Message: string.Format(
              format: Resources.MessageExceptionDuplicationDeLaCle, 
              arg0: Cle, 
              arg1: NomDeLaSection
            ), 
            NumeroDeLigne: NumeroDeLaLigneEnCours
          );

        case ComportementDesProprietesDupliquees.AutoriserEtConserverLaPremiereValeur:

          /**
           * [FR] Rien à faire ici : nous avons déjà la première valeur assignée.
           * [EN] Nothing to do here: we already have the first value assigned.
           **/
          break;

        case ComportementDesProprietesDupliquees.AutoriserEtConserverLaDerniereValeur:

          /**
           * [FR] Lorsque l'analyse est terminée, la valeur actuelle est remplacée par la dernière valeur.
           * [EN] When the analysis is complete, the current value is replaced by the last value.
           **/
          CollectionDeDonneesDeCles[NomDeLaCle: Cle] = Valeur;
          break;

        case ComportementDesProprietesDupliquees.AutoriserEtConcatenerLesValeurs:

          CollectionDeDonneesDeCles[NomDeLaCle: Cle] += Configuration.ConcatenerLesChainesDeProprietesDupliquees + Valeur;
          break;
      }
    }

    /**
     * <summary>
     *   [FR] Ajoute une clé à une instance concrète <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/>,<br/>
     *        en vérifiant si les clés dupliquées sont autorisées dans la configuration.<br/>
     *   [EN] Adds a key to a concrete instance <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/>,<br/>
     *        checking whether duplicate keys are allowed in the configuration.
     * </summary>
     * <param name="Cle">
     *   [FR] Nom de la clé<br/>
     *   [EN] Key name
     * </param>
     * <param name="Valeur">
     *   [FR] Valeur de la clé<br/>
     *   [EN] Key value
     * </param>
     * <param name="CollectionDeDonneesDeCles">
     *   [FR] <see cref="GalacticShrine.Modele.Configuration.Ini.Propriete"/> collection dans laquelle la clé doit être insérée.<br/>
     *   [EN] <see cref="GalacticShrine.Modele.Configuration.Ini.Propriete"/> collection in which the key is to be inserted.
     * </param>
     * <param name="NomDeLaSection">
     *   [FR] Nom de la section où se trouve la <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/>.<br/>
     *        Utilisé uniquement à des fins de journalisation.<br/>
     *   [EN] Name of the section where the <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> is located.<br/>
     *        Used for logging purposes only.
     * </param>
     **/
    private void AjouterUneCleA_LaCollectionDeValeursDeCle(string Cle, string Valeur, ProprieteCollection CollectionDeDonneesDeCles, string NomDeLaSection) {

      /**
       * [FR] Vérifier l'existence d'un double des clés
       * [EN] Check for duplicate keys
       **/
      if(CollectionDeDonneesDeCles.Contient(Cle)) {

        /**
         * [FR] Nous disposons déjà d'une clé portant le même nom, définie dans la section actuelle.
         * [EN] We already have a key with the same name, defined in the current section.
         **/
        ManipulerLesClesDupliqueesDansLaCollection(
          Cle: Cle, 
          Valeur: Valeur, 
          CollectionDeDonneesDeCles: CollectionDeDonneesDeCles, 
          NomDeLaSection: NomDeLaSection
        );
      }
      else {

        /**
         * [FR] Sauvegarder les clés
         * [EN] Save keys
         **/
        CollectionDeDonneesDeCles.Ajouter(Cle: Cle, Valeur: Valeur);
      }

      if(Configuration.AnalyseDesCommentaires) {

        CollectionDeDonneesDeCles.RechercheParCle(NomDeLaCle: Cle).Commentaire = ListeDeCommentairesActuelleTemp;
        ListeDeCommentairesActuelleTemp.Clear();
      }
    }

    /**
     * <summary>
     *   [FR] Traite une ligne et analyse les données qui s'y trouvent (section ou paire clé/valeur avec ou sans commentaires)
     *   [EN] Processes a line and analyzes its data (section or key/value pair with or without comments)
     * </summary>
     **/
    protected virtual void LigneDeProcessus(TamponDeChaine LigneActuelle, DonneesIni DonneesIni) {

      if(LigneActuelle.EstVide || LigneActuelle.EstUnEspaceBlanc)
        return;

      /**
       * [FR] TODO : changer ceci en un tableau global (niveau DataIni) de commentaires
       *             Extrait les commentaires de la ligne courante et les stocke dans une liste temporaire
       * [EN] TODO : change this to a global array (DataIni level) of comments
       *             Extracts comments from the current line and stores them in a temporary list
       **/

      if(CommentaireDeProcessus(LigneActuelle))
        return;

      if(SectionDeProcessus(LigneActuelle, DonneesIni))
        return;

      if(ProprieteDeProcessus(LigneActuelle, DonneesIni))
        return;

      if(Configuration.SauterLesLignesInvalides)
        return;
      //Resources.MessageExceptionErreurFormatIni,
      throw new AnalyseException(
        Message: string.Format(
          format: Resources.MessageExceptionErreurFormatIni, 
          arg0: LigneActuelle, 
          arg1: nameof(Configuration), 
          arg2: nameof(Configuration.SauterLesLignesInvalides)
        ), 
        NumeroDeLigne: NumeroDeLaLigneEnCours, 
        ContenuDeLigne: LigneActuelle.ChangementsDansLesRejets().ToString()
      );
    }
  }
}