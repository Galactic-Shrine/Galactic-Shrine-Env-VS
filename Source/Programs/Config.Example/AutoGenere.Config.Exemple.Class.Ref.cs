/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System.Text;
using GalacticShrine.IO;
using static GalacticShrine.DossierReference;
using static GalacticShrine.FichierReference;

namespace GalacticShrine.ConfigExample {

  internal class AutoGenere {

    //private static Format Terminal { get; set; } (using GalacticShrine.Terminal;)

    public AutoGenere(string Extention = "ini") {

      try {

        DateTime date = DateTime.Now;

        string Nom = Chemin.Combiner(Chemin1: $"{GalacticShrine.Repertoire["Config"]}", Chemin2: $"App.{Extention}");

        /**
         * [FR] Nous créons le contenu du fichier.
         * [EN] We create the contents of the file.
         **/
        string[] Config = {
          $"# Copyright © 2018 - {date.ToString("yyyy")}, Galactic-Shrine - Tous droits réservés.",
          "#",
          $"# Fichier Auto-généré le: {date.ToString("dddd d MMMM yyyy")} à {date.ToString("HH:mm K UTC")}",
          "",
          "[GeneralConfiguration]",
          "",
          "DataType = MySql",
          "# valeur accepté lumineux ou sombre par défaut sombre",
          "DefaultTemplate = Sombre",
          "",
          "[Database.Users]",
          "",
          "User = Root"
        };

        /**
         * [FR] On vérifier si le dossier existe.
         * [EN] Check if the folder exists.
         **/
        if(!VerifieSiExiste(Localisation: new DossierReference(Chemins: $"{GalacticShrine.Repertoire["Config"]}"))) {

          /**
           * [FR] Nous créons un nouveau dossier.
           * [EN] We create a new folder.
           **/
          Creer(Localisation: new (Chemins: $"{GalacticShrine.Repertoire["Config"]}")); // Creer(Localisation: new DossierReference(Chemins: "Config"));
        }

        /**
         * [FR] On vérifier si le fichier existe
         * [EN] Check if the file exists. 
         **/
        if(!VerifieSiExiste(Localisation: new FichierReference(Chemins: Nom))) {

          /**
           * [FR] Nous créons un nouveau fichier et ajoutons son contenu.
           * [EN] We create a new file and add its contents. 
           **/
          EcrireToutesLeslignes(Localisation: new (Chemins: Nom), Contenu: Config, Encodage: Encoding.UTF8);
          // EcrireToutesLeslignes(Localisation: new FichierReference(Chemins: Nom), Contenu: Config, Encodage: Encoding.UTF8);
        }
      }
      catch(System.Exception Ex) {

        //Terminal = new Format(Theme: Sombre); (using GalacticShrine.Terminal; using static GalacticShrine.UI.Terminal.Theme;)
        //Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Ex.ToString(), Couleur: Couleurs.Txt.Danger);
        Program.MessageException(Message: Ex.ToString(), Retablir: false);
        // Console.WriteLine(value: Ex.ToString());
      }
    }
  }
}
