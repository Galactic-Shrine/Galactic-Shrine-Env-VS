/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticShrine.Outils.Enumeration.Journalisation {
  [Serializable]
  /**
   * <summary>
   *   [FR] Type d'événement du journal
   *   [EN] Log event type
   * </summary>
   **/
  public enum TypeDevenement {

    /**
     * <summary>
     *   [FR] L'événement du journal est une erreur fatale
     *   [EN] The log event is a fatal error
     * </summary>
     **/
    Fatale = 0b1,

    /**
     * <summary>
     *   [FR] L'événement du protocole est une erreur
     *   [EN] The log event is an error
     * </summary>
     **/
    Erreur = 0b10,

    /**
     * <summary>
     *   [FR] L'événement du journal est un avertissement
     *   [EN] The log event is a warning
     * </summary>
     **/
    Avertissement = 0b100,

    /**
     * <summary>
     *   [FR] Sortie de l'événement log sur la console
     *   [EN] Output the log event to the console
     * </summary>
     **/
    Console = 0b1000,

    /**
     * <summary>
     *   [FR] Sortie de l'événement dans le journal sur disque dur
     *   [EN] Output the event to the on-disk log
     * </summary>
     **/
    JournalDeBord = 0b10000,

    /**
     * <summary>
     *   [FR] L'événement de journal ne doit être affiché que si la journalisation verbale est activée.
     *   [EN] The log event should only be displayd if verbose logging is enabled
     * </summary>
     **/
    Verbeux = 0b100000,

    /**
     * <summary>
     *   [FR] L'événement log ne doit être affiché que si l'enregistrement très verbeux est activé.
     *   [EN] The log event should only be displayd if very verbose logging is enabled
     * </summary>
     **/
    TresVerbeux = 0b1000000
  }
}
