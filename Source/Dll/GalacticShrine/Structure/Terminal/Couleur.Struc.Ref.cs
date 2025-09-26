/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;

namespace GalacticShrine.Structure.Terminal {

  public struct Couleur {

    public ConsoleColor ArrierePlan { get; private set; }

    public ConsoleColor PremierPlan { get; private set; }

    public Couleur(ConsoleColor ArrierePlanTemp, ConsoleColor PremierPlanTemp) : this() {

      ArrierePlan = ArrierePlanTemp;
      PremierPlan = PremierPlanTemp;
    }
  }
}
