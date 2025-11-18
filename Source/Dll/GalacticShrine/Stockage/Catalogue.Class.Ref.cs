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

	public sealed class Catalogue {

		/** 
		 * <summary>
		 *   [FR] Groupes dans le catalogue.  
		 *   [EN] Groups in the catalog.
		 * </summary>
		 **/
		private readonly Dictionary<string, Groupe> _Groupes;

		/** 
		 * <summary>
		 *   [FR] Initialise une nouvelle instance de la classe <see cref="Catalogue"/>.  
		 *   [EN] Initializes a new instance of the <see cref="Catalogue"/> class.
		 * </summary>
		 * 
		 * <param name="Groupes">
		 *   [FR] Groupes à inclure dans le catalogue.  
		 *   [EN] Groups to include in the catalog.
		 * </param>
		 **/
		public Catalogue(IDictionary<string, Groupe> Groupes) => _Groupes = new Dictionary<string, Groupe>(Groupes, StringComparer.OrdinalIgnoreCase);

		// Accès direct : lève KeyNotFoundException si le groupe n'existe pas
		public Groupe this[string NomDuGroupe] => _Groupes[NomDuGroupe];

		/** 
		 * <summary>
		 *   [FR] Noms des groupes dans le catalogue.  
		 *   [EN] Names of the groups in the catalog.
		 * </summary>
		 **/
		public IEnumerable<string> NomDuGroupe => _Groupes.Keys;
	}
}
