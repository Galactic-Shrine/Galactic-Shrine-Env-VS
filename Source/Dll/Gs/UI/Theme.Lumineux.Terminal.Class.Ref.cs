/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using Gs.Properties;
using System;
using System.Collections.Generic;
using Gs.Structure.Terminal;

namespace Gs.UI.Terminal {

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
				{ "text.magenta",				AjouterCouleur(null,ConsoleColor.DarkMagenta) },
				{ "text.sourdine",			AjouterCouleur(null, ConsoleColor.Gray) },
				{ "text.primaire",			AjouterCouleur(null, ConsoleColor.DarkGray) },
				{ "text.avertissement", AjouterCouleur(null, ConsoleColor.DarkYellow) },
				{ "text.danger",				AjouterCouleur(null, ConsoleColor.DarkRed) },
				{ "text.succes",				AjouterCouleur(null, ConsoleColor.DarkGreen) },
				{ "text.info",					AjouterCouleur(null, ConsoleColor.DarkCyan) },
				{ "bg.default",					AjouterCouleur(null, null) },
				{ "bg.magenta",					AjouterCouleur(ConsoleColor.DarkMagenta, ConsoleColor.White) },
				{ "bg.sourdine",				AjouterCouleur(ConsoleColor.Gray, ConsoleColor.Black) },
				{ "bg.primaire",				AjouterCouleur(ConsoleColor.DarkGray, ConsoleColor.White) },
				{ "bg.avertissement",		AjouterCouleur(ConsoleColor.DarkYellow, ConsoleColor.White) },
				{ "bg.danger",					AjouterCouleur(ConsoleColor.DarkRed, ConsoleColor.White) },
				{ "bg.succes",					AjouterCouleur(ConsoleColor.DarkGreen, ConsoleColor.White) },
				{ "bg.info",						AjouterCouleur(ConsoleColor.DarkCyan, ConsoleColor.White) }
			};
      Couleurs = Couleur;
		}
	}
}
