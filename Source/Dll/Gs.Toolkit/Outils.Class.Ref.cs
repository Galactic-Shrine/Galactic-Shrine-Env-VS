/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;

namespace Gs.Outils {

  /**
   * <summary>
   *   [FR] Classe de base pour les outil.
   *   [EN] Basic class for tools.
   * </summary>
   **/
  [Serializable]
  public abstract class Outils {

    /**
     * <summary>
     *   [FR] Le comparateur à utiliser pour les références de systèmes de fichiers
     *   [EN] The comparator to use for file system references
     * </summary>
     **/
    public static readonly StringComparer Comparateur = StringComparer.OrdinalIgnoreCase;

    /**
     * <summary>
     *   [FR] La comparaison à utiliser pour les références de systèmes de fichiers
     *   [EN] The comparison to be used for file system references
     * </summary>
     **/
    public static readonly StringComparison Comparaison = StringComparison.OrdinalIgnoreCase;
  }
}
