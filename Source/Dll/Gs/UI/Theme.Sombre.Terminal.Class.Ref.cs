/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using Gs.Structure.Terminal;

namespace Gs.UI.Terminal {

	public class ThemeSombre : ThemeParams {

    public ThemeSombre(bool bConsoleParDefault = true) {

      ConsoleParDefault = bConsoleParDefault;
      DefinirLesCouleurs();
      DefinirLesComposants();
    }

    public override void DefinirLesCouleurs () {

      if (ConsoleParDefault) {

        RetablirCouleur();
      }
      else {

        ConsoleArrierePlan = ConsoleColor.Black;
        ConsolePremierPlan = ConsoleColor.White;
      }
    }

		public override void DefinirLesComposants() {

			var Couleur = new Dictionary<string, Couleur> {

				{ "text.default",				AjouterCouleur(null, null) },
				{ "text.magenta",				AjouterCouleur(null,ConsoleColor.Magenta) },
				{ "text.sourdine",			AjouterCouleur(null, ConsoleColor.DarkGray) },
				{ "text.primaire",			AjouterCouleur(null, ConsoleColor.Gray) },
				{ "text.avertissement", AjouterCouleur(null, ConsoleColor.Yellow) },
				{ "text.danger",				AjouterCouleur(null, ConsoleColor.Red) },
				{ "text.succes",				AjouterCouleur(null, ConsoleColor.DarkGreen) },
				{ "text.info",					AjouterCouleur(null, ConsoleColor.DarkCyan) },
				{ "bg.default",					AjouterCouleur(null, null) },
				{ "bg.magenta",					AjouterCouleur(ConsoleColor.DarkMagenta,ConsoleColor.White) },
				{ "bg.sourdine",				AjouterCouleur(ConsoleColor.DarkGray, ConsoleColor.Black) },
				{ "bg.primaire",				AjouterCouleur(ConsoleColor.Gray, ConsoleColor.White) },
				{ "bg.avertissement",		AjouterCouleur(ConsoleColor.Yellow, ConsoleColor.Black) },
				{ "bg.danger",					AjouterCouleur(ConsoleColor.Red, ConsoleColor.White) },
				{ "bg.succes",					AjouterCouleur(ConsoleColor.DarkGreen, ConsoleColor.White) },
				{ "bg.info",						AjouterCouleur(ConsoleColor.DarkCyan, ConsoleColor.White) }
			};
      Couleurs = Couleur;
		}
	}
}
