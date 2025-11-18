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

namespace GalacticShrine.Stockage {

	/**
   * <summary>
   *   [FR] Catalogue de groupes nommés (par exemple un catalogue d’extensions de fichiers).<br/>
   *   [EN] Catalog of named groups (for example, a catalog of file extensions).
   * </summary>
   **/
	public sealed class Catalogue {

		/** 
     * <summary>
     *   [FR] Groupes dans le catalogue, indexés par nom (insensibles à la casse).<br/>
     *   [EN] Groups in the catalog, indexed by name (case-insensitive).
     * </summary>
     **/
		private readonly Dictionary<string, Groupe> _Groupes;

		/** 
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="Catalogue"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="Catalogue"/> class.
     * </summary>
     * <param name="Groupes">
     *   [FR] Groupes à inclure dans le catalogue (clés = noms de groupes).<br/>
     *   [EN] Groups to include in the catalog (keys = group names).
     * </param>
     **/
		public Catalogue(IDictionary<string, Groupe> Groupes) {

			ArgumentNullException.ThrowIfNull(Groupes);

			_Groupes = new Dictionary<string, Groupe>(StringComparer.OrdinalIgnoreCase);

			foreach(KeyValuePair<string, Groupe> Paire in Groupes) {

				if(string.IsNullOrWhiteSpace(Paire.Key)) {

					throw new ArgumentException("Le nom de groupe ne peut pas être nul ou vide.", nameof(Groupes));
				}

				if(Paire.Value is null) {

					throw new ArgumentException("Les groupes ne peuvent pas être nuls.", nameof(Groupes));
				}

				_Groupes.Add(Paire.Key, Paire.Value);
			}
		}

		// Accès direct : lève KeyNotFoundException si le groupe n'existe pas

		/**
     * <summary>
     *   [FR] Accède à un groupe par son nom. Lève une <see cref="KeyNotFoundException"/> si le groupe n’existe pas.<br/>
     *   [EN] Accesses a group by its name. Throws <see cref="KeyNotFoundException"/> if the group does not exist.
     * </summary>
     * <param name="NomDuGroupe">
     *   [FR] Nom du groupe à récupérer.<br/>
     *   [EN] Name of the group to retrieve.
     * </param>
     * <returns>
     *   [FR] Groupe correspondant au nom fourni.<br/>
     *   [EN] Group corresponding to the given name.
     * </returns>
     **/
		public Groupe this[string NomDuGroupe] {

			get {

				ArgumentException.ThrowIfNullOrEmpty(NomDuGroupe, nameof(NomDuGroupe));
				return _Groupes[NomDuGroupe];
			}
		}

		/** 
     * <summary>
     *   [FR] Noms des groupes dans le catalogue.<br/>
     *   [EN] Names of the groups in the catalog.
     * </summary>
     **/
		public IEnumerable<string> NomDuGroupe => _Groupes.Keys;

		/**
     * <summary>
     *   [FR] Groupes contenus dans le catalogue.<br/>
     *   [EN] Groups contained in the catalog.
     * </summary>
     **/
		public IEnumerable<Groupe> Groupes => _Groupes.Values;

		/**
     * <summary>
     *   [FR] Nombre de groupes enregistrés dans le catalogue.<br/>
     *   [EN] Number of groups registered in the catalog.
     * </summary>
     **/
		public int Compter => _Groupes.Count;

		/**
     * <summary>
     *   [FR] Indique si le catalogue contient un groupe portant le nom donné.<br/>
     *   [EN] Indicates whether the catalog contains a group with the given name.
     * </summary>
     * <param name="NomDuGroupe">
     *   [FR] Nom du groupe à vérifier.<br/>
     *   [EN] Group name to check.
     * </param>
     * <returns>
     *   [FR] <c>true</c> si le groupe existe ; sinon <c>false</c>.<br/>
     *   [EN] <c>true</c> if the group exists; otherwise <c>false</c>.
     * </returns>
     **/
		public bool ContientLeGroupe(string NomDuGroupe) {

			ArgumentException.ThrowIfNullOrEmpty(NomDuGroupe, nameof(NomDuGroupe));
			return _Groupes.ContainsKey(NomDuGroupe);
		}

		/**
     * <summary>
     *   [FR] Tente de récupérer un groupe par son nom, sans lever d’exception si celui-ci est introuvable.<br/>
     *   [EN] Tries to retrieve a group by its name without throwing if it is not found.
     * </summary>
     * <param name="NomDuGroupe">
     *   [FR] Nom du groupe à récupérer.<br/>
     *   [EN] Name of the group to retrieve.
     * </param>
     * <param name="Groupe">
     *   [FR] Groupe résultant si trouvé, sinon <c>null</c>.<br/>
     *   [EN] Resulting group if found; otherwise <c>null</c>.
     * </param>
     * <returns>
     *   [FR] <c>true</c> si le groupe a été trouvé ; sinon <c>false</c>.<br/>
     *   [EN] <c>true</c> if the group was found; otherwise <c>false</c>.
     * </returns>
     **/
		public bool EssayerObtenirGroupe(string NomDuGroupe, out Groupe Groupe) {

			ArgumentException.ThrowIfNullOrEmpty(NomDuGroupe, nameof(NomDuGroupe));
			return _Groupes.TryGetValue(NomDuGroupe, out Groupe);
		}
	}
}
