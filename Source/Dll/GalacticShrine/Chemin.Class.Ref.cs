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
using System.IO;

namespace GalacticShrine.IO {

  /**
   * <summary>
   *   [FR] Fournit des méthodes utilitaires pour la gestion des chemins de fichiers et de répertoires.
   *   [EN] Provides utility methods for handling file and directory paths.
   * </summary>
   **/
  public static partial class Chemin {

    /**
     * <summary>
     *   [FR] Combine plusieurs segments de chemin en un seul.
     *   [EN] Combines multiple path segments into one.
     * </summary>
     * <param name="Chemin">
     *   [FR] Un tableau de segments de chemin à combiner.
     *   [EN] An array of path segments to combine.
     * </param>
     * <returns>
     *   [FR] Le chemin combiné.
     *   [EN] The combined path.
     * </returns>
     **/
    public static string Combiner(params string[] Chemin) => Path.Combine(paths: Chemin);

    /**
     * <summary>
     *   [FR] Combine deux segments de chemin en un seul.
     *   [EN] Combines two path segments into one.
     * </summary>
     * <param name="Chemin1">
     *   [FR] Le premier segment du chemin.
     *   [EN] The first path segment.
     * </param>
     * <param name="Chemin2">
     *   [FR] Le deuxième segment du chemin.
     *   [EN] The second path segment.
     * </param>
     * <returns>
     *   [FR] Le chemin combiné.
     *   [EN] The combined path.
     * </returns>
     **/
    public static string Combiner(string Chemin1, string Chemin2) => Path.Combine(path1: Chemin1, path2: Chemin2);

    /**
     * <summary>
     *   [FR] Combine trois segments de chemin en un seul.
     *   [EN] Combines three path segments into one.
     * </summary>
     * <param name="Chemin1">
     *   [FR] Le premier segment du chemin.
     *   [EN] The first path segment.
     * </param>
     * <param name="Chemin2">
     *   [FR] Le deuxième segment du chemin.
     *   [EN] The second path segment.
     * </param>
     * <param name="Chemin3">
     *   [FR] Le troisième segment du chemin.
     *   [EN] The third path segment.
     * </param>
     * <returns>
     *   [FR] Le chemin combiné.
     *   [EN] The combined path.
     * </returns>
     **/
    public static string Combiner(string Chemin1, string Chemin2, string Chemin3) => Path.Combine(path1: Chemin1, path2: Chemin2, path3: Chemin3);

    /**
     * <summary>
     *   [FR] Combine quatre segments de chemin en un seul.
     *   [EN] Combines four path segments into one.
     * </summary>
     * <param name="Chemin1">
     *   [FR] Le premier segment du chemin.
     *   [EN] The first path segment.
     * </param>
     * <param name="Chemin2">
     *   [FR] Le deuxième segment du chemin.
     *   [EN] The second path segment.
     * </param>
     * <param name="Chemin3">
     *   [FR] Le troisième segment du chemin.
     *   [EN] The third path segment.
     * </param>
     * <param name="Chemin4">
     *   [FR] Le quatrième segment du chemin.
     *   [EN] The fourth path segment.
     * </param>
     * <returns>
     *   [FR] Le chemin combiné.
     *   [EN] The combined path.
     * </returns>
     **/
    public static string Combiner(string Chemin1, string Chemin2, string Chemin3, string Chemin4) => Path.Combine(path1: Chemin1, path2: Chemin2, path3: Chemin3, path4: Chemin4);

    /**
     * <summary>
     *   [FR] Retourne le nom du fichier à partir d'un chemin donné.
     *   [EN] Returns the file name from a given path.
     * </summary>
     * <param name="Chemin">
     *   [FR] Le chemin complet du fichier.
     *   [EN] The full file path.
     * </param>
     * <returns>
     *   [FR] Le nom du fichier ou null si le chemin est invalide.
     *   [EN] The file name or null if the path is invalid.
     * </returns>
     **/
    public static string? ObtenirLeNomDeFichier(string? Chemin) => Path.GetFileName(path: Chemin);

    /**
     * <summary>
     *   [FR] Retourne le nom du fichier à partir d'un chemin donné sous forme de ReadOnlySpan.
     *   [EN] Returns the file name from a given path as a ReadOnlySpan.
     * </summary>
     * <param name="Chemin">
     *   [FR] Le chemin complet du fichier.
     *   [EN] The full file path.
     * </param>
     * <returns>
     *   [FR] Le nom du fichier sous forme de ReadOnlySpan.
     *   [EN] The file name as a ReadOnlySpan.
     * </returns>
     **/
    public static ReadOnlySpan<char> ObtenirLeNomDeFichier(ReadOnlySpan<char> Chemin) => Path.GetFileName(path: Chemin);
  }
}
