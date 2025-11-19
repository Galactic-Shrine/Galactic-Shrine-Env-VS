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
using System.Threading;
using System.Threading.Tasks;

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

		/**
     * <summary>
     *   [FR] Sauvegarde une chaîne de données de manière asynchrone.
     *   [EN] Asynchronously saves a string of data.
     * </summary>
     * <param name="Donnees">
     *   [FR] Données à sauvegarder.
     *   [EN] Data to be saved.
     * </param>
     * <param name="Cle">
     *   [FR] Clé (ou identifiant) pour retrouver ultérieurement les données.
     *   [EN] Key (or identifier) to later retrieve the data.
     * </param>
     * <param name="JetonAnnulation">
     *   [FR] Jeton de demande d'annulation de l'opération.
     *   [EN] Token used to request cancellation of the operation.
     * </param>
     * <returns>
     *   [FR] Une tâche représentant l'opération asynchrone.
     *   [EN] A task representing the asynchronous operation.
     * </returns>
     **/
		Task SauvegarderAsync(string Donnees, string Cle, CancellationToken JetonAnnulation = default);

		/**
     * <summary>
     *   [FR] Charge de manière asynchrone les données précédemment sauvegardées.
     *   [EN] Asynchronously loads previously saved data.
     * </summary>
     * <param name="Cle">
     *   [FR] Clé (ou identifiant) pour laquelle on veut récupérer les données.
     *   [EN] Key (or identifier) for which we want to retrieve the data.
     * </param>
     * <param name="JetonAnnulation">
     *   [FR] Jeton de demande d'annulation de l'opération.
     *   [EN] Token used to request cancellation of the operation.
     * </param>
     * <returns>
     *   [FR] Une tâche retournant la chaîne de données, ou <c>null</c> si introuvable.
     *   [EN] A task returning the string data, or <c>null</c> if not found.
     * </returns>
     **/
		Task<string> ChargerAsync(string Cle, CancellationToken JetonAnnulation = default);
	}
}
