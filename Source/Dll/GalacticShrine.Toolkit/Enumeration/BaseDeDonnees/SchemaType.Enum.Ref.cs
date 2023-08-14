/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

namespace GalacticShrine.Outils.Enumeration.BaseDeDonnees {

  /**
   * <summary>
   *   Choix possible de retour pour un GetSchema
   * </summary>
   **/
  public enum SchemaType {

    /**
     * <summary>
     *   Retourne les types possible de la base de données
     * </summary>
     **/
    DataType = 0b1110000100,

    /**
     * <summary>
     *   Retourne les nom et types des Colonnes d'une table
     * </summary>
     **/
    Columns = 0b1110000101,

    /**
     * <summary>
     *   Retourne les indexs d'une table
     * </summary>
     **/
    Indexes = 0b1110000110,

    /**
     * <summary>
     *   Retourne les tables existantes dans la base
     * </summary>
     **/
    Tables = 0b1110000111
  }
}
