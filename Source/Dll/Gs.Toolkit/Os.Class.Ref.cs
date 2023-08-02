/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System.Runtime.InteropServices;

namespace Gs.Outils.Platform {

  /**
   * <summary>
   *   [FR] Fournit des informations sur le système actuel
   *   [EN] Provides information on the current system
   * </summary>
   **/
  public static class OS {

    public static bool EstWin() {

      return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    public static bool EstOsx() {

      return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }

    public static bool EstGnu() {

      return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }

    /**
   * <summary>
   *   [FR] Obtenir les informations courantes
   *   [EN] Get current information
   * </summary>
   **/
    public static string ObtenirLesInfoCourantes() {

      object obj = (EstWin() ? "Windows" : null);
      if(obj == null) {

        obj = (EstOsx() ? "OSX" : null);
        if(obj == null) {

          if(!EstGnu()) {

            return null;
          }

          obj = "Linux";
        }
      }

      return (string)obj;
    }
  }
}
