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
using System.Reflection;
using GalacticShrine.IO;
using static GalacticShrine.DossierReference;

namespace GalacticShrine {

	/**
   * <summary>
   *   [FR] Point d'entrée statique pour l'environnement Galactic-Shrine (répertoires de base, constantes globales, etc.).<br/>
   *        Cette classe expose notamment un ensemble de chemins standards via le dictionnaire <see cref="Repertoire"/>.
   *   [EN] Static entry point for the Galactic-Shrine environment (base directories, global constants, etc.).<br/>
   *        This class mainly exposes a set of standard paths through the <see cref="Repertoire"/> dictionary.
   * </summary>
   * <remarks>
   *   [FR] Cette classe est abstraite afin d'empêcher son instanciation directe ; tous les membres actuels sont statiques.<br/>
   *   [EN] This class is abstract to prevent direct instantiation; all current members are static.
   * </remarks>
   **/
	[Serializable]
	public abstract class GalacticShrine {

		/**
     * <summary>
     *   [FR] Indique si le système d'exploitation actuel est un système 64 bits.<br/>
     *        Raccourci de <see cref="Environment.Is64BitOperatingSystem"/>.
     *   [EN] Indicates whether the current operating system is 64-bit.<br/>
     *        Shortcut for <see cref="Environment.Is64BitOperatingSystem"/>.
     * </summary>
     * <returns>
     *   [FR] <c>true</c> si l'OS est 64 bits, sinon <c>false</c>.<br/>
     *   [EN] <c>true</c> if the OS is 64-bit; otherwise <c>false</c>.
     * </returns>
     **/
		public static readonly bool EstEn64Bit = Environment.Is64BitOperatingSystem;

		/**
     * <summary>
     *   [FR] Table des répertoires et chemins standards utilisés par Galactic-Shrine.<br/>
     *        Les clés connues sont&nbsp;:
     *        <list type="bullet">
     *          <item><description><c>"ProgramFiles"</c> : <see cref="string"/> chemin vers Program Files si disponible.</description></item>
     *          <item><description><c>"Documents"</c>    : <see cref="string"/> dossier Documents de l'utilisateur.</description></item>
     *          <item><description><c>"Roaming"</c>      : <see cref="string"/> AppData Roaming.</description></item>
     *          <item><description><c>"Societe"</c>      : <see cref="DossierReference"/> parent du répertoire de l’assemblage.</description></item>
     *          <item><description><c>"Racine"</c>       : <see cref="DossierReference"/> répertoire de l’assemblage.</description></item>
     *          <item><description><c>"DLog"</c>         : <see cref="DossierReference"/> logs utilisateur (Documents\&lt;Entreprise&gt;\Logs).</description></item>
     *          <item><description><c>"Config"</c>       : <see cref="DossierReference"/> sous-dossier <c>Config</c> du répertoire de l’assemblage.</description></item>
     *          <item><description><c>"Log"</c>          : <see cref="DossierReference"/> sous-dossier <c>Logs</c> du répertoire de l’assemblage.</description></item>
     *          <item><description><c>"Source"</c>       : <see cref="DossierReference"/> répertoire logique <c>Source</c> à partir de l’assemblage.</description></item>
     *        </list>
     *   [EN] Map of standard directories and paths used by Galactic-Shrine.<br/>
     *        Known keys are:
     *        <list type="bullet">
     *          <item><description><c>"ProgramFiles"</c>: <see cref="string"/> path to Program Files when available.</description></item>
     *          <item><description><c>"Documents"</c>   : <see cref="string"/> user Documents folder.</description></item>
     *          <item><description><c>"Roaming"</c>     : <see cref="string"/> AppData Roaming.</description></item>
     *          <item><description><c>"Societe"</c>     : <see cref="DossierReference"/> parent of the assembly directory.</description></item>
     *          <item><description><c>"Racine"</c>      : <see cref="DossierReference"/> assembly directory.</description></item>
     *          <item><description><c>"DLog"</c>        : <see cref="DossierReference"/> user logs (Documents\&lt;Company&gt;\Logs).</description></item>
     *          <item><description><c>"Config"</c>      : <see cref="DossierReference"/> <c>Config</c> subdirectory of the assembly directory.</description></item>
     *          <item><description><c>"Log"</c>         : <see cref="DossierReference"/> <c>Logs</c> subdirectory of the assembly directory.</description></item>
     *          <item><description><c>"Source"</c>      : <see cref="DossierReference"/> logical <c>Source</c> directory relative to the assembly.</description></item>
     *        </list>
     * </summary>
     * <remarks>
     *   [FR] Le dictionnaire est insensible à la casse sur la clé.<br/>
     *   [EN] The dictionary is case-insensitive on its keys.
     * </remarks>
     **/
		public static readonly Dictionary<string, object> Repertoire = CreerTableDesRepertoires();

		/**
     * <summary>
     *   [FR] Obtient une valeur de type <see cref="DossierReference"/> à partir du dictionnaire <see cref="Repertoire"/>, si possible.
     *   [EN] Gets a <see cref="DossierReference"/> value from the <see cref="Repertoire"/> dictionary when possible.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom logique (clé) du répertoire, par exemple <c>"Config"</c>, <c>"Log"</c>, <c>"Source"</c>.
     *   [EN] Logical directory name (key), e.g. <c>"Config"</c>, <c>"Log"</c>, <c>"Source"</c>.
     * </param>
     * <returns>
     *   [FR] Une instance de <see cref="DossierReference"/> si la clé existe et que la valeur est de ce type, sinon <c>null</c>.
     *   [EN] A <see cref="DossierReference"/> instance if the key exists and the value is of this type; otherwise <c>null</c>.
     * </returns>
     * <example>
     *   [FR] Récupérer le répertoire de configuration :
     *   <code>
     *   DossierReference? dossierConfig = GalacticShrine.ObtenirLeDossier("Config");
     *   </code>
     *   [EN] Retrieve the configuration directory:
     *   <code>
     *   DossierReference? configFolder = GalacticShrine.ObtenirLeDossier("Config");
     *   </code>
     * </example>
     **/
		public static DossierReference? ObtenirLeDossier(string Nom) {

			ArgumentException.ThrowIfNullOrEmpty(Nom, nameof(Nom));

			if(!Repertoire.TryGetValue(Nom, out object? Valeur)) {

				return null;
			}

			return Valeur as DossierReference;
		}

		/**
     * <summary>
     *   [FR] Obtient une valeur de type <see cref="string"/> (chemin brut) à partir du dictionnaire <see cref="Repertoire"/>, si possible.
     *   [EN] Gets a <see cref="string"/> (raw path) value from the <see cref="Repertoire"/> dictionary when possible.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom logique (clé) du chemin, par exemple <c>"ProgramFiles"</c>, <c>"Documents"</c>.
     *   [EN] Logical name (key) of the path, e.g. <c>"ProgramFiles"</c>, <c>"Documents"</c>.
     * </param>
     * <returns>
     *   [FR] Une chaîne contenant le chemin si la clé existe et que la valeur est une chaîne, sinon <c>null</c>.
     *   [EN] A string containing the path if the key exists and the value is a string; otherwise <c>null</c>.
     * </returns>
     **/
		public static string? ObtenirLeChemin(string Nom) {

			ArgumentException.ThrowIfNullOrEmpty(Nom, nameof(Nom));

			if(!Repertoire.TryGetValue(Nom, out object? Valeur)) {

				return null;
			}

			return Valeur as string;
		}

		/**
     * <summary>
     *   [FR] Construit la table des répertoires logiques utilisée par <see cref="Repertoire"/>.
     *   [EN] Builds the logical directory map used by <see cref="Repertoire"/>.
     * </summary>
     * <returns>
     *   [FR] Dictionnaire initialisé avec les chemins standards.
     *   [EN] Dictionary initialized with standard paths.
     * </returns>
     **/
		private static Dictionary<string, object> CreerTableDesRepertoires() {

			var Table = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

			// Chemins simples (chaînes)
			string? CheminProgramFiles = ObtenirRepertoireProgramFiles();
			if(!string.IsNullOrWhiteSpace(CheminProgramFiles)) {

				Table["ProgramFiles"] = CheminProgramFiles;
			}

			string CheminDocuments = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			if(!string.IsNullOrWhiteSpace(CheminDocuments)) {

				Table["Documents"] = CheminDocuments;
			}

			string CheminRoaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			if(!string.IsNullOrWhiteSpace(CheminRoaming)) {

				Table["Roaming"] = CheminRoaming;
			}

			// Chemins dépendants de l'assemblage
			string EmplacementAssemblage = Assembly.GetExecutingAssembly().ObtenirL_EmplacementD_Origine();
			string? DossierAssemblage = ObtenirLeNomDuRepertoire(EmplacementAssemblage);

			if(!string.IsNullOrWhiteSpace(DossierAssemblage)) {

				// Parent du répertoire de l’assemblage (ex : "Societe")
				var DossierSociete = new DossierReference(
					Chemins: Chemin.Combiner(
						DossierAssemblage,
						".."
					)
				);

				// Répertoire de l’assemblage (ex : "bin\Debug\net8.0")
				var DossierRacine = new DossierReference(
					Chemins: Chemin.Combiner(
						DossierAssemblage
					)
				);

				// Sous-dossiers "Config" et "Logs" au même niveau que l’assemblage
				var DossierConfig = new DossierReference(
					Chemins: Chemin.Combiner(
						DossierAssemblage,
						"Config"
					)
				);

				var DossierLog = new DossierReference(
					Chemins: Chemin.Combiner(
						DossierAssemblage,
						"Logs"
					)
				);

				// Dossier "Source" au-dessus du répertoire de l’assemblage
				var DossierSource = new DossierReference(
					Chemins: Chemin.Combiner(
						Chemin.Combiner(
							DossierAssemblage,
							".."
						),
						"Source"
					)
				);

				Table["Societe"] = DossierSociete;
				Table["Racine"] = DossierRacine;
				Table["Config"] = DossierConfig;
				Table["Log"] = DossierLog;
				Table["Source"] = DossierSource;
			}

			// Dossier de logs utilisateur : Documents\<Entreprise>\Logs
			string NomEntreprise = AssemblageReference.ObtenirL_Entreprise() ?? "Galactic-Shrine";
			string DossierLogsUtilisateur = Chemin.Combiner(
				Environment.GetFolderPath(Environment.SpecialFolder.Personal),
				Chemin.Combiner(
					NomEntreprise,
					"Logs"
				)
			);

			Table["DLog"] = new DossierReference(DossierLogsUtilisateur);

			return Table;
		}

		/**
     * <summary>
     *   [FR] Tente de déterminer un chemin pertinent pour "Program Files" sur la plateforme courante.
     *   [EN] Tries to determine a suitable "Program Files" path on the current platform.
     * </summary>
     * <returns>
     *   [FR] Chemin vers Program Files, ou <c>null</c> si aucun chemin cohérent n'est disponible.
     *   [EN] Path to Program Files, or <c>null</c> if no meaningful path is available.
     * </returns>
     **/
		private static string? ObtenirRepertoireProgramFiles() {

			// 1. SpécialFolder.ProgramFiles (meilleure source sous Windows / .NET)
			string CheminSpecial = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
			if(!string.IsNullOrWhiteSpace(CheminSpecial)) {

				return CheminSpecial;
			}

			// 2. Variables d'environnement classiques
			string? VariableProgramFiles = Environment.GetEnvironmentVariable("ProgramFiles");
			if(!string.IsNullOrWhiteSpace(VariableProgramFiles)) {

				return VariableProgramFiles;
			}

			string? VariableProgramFilesX86 = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
			if(!string.IsNullOrWhiteSpace(VariableProgramFilesX86)) {

				return VariableProgramFilesX86;
			}

			// 3. Plateformes non Windows : pas de convention évidente → on renvoie null.
			return null;
		}
	}
}
