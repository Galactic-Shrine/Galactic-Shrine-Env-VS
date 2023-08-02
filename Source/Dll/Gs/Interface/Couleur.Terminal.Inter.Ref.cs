/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using Gs.Structure.Terminal;

namespace Gs.Interface.Terminal {

	public interface CouleurInterface {

    Dictionary<string, Couleur> Couleurs { get; set; }

    Couleur AjouterCouleur(ConsoleColor? ArrierePlan, ConsoleColor? PremierPlan);

    void DefinirLesCouleurs();

    void DefinirLesComposants();
  }
}
