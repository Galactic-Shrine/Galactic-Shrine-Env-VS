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

namespace GalacticShrine.Structure.Terminal {

	/**
   * <summary>
   *   [FR] Représente une paire de couleurs console (arrière-plan et premier plan).
   *   [EN] Represents a pair of console colors (background and foreground).
   * </summary>
   * <remarks>
   *   [FR] Ce type est immuable une fois créé (readonly struct).
   *   [EN] This type is immutable once created (readonly struct).
   * </remarks>
   **/
	public readonly struct Couleur {

		/**
     * <summary>
     *   [FR] Couleur d'arrière-plan de la console.
     *   [EN] Console background color.
     * </summary>
     **/
		public ConsoleColor ArrierePlan { get; init; }

		/**
     * <summary>
     *   [FR] Couleur de premier plan (texte) de la console.
     *   [EN] Console foreground (text) color.
     * </summary>
     **/
		public ConsoleColor PremierPlan { get; init; }

		/**
     * <summary>
     *   [FR] Initialise une nouvelle instance de <see cref="Couleur"/> avec les couleurs spécifiées.
     *   [EN] Initializes a new instance of <see cref="Couleur"/> with the specified colors.
     * </summary>
     * <param name="CouleurArrierePlan">
     *   [FR] Couleur d'arrière-plan à utiliser.
     *   [EN] Background color to use.
     * </param>
     * <param name="CouleurPremierPlan">
     *   [FR] Couleur de premier plan (texte) à utiliser.
     *   [EN] Foreground (text) color to use.
     * </param>
     * <example>
     *   <code>
     *   // [FR] Exemple : création d'une couleur texte blanc sur fond noir.
     *   // [EN] Example: create a white text on black background color.
     *   var couleur = new Couleur(ConsoleColor.Black, ConsoleColor.White);
     *   </code>
     * </example>
     **/
		public Couleur(ConsoleColor CouleurArrierePlan, ConsoleColor CouleurPremierPlan) {

			ArrierePlan = CouleurArrierePlan;
			PremierPlan = CouleurPremierPlan;
		}
	}
}
