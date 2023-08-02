using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Gs.Outils.Platform;
using Gs.Enumeration;
using Gs.UI.Terminal;
using Gs.Terminal;
using static Gs.Terminal.Couleurs;

namespace Gs.Test.Terminal {
	
	internal class Program {

		private static Format BeauTerminal { get; set; }

		static void Main() {
			try {
				switch(OS.ObtenirLesInfoCourantes()) {
					case "Windows":
						BeauTerminal = new Format(Theme.Sombre);
						break;
					case "Linux":
						BeauTerminal = new Format(new ThemeSombre());
						break;
					case "OSX":
						BeauTerminal = new Format(Theme.Lumineux);
						break;
				}
				Menu();
				BeauTerminal.RetablirCouleur();
				BeauTerminal.Eclaircir();
				return ;
			}
			catch(ArgumentOutOfRangeException) {
				MessageException("Ah mes yeux ! Pourquoi cette console est trop petite ?");
			}
			catch(Exception ex) {
				MessageException(ex.ToString());
			}
		}

		static void Menu () {

			string AppName = typeof(Program).Assembly.GetName().Name;
			Version AppVersion = Assembly.GetExecutingAssembly().GetName().Version;
			string UserName = Environment.UserName + "@" + Environment.UserDomainName;

			BeauTerminal.Eclaircir();
			BeauTerminal.LigneSeparatriceDecoree('−', Couleurs.Bg.Avertissement);
			BeauTerminal.Aligner(Alignement.Centre, AppName.Replace("T", " T") + " .NET " + AppVersion, Couleurs.Bg.Avertissement);
			BeauTerminal.LigneSeparatriceDecoree('−', Couleurs.Bg.Avertissement);
			BeauTerminal.LigneSeparatrice(Bg.Sourdine);
			BeauTerminal.Ecrire($"{"[1] ",-4}", Bg.Sourdine); BeauTerminal.Ecrire(true, "Couleur", Bg.Sourdine);
			BeauTerminal.Ecrire($"{"[2] ",-4}", Bg.Sourdine); BeauTerminal.Ecrire(true, "Écrire", Bg.Sourdine);
			BeauTerminal.Ecrire($"{"[3] ",-4}", Bg.Sourdine); BeauTerminal.Ecrire(true, "Ligne d'écriture", Bg.Sourdine);
			BeauTerminal.LigneSeparatrice(Bg.Sourdine);
			BeauTerminal.Ecrire($"{"[4] ",-4}", Bg.Sourdine); BeauTerminal.Ecrire(true, "Alignement du texte", Bg.Sourdine);
			BeauTerminal.Ecrire($"{"[5] ",-4}", Bg.Sourdine); BeauTerminal.Ecrire(true, "Lignes vides", Bg.Sourdine);
			BeauTerminal.Ecrire($"{"[6] ",-4}", Bg.Sourdine); BeauTerminal.Ecrire(true, "Lignes de division (Linge de décoration)", Bg.Sourdine);
			BeauTerminal.LigneSeparatrice(Bg.Sourdine);
			BeauTerminal.Ecrire($"{"[D] ",-4}", Bg.Sourdine); BeauTerminal.Ecrire(true, "Thème sombre", Bg.Sourdine);
			BeauTerminal.Ecrire($"{"[L] ",-4}", Bg.Sourdine); BeauTerminal.Ecrire(true, "Thème Lumineux", Bg.Sourdine);
			BeauTerminal.LigneSeparatrice(Bg.Sourdine);
			BeauTerminal.LigneSeparatrice(Bg.Avertissement);
			BeauTerminal.LigneSeparatrice();
			BeauTerminal.Aligner(Alignement.Centre, "Appuyez sur la touche [Q] puis sur Entrée pour quitter", Txt.Danger);
			BeauTerminal.LigneSeparatrice();
			BeauTerminal.Ecrire($"{UserName} \n", Txt.Succes);
			BeauTerminal.Ecrire($"# ", Txt.Danger);
			string opt = Console.ReadLine();
			opt = opt.ToLower();

			BeauTerminal.Eclaircir();
			switch(opt) {
				case "1": ThemeColors(); break;
				case "2": Ecrire(); break;
				case "3": WriteLine(); break;
				case "4": TextAlign(); break;
				case "5": LigneSeparatrice(); break;
				case "6": LigneSeparatriceDecoree(); break;
				case "d":
				case "l":
					ThemeSwitch(opt);
					break;
				case "q": break;
				default: Menu(); break;
			}
		}

		static void Back () {
			BeauTerminal.LigneSeparatrice();
			BeauTerminal.Ecrire("Appuyez sur n'importe quelle touche pour continuer");
			Console.ReadKey();
			Menu();
		}

		static void MessageException (string message) {
			BeauTerminal.RetablirCouleur();
			BeauTerminal.Eclaircir();
			BeauTerminal.Ecrire(true, message, Bg.Danger);
		}

		static void ThemeColors () {
			try {
				BeauTerminal.Ecrire(true, "Text Default", Txt.Default);
				BeauTerminal.Ecrire(true, "Text Magenta", Txt.Magenta);
				BeauTerminal.Ecrire(true, "Text Sourdine", Txt.Sourdine);
				BeauTerminal.Ecrire(true, "Text Primaire", Txt.Primaire);
				BeauTerminal.Ecrire(true, "Text Succes", Txt.Succes);
				BeauTerminal.Ecrire(true, "Text Info", Txt.Info);
				BeauTerminal.Ecrire(true, "Text Avertissement", Txt.Avertissement);
				BeauTerminal.Ecrire(true, "Text Danger", Txt.Danger);
				BeauTerminal.Ecrire(true, "Background Default", Bg.Default);
				BeauTerminal.Ecrire(true, "Background Magenta", Bg.Magenta);
				BeauTerminal.Ecrire(true, "Background Sourdine", Bg.Sourdine);
				BeauTerminal.Ecrire(true, "Background Primaire", Bg.Primaire);
				BeauTerminal.Ecrire(true, "Background Succes", Bg.Succes);
				BeauTerminal.Ecrire(true, "Background Info", Bg.Info);
				BeauTerminal.Ecrire(true, "Background Avertissement", Bg.Avertissement);
				BeauTerminal.Ecrire(true, "Background Danger", Bg.Danger);

				Back();
			}
			catch(Exception ex) {

				MessageException(ex.ToString());
			}
		}

		static void Ecrire () {

			try {
				BeauTerminal.Ecrire(" Default ", Bg.Default);
				BeauTerminal.Ecrire(" Magenta ", Bg.Magenta);
				BeauTerminal.Ecrire(" Sourdine   ", Bg.Sourdine);
				BeauTerminal.Ecrire(" Primaire ", Bg.Primaire);
				BeauTerminal.Ecrire(" Succes ", Bg.Succes);
				BeauTerminal.Ecrire(" Info    ", Bg.Info);
				BeauTerminal.Ecrire(" Avertissement ", Bg.Avertissement);
				BeauTerminal.Ecrire(true, " Danger  ", Bg.Danger);

				Back();
			}
			catch(Exception ex) {
				MessageException(ex.ToString());
			}
		}

		static void WriteLine () {
			try {
				BeauTerminal.Ecrire("Short Text at First Preceded with a ", Bg.Info);
				BeauTerminal.Ecrire(true, " Long Multi line text with Line Enveloppement that bring a new line", Bg.Succes);
				BeauTerminal.Enveloppement("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer sed turpis in ligula aliquet ornare tristique sed ante. Nam pretium ullamcorper condimentum. Aliquam quis sodales ex, vitae gravida metus. Suspendisse potenti. Maecenas nunc sapien, semper vel tincidunt sed, scelerisque ut est. Nunc eu venenatis libero. Nulla consectetur pretium leo. Nullam suscipit scelerisque neque fringilla volutpat. Aliquam condimentum, neque quis malesuada ultrices, mauris velit tincidunt arcu, vel sodales tortor felis quis velit. ", Bg.Avertissement);
				BeauTerminal.Enveloppement("Aliquam tempus ullamcorper orci, vitae pretium leo maximus ut. Aliquam iaculis leo sed tempor mattis.", Bg.Danger);

				Back();
			}
			catch(Exception ex) {
				MessageException(ex.ToString());
			}
		}

		static void TextAlign () {
			try {
				BeauTerminal.Aligner(Alignement.Centre, "Texte aligné au centre", Bg.Info);
				BeauTerminal.Aligner(Alignement.Droite, "Texte aligné à droite", Txt.Default);
				BeauTerminal.Aligner(Alignement.Gauche, "Texte aligné à gauche", Txt.Danger);
				BeauTerminal.AlignerLaDivision("<-Gauche|Droite->", Bg.Succes);

				Back();
			}
			catch(Exception ex) {
				MessageException(ex.ToString());
			}
		}

		static void LigneSeparatrice () {
			try {
				BeauTerminal.LigneSeparatrice();
				BeauTerminal.LigneSeparatrice(Couleurs.Bg.Danger);
				BeauTerminal.LigneSeparatrice(3);
				BeauTerminal.LigneSeparatrice(3, Bg.Succes);

				Back();
			}
			catch(Exception ex) {
				MessageException(ex.ToString());
			}
		}

		static void LigneSeparatriceDecoree () {
			try {
				BeauTerminal.LigneSeparatriceDecoree('-', Couleurs.Bg.Default);
				BeauTerminal.LigneSeparatriceDecoree('#', Couleurs.Bg.Sourdine);
				BeauTerminal.LigneSeparatriceDecoree('+', Couleurs.Bg.Primaire);
				BeauTerminal.LigneSeparatriceDecoree('¤', Couleurs.Bg.Succes);
				BeauTerminal.LigneSeparatriceDecoree('♥', Couleurs.Bg.Info);
				BeauTerminal.LigneSeparatriceDecoree('~', Couleurs.Bg.Avertissement);
				BeauTerminal.LigneSeparatriceDecoree('°', Couleurs.Bg.Danger);

				Back();
			}
			catch(Exception ex) {
				MessageException(ex.ToString());
			}
		}

		public static void ThemeSwitch (string option) {
			if(!String.IsNullOrEmpty(option)) {
				switch(option) {
					case "d":
						BeauTerminal = new Format(Theme.Sombre);
						break;
					case "l":
						BeauTerminal = new Format(Theme.Lumineux);
						break;
				}
			}
			Menu();
		}
	}
}