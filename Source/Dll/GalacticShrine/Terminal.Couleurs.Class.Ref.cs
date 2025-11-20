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
namespace GalacticShrine.Terminal {

	/**
   * <summary>
   *   [FR] Classe statique Couleurs, offrant des constantes pour identifier des couleurs (texte / fond) dans un terminal.
   *   [EN] Static Couleurs class, providing constants to identify text/background colors in a terminal.
   * </summary>
   **/
	public static class Couleurs {

		/**
     * <summary>
     *   [FR] Classe statique de constantes de couleurs pour le texte.
     *   [EN] Static class of color constants for text.
     * </summary>
     **/
		public static class Txt {

			public const string Default       = "text.default";
			public const string Black         = "text.black";
			public const string DarkBlue      = "text.darkBlue";
			public const string DarkGreen     = "text.darkGreen";
			public const string DarkCyan      = "text.darkCyan";
			public const string DarkRed       = "text.darkRed";
			public const string DarkMagenta   = "text.darkMagenta";
			public const string DarkYellow    = "text.darkYellow";
			public const string DarkGray      = "text.darkGray";
			public const string Blue          = "text.blue";
			public const string Green         = "text.green";
			public const string Cyan          = "text.cyan";
			public const string Red           = "text.red";
			public const string Magenta       = "text.magenta";
			public const string Yellow        = "text.yellow";
			public const string Gray          = "text.gray";
			public const string Sourdine      = "text.sourdine";
			public const string Primaire      = "text.primaire";
			public const string Succes        = "text.succes";
			public const string Info          = "text.info";
			public const string Avertissement = "text.avertissement";
			public const string Danger        = "text.danger";
		}

		/**
     * <summary>
     *   [FR] Classe statique de constantes de couleurs pour le fond (background).
     *   [EN] Static class of color constants for background.
     * </summary>
     **/
		public static class Bg {

			public const string Default       = "bg.default";
			public const string Black         = "bg.black";
			public const string DarkBlue      = "bg.darkBlue";
			public const string DarkGreen     = "bg.darkGreen";
			public const string DarkCyan      = "bg.darkCyan";
			public const string DarkRed       = "bg.darkRed";
			public const string DarkMagenta   = "bg.darkMagenta";
			public const string DarkYellow    = "bg.darkYellow";
			public const string DarkGray      = "bg.darkGray";
			public const string Blue          = "bg.blue";
			public const string Green         = "bg.green";
			public const string Cyan          = "bg.cyan";
			public const string Red           = "bg.red";
			public const string Magenta       = "bg.magenta";
			public const string Yellow        = "bg.yellow";
			public const string Gray          = "bg.gray";
			public const string Sourdine      = "bg.sourdine";
			public const string Primaire      = "bg.primaire";
			public const string Succes        = "bg.succes";
			public const string Info          = "bg.info";
			public const string Avertissement = "bg.avertissement";
			public const string Danger        = "bg.danger";
		}

		/**
    * <summary>
    *   [FR] Retourne la couleur de texte en fonction d'un booléen (<paramref name="EstValide"/>).
    *        Si vrai, renvoie la couleur <c>Txt.Primaire</c>, sinon <c>Txt.Sourdine</c>.
    *   [EN] Returns the text color depending on a boolean (<paramref name="EstValide"/>).
    *        If true, returns <c>Txt.Primaire</c>, otherwise <c>Txt.Sourdine</c>.
    * </summary>
    * <param name="EstValide">
    *   [FR] Indique si l'état est valide ou non.
    *   [EN] Indicates whether the state is valid or not.
    * </param>
    * <returns>
    *   [FR] Une chaîne représentant la clé de la couleur à utiliser.
    *   [EN] A string representing the color key to use.
    * </returns>
    **/
		public static string TxtStatus(bool EstValide) {

			return EstValide ? Txt.Primaire : Txt.Sourdine;
		}
	}
}
