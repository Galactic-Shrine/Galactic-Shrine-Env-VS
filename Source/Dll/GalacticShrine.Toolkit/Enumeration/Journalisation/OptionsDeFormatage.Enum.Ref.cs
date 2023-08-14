/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticShrine.Outils.Enumeration.Journalisation {

  /**
   * <summary>
   *   [FR] Options de formatage des messages
   *   [EN] Options for formatting messages
   * </summary>
   **/
  [Flags]
  public enum OptionsDeFormatage {

    /**
     * <summary>
     *   [FR] Format normalement
     *   [EN] Format normally
     * </summary>
     **/
    Aucun = 0b0,

    /**
     * <summary>
     *   [FR] N'écrivez jamais un préfixe de gravité. 
     *        Utile pour les messages préformatés qui doivent être dans un format particulier pour les fichiers, 
     *        par exemple, la fenêtre de sortie de Visual Studio 
     *   [EN] Never write a severity prefix.
     *        Useful for pre-formatted messages that need to be in a particular format for,
     *        eg. the Visual Studio output window
     * </summary>
     **/
    PasDePrefixeDeGravite = 0b1,

    /**
     * <summary>
     *   [FR] Ne pas envoyer de texte à la console
     *   [EN] Do not output text to the console
     * </summary>
     **/
    PasDeSortieConsole = 0b10
  }
}
