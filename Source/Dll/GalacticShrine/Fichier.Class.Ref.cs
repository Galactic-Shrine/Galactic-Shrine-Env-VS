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
using GalacticShrine.Stockage;

namespace GalacticShrine {

	/**
   * <summary>
   *   [FR] Fonctions utilitaires liées aux fichiers (catalogues, extensions, etc.).<br/>
   *   [EN] File-related utilities (catalogs, extensions, etc.).
   * </summary>
   **/
	public static class Fichier {

		/** 
     * <summary>
     *   [FR] Catalogue d’extensions de fichiers organisées par groupes logiques (Images, Audio, Vidéo, etc.).<br/>
     *   [EN] Catalog of file extensions organized by logical groups (Images, Audio, Video, etc.).
     * </summary>
     * <remarks>
     *   [FR] Le catalogue est insensible à la casse sur les noms de groupes.<br/>
     *   [EN] The catalog is case-insensitive on group names.
     * </remarks>
     **/
		public static readonly Catalogue Extension = new Catalogue(
			new Dictionary<string, Groupe>(StringComparer.OrdinalIgnoreCase)
			{

        /**
         * [FR] Ce catalogue peut être étendu si nécessaire (ajout de groupes ou d’extensions spécifiques projet).<br/>
         * [EN] This catalog can be extended if needed (add project-specific groups or extensions).
         **/

        ["Images"] = new Groupe(
					"Images",
					".png", ".jpg", ".jpeg", ".bmp", ".gif", ".tiff", ".tif", ".dds", ".tga", ".webp"
				),

				["Audio"] = new Groupe(
					"Audio",
					".mp3", ".wav", ".ogg", ".flac", ".aac", ".wma"
				),

				["Vidéo"] = new Groupe(
					"Vidéo",
					".mp4", ".avi", ".mkv", ".mov", ".wmv", ".flv"
				),

				["Documents"] = new Groupe(
					"Documents",
					".txt", ".md", ".rtf", ".doc", ".docx", ".pdf", ".log"
				),

				["Scripts"] = new Groupe(
					"Scripts",
					".cs", ".js", ".py", ".lua", ".bat", ".sh"
				),

				["Archives"] = new Groupe(
					"Archives",
					".zip", ".rar", ".7z", ".tar", ".gz"
				),

				["Fonts"] = new Groupe(
					"Fonts",
					".ttf", ".otf", ".woff", ".woff2"
				),

				["Config"] = new Groupe(
					"Config",
					".ini", ".json", ".cfg", ".conf", ".cnf", ".config", ".xml", ".yaml", ".yml", ".toml", ".properties", ".env"
				),

				["Binaire"] = new Groupe(
					"Binaire",
					".bin"
				),

        /**
         * [FR] Groupe spécifique pour les fichiers Galactic-Shrine.<br/>
         * [EN] Specific group for Galactic-Shrine files.
         **/
        ["Gs"] = new Groupe(
					"Gs",
					".gsc", ".gscc", ".gsp"
				)
			}
		);

		/**
     * <summary>
     *   [FR] Vérifie si une extension appartient à un groupe donné du catalogue <see cref="Extension"/>.<br/>
     *   [EN] Checks whether an extension belongs to a given group in the <see cref="Extension"/> catalog.
     * </summary>
     * <param name="ExtensionDeFichier">
     *   [FR] Extension à tester (avec ou sans point, ex. <c>".png"</c> ou <c>"png"</c>).<br/>
     *   [EN] Extension to test (with or without dot, e.g. <c>".png"</c> or <c>"png"</c>).
     * </param>
     * <param name="NomDuGroupe">
     *   [FR] Nom logique du groupe (ex. <c>"Images"</c>, <c>"Audio"</c>, <c>"Gs"</c>).<br/>
     *   [EN] Logical group name (e.g. <c>"Images"</c>, <c>"Audio"</c>, <c>"Gs"</c>).
     * </param>
     * <returns>
     *   [FR] <c>true</c> si l’extension appartient au groupe ; sinon <c>false</c> (groupe absent ou non correspondant).<br/>
     *   [EN] <c>true</c> if the extension belongs to the group; otherwise <c>false</c> (group missing or not matching).
     * </returns>
     * <example>
     *   [FR]
     *   <code>
     *   bool estImage = Fichier.EstExtensionDansGroupe(".png", "Images");
     *   </code>
     *   [EN]
     *   <code>
     *   bool isImage = Fichier.EstExtensionDansGroupe(".png", "Images");
     *   </code>
     * </example>
     **/
		public static bool EstExtensionDansGroupe(string ExtensionDeFichier, string NomDuGroupe) {

			ArgumentException.ThrowIfNullOrEmpty(ExtensionDeFichier, nameof(ExtensionDeFichier));
			ArgumentException.ThrowIfNullOrEmpty(NomDuGroupe, nameof(NomDuGroupe));

			string ExtensionNormalisee = NormaliserExtension(ExtensionDeFichier);

			return Extension.EssayerObtenirGroupe(NomDuGroupe, out Groupe GroupeTrouve)
				&& GroupeTrouve.Contient(ExtensionNormalisee);
		}

		/**
     * <summary>
     *   [FR] Retourne le premier nom de groupe auquel appartient une extension, ou <c>null</c> si aucun groupe ne correspond.<br/>
     *   [EN] Returns the first group name to which an extension belongs, or <c>null</c> if no group matches.
     * </summary>
     * <param name="ExtensionDeFichier">
     *   [FR] Extension à rechercher (avec ou sans point).<br/>
     *   [EN] Extension to search for (with or without dot).
     * </param>
     * <returns>
     *   [FR] Nom du groupe trouvé, ou <c>null</c> si aucun groupe ne contient l’extension.<br/>
     *   [EN] Group name if found, or <c>null</c> if no group contains the extension.
     * </returns>
     * <example>
     *   [FR]
     *   <code>
     *   string? groupe = Fichier.TrouverPremierGroupePourExtension("mp3"); // "Audio"
     *   </code>
     *   [EN]
     *   <code>
     *   string? group = Fichier.TrouverPremierGroupePourExtension("mp3"); // "Audio"
     *   </code>
     * </example>
     **/
		public static string? TrouverPremierGroupePourExtension(string ExtensionDeFichier) {

			ArgumentException.ThrowIfNullOrEmpty(ExtensionDeFichier, nameof(ExtensionDeFichier));

			string ExtensionNormalisee = NormaliserExtension(ExtensionDeFichier);

			foreach(string NomGroupe in Extension.NomDuGroupe) {

				Groupe GroupeCourant = Extension[NomGroupe];
				if(GroupeCourant.Contient(ExtensionNormalisee)) {

					return NomGroupe;
				}
			}

			return null;
		}

		/**
     * <summary>
     *   [FR] Normalise une extension en s’assurant qu’elle commence par un point ('.').<br/>
     *   [EN] Normalizes an extension by ensuring it starts with a dot ('.').
     * </summary>
     * <param name="ExtensionDeFichier">
     *   [FR] Extension à normaliser.<br/>
     *   [EN] Extension to normalize.
     * </param>
     * <returns>
     *   [FR] Extension normalisée (toujours avec un point initial).<br/>
     *   [EN] Normalized extension (always with a leading dot).
     * </returns>
     **/
		private static string NormaliserExtension(string ExtensionDeFichier) {

			string Resultat = ExtensionDeFichier.Trim();

			if(Resultat.Length == 0) {

				throw new ArgumentException("L’extension ne peut pas être vide après normalisation.", nameof(ExtensionDeFichier));
			}

			if(!Resultat.StartsWith(".", StringComparison.Ordinal)) {

				Resultat = "." + Resultat;
			}

			return Resultat;
		}
	}
}
