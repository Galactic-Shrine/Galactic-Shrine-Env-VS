/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System.Runtime.InteropServices;
using GalacticShrine.Enumeration.Outils;

namespace GalacticShrine.Outils {

  /**
   * <summary>
   *   [FR] Fournit des informations sur le système actuel<br>
   *   [EN] Provides information on the current system
   * </summary>
   **/
  public static class OS {

    public static bool EstWin => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static bool EstOsx => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

    public static bool EstGnu => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    /**
     * <summary>
     *   [FR] Obtenir le nom du systeme d'exploitation courantes<br>
     *   [EN] Get the name of the current operating system
     * </summary>
     * <returns>
     *   [FR] Chaîne(<see cref="string"/>) du nom du système<br>
     *   [EN] System name <see cref="string"/>
     * </returns>
     **/
    public static string ObtenirNomCourantes {

      get {

        string? obj;

        if(EstWin)
          obj = "Windows";
        else 
          obj = null;

        if(obj == null) {

          if(EstOsx)
            obj = "OSX";
          else 
            obj = null;

          if(obj == null) {

            if(!EstGnu) 
              return null;
            
            obj = "Linux";
          }
        }

        return obj;
      }
    }

    /**
     * <summary>
     *   [FR] Obtenir l'id du systeme d'exploitation courantes<br>
     *   [EN] Get current operating system id
     * </summary>
     * <returns>
     *   [FR] une énumération de type <see cref="SystemeExploitation"/>.<br>
     *   [EN] an enumeration of type <see cref="SystemeExploitation"/>.
     * </returns>
     **/
    public static SystemeExploitation ObtenirIdCourantes {

      get {

        if(!EstWin) {

          if(EstGnu) {

            return SystemeExploitation.Linux;
          }

          if(EstOsx) {
            
            return SystemeExploitation.Mac;
          }
        }

        return SystemeExploitation.Windows;
      }
    }
  }
}
