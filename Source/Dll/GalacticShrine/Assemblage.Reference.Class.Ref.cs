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
using System.Reflection;

namespace GalacticShrine {

  public static class AssemblageReference {

    /**
     * <summary>
     *   [FR] Récupère l'emplacement original (chemin et nom de fichier) d'un assemblage.
     *        Cette méthode utilise la propriété Assembly.CodeBase pour résoudre la propriété originale
     *        dans le cas où la copie d'ombre est activée.
     *   [EN] Gets the original location (path and filename) of an assembly.
     *        This method is using Assembly.CodeBase property to property resolve original
     *        assembly path in case shadow copying is enabled.
     * </summary>
     * <returns>
     *   [FR] Chemin absolu et nom de fichier de l'assemblage.
     *   [EN] Absolute path and filename to the assembly.
     * </returns>
     **/
    public static string ObtenirL_EmplacementD_Origine(this Assembly Assemblage) => new Uri(uriString: Assemblage.Location).LocalPath;

    /**
     * <summary>
     *    Récupère la valeur de l'attribut AssemblyCompanyAttribute sous forme de chaîne.
     * </summary>
     * <param name="assembly">
     *    L'assembly à partir duquel récupérer l'attribut. Si null, l'assembly actuel est utilisé.
     * </param>
     * <returns>
     *    Le nom de l'entreprise défini dans AssemblyCompanyAttribute, ou null si l'attribut n'est pas trouvé.
     * </returns>
     **/
    public static string ObtenirL_Entreprise(Assembly assembly = null) {
      // Utiliser l'assembly actuel si aucun n'est spécifié
      assembly ??= Assembly.GetExecutingAssembly();

      // Récupérer l'attribut AssemblyCompanyAttribute
      AssemblyCompanyAttribute companyAttribute = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();

      // Retourner la valeur de l'attribut ou null si non trouvé
      return companyAttribute?.Company;
    }
  }
}
