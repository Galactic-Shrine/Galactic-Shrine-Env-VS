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
	 *   [FR] Représente un groupe d’éléments.  
	 *   [EN] Represents a group of items.
	 * </summary>
	 **/
	public sealed class Groupe {

		/** 
		 * <summary>
		 *   [FR] Éléments du groupe.  
		 *   [EN] Items of the group.
		 * </summary>
		 **/
		private readonly string[] _Elements;

		/** 
		 * <summary>
		 *   [FR] Nom du groupe.  
		 *   [EN] Name of the group.
		 * </summary>
		 **/
		public string Noms { get; }

		/** 
		 * <summary>
		 *   [FR] Initialise une nouvelle instance de la classe <see cref="Groupe"/>.  
		 *   [EN] Initializes a new instance of the <see cref="Groupe"/> class.
		 * </summary>
		 * 
		 * <param name="Nom">
		 *   [FR] Nom du groupe.  
		 *   [EN] Name of the group.
		 * </param>
		 * 
		 * <param name="Element">
		 *   [FR] Éléments du groupe.  
		 *   [EN] Items of the group.
		 * </param>
		 **/
		public Groupe(string Nom, params string[] Element) {

			Noms = Nom ?? throw new ArgumentNullException(nameof(Nom));

			_Elements = Element ?? throw new ArgumentNullException(nameof(Element));
		}

		// Indexeurs
		public string this[int i] => _Elements[i];			// ex: [0]
		public string this[Index i] => _Elements[i];   // ex: [^1]
		public string[] this[Range r] => _Elements[r]; // ex: [1..]
		public int Compter => _Elements.Length;        // Nombre d’éléments dans le groupe

		/** 
		 * <summary>
		 *   [FR] Éléments du groupe en lecture seule.  
		 *   [EN] Read-only items of the group.
		 * </summary>
		 **/
		public IReadOnlyList<string> Element => Array.AsReadOnly(_Elements);
	}
}
