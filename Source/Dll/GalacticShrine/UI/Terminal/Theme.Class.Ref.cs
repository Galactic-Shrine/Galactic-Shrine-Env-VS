/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using GalacticShrine.Interface.Terminal;

namespace GalacticShrine.UI.Terminal {

  /**
   * <summary>
   *   [FR] Classe statique Theme proposant des propriétés pour obtenir un thème sombre ou lumineux.
   * [  EN] Static Theme class providing properties to get a dark or light theme.
   * </summary>
   **/
  public static partial class Theme {

    /**
     * <summary>
     *   [FR] Obtient un thème sombre (ThemeSombre).
     *   [EN] Gets a dark theme (ThemeSombre).
     * </summary>
     * <returns>
     *   [FR] Retourne un objet implémentant <see cref="CouleurInterface"/> configuré en mode sombre.
     *   [EN] Returns a <see cref="CouleurInterface"/> object configured for dark mode.
     * </returns>
     **/
    public static CouleurInterface Sombre {

      get {

        return new ThemeSombre(false);
      }
    }

    /**
     * <summary>
     *   [FR] Obtient un thème lumineux (ThemeLumineux).
     *   [EN] Gets a light theme (ThemeLumineux).
     * </summary>
     * <returns>
     *   [FR] Retourne un objet implémentant <see cref="CouleurInterface"/> configuré en mode clair.
     *   [EN] Returns a <see cref="CouleurInterface"/> object configured for light mode.
     * </returns>
     **/
    public static CouleurInterface Lumineux {

      get {

        return new ThemeLumineux(false);
      }
    }
  }
}
