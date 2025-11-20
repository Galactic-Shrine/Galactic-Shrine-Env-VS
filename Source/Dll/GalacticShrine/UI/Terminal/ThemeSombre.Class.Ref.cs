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
using GalacticShrine.Structure.Terminal;

namespace GalacticShrine.UI.Terminal {
  /**
   * <summary>
   *   [FR] Implémentation d'un thème sombre (couleurs sombres en arrière-plan).
   *   [EN] Implementation of a dark theme (dark background colors).
   * </summary>
   **/
  public class ThemeSombre : ThemeParams {

    /**
     * <summary>
     *   [FR] Constructeur du thème sombre. Initialise et configure les couleurs et composants.
     *   [EN] Dark theme constructor. Initializes and configures colors and components.
     * </summary>
     * <param name="bConsoleParDefault">
     *   [FR] Si <c>true</c>, utilise les couleurs par défaut de la console. Sinon, couleurs sombres personnalisées.
     *   [EN] If <c>true</c>, uses the console's default colors. Otherwise, uses custom dark colors.
     * </param>
     **/
    public ThemeSombre(bool bConsoleParDefault = true) {

      ConsoleParDefault = bConsoleParDefault;
      DefinirLesCouleurs();
      DefinirLesComposants();
    }

    /**
     * <summary>
     *   [FR] Définit les couleurs principales du thème sombre, soit en console par défaut, soit en noir/blanc inversé.
     *   [EN] Defines main colors for the dark theme, either console default or black/white inverted.
     * </summary>
     **/
    public override void DefinirLesCouleurs() {

      if(ConsoleParDefault) {

        RetablirCouleur();
      }
      else {

        ConsoleArrierePlan = ConsoleColor.Black;
        ConsolePremierPlan = ConsoleColor.White;
      }
    }

    /**
     * <summary>
     * [FR] Configure les différents composants (textes, fonds) avec des couleurs sombres.
     * [EN] Configures the various components (text, background) with dark-oriented colors.
     * </summary>
     **/
    public override void DefinirLesComposants() {

			var Couleur = new Dictionary<string, Couleur> {

        { "text.default",       AjouterCouleur(null, null) },
        { "text.black",         AjouterCouleur(null, ConsoleColor.Black) },
        { "text.darkBlue",      AjouterCouleur(null, ConsoleColor.DarkBlue) },
        { "text.darkGreen",     AjouterCouleur(null, ConsoleColor.DarkGreen) },
        { "text.darkCyan",      AjouterCouleur(null, ConsoleColor.DarkCyan) },
        { "text.darkRed",       AjouterCouleur(null, ConsoleColor.DarkRed) },
        { "text.darkMagenta",   AjouterCouleur(null, ConsoleColor.DarkMagenta) },
        { "text.darkYellow",    AjouterCouleur(null, ConsoleColor.DarkYellow) },
        { "text.darkGray",      AjouterCouleur(null, ConsoleColor.DarkGray) },
        { "text.blue",          AjouterCouleur(null, ConsoleColor.Blue) },
        { "text.green",         AjouterCouleur(null, ConsoleColor.Green) },
        { "text.cyan",          AjouterCouleur(null, ConsoleColor.Cyan) },
        { "text.red",           AjouterCouleur(null, ConsoleColor.Red) },
        { "text.magenta",       AjouterCouleur(null, ConsoleColor.Magenta) },
        { "text.yellow",        AjouterCouleur(null, ConsoleColor.Yellow) },
        { "text.gray",          AjouterCouleur(null, ConsoleColor.Gray) },
        { "text.sourdine",			AjouterCouleur(null, ConsoleColor.DarkGray) },
				{ "text.primaire",			AjouterCouleur(null, ConsoleColor.Gray) },
				{ "text.avertissement", AjouterCouleur(null, ConsoleColor.Yellow) },
				{ "text.danger",				AjouterCouleur(null, ConsoleColor.Red) },
				{ "text.succes",				AjouterCouleur(null, ConsoleColor.DarkGreen) },
				{ "text.info",					AjouterCouleur(null, ConsoleColor.DarkCyan) },
				{ "bg.default",					AjouterCouleur(null,                     null) },
        { "bg.black",           AjouterCouleur(ConsoleColor.Black,       null) },
        { "bg.darkBlue",        AjouterCouleur(ConsoleColor.DarkBlue,    null) },
        { "bg.darkGreen",       AjouterCouleur(ConsoleColor.DarkGreen,   null) },
        { "bg.darkCyan",        AjouterCouleur(ConsoleColor.DarkCyan,    null) },
        { "bg.darkRed",         AjouterCouleur(ConsoleColor.DarkRed,     null) },
        { "bg.darkMagenta",     AjouterCouleur(ConsoleColor.DarkMagenta, null) },
        { "bg.darkYellow",      AjouterCouleur(ConsoleColor.DarkYellow,  null) },
        { "bg.darkGray",        AjouterCouleur(ConsoleColor.DarkGray,    null) },
        { "bg.blue",            AjouterCouleur(ConsoleColor.Blue,        null) },
        { "bg.green",           AjouterCouleur(ConsoleColor.Green,       null) },
        { "bg.cyan",            AjouterCouleur(ConsoleColor.Cyan,        null) },
        { "bg.red",             AjouterCouleur(ConsoleColor.Red,         null) },
        { "bg.magenta",         AjouterCouleur(ConsoleColor.Magenta,     null) },
        { "bg.yellow",          AjouterCouleur(ConsoleColor.Yellow,      null) },
        { "bg.gray",            AjouterCouleur(ConsoleColor.Gray,        null) },
        { "bg.sourdine",				AjouterCouleur(ConsoleColor.DarkGray,    ConsoleColor.Black) },
				{ "bg.primaire",				AjouterCouleur(ConsoleColor.Gray,        ConsoleColor.White) },
				{ "bg.avertissement",		AjouterCouleur(ConsoleColor.Yellow,      ConsoleColor.Black) },
				{ "bg.danger",					AjouterCouleur(ConsoleColor.Red,         ConsoleColor.White) },
				{ "bg.succes",					AjouterCouleur(ConsoleColor.DarkGreen,   ConsoleColor.White) },
				{ "bg.info",						AjouterCouleur(ConsoleColor.DarkCyan,    ConsoleColor.White) }
			};
      Couleurs = Couleur;
		}
	}
}
