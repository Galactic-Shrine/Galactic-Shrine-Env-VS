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
     *   [FR] Symbole de l'invite de commande associé au contrôleur (ex. « # »).
     *   [EN] Prompt symbol associated with the controller (e.g. "#").
     * </summary>
     **/
		public const string InviteControleur = "# ";

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

		/**
     * <summary>
     *   [FR] Affiche l'identifiant du contrôleur dans le terminal en colorant :
     *        - la partie utilisateur en rouge ;
     *        - la partie domaine en magenta (proche d'un violet / purple) ;
     *        - la partie invite de contrôleur en sourdine (gris).
     *   [EN] Displays the controller identifier in the terminal coloring:
     *        - the user part in red;
     *        - the domain part in magenta (close to purple);
     *        - the controller prompt part in muted gray.
     * </summary>
     * <param name="FormatTerminal">
     *   [FR] Instance de <see cref="Format"/> utilisée pour écrire avec les couleurs du thème.
     *   [EN] <see cref="Format"/> instance used to write using the theme colors.
     * </param>
     * <param name="CouleurUtilisateur">
     *   [FR] Clé de couleur pour la partie utilisateur (par défaut, rouge : <c>Couleurs.Txt.Red</c>).
     *   [EN] Color key for the user part (defaults to red: <c>Couleurs.Txt.Red</c>).
     * </param>
     * <param name="CouleurDomaine">
     *   [FR] Clé de couleur pour la partie domaine (par défaut, magenta : <c>Couleurs.Txt.Magenta</c>).
     *   [EN] Color key for the domain part (defaults to magenta: <c>Couleurs.Txt.Magenta</c>).
     * </param>
     * <param name="CouleurInviteControleur">
     *   [FR] Clé de couleur pour la partie invite contrôleur (par défaut, sourdine : <c>Couleurs.Txt.Sourdine</c>).
     *   [EN] Color key for the controller prompt part (defaults to muted: <c>Couleurs.Txt.Sourdine</c>).
     * </param>
     * <remarks>
     *   [FR] Si l'identifiant ne contient pas de caractère '@', toute la partie contrôleur est colorée avec
     *        <paramref name="CouleurUtilisateur"/>, puis l'invite est affichée avec <paramref name="CouleurInviteControleur"/>.
     *   [EN] If the identifier does not contain '@', the whole controller part is colored using
     *        <paramref name="CouleurUtilisateur"/>, then the prompt is displayed with <paramref name="CouleurInviteControleur"/>.
     * </remarks>
     **/
		public static void AfficherControleur(
			Format FormatTerminal,
			string CouleurUtilisateur = Couleurs.Txt.Red,
			string CouleurDomaine = Couleurs.Txt.Magenta,
			string CouleurInviteControleur = Couleurs.Txt.Sourdine) {

			ArgumentNullException.ThrowIfNull(FormatTerminal);

			var Valeur = Controleur;
			var IndexArobase = Valeur.IndexOf('@');

			// Aucun domaine -> tout en "utilisateur".
			if(IndexArobase < 0) {

				FormatTerminal.Ecrire(Valeur, CouleurUtilisateur);
				FormatTerminal.Ecrire(InviteControleur, CouleurInviteControleur);
				return;
			}

			var Utilisateur = Valeur.Substring(0, IndexArobase);
			var Domaine = Valeur.Substring(IndexArobase + 1);

			// Utilisateur en rouge
			FormatTerminal.Ecrire(Utilisateur, CouleurUtilisateur);

			// '@' neutre
			FormatTerminal.Ecrire("@", Couleurs.Txt.Default);

			// Domaine en "purple" -> magenta
			FormatTerminal.Ecrire(Domaine, CouleurDomaine);

			// Invite de contrôleur en sourdine
			FormatTerminal.Ecrire(InviteControleur, CouleurInviteControleur);
		}
	}
}
