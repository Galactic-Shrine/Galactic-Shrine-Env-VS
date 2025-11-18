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
   *   [FR] Représente un groupe d’éléments (par exemple un groupe d’extensions de fichiers).<br/>
   *   [EN] Represents a group of items (for example, a group of file extensions).
   * </summary>
   **/
	public sealed class Groupe {

		/** 
     * <summary>
     *   [FR] Éléments du groupe (stockés en interne).<br/>
     *   [EN] Group items (stored internally).
     * </summary>
     **/
		private readonly string[] _Elements;

		/** 
     * <summary>
     *   [FR] Vue en lecture seule des éléments du groupe.<br/>
     *   [EN] Read-only view of the group items.
     * </summary>
     **/
		private readonly IReadOnlyList<string> _ElementsLectureSeule;

		/** 
     * <summary>
     *   [FR] Nom du groupe.<br/>
     *   [EN] Name of the group.
     * </summary>
     **/
		public string Noms { get; }

		/** 
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="Groupe"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="Groupe"/> class.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom du groupe.<br/>
     *   [EN] Name of the group.
     * </param>
     * <param name="Element">
     *   [FR] Éléments du groupe.<br/>
     *   [EN] Items of the group.
     * </param>
     **/
		public Groupe(string Nom, params string[] Element) {

			ArgumentException.ThrowIfNullOrEmpty(Nom, nameof(Nom));
			ArgumentNullException.ThrowIfNull(Element);

			Noms = Nom;

			_Elements = Element.Length == 0
				? Array.Empty<string>()
				: (string[])Element.Clone();

			_ElementsLectureSeule = Array.AsReadOnly(_Elements);
		}

		/**
     * <summary>
     *   [FR] Accède à un élément par index (0-based).<br/>
     *   [EN] Accesses an item by index (0-based).
     * </summary>
     * <param name="Indice">
     *   [FR] Index de l’élément.<br/>
     *   [EN] Item index.
     * </param>
     * <returns>
     *   [FR] Élément à l’index donné.<br/>
     *   [EN] Item at the given index.
     * </returns>
     **/
		public string this[int Indice] => _Elements[Indice];

		/**
     * <summary>
     *   [FR] Accède à un élément par index à partir de la fin (Index C# 8, ex. <c>[^1]</c>).<br/>
     *   [EN] Accesses an item using an index from the end (C# 8 Index, e.g. <c>[^1]</c>).
     * </summary>
     * <param name="Indice">
     *   [FR] Index à partir de la fin.<br/>
     *   [EN] Index from the end.
     * </param>
     * <returns>
     *   [FR] Élément à l’index donné.<br/>
     *   [EN] Item at the given index.
     * </returns>
     **/
		public string this[Index Indice] => _Elements[Indice];

		/**
     * <summary>
     *   [FR] Accède à une plage d’éléments (syntaxe C# range, ex. <c>[1..]</c>).<br/>
     *   [EN] Accesses a range of items (C# range syntax, e.g. <c>[1..]</c>).
     * </summary>
     * <param name="Plage">
     *   [FR] Plage d’index à récupérer.<br/>
     *   [EN] Range of indices to retrieve.
     * </param>
     * <returns>
     *   [FR] Tableau contenant les éléments de la plage.<br/>
     *   [EN] Array containing the items in the range.
     * </returns>
     **/
		public string[] this[Range Plage] => _Elements[Plage];

		/**
     * <summary>
     *   [FR] Nombre d’éléments dans le groupe.<br/>
     *   [EN] Number of items in the group.
     * </summary>
     **/
		public int Compter => _Elements.Length;

		/** 
     * <summary>
     *   [FR] Éléments du groupe en lecture seule.<br/>
     *   [EN] Read-only items of the group.
     * </summary>
     **/
		public IReadOnlyList<string> Element => _ElementsLectureSeule;

		/**
     * <summary>
     *   [FR] Indique si le groupe contient l’élément spécifié (comparaison insensible à la casse).<br/>
     *   [EN] Indicates whether the group contains the specified item (case-insensitive comparison).
     * </summary>
     * <param name="ElementRecherche">
     *   [FR] Élément à rechercher.<br/>
     *   [EN] Item to search for.
     * </param>
     * <returns>
     *   [FR] <c>true</c> si l’élément est présent ; sinon <c>false</c>.<br/>
     *   [EN] <c>true</c> if the item is present; otherwise <c>false</c>.
     * </returns>
     **/
		public bool Contient(string ElementRecherche) {

			ArgumentException.ThrowIfNullOrEmpty(ElementRecherche, nameof(ElementRecherche));

			foreach(string ElementCourant in _Elements) {

				if(string.Equals(ElementCourant, ElementRecherche, StringComparison.OrdinalIgnoreCase)) {

					return true;
				}
			}

			return false;
		}

		/**
     * <summary>
     *   [FR] Retourne le nom du groupe.<br/>
     *   [EN] Returns the group name.
     * </summary>
     **/
		public override string ToString() => Noms;
	}
}
