/**
 * Copyright © 2023-2025, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2023-2025, Galactic-Shrine - Tous droits réservés.
 * 
 * Mozilla Public License 2.0 / Licence Publique Mozilla 2.0
 *
 * This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
 * If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.
 * Modifications to this file must be shared under the same Mozilla Public License, v. 2.0.
 *
 * Cette Forme de Code Source est soumise aux termes de la Licence Publique Mozilla, version 2.0.
 * Si une copie de la MPL ne vous a pas été distribuée avec ce fichier, vous pouvez en obtenir une à l'adresse suivante : https://mozilla.org/MPL/2.0/.
 * Les modifications apportées à ce fichier doivent être partagées sous la même Licence Publique Mozilla, v. 2.0.
 **/
using System;
using GalacticShrine.Enumeration;

namespace GalacticShrine.Terminal {

	/**
   * <summary>
   *   [FR] Classe statique Sortie fournissant des méthodes simplifiées pour écrire et aligner du texte dans le terminal.
   *   [EN] Static Sortie class providing simplified methods to write and align text in the terminal.
   * </summary>
   **/
	public static class Sortie {

		/**
     * <summary>
     *   [FR] Écrit la valeur de la chaîne spécifiée sur le flux de sortie standard.
     *        Raccourci de <see cref="System.Console.Write(string?)"/>.
     *   [EN] Writes the specified string value to the standard output stream.
     *        Shortcut for <see cref="System.Console.Write(string?)"/>.
     * </summary>
     * <param name="Texte">
     *   [FR] Texte à écrire.
     *   [EN] Text to write.
     * </param>
     **/
		public static void Ecrire(string Texte) => Console.Write(Texte);

		/**
     * <summary>
     *   [FR] Écrit une chaîne formatée sur le flux de sortie standard.
     *        Raccourci de <see cref="System.Console.Write(string?, object?)"/>.
     *   [EN] Writes a formatted string value to the standard output stream.
     *        Shortcut for <see cref="System.Console.Write(string?, object?)"/>.
     * </summary>
     * <param name="Texte">
     *   [FR] Texte à écrire, pouvant contenir des éléments de format.
     *   [EN] Text to write, which may contain format items.
     * </param>
     * <param name="Argument">
     *   [FR] Argument inséré dans le texte formaté.
     *   [EN] Argument inserted into the formatted text.
     * </param>
     **/
		public static void Ecrire(string Texte, object Argument) => Console.Write(Texte, Argument);

		/**
     * <summary>
     *   [FR] Écrit le texte en option sur toute la largeur de la ligne.
     *        Utilise <see cref="Console.WindowWidth"/> pour calculer le remplissage.
     *   [EN] Writes text, optionally filling the entire console line width.
     *        Uses <see cref="Console.WindowWidth"/> to compute padding.
     * </summary>
     * <param name="ReserveToutLaLigne">
     *   [FR] Si <c>true</c>, le texte remplit la ligne entière avec un <c>PadRight</c>.
     *   [EN] If <c>true</c>, the text fills the entire line using <c>PadRight</c>.
     * </param>
     * <param name="Texte">
     *   [FR] Texte à écrire.
     *   [EN] Text to write.
     * </param>
     **/
		public static void Ecrire(bool ReserveToutLaLigne, string Texte) {

			switch(ReserveToutLaLigne) {

				case true:

				int Dimension = Console.WindowWidth - 1;

				if(Dimension != Console.CursorLeft + 1) {

					Dimension = Dimension - Console.CursorLeft;
				}

				if(Dimension < Texte.Length) {

					Dimension = Console.WindowWidth + Dimension;
				}

				if(Dimension < 0) {

					Dimension = Texte.Length;
				}

				Console.WriteLine($"{Texte.PadRight(Dimension)}");
				break;

				default:

				Ecrire(Texte);
				break;
			}
		}

		/**
     * <summary>
     *   [FR] Écrit le texte formaté en option sur toute la largeur de la ligne.
     *        Utilise <see cref="Console.WindowWidth"/> pour calculer le remplissage.
     *   [EN] Writes formatted text, optionally filling the entire console line width.
     *        Uses <see cref="Console.WindowWidth"/> to compute padding.
     * </summary>
     * <param name="ReserveToutLaLigne">
     *   [FR] Si <c>true</c>, le texte remplit la ligne entière avec un <c>PadRight</c>.
     *   [EN] If <c>true</c>, the text fills the entire line using <c>PadRight</c>.
     * </param>
     * <param name="Texte">
     *   [FR] Texte à écrire, pouvant contenir des éléments de format.
     *   [EN] Text to write, which may contain format items.
     * </param>
     * <param name="Argument">
     *   [FR] Argument inséré dans le texte formaté.
     *   [EN] Argument inserted into the formatted text.
     * </param>
     **/
		public static void Ecrire(bool ReserveToutLaLigne, string Texte, object Argument) {

			switch(ReserveToutLaLigne) {

				case true:

				int Dimension = Console.WindowWidth - 1;

				if(Dimension != Console.CursorLeft + 1) {

					Dimension = Dimension - Console.CursorLeft;
				}

				if(Dimension < Texte.Length) {

					Dimension = Console.WindowWidth + Dimension;
				}

				if(Dimension < 0) {

					Dimension = Texte.Length;
				}

				Console.WriteLine($"{Texte.PadRight(Dimension)}", Argument);
				break;

				default:

				Ecrire(Texte, Argument);
				break;
			}
		}

		/**
     * <summary>
     *   [FR] Aligne le texte sur toute la largeur de la fenêtre en le remplissant à droite.
     *   [EN] Aligns the text to fill the console width by padding on the right.
     * </summary>
     * <param name="Texte">
     *   [FR] Texte à aligner.
     *   [EN] Text to align.
     * </param>
     **/
		public static void Aligner(string Texte) =>
			Ecrire(true, $"{Texte.PadRight(Console.WindowWidth - 1)}");

		/**
     * <summary>
     *   [FR] Aligne le texte formaté sur toute la largeur de la fenêtre en le remplissant à droite.
     *   [EN] Aligns the formatted text to fill the console width by padding on the right.
     * </summary>
     * <param name="Texte">
     *   [FR] Texte à aligner, pouvant contenir des éléments de format.
     *   [EN] Text to align, which may contain format items.
     * </param>
     * <param name="Argument">
     *   [FR] Argument inséré dans le texte formaté.
     *   [EN] Argument inserted into the formatted text.
     * </param>
     **/
		public static void Aligner(string Texte, object Argument) =>
			Ecrire(true, $"{Texte.PadRight(Console.WindowWidth - 1)}", Argument);

		/**
     * <summary>
     *   [FR] Aligne le texte selon l'alignement spécifié.
     *   [EN] Aligns the text according to the specified alignment.
     * </summary>
     * <param name="Alignement">
     *   [FR] Type d'alignement à appliquer (Droite, Gauche, Centre).
     *   [EN] Type of alignment to apply (Right, Left, Center).
     * </param>
     * <param name="Texte">
     *   [FR] Texte à aligner.
     *   [EN] Text to align.
     * </param>
     **/
		public static void Aligner(Alignement Alignement, string Texte) {

			switch(Alignement) {

				case Alignement.Droite:

				// Aligne le texte à droite (espaces à gauche).
				Ecrire(true, $"{Texte.PadLeft(Console.WindowWidth - 1)}");
				break;

				case Alignement.Gauche:

				// Aligne le texte à gauche (espaces à droite).
				Ecrire(true, $"{Texte.PadRight(Console.WindowWidth - 1)}");
				break;

				case Alignement.Centre:

				decimal Taille = Console.WindowWidth - 1 - Texte.Length;
				int TailleDroite = (int)Math.Round(Taille / 2);
				int TailleGauche = (int)(Taille - TailleDroite);
				string MargeGauche = new string(' ', TailleGauche);
				string MargeDroite = new string(' ', TailleDroite);

				Ecrire(MargeGauche);
				Ecrire(Texte);
				Ecrire(true, MargeDroite);
				break;

				default:

				Ecrire(true, $"{Texte.PadRight(Console.WindowWidth - 1)}");
				break;
			}
		}

		/**
     * <summary>
     *   [FR] Aligne le texte formaté selon l'alignement spécifié.
     *   [EN] Aligns the formatted text according to the specified alignment.
     * </summary>
     * <param name="Alignement">
     *   [FR] Type d'alignement à appliquer (Droite, Gauche, Centre).
     *   [EN] Type of alignment to apply (Right, Left, Center).
     * </param>
     * <param name="Texte">
     *   [FR] Texte à aligner, pouvant contenir des éléments de format.
     *   [EN] Text to align, which may contain format items.
     * </param>
     * <param name="Argument">
     *   [FR] Argument inséré dans le texte formaté.
     *   [EN] Argument inserted into the formatted text.
     * </param>
     **/
		public static void Aligner(Alignement Alignement, string Texte, object Argument) {

			switch(Alignement) {

				case Alignement.Droite:

				// Aligne le texte à droite (espaces à gauche).
				Ecrire(true, $"{Texte.PadLeft(Console.WindowWidth - 1)}", Argument);
				break;

				case Alignement.Gauche:

				// Aligne le texte à gauche (espaces à droite).
				Ecrire(true, $"{Texte.PadRight(Console.WindowWidth - 1)}", Argument);
				break;

				case Alignement.Centre:

				decimal Taille = Console.WindowWidth - 1 - Texte.Length;
				int TailleDroite = (int)Math.Round(Taille / 2);
				int TailleGauche = (int)(Taille - TailleDroite);
				string MargeGauche = new string(' ', TailleGauche);
				string MargeDroite = new string(' ', TailleDroite);

				Ecrire(MargeGauche);
				Ecrire(Texte, Argument);
				Ecrire(true, MargeDroite);
				break;

				default:

				Ecrire(true, $"{Texte.PadRight(Console.WindowWidth - 1)}", Argument);
				break;
			}
		}

		/**
     * <summary>
     *   [FR] Dessine une ligne composée du caractère spécifié sur toute la largeur de la fenêtre.
     *   [EN] Draws a line made of the specified character across the console width.
     * </summary>
     * <param name="Caractere">
     *   [FR] Caractère utilisé pour dessiner la ligne.
     *   [EN] Character used to draw the line.
     * </param>
     **/
		public static void LigneSeparatriceDecoree(char Caractere) {

			string Texte = new string(Caractere, Console.WindowWidth - 1);
			Ecrire(true, Texte);
		}

		/**
     * <summary>
     *   [FR] Dessine une ou plusieurs lignes séparatrices vides (espaces).
     *   [EN] Draws one or more blank separator lines (spaces).
     * </summary>
     * <param name="Lignes">
     *   [FR] Nombre de lignes séparatrices à dessiner (1 si <c>null</c>).
     *   [EN] Number of separator lines to draw (1 if <c>null</c>).
     * </param>
     **/
		public static void LigneSeparatrice(int? Lignes = 1) {

			int NombreLignes = Lignes ?? 1;

			for(int i = 0; i < NombreLignes; i++) {

				LigneSeparatriceDecoree(' ');
			}
		}

		/**
     * <summary>
     *   [FR] Affiche deux textes aux extrémités gauche et droite de la fenêtre.
     *   [EN] Displays two texts at the left and right extremes of the console window.
     * </summary>
     * <param name="Gauche">
     *   [FR] Texte aligné à gauche.
     *   [EN] Text aligned to the left.
     * </param>
     * <param name="Droit">
     *   [FR] Texte aligné à droite.
     *   [EN] Text aligned to the right.
     * </param>
     **/
		public static void Extreme(string Gauche, string Droit) {

			decimal Taille = Console.WindowWidth - 1;
			int MargeDroite = (int)Math.Round(Taille / 2);
			int MargeGauche = (int)(Taille - MargeDroite);

			Ecrire(Gauche.PadRight(MargeDroite));
			Ecrire(true, Droit.PadLeft(MargeGauche));
		}
	}
}
