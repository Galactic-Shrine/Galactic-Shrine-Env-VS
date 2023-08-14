/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using GalacticShrine.Interface.Terminal;

namespace GalacticShrine.UI.Terminal {

  public static partial class Theme {

    public static CouleurInterface Sombre {

      get {

        return new ThemeSombre(false);
      }
    }

    public static CouleurInterface Lumineux {
      
      get {

        return new ThemeLumineux(false);
      }
    }
  }
}

