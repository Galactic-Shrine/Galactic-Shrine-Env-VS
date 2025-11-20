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
using System;
using System.Collections.Generic;
using GalacticShrine.Structure.Terminal;

namespace GalacticShrine.Interface.Terminal {
  /**
   * <summary>
   * [FR] Interface définissant la gestion et l'ajout de couleurs dans un contexte de terminal.
   * [EN] Interface defining color management and addition in a terminal context.
   * </summary>
   **/
  public interface CouleurInterface {
    /**
     * <summary>
     * [FR] Dictionnaire stockant des couleurs associées à une clé (chaîne de caractères).
     * [EN] Dictionary storing colors mapped to a string key.
     * </summary>
     **/
    Dictionary<string, Couleur> Couleurs { get; set; }

    /**
     * <summary>
     * [FR] Ajoute et retourne un objet Couleur, en tenant compte du premier plan / arrière-plan.
     * [EN] Adds and returns a Couleur object, considering foreground/background colors.
     * </summary>
     * <param name="ArrierePlan">
     * [FR] Couleur de fond (ou <c>null</c> si aucune).
     * [EN] Background color (or <c>null</c> if none).
     * </param>
     * <param name="PremierPlan">
     * [FR] Couleur de premier plan (ou <c>null</c> si aucune).
     * [EN] Foreground color (or <c>null</c> if none).
     * </param>
     * <returns>
     * [FR] Retourne l'objet Couleur créé ou récupéré.
     * [EN] Returns the created or retrieved Couleur object.
     * </returns>
     **/
    Couleur AjouterCouleur(ConsoleColor? ArrierePlan, ConsoleColor? PremierPlan);

    /**
     * <summary>
     * [FR] Définit toutes les couleurs à utiliser dans le terminal.
     * [EN] Sets all the colors to be used in the terminal.
     * </summary>
     **/
    void DefinirLesCouleurs();

    /**
     * <summary>
     * [FR] Configure les composants qui dépendent des couleurs (implémentation spécifique).
     * [EN] Configures the components that depend on colors (specific implementation).
     * </summary>
     **/
    void DefinirLesComposants();
  }
}
