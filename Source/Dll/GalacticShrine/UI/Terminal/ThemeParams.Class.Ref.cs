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
using System.Collections.Generic;

using GalacticShrine.Interface.Terminal;
using GalacticShrine.Structure.Terminal;

namespace GalacticShrine.UI.Terminal {

	/**
   * <summary>
   *   [FR] Classe abstraite définissant les paramètres et méthodes de base pour un thème (couleurs).
   *   [EN] Abstract class defining base parameters and methods for a theme (colors).
   * </summary>
   **/
	public abstract class ThemeParams : CouleurInterface {

		/**
     * <summary>
     *   [FR] Indique si la console doit reprendre ses couleurs par défaut.
     *   [EN] Indicates whether the console should use its default colors.
     * </summary>
     **/
		protected bool ConsoleParDefault { get; set; }

		/**
     * <summary>
     *   [FR] Couleur d'arrière-plan de la console.
     *   [EN] Console background color.
     * </summary>
     **/
		protected ConsoleColor ConsoleArrierePlan { get; set; }

		/**
     * <summary>
     *   [FR] Couleur de premier plan de la console.
     *   [EN] Console foreground color.
     * </summary>
     **/
		protected ConsoleColor ConsolePremierPlan { get; set; }

		/**
     * <summary>
     *   [FR] Dictionnaire reliant une clé à un objet <see cref="Couleur"/> configuré.
     *   [EN] Dictionary mapping a key to a configured <see cref="Couleur"/> object.
     * </summary>
     * <remarks>
     *   [FR] Cette propriété est initialisée par défaut à un dictionnaire vide et n'est jamais <c>null</c>.
     *   [EN] This property is initialized by default to an empty dictionary and is never <c>null</c>.
     * </remarks>
     **/
		public Dictionary<string, Couleur> Couleurs { get; set; } = new();

		/**
     * <summary>
     *   [FR] Crée et retourne un objet <see cref="Couleur"/> en tenant compte d'éventuelles couleurs de fallback.
     *   [EN] Creates and returns a <see cref="Couleur"/> object, using fallback colors if not specified.
     * </summary>
     * <param name="ArrierePlan">
     *   [FR] Couleur de fond (ou <c>null</c> pour prendre <see cref="ConsoleArrierePlan"/>).
     *   [EN] Background color (or <c>null</c> to use <see cref="ConsoleArrierePlan"/>).
     * </param>
     * <param name="PremierPlan">
     *   [FR] Couleur de premier plan (ou <c>null</c> pour prendre <see cref="ConsolePremierPlan"/>).
     *   [EN] Foreground color (or <c>null</c> to use <see cref="ConsolePremierPlan"/>).
     * </param>
     * <returns>
     *   [FR] Un nouvel objet <see cref="Couleur"/>.
     *   [EN] A new <see cref="Couleur"/> object.
     * </returns>
     **/
		public Couleur AjouterCouleur(ConsoleColor? ArrierePlan, ConsoleColor? PremierPlan) {

			var Couleur = new Couleur(

				ArrierePlan ?? ConsoleArrierePlan,
				PremierPlan ?? ConsolePremierPlan
			);
			return Couleur;
		}

		/**
     * <summary>
     *   [FR] Rétablit la couleur par défaut de la console et met à jour <see cref="ConsoleArrierePlan"/> / <see cref="ConsolePremierPlan"/>.
     *   [EN] Resets the console to its default color and updates <see cref="ConsoleArrierePlan"/> / <see cref="ConsolePremierPlan"/>.
     * </summary>
     **/
		public void RetablirCouleur() {

			Console.ResetColor();
			ConsoleArrierePlan = Console.BackgroundColor;
			ConsolePremierPlan = Console.ForegroundColor;
		}

		/**
     * <summary>
     *   [FR] Méthode abstraite pour définir les couleurs utilisées par ce thème.
     *   [EN] Abstract method to define the colors used by this theme.
     * </summary>
     **/
		public abstract void DefinirLesCouleurs();

		/**
     * <summary>
     *   [FR] Méthode abstraite pour définir ou configurer les composants dépendant des couleurs.
     *   [EN] Abstract method to define/configure color-dependent components.
     * </summary>
     **/
		public abstract void DefinirLesComposants();
	}
}
