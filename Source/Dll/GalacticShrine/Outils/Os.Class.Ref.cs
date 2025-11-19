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
using System.Runtime.InteropServices;
using GalacticShrine.Enumeration;

namespace GalacticShrine.Outils {

  /**
   * <summary>
   *   [FR] Fournit des informations sur le système actuel<br>
   *   [EN] Provides information on the current system
   * </summary>
   **/
  public static class OS {

    /**
     * <summary>
     *   [FR] Retourne vrai si c'est un système Windows
     *   [EN] Returns true if the current OS is Windows
     * </summary>
     **/
    public static bool EstWin => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    /**
     * <summary>
     *   [FR] Retourne vrai si c'est un système macOS
     *   [EN] Returns true if the current OS is macOS
     * </summary>
     **/
    public static bool EstOsx => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    /**
     * <summary>
     *   [FR] Retourne vrai si c'est un système Linux
     *   [EN] Returns true if the current OS is Linux
     * </summary>
     **/
    public static bool EstGnu => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

		/**
     * <summary>
     *   [FR] Obtenir le nom du systeme d'exploitation courantes<br>
     *   [EN] Get the name of the current operating system
     * </summary>
     * <returns>
     *   [FR] Chaîne(<see cref="string"/>) du nom du système<br>
     *   [EN] System name <see cref="string"/>
     * </returns>
     **/
		public static string ObtenirNomCourantes {

			get {

				if(EstWin)
					return "Windows";

				if(EstOsx)
					return "macOS";

				if(EstGnu)
					return "Linux";

				return "Inconnu";
			}
		}

		/**
     * <summary>
     *   [FR] Obtenir l'id du systeme d'exploitation courantes<br>
     *   [EN] Get current operating system id
     * </summary>
     * <returns>
     *   [FR] une énumération de type <see cref="SystemeExploitation"/>.<br>
     *   [EN] an enumeration of type <see cref="SystemeExploitation"/>.
     * </returns>
     **/
		public static SystemeExploitation ObtenirIdCourantes {

			get {

				if(EstWin)
					return SystemeExploitation.Windows;

				if(EstOsx)
					return SystemeExploitation.Mac;

				if(EstGnu)
					return SystemeExploitation.Linux;

				return SystemeExploitation.Inconnu;
			}
		}
	}
}
