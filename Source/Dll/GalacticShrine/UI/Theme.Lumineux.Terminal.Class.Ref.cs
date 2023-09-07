/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using GalacticShrine.Structure.Terminal;

namespace GalacticShrine.UI.Terminal {

	public class ThemeLumineux : ThemeParams {

    public ThemeLumineux(bool bConsoleDefault = true) {

      ConsoleParDefault = bConsoleDefault;
      DefinirLesCouleurs();
      DefinirLesComposants();
    }

    public override void DefinirLesCouleurs () {

      if(ConsoleParDefault) {

        RetablirCouleur();
      }
      else {

        ConsoleArrierePlan = ConsoleColor.White;
        ConsolePremierPlan = ConsoleColor.Black;
      }
    }

		public override void DefinirLesComposants () {

      var Couleur = new Dictionary<string, Couleur> {

        { "text.default",				AjouterCouleur(null, null) },
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
        { "text.sourdine",			AjouterCouleur(null, ConsoleColor.Gray) },
				{ "text.primaire",			AjouterCouleur(null, ConsoleColor.DarkGray) },
				{ "text.avertissement", AjouterCouleur(null, ConsoleColor.DarkYellow) },
				{ "text.danger",				AjouterCouleur(null, ConsoleColor.DarkRed) },
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
        { "bg.sourdine",				AjouterCouleur(ConsoleColor.Gray,        ConsoleColor.Black) },
				{ "bg.primaire",				AjouterCouleur(ConsoleColor.DarkGray,    ConsoleColor.White) },
				{ "bg.avertissement",		AjouterCouleur(ConsoleColor.DarkYellow,  ConsoleColor.White) },
				{ "bg.danger",					AjouterCouleur(ConsoleColor.DarkRed,     ConsoleColor.White) },
				{ "bg.succes",					AjouterCouleur(ConsoleColor.DarkGreen,   ConsoleColor.White) },
				{ "bg.info",						AjouterCouleur(ConsoleColor.DarkCyan,    ConsoleColor.White) }
			};
      Couleurs = Couleur;
		}
	}
}
