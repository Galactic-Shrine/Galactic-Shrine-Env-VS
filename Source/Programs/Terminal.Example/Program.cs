/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Reflection;
using GalacticShrine.Enumeration.Outils;
using GalacticShrine.Enumeration;
using GalacticShrine.IO; // Utiliser pour la fonction : Chemin.Combiner()
using GalacticShrine.Outils;
using GsP = GalacticShrine.Properties;
using GalacticShrine.Terminal;
using GalacticShrine.TerminalExample.Properties;
using GalacticShrine.UI.Terminal;
using static GalacticShrine.DossierReference;
using static GalacticShrine.Terminal.Couleurs;
using static GalacticShrine.Terminal.Terminal;
using static GalacticShrine.UI.Terminal.Theme;

namespace GalacticShrine.TerminalExample {

  internal class Program {

    #region Important pour le démarrage de l'application
    private static Format Terminal { get; set; }

    #endregion

    static void Main() {
      
      try {

        /* Example par Nom 
        switch(OS.ObtenirNomCourantes) {
          
          case "Windows":
            Terminal = new Format(Theme: Sombre);
            break;

          case "Linux":
            Terminal = new Format(Theme: new ThemeSombre());
            break;

          case "OSX":
            Terminal = new Format(Theme: Lumineux);
            break;
         }
         */
        /* Example par Id */
        switch(OS.ObtenirIdCourantes) {

          case SystemeExploitation.Windows:

            Terminal = new Format(Theme: Sombre);
            break;

          case SystemeExploitation.Linux:

            Terminal = new Format(Theme: new ThemeSombre());
            break;

          case SystemeExploitation.Mac:

            Terminal = new Format(Theme: Lumineux);
            break;
        }


        Menu();
				Terminal.RetablirCouleur();
				Terminal.Eclaircir();
			}
			catch(ArgumentOutOfRangeException) {

				MessageException(Message: GsP.Resources.ConsoleEstTropPetite);
			}
			catch(Exception ex) {

				MessageException(Message: ex.ToString());
			}
		}

		static void Menu() {

			string AppName = typeof(Program).Assembly.GetName().Name;
			Version AppVersion = Assembly.GetExecutingAssembly().GetName().Version;

      Terminal.Eclaircir();
			Terminal.LigneSeparatriceDecoree(Character: '−', Couleur: Bg.Avertissement);
			Terminal.Aligner(Alignement: Alignement.Centre, Texte: AppName.Replace("T", " T") + " .NET " + AppVersion, Couleur: Bg.Avertissement);
			Terminal.LigneSeparatriceDecoree(Character: '−', Couleur: Bg.Avertissement);
			Terminal.LigneSeparatrice(Couleur: Bg.Sourdine);

			Terminal.Ecrire(Texte: $"{"[1] ",-4}", Couleur: Bg.Sourdine); 
			Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.Couleur, Couleur: Bg.Sourdine);
			Terminal.Ecrire(Texte: $"{"[2] ",-4}", Couleur: Bg.Sourdine); 
			Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.Ecrire, Couleur: Bg.Sourdine);
			Terminal.Ecrire(Texte: $"{"[3] ",-4}", Couleur: Bg.Sourdine); 
			Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.LigneEcriture, Couleur: Bg.Sourdine);
			Terminal.LigneSeparatrice(Bg.Sourdine);
			Terminal.Ecrire(Texte: $"{"[4] ",-4}", Couleur:	Bg.Sourdine); 
			Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.AlignementDuTexte, Couleur: Bg.Sourdine);
			Terminal.Ecrire(Texte: $"{"[5] ",-4}", Couleur:	Bg.Sourdine); 
			Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.LignesVides, Couleur: Bg.Sourdine);
			Terminal.Ecrire(Texte: $"{"[6] ",-4}", Couleur: Bg.Sourdine); 
			Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.LignesDeDivision, Couleur: Bg.Sourdine);
			Terminal.LigneSeparatrice(Bg.Sourdine);
      Terminal.Ecrire(Texte: $"{"[V] ",-4}", Couleur: Bg.Sourdine); 
			Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.VariableValeurs, Couleur: Bg.Sourdine);
      Terminal.Ecrire(Texte: $"{"[D] ",-4}", Couleur: Bg.Sourdine); 
			Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ThemeSombre, Couleur: Bg.Sourdine);
			Terminal.Ecrire(Texte: $"{"[L] ",-4}", Couleur: Bg.Sourdine);	
			Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ThemeLumineux, Couleur: Bg.Sourdine);

			Terminal.LigneSeparatrice(Couleur: Bg.Sourdine);
			Terminal.LigneSeparatrice(Couleur: Bg.Avertissement);
			Terminal.LigneSeparatrice();
			Terminal.Aligner(Alignement: Alignement.Centre, Texte: $"{Resources.AppuyezPourQuitter}", Couleur: Txt.Danger);
			Terminal.LigneSeparatrice();
			Terminal.Ecrire(Texte: $"{Controleur}", Couleur: Txt.Succes);
      Terminal.Ecrire(Texte: "# ", Couleur: Txt.Sourdine);

      string opt = Console.ReadLine();
			opt = opt.ToLower();

      Terminal.Eclaircir();
			switch(opt) {

				case "1":

					CouleurDeTheme();
					break;
				case "2":

					Ecrire();
					break;
				case "3":

          LigneDecriture();
					break;
				case "4":

					TexteAlignement();
					break;
				case "5":

					LigneSeparatrice();
					break;
				case "6":

					LigneSeparatriceDecoree();
					break;
        case "v":

					Variable();
					break;
        case "d":
				case "l":

					ChangementDeTheme(option: opt);
					break;
				case "q":
					break;
				default:

					Menu();
					break;
			}
		}

		static void Retour() {

			Terminal.LigneSeparatrice();
			Terminal.Aligner(Alignement: Alignement.Centre, Texte: Resources.AppuyezNimporteQuelleTouche);
			Console.ReadKey();
			Menu();
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

    static void CouleurDeTheme() {

			try {

				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteDefault,						 Couleur: Txt.Default);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteNoir,                Couleur: Txt.Black);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteBleuFonce,           Couleur: Txt.DarkBlue);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteVertFonce,           Couleur: Txt.DarkGreen);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteCyanFonce,           Couleur: Txt.DarkCyan);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteRougeFonce,          Couleur: Txt.DarkRed);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteMagentaFonce,        Couleur: Txt.DarkMagenta);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteJauneFonce,          Couleur: Txt.DarkYellow);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteGrisFonce,           Couleur: Txt.DarkGray);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteBleu,                Couleur: Txt.Blue);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteVert,                Couleur: Txt.Green);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteCyan,                Couleur: Txt.Cyan);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteRouge,               Couleur: Txt.Red);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteMagenta,             Couleur: Txt.Magenta);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteJaune,               Couleur: Txt.Yellow);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteGris,                Couleur: Txt.Gray);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteSourdine,						 Couleur: Txt.Sourdine);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TextePrimaire,						 Couleur: Txt.Primaire);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteSucces,							 Couleur: Txt.Succes);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteInfo,								 Couleur: Txt.Info);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteAvertissement,			 Couleur: Txt.Avertissement);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteDanger,							 Couleur: Txt.Danger);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanDefault,			 Couleur: Bg.Default);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanNoir,          Couleur: Bg.Black);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanBleuFonce,     Couleur: Bg.DarkBlue);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanVertFonce,     Couleur: Bg.DarkGreen);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanCyanFonce,     Couleur: Bg.DarkCyan);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanRougeFonce,    Couleur: Bg.DarkRed);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanMagentaFonce,  Couleur: Bg.DarkMagenta);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanJauneFonce,    Couleur: Bg.DarkYellow);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanGrisFonce,     Couleur: Bg.DarkGray);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanBleu,          Couleur: Bg.Blue);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanVert,          Couleur: Bg.Green);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanCyan,          Couleur: Bg.Cyan);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanRouge,         Couleur: Bg.Red);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanMagenta,       Couleur: Bg.Magenta);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanJaune,         Couleur: Bg.Yellow);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrièrePlanGris,          Couleur: Bg.Gray);
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanSourdine,			 Couleur: Bg.Sourdine);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanPrimaire,			 Couleur: Bg.Primaire);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanSucces,				 Couleur: Bg.Succes);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanInfo,					 Couleur: Bg.Info);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanAvertissement, Couleur: Bg.Avertissement);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanDanger,				 Couleur: Bg.Danger);
        Terminal.Ecrire2Couleur(ReserveToutLaLigne: true, Texte: Resources.PersonnaliserLesCouleur, CouleurTexte: Txt.Red, CouleurArrierePlan: Bg.Blue);
        Terminal.Ecrire2Couleur(ReserveToutLaLigne: true, Texte: Resources.PersonnaliserLesCouleur, CouleurTexte: Txt.DarkGreen, CouleurArrierePlan: Bg.DarkYellow);
        Terminal.Ecrire2Couleur(ReserveToutLaLigne: true, Texte: Resources.PersonnaliserLesCouleur, CouleurTexte: Txt.Cyan, CouleurArrierePlan: Bg.DarkCyan);

        Retour();
			}
			catch(Exception ex) {

				MessageException(Message: ex.ToString());
			}
		}

    static void Variable() {

      string ParamTest = "Argumentation";

      try {

        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "Fichier GalacticShrine.dll :");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"GalacticShrine.Repertoire[\"ProgramFiles\"] => {GalacticShrine.Repertoire["ProgramFiles"]}");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"GalacticShrine.Repertoire[\"Documents\"] => {GalacticShrine.Repertoire["Documents"]}");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"GalacticShrine.Repertoire[\"Racine\"] => {GalacticShrine.Repertoire["Racine"]}");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"GalacticShrine.Repertoire[\"Societe\"] => {GalacticShrine.Repertoire["Societe"]}");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"GalacticShrine.Repertoire[\"Config\"] => {GalacticShrine.Repertoire["Config"]}");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"GalacticShrine.Repertoire[\"Source\"] => {GalacticShrine.Repertoire["Source"]}");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"GalacticShrine.DossierReference.Rs => {Rs}");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"GalacticShrine.DossierReference.RepertoireSeparateur => {RepertoireSeparateur}");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: "");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"GalacticShrine.Resources.RepertoiresSeparateursInvalides => {GsP.Resources.RepertoiresSeparateursInvalides}", Argument: $"{ParamTest}");
        Terminal.Ecrire(ReserveToutLaLigne: true, Texte: $"GalacticShrine.Resources.FichierTermineInvalide => {GsP.Resources.FichierTermineInvalide}");
        Terminal.LigneSeparatrice();
        
        Retour();
      }
      catch(Exception ex) {

        MessageException(Message: ex.ToString());
      }
    }

    static void Ecrire() {

			try {

				Terminal.Ecrire(Texte: $" {Resources.Default} \n",       Couleur: Bg.Default);
				Terminal.Ecrire(Texte: $" {Resources.Magenta} \n",       Couleur: Bg.Magenta);
				Terminal.Ecrire(Texte: $" {Resources.Sourdine} \n",      Couleur: Bg.Sourdine);
				Terminal.Ecrire(Texte: $" {Resources.Primaire} \n",      Couleur: Bg.Primaire);
				Terminal.Ecrire(Texte: $" {Resources.Succes} \n",        Couleur: Bg.Succes);
				Terminal.Ecrire(Texte: $" {Resources.Info} \n",          Couleur: Bg.Info);
				Terminal.Ecrire(Texte: $" {Resources.Avertissemens} \n", Couleur: Bg.Avertissement);
				Terminal.Ecrire(Texte: $" {Resources.Danger} \n",				 Couleur: Bg.Danger);
        Terminal.Ecrire2Couleur(Texte: $" {Resources.PersonnaliserLesCouleur} \n", CouleurTexte: Txt.Red, CouleurArrierePlan: Bg.Blue);
        Terminal.Ecrire2Couleur(Texte: $" {Resources.PersonnaliserLesCouleur} \n", CouleurTexte: Txt.DarkGreen, CouleurArrierePlan: Bg.DarkYellow);
        Terminal.Ecrire2Couleur(Texte: $" {Resources.PersonnaliserLesCouleur} \n", CouleurTexte: Txt.Cyan, CouleurArrierePlan: Bg.DarkCyan);

        Retour();
			}
			catch(Exception ex) {

				MessageException(Message: ex.ToString());
			}
		}

		static void LigneDecriture() {

			try {

				Terminal.Ecrire(Texte: "Short Text at First Preceded with a ", Couleur: Bg.Info);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: " Long Multi line text with Line Enveloppement that bring a new line", Couleur: Bg.Succes);
				Terminal.Enveloppement(Texte: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer sed turpis in ligula aliquet ornare tristique sed ante. Nam pretium ullamcorper condimentum. Aliquam quis sodales ex, vitae gravida metus. Suspendisse potenti. Maecenas nunc sapien, semper vel tincidunt sed, scelerisque ut est. Nunc eu venenatis libero. Nulla consectetur pretium leo. Nullam suscipit scelerisque neque fringilla volutpat. Aliquam condimentum, neque quis malesuada ultrices, mauris velit tincidunt arcu, vel sodales tortor felis quis velit. ", Couleur: Bg.Avertissement);
				Terminal.Enveloppement(Texte: "Aliquam tempus ullamcorper orci, vitae pretium leo maximus ut. Aliquam iaculis leo sed tempor mattis.", Couleur: Bg.Danger);

				Retour();
			}
			catch(Exception ex) {

				MessageException(Message: ex.ToString());
			}
		}

		static void TexteAlignement () {

			try {

				Terminal.Aligner(Alignement: Alignement.Centre, Texte: Resources.TexteAligneAuCentre, Couleur: Bg.Info);
				Terminal.Aligner(Alignement: Alignement.Droite, Texte: Resources.TexteAligneADroite,	Couleur: Txt.Default);
				Terminal.Aligner(Alignement: Alignement.Gauche, Texte: Resources.TexteAligneAGauche,	Couleur: Txt.Danger);
				Terminal.AlignerLaDivision(Texte: Resources.DivisionGaucheDroite, Couleur: Bg.Succes);

				Retour();
			}
			catch(Exception ex) {

				MessageException(Message: ex.ToString());
			}
		}

		static void LigneSeparatrice () {

			try {

				Terminal.LigneSeparatrice();
				Terminal.LigneSeparatrice(Couleur: Bg.Danger);
				Terminal.LigneSeparatrice(Lignes: 3);
				Terminal.LigneSeparatrice(Lignes: 3, Couleur: Bg.Succes);

				Retour();
			}
			catch(Exception ex) {

				MessageException(Message: ex.ToString());
			}
		}

		static void LigneSeparatriceDecoree() {

			try {

				Terminal.LigneSeparatriceDecoree(Character: '-', Couleur: Bg.Default);
				Terminal.LigneSeparatriceDecoree(Character: '#', Couleur: Bg.Sourdine);
				Terminal.LigneSeparatriceDecoree(Character: '+', Couleur: Bg.Primaire);
				Terminal.LigneSeparatriceDecoree(Character: '¤', Couleur: Bg.Succes);
				Terminal.LigneSeparatriceDecoree(Character: '♥', Couleur: Bg.Info);
				Terminal.LigneSeparatriceDecoree(Character: '~', Couleur: Bg.Avertissement);
				Terminal.LigneSeparatriceDecoree(Character: '°', Couleur: Bg.Danger);

        Retour();
			}
			catch(Exception ex) {

				MessageException(Message: ex.ToString());
			}
		}

		public static void ChangementDeTheme (string option) {

			if(!String.IsNullOrEmpty(option)) {

				switch(option) {

					case "d":

						Terminal = new Format(Theme: Sombre);
						break;
					case "l":

						Terminal = new Format(Theme: Lumineux);
						break;
				}
			}

			Menu();
		}
	}
}