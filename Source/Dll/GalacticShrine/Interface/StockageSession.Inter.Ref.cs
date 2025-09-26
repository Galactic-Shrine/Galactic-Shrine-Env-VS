/**
 * Copyright © 2017-2024, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2024, Galactic-Shrine - Tous droits réservés.
 **/

namespace GalacticShrine.Interface {

  /**
   * <summary>
   *   [FR] Interface décrivant la sauvegarde et le chargement de données de session.
   *   [EN] Interface describing session data save/load operations.
   * </summary>
   **/
  public interface StockageSessionInterface {

    /**
     * <summary>
     *   [FR] Sauvegarde une chaîne de données associée à une clé donnée.
     *   [EN] Saves a string of data associated with a given key.
     * </summary>
     * <param name="Donnees">
     *   [FR] Données à sauvegarder.
     *   [EN] Data to be saved.
     * </param>
     * <param name="Cle">
     *   [FR] Clé (ou identifiant) pour retrouver ultérieurement les données.
     *   [EN] Key (or identifier) to later retrieve the data.
     * </param>
     **/
    void Sauvegarder(string Donnees, string Cle);

    /**
     * <summary>
     *   [FR] Charge la chaîne de données précédemment sauvegardée pour la clé spécifiée.
     *   [EN] Loads the previously saved string of data for the specified key.
     * </summary>
     * <param name="Cle">
     *   [FR] Clé (ou identifiant) pour laquelle on veut récupérer les données.
     *   [EN] Key (or identifier) for which we want to retrieve the data.
     * </param>
     * <returns>
     *   [FR] Retourne la chaîne de données, ou <c>null</c> si introuvable.
     *   [EN] Returns the string data, or <c>null</c> if not found.
     * </returns>
     **/
    string Charger(string Cle);
  }
}
