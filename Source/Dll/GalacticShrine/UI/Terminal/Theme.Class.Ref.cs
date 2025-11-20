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
