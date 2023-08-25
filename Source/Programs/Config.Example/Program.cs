/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System.Reflection;
using GalacticShrine.Terminal;
using GalacticShrine.IO; // Utiliser pour la fonction : Chemin.Combiner()
using GalacticShrine.Outils; // Utiliser pour la fonction : ObtenirLemplacementDorigine()
using static GalacticShrine.UI.Terminal.Theme;
using static GalacticShrine.Terminal.Couleurs;
using static GalacticShrine.DossierReference;
using GalacticShrine.Configuration;

namespace GalacticShrine.ConfigExample {

  internal class Program {

    public static readonly DossierReference RepertoireRacineApplication = new (Chemins: Chemin.Combiner(Chemin: ObtenirLeNomDuRepertoire(Chemin: Assembly.GetExecutingAssembly().ObtenirLemplacementDorigine())));

    private static Format Terminal { get; set; }

    static void Main(string[] args) {

      new AutoGenere();

      Ini ini = new();

      ini.Schema.AttributionDuCommentaire = "#";

      DonneesIni Config = ini.Analyse("app.ini");


      switch(Config["GeneralConfiguration"]["DefaultTemplate"]) {

        case "Lumineux":
        case "lumineux":

          Terminal = new Format(Theme: Lumineux);
          break;
        
        case "Sombre":
        case "sombre":
        default:

          Terminal = new Format(Theme: Sombre);
          break;
      }

      Terminal.Eclaircir();
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "Hello, World!", Couleur: Txt.Sourdine);
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "");
      Terminal.Ecrire(ReserveToutLaLigne: false, Texte: $"Config : ");
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"{Config}", Couleur: Txt.Danger);
      Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "");
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