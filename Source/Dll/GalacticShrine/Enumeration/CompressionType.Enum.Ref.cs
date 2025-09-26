/**
 * Copyright © 2017-2025, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2025, Galactic-Shrine - Tous droits réservés.
 **/

namespace GalacticShrine.Enumeration {

  /**
   * <summary>
   *   [FR] Enumération pour définir les types de compression utilisés dans le système.
   *   [EN] Enumeration to define the compression types used in the system.
   * </summary>
   **/
  public enum CompressionType {

    /**
     * <summary>
     *   [FR] Compression standard à un seul thread.
     *   [EN] Standard single-threaded compression.
     * </summary>
     **/
    Standard,

    /**
     * <summary>
     *   [FR] Compression multi-thread pour des performances accrues avec de gros fichiers.
     *   [EN] Multi-threaded compression for improved performance with large files.
     * </summary>
     **/
    MultiThread
  }
}