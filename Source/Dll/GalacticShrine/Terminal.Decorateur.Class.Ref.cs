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
using System.Text;

namespace GalacticShrine.Terminal {

	/**
   * <summary>
   *   [FR] Classe statique Decorateur fournissant des méthodes pour formater et afficher du texte
   *        dans le terminal en fonction de la largeur de l'écran.
   *   [EN] Static Decorateur class providing methods to format and display text in the terminal
   *        according to the screen width.
   * </summary>
   **/
	public static class Decorateur {

		/**
     * <summary>
     *   [FR] Largeur de l'écran du terminal, calculée en fonction de la largeur de la fenêtre de la console.
     *   [EN] Terminal screen width, calculated from the console window width.
     * </summary>
     **/
		static int LargeurEcran { get; set; }

		/**
     * <summary>
     *   [FR] Affiche le texte fourni dans le terminal en gérant le retour à la ligne
     *        en fonction de la largeur de l'écran.
     *   [EN] Displays the provided text in the terminal, managing line wrapping
     *        based on the screen width.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à afficher dans le terminal.
     *   [EN] The text to display in the terminal.
     * </param>
     **/
		public static void Texte(string Texte) {

			ArgumentNullException.ThrowIfNull(Texte);

			var Ligne = new StringBuilder();
			var Mots = Texte.Split(' ');
			LargeurEcran = Math.Max(1, Console.WindowWidth - 3);

			foreach(var Element in Mots) {

				Lignes(ref Ligne, Element);
				Elements(ref Ligne, Element);
			}

			var ContenuFinal = Ligne.ToString();

			if(!string.IsNullOrEmpty(ContenuFinal.Trim())) {

				Sortie.Ecrire(true, ContenuFinal.TrimEnd());
			}
		}

		/**
     * <summary>
     *   [FR] Gère l'écriture des lignes dans le terminal en fonction de la largeur de l'écran.
     *   [EN] Manages writing lines to the terminal based on the screen width.
     * </summary>
     * <param name="Ligne">
     *   [FR] Référence au <see cref="StringBuilder"/> accumulant le contenu de la ligne actuelle.
     *   [EN] Reference to the <see cref="StringBuilder"/> accumulating the current line content.
     * </param>
     * <param name="Element">
     *   [FR] Le mot actuel à prendre en compte.
     *   [EN] The current word to take into account.
     * </param>
     **/
		static void Lignes(ref StringBuilder Ligne, string Element) {

			if(((Ligne.Length + Element.Length) >= LargeurEcran) ||
				 Ligne.ToString().Contains(Environment.NewLine)) {

				Sortie.Ecrire(true, Ligne.ToString().TrimEnd());
				Ligne.Clear();
			}
		}

		/**
     * <summary>
     *   [FR] Gère l'ajout des éléments (mots) à la ligne en cours,
     *        en tenant compte de la largeur de l'écran.
     *   [EN] Manages adding elements (words) to the current line,
     *        taking the screen width into account.
     * </summary>
     * <param name="Ligne">
     *   [FR] Référence au <see cref="StringBuilder"/> accumulant le contenu de la ligne actuelle.
     *   [EN] Reference to the <see cref="StringBuilder"/> accumulating the current line content.
     * </param>
     * <param name="Element">
     *   [FR] Le mot actuel à ajouter à la ligne.
     *   [EN] The current word to add to the line.
     * </param>
     **/
		static void Elements(ref StringBuilder Ligne, string Element) {

			if(Element.Length >= LargeurEcran) {

				if(Ligne.Length > 0) {

					Sortie.Ecrire(true, $" {Ligne.ToString().TrimEnd()}");
					Ligne.Clear();
				}

				int TailleDesMorceaux = Element.Length - LargeurEcran;
				string Morceau = Element.Substring(0, LargeurEcran);

				Ligne.Append($"{Morceau} ");
				Lignes(ref Ligne, Element);

				Element = Element.Substring(LargeurEcran, TailleDesMorceaux);
				Elements(ref Ligne, Element);
			}
			else {

				Ligne.Append($"{Element} ");
			}
		}
	}
}
