using System;
using System.Reflection;
using GsP = Gs.Properties;
using Gs.Outils.Platform;
using Gs.Enumeration;
using Gs.Terminal;
using Gs.UI.Terminal;
using static Gs.Terminal.Couleurs;
using static Gs.Terminal.Terminal;
using Gs.Test.Terminal.Properties;

namespace Gs.Test.Terminal {

  internal class Program {

		private static Format Terminal { get; set; }

		static void Main() {
      
      try {
       
        switch(OS.ObtenirLesInfoCourantes()) {

					case "Windows":

						Terminal = new Format(Theme.Sombre);
						break;

					case "Linux":

						Terminal = new Format(new ThemeSombre());
						break;

					case "OSX":

						Terminal = new Format(Theme.Lumineux);
						break;
				}

				Menu();
				Terminal.RetablirCouleur();
				Terminal.Eclaircir();
			}
			catch(ArgumentOutOfRangeException) {

				MessageException(GsP.Resources.ConsoleEstTropPetite);
			}
			catch(Exception ex) {

				MessageException(ex.ToString());
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

		static void MessageException (string message) {

			Terminal.RetablirCouleur();
			Terminal.Eclaircir();
			Terminal.Ecrire(ReserveToutLaLigne: true, Texte: message, Couleur: Bg.Danger);
		}

		static void CouleurDeTheme() {

			try {

				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteDefault,						 Couleur: Txt.Default);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteMagenta,						 Couleur: Txt.Magenta);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteSourdine,						 Couleur: Txt.Sourdine);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TextePrimaire,						 Couleur: Txt.Primaire);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteSucces,							 Couleur: Txt.Succes);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteInfo,								 Couleur: Txt.Info);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteAvertissement,			 Couleur: Txt.Avertissement);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.TexteDanger,							 Couleur: Txt.Danger);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanDefault,			 Couleur: Bg.Default);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanMagenta,			 Couleur: Bg.Magenta);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanSourdine,			 Couleur: Bg.Sourdine);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanPrimaire,			 Couleur: Bg.Primaire);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanSucces,				 Couleur: Bg.Succes);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanInfo,					 Couleur: Bg.Info);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanAvertissement, Couleur: Bg.Avertissement);
				Terminal.Ecrire(ReserveToutLaLigne: true, Texte: Resources.ArrierePlanDanger,				 Couleur: Bg.Danger);

				Retour();
			}
			catch(Exception ex) {

				MessageException(ex.ToString());
			}
		}

    static void Variable() {

      try {

        Terminal.Ecrire(Texte: $"Gs.ProgramFiles => {Gs.ProgramFiles} \n");
        Terminal.Ecrire(Texte: $"Gs.Documents => {Gs.Documents} \n");
        Terminal.Ecrire(Texte: $"Gs.Rs => {Gs.Rs} \n");
        Terminal.Ecrire(Texte: $"Gs.RepertoireSeparateur => {Gs.RepertoireSeparateur} \n");

        Retour();
      }
      catch(Exception ex) {

        MessageException(ex.ToString());
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

				Retour();
			}
			catch(Exception ex) {

				MessageException(ex.ToString());
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

				MessageException(ex.ToString());
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

				MessageException(ex.ToString());
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

				MessageException(ex.ToString());
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

				MessageException(ex.ToString());
			}
		}

		public static void ChangementDeTheme (string option) {

			if(!String.IsNullOrEmpty(option)) {

				switch(option) {

					case "d":

						Terminal = new Format(Theme: Theme.Sombre);
						break;
					case "l":

						Terminal = new Format(Theme: Theme.Lumineux);
						break;
				}
			}

			Menu();
		}
	}
}