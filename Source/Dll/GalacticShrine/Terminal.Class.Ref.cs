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

namespace GalacticShrine.Terminal {

	/**
   * <summary>
   *   [FR] Fournit des informations utilitaires liées au terminal.
   *   [EN] Provides terminal-related utility information.
   * </summary>
   **/
	public static class Terminal {

		/**
     * <summary>
     *   [FR] Identifiant du contrôleur au format <c>Utilisateur@Domaine</c> lorsque possible,
     *        ou uniquement le nom d'utilisateur si le domaine n'est pas disponible.
     *   [EN] Controller identifier in the form <c>User@Domain</c> when possible,
     *        or only the user name if the domain is not available.
     * </summary>
     **/
		public static string Controleur { get; } = ConstruireControleur();

		/**
     * <summary>
     *   [FR] Construit l'identifiant du contrôleur en tenant compte de la compatibilité multi-plateforme.
     *   [EN] Builds the controller identifier taking cross-platform compatibility into account.
     * </summary>
     * <returns>
     *   [FR] Une chaîne représentant l'identifiant du contrôleur.
     *   [EN] A string representing the controller identifier.
     * </returns>
     **/
		private static string ConstruireControleur() {

			try {

				var Utilisateur = Environment.UserName;
				var Domaine = Environment.UserDomainName;

				if(string.IsNullOrWhiteSpace(Domaine)) {

					return Utilisateur;
				}

				return $"{Utilisateur}@{Domaine}";
			}
			catch(PlatformNotSupportedException) {

				// [FR] En environnement non Windows, on se replie sur le nom d'utilisateur uniquement.
				// [EN] On non-Windows platforms, fall back to user name only.
				return Environment.UserName;
			}
		}
	}
}
