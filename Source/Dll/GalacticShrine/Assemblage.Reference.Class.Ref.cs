/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
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
  }
}
