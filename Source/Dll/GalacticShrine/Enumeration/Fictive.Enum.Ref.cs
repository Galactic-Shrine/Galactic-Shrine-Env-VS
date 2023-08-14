/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

namespace GalacticShrine.Enumeration {

  /**
   * <summary>
   *   [FR] Énumération fictive pour permettre d'invoquer le constructeur qui prend un chemin complet aseptisé
   *   [EN] Dummy enum to allow invoking the constructor witch takes a sanitized full path
   * </summary>
   **/
  public enum Aseptise {

    /**
     * <summary>
     *   [FR] Aucun
     *   [EN] None
     * </summary>
     **/
    Aucun = 0x0
  }

  /**
   * <summary>
   *   [FR] Valeur spéciale utilisée pour invoquer la surcharge du constructeur non désinfectant.
   *   [EN] Special value  used to invoke the non-sanitizing constructor overload.
   * </summary>
   **/
  public enum Desinfecter {

    /**
     * <summary>
     *   [FR] Aucun
     *   [EN] None
     * </summary>
     **/
    Aucun = 0x0
  }
}
