/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using Gs.Interface.Terminal;
using Gs.Structure.Terminal;

namespace Gs.UI.Terminal {	

	public abstract class ThemeParams : CouleurInterface {

    protected bool ConsoleParDefault { get; set; }

    protected ConsoleColor ConsoleArrierePlan { get; set; }

    protected ConsoleColor ConsolePremierPlan { get; set; }

    public Dictionary<string, Couleur> ?Couleurs { get; set; }

    public Couleur AjouterCouleur(ConsoleColor? ArrierePlan, ConsoleColor? PremierPlan) {

      var Couleur = new Couleur(
        ArrierePlan ?? ConsoleArrierePlan,
        PremierPlan ?? ConsolePremierPlan
        );
      return Couleur;
    }

    public void RetablirCouleur() {

      Console.ResetColor();
      ConsoleArrierePlan = Console.BackgroundColor;
      ConsolePremierPlan = Console.ForegroundColor;
    }

    public abstract void DefinirLesCouleurs();

    public abstract void DefinirLesComposants();
  }
}
