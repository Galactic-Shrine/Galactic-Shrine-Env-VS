/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
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
