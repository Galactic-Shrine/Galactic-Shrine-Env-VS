/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using GalacticShrine.Terminal;
using static GalacticShrine.UI.Terminal.Theme;
using static GalacticShrine.Terminal.Couleurs;
using GalacticShrine.Configuration;

namespace GalacticShrine.ConfigIntegreExample {

  internal class Program {

    #region Important pour le démarrage de l'application
    private static Format Terminal { get; set; }
    #endregion

    static void Main(string[] args) {
      #region Important pour le démarrage de l'application
      var Priver = @"# Copyright © 2018 - 2023, Galactic-Shrine - Tous droits réservés
        [GeneralConfiguration]

        DataType = MySql

        # valeur accepté lumineux ou sombre par défaut sombre
        DefaultTemplate = Sombre

        [Database.Users]
        User = Root";

      Ini ini = new();

      ini.Schema.AttributionDuCommentaire = "#";

      DonneesIni Config = ini.Analyse(Priver);

      Terminal = Config["GeneralConfiguration"]["DefaultTemplate"] switch {

        "Lumineux" or "lumineux" => new Format(Theme: Lumineux),
        "Sombre" or "sombre" or _ => new Format(Theme: Sombre)
      };
      #endregion

      Terminal.Eclaircir();
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "Hello, World!", Couleur: Txt.Sourdine);
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "");
      Terminal.Ecrire(ReserveToutLaLigne: false, Texte: $"Config : ");
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"{Config}", Couleur: Txt.Danger);
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "");
      Terminal.Ecrire(ReserveToutLaLigne: false, Texte: "Section : ");
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "GeneralConfiguration", Couleur: Txt.Magenta);
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "");
      Terminal.Ecrire(ReserveToutLaLigne: false, Texte: "Paramètre : ");
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "DataType", Couleur: Txt.Info);
      Terminal.Ecrire(ReserveToutLaLigne: false, Texte: "Valeur : ");
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Config["GeneralConfiguration"]["DataType"], Couleur: Txt.Succes);
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "");
      Terminal.Ecrire(ReserveToutLaLigne: false, Texte: "Paramètre : ");
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "DefaultTemplate", Couleur: Txt.Info);
      Terminal.Ecrire(ReserveToutLaLigne: false, Texte: "Valeur : ");
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Config["GeneralConfiguration"]["DefaultTemplate"], Couleur: Txt.Succes);
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "");
      Terminal.Ecrire(ReserveToutLaLigne: false, Texte: "Section : ");
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "Database.Users", Couleur: Txt.Magenta);
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "");
      Terminal.Ecrire(ReserveToutLaLigne: false, Texte: "Paramètre : ");
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "User", Couleur: Txt.Info);
      Terminal.Ecrire(ReserveToutLaLigne: false, Texte: "Valeur : ");
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Config["Database.Users"]["User"], Couleur: Txt.Succes);

      Console.ReadLine();
    }

    public static void MessageException(string Message, bool Retablir = true) {

      if(Retablir) {

        Terminal.RetablirCouleur();
        Terminal.Eclaircir();
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Message, Couleur: Txt.Danger);
      }
      else {

        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Message, Couleur: Txt.Danger);
      }
    }
  }
}