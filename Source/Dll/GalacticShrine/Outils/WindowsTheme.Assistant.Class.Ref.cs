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
using Microsoft.Win32;
using GalacticShrine.Enumeration;

namespace GalacticShrine.Outils {

	/**
   * <summary>
   *   [FR] Assistant pour obtenir le thème Windows (clair/sombre). Sur les systèmes non Windows, retourne toujours <see cref="WindowsTheme.Inconnu"/>.<br />
   *   [EN] Helper to get the Windows theme (light/dark). On non-Windows systems, always returns <see cref="WindowsTheme.Inconnu"/>.
   * </summary>
   **/
	public static class WindowsThemeAssistant {

		/**
     * <summary>
     *   [FR] Chemin du registre pour les thèmes.<br />
     *   [EN] Registry path for themes.
     * </summary>
     **/
		private const string _CheminDuRegistre = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

		/**
     * <summary>
     *   [FR] Obtient le thème de l'application.<br />
     *   [EN] Gets the application theme.
     * </summary>
     * <returns>
     *   [FR] Une valeur de <see cref="WindowsTheme"/> représentant le thème de l'application.<br />
     *   [EN] A <see cref="WindowsTheme"/> value representing the application theme.
     * </returns>
     **/
		private static WindowsTheme ObtenirThemeApp() {

			// Sur les systèmes non Windows, on ne tente pas de lire le registre.
			if(!OS.EstWin)
				return WindowsTheme.Inconnu;

			/**
       * <summary>
       *   [FR] Clé de registre pour le thème de l'application.<br />
       *   [EN] Registry value for the application theme.
       * </summary>
       **/
			const string CleDeRegistre = "AppsUseLightTheme";

			try {

				using(RegistryKey? Cle = Registry.CurrentUser.OpenSubKey(_CheminDuRegistre)) {

					if(Cle == null)
						return WindowsTheme.Inconnu;

					object? Valeur = Cle.GetValue(CleDeRegistre);

					if(Valeur is int IntValeur) {

						// 1 = clair, 0 = sombre
						return IntValeur == 0 ? WindowsTheme.Sombre : WindowsTheme.Clair;
					}

					return WindowsTheme.Inconnu;
				}
			}
			catch {
				// En cas d'erreur d'accès au registre, on renvoie un thème inconnu.
				return WindowsTheme.Inconnu;
			}
		}

		/**
     * <summary>
     *   [FR] Obtient le thème du système.<br />
     *   [EN] Gets the system theme.
     * </summary>
     * <returns>
     *   [FR] Une valeur de <see cref="WindowsTheme"/> représentant le thème du système.<br />
     *   [EN] A <see cref="WindowsTheme"/> value representing the system theme.
     * </returns>
     **/
		private static WindowsTheme ObtenirThemeSystem() {

			// Sur les systèmes non Windows, on ne tente pas de lire le registre.
			if(!OS.EstWin)
				return WindowsTheme.Inconnu;

			/**
       * <summary>
       *   [FR] Clé de registre pour le thème du système.<br />
       *   [EN] Registry value for the system theme.
       * </summary>
       **/
			const string CleDeRegistre = "SystemUsesLightTheme";

			try {

				using(RegistryKey? Cle = Registry.CurrentUser.OpenSubKey(_CheminDuRegistre)) {

					if(Cle == null)
						return WindowsTheme.Inconnu;

					object? Valeur = Cle.GetValue(CleDeRegistre);

					if(Valeur is int IntValeur) {

						// 1 = clair, 0 = sombre
						return IntValeur == 0 ? WindowsTheme.Sombre : WindowsTheme.Clair;
					}

					return WindowsTheme.Inconnu;
				}
			}
			catch {
				// En cas d'erreur d'accès au registre, on renvoie un thème inconnu.
				return WindowsTheme.Inconnu;
			}
		}

		/**
     * <summary>
     *   [FR] Obtenir le thème de l'application sous forme de texte.<br />
     *   [EN] Get the application theme as text.
     * </summary>
     * <returns>
     *   [FR] Chaîne représentant le thème de l'application : <c>Clair</c>, <c>Sombre</c> ou <c>Inconnu</c>.<br />
     *   [EN] A string representing the application theme: <c>Clair</c>, <c>Sombre</c> or <c>Inconnu</c>.
     * </returns>
     **/
		public static string ObtenirThemeAppText() => ObtenirThemeApp().ToString();

		/**
     * <summary>
     *   [FR] Obtenir le thème de l'application sous forme d'identifiant.<br />
     *   [EN] Get the application theme as an identifier.
     * </summary>
     * <returns>
     *   [FR] Entier correspondant à la valeur de <see cref="WindowsTheme"/>.<br />
     *   [EN] Integer corresponding to the <see cref="WindowsTheme"/> value.
     * </returns>
     **/
		public static int ObtenirThemeAppId() => (int)ObtenirThemeApp();

		/**
     * <summary>
     *   [FR] Obtenir le thème du système sous forme de texte.<br />
     *   [EN] Get the system theme as text.
     * </summary>
     * <returns>
     *   [FR] Chaîne représentant le thème du système : <c>Clair</c>, <c>Sombre</c> ou <c>Inconnu</c>.<br />
     *   [EN] A string representing the system theme: <c>Clair</c>, <c>Sombre</c> or <c>Inconnu</c>.
     * </returns>
     **/
		public static string ObtenirThemeSystemText() => ObtenirThemeSystem().ToString();

		/**
     * <summary>
     *   [FR] Obtenir le thème du système sous forme d'identifiant.<br />
     *   [EN] Get the system theme as an identifier.
     * </summary>
     * <returns>
     *   [FR] Entier correspondant à la valeur de <see cref="WindowsTheme"/>.<br />
     *   [EN] Integer corresponding to the <see cref="WindowsTheme"/> value.
     * </returns>
     **/
		public static int ObtenirThemeSystemId() => (int)ObtenirThemeSystem();
	}
}
