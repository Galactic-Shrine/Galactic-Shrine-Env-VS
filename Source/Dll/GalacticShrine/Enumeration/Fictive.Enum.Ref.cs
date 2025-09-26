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
