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
   *   [FR] Énumération définissant les niveaux possibles de journalisation (logs).
   *   [EN] Enumeration defining possible logging levels.
   * </summary>
   **/
  public enum Journalisations {

    /**
     * <summary>
     *   [FR] Niveau non spécifié ou autre.
     *   [EN] Unspecified or other level.
     * </summary>
     **/
    Autre,

    /**
     * <summary>
     *   [FR] Avertissement : problème potentiel, n'interrompt pas le fonctionnement.
     *   [EN] Warning: potential issue, doesn't stop execution.
     * </summary>
     **/
    Avertissement,

    /**
     * <summary>
     *   [FR] Erreur : peut nécessiter une intervention ou stopper l'opération en cours.
     *   [EN] Error: may require intervention or stop current operation.
     * </summary>
     **/
    Erreur,

    /**
     * <summary>
     *   [FR] Information générale.
     *   [EN] General informational level.
     * </summary>
     **/
    Info,

    /**
     * <summary>
     *   [FR] Succès : opération réussie.
     *   [EN] Success: operation completed successfully.
     * </summary>
     **/
    Succes,

    /**
     * <summary>
     *   [FR] Niveau personnalisé défini par l'application/utilisateur.
     *   [EN] Custom level defined by the application/user.
     * </summary>
     **/
    Personnalise
  }
}
