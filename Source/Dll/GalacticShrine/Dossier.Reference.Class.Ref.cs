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
using System.IO;

using GalacticShrine.Enumeration;

namespace GalacticShrine {

	/**
   * <summary>
   *   [FR] Représentation d'un chemin de répertoire absolu.
   *        Permet un hachage et des comparaisons rapides.
   *   [EN] Representation of an absolute directory path. 
   *        Allows fast hashing and comparisons.
   * </summary>
   **/
	[Serializable]
	public class DossierReference : FichierSystemeReference, IEquatable<DossierReference> {

		/**
     * <summary>
     *   [FR] Séparateur de répertoire.
     *        Raccourci de <see cref="Path.DirectorySeparatorChar"/>.
     *   [EN] Directory separator.
     *        Shortcut of <see cref="Path.DirectorySeparatorChar"/>.
     * </summary>
     **/
		public static readonly char Rs = Path.DirectorySeparatorChar;

		/**
     * <summary>
     *   [FR] Séparateur de répertoire.
     *        Raccourci de <see cref="Path.DirectorySeparatorChar"/>.
     *   [EN] Directory separator.
     *        Shortcut of <see cref="Path.DirectorySeparatorChar"/>.
     * </summary>
     **/
		public static readonly char RepertoireSeparateur = Path.DirectorySeparatorChar;

		/**
     * <summary>
     *   [FR] Constructeur par défaut.
     *   [EN] Default constructor.
     * </summary>
     * <param name="Chemins">
     *   [FR] Chemin d'accès à ce répertoire.
     *   [EN] Path to this directory.
     * </param>
     **/
		public DossierReference(string Chemins)
			: base(SeparateurDeVoieDeFuite(ObtenirCheminCompletValide(Chemins))) {
		}

		/**
     * <summary>
     *   [FR] Construit une DossierReference à partir d'un objet <see cref="DirectoryInfo"/>.
     *   [EN] Constructs a DossierReference from a <see cref="DirectoryInfo"/> object.
     * </summary>
     * <param name="InfoRepertoire">
     *   [FR] Informations sur le répertoire.
     *   [EN] Directory information.
     * </param>
     **/
		public DossierReference(DirectoryInfo InfoRepertoire)
			: base(SeparateurDeVoieDeFuite(ObtenirNomCompletDepuisDirectoryInfo(InfoRepertoire))) {
		}

		/**
     * <summary>
     *   [FR] Constructeur pour créer un objet répertoire directement à partir d'une chaîne aseptisée.
     *   [EN] Constructor for creating a directory object directly from a sanitized string.
     * </summary>
     * <param name="NomAuComplet">
     *   [FR] Le nom complet et aseptisé du chemin d'accès.
     *   [EN] The full, sanitized path name.
     * </param>
     * <param name="Assainisseur">
     *   [FR] Argument factice utilisé pour résoudre cette surcharge.
     *   [EN] Dummy argument used to resolve this overload.
     * </param>
     **/
		public DossierReference(string NomAuComplet, Desinfecter Assainisseur)
			: base(NomAuComplet) {
		}

		/**
     * <summary>
     *   [FR] Récupère le répertoire parent de ce répertoire.
     *   [EN] Gets the parent directory of this directory.
     * </summary>
     * <returns>
     *   [FR] Un nouvel objet DossierReference représentant le répertoire parent, ou null si ce répertoire est une racine.
     *   [EN] A new DossierReference representing the parent directory, or null if this directory is a root.
     * </returns>
     **/
		public DossierReference RepertoireParrent {

			get {

				if(EstLeRepertoireRacine()) {

					return null;
				}

				int LongueurDuParent = NomComplet.LastIndexOf(Rs);
				if(LongueurDuParent == 2 && NomComplet[1] == ':') {

					LongueurDuParent++;
				}

				return new DossierReference(NomComplet.Substring(0, LongueurDuParent), Desinfecter.Aucun);
			}
		}

		/**
     * <summary>
     *   [FR] Obtient le nom du répertoire (dernier segment du chemin).
     *   [EN] Gets the directory name (last segment of the path).
     * </summary>
     * <returns>
     *   [FR] Le nom du répertoire.
     *   [EN] The name of the directory.
     * </returns>
     **/
		public string ObtenirLeNomDuRepertoire() => Path.GetFileName(NomComplet);

		/**
     * <summary>
     *   [FR] Renvoie la partie répertoire d'un chemin de fichier.
     *   [EN] Returns the directory part of a file path.
     * </summary>
     * <param name="Chemin">
     *   [FR] Chemin du fichier.
     *   [EN] File path.
     * </param>
     * <returns>
     *   [FR] Le chemin du répertoire, ou null si le chemin est nul, vide ou une racine.
     *   [EN] The directory path, or null if the path is null, empty, or a root.
     * </returns>
     **/
		public static string? ObtenirLeNomDuRepertoire(string? Chemin) => Path.GetDirectoryName(Chemin);

		/**
     * <summary>
     *   [FR] Détermine si ce chemin représente un répertoire racine dans le système de fichiers.
     *   [EN] Determines whether this path represents a root directory in the file system.
     * </summary>
     * <returns>
     *   [FR] Vrai si ce chemin est un répertoire racine, sinon Faux.
     *   [EN] True if this path is a root directory, otherwise False.
     * </returns>
     **/
		public bool EstLeRepertoireRacine() => NomComplet[^1] == Rs;

		/**
     * <summary>
     *   [FR] Comparaison par rapport à un autre objet pour l'égalité.
     *   [EN] Compares against another object for equality.
     * </summary>
     * <param name="Objet">
     *   [FR] Autre instance à comparer.
     *   [EN] Other instance to compare.
     * </param>
     * <returns>
     *   [FR] Vrai si les noms représentent le même objet, Faux sinon.
     *   [EN] True if the names represent the same object, False otherwise.
     * </returns>
     **/
		public bool Equals(DossierReference Objet) => Objet == this;

		/**
     * <summary>
     *   [FR] Comparaison par rapport à un autre objet pour l'égalité.
     *   [EN] Compares against another object for equality.
     * </summary>
     * <param name="Objet">
     *   [FR] Autre instance à comparer.
     *   [EN] Other instance to compare.
     * </param>
     * <returns>
     *   [FR] Vrai si les noms représentent le même objet, Faux sinon.
     *   [EN] True if the names represent the same object, False otherwise.
     * </returns>
     **/
		public override bool Equals(object Objet) => (Objet is DossierReference) && ((DossierReference)Objet) == this;

		/**
     * <summary>
     *   [FR] Retourne un code de hachage pour cet objet.
     *   [EN] Returns a hash code for this object.
     * </summary>
     * <returns>
     *   [FR] Code de hachage pour cet objet.
     *   [EN] Hash code for this object.
     * </returns>
     **/
		public override int GetHashCode() => Comparateur.GetHashCode(NomComplet);

		/**
     * <summary>
     *   [FR] Récupère le répertoire parent d'un fichier.
     *   [EN] Gets the parent directory for a file.
     * </summary>
     * <param name="Fichier">
     *   [FR] Le fichier dont on souhaite récupérer le répertoire.
     *   [EN] The file to get directory for.
     * </param>
     * <returns>
     *   [FR] Référence du répertoire contenant le fichier donné.
     *   [EN] Directory reference for the given file.
     * </returns>
     **/
		public static DossierReference ObtenirLeRepertoireParrent(FichierReference Fichier) {

			ArgumentNullException.ThrowIfNull(Fichier);

			int LongueurDuParent = Fichier.NomComplet.LastIndexOf(Rs);
			if(LongueurDuParent == 2 && Fichier.NomComplet[1] == ':') {

				LongueurDuParent++;
			}

			return new DossierReference(Fichier.NomComplet.Substring(0, LongueurDuParent), Desinfecter.Aucun);
		}

		/**
     * <summary>
     *   [FR] Récupère le chemin d'un dossier spécial.
     *   [EN] Gets the path for a special folder.
     * </summary>
     * <param name="Dossier">
     *   [FR] Le dossier spécial à récupérer.
     *   [EN] The special folder to get the path for.
     * </param>
     * <returns>
     *   [FR] Référence du répertoire pour le dossier donné, ou null s'il n'est pas disponible.
     *   [EN] Directory reference for the given folder, or null if it is not available.
     * </returns>
     **/
		public static DossierReference? ObtenirLeDossierSpecial(Environment.SpecialFolder Dossier) {

			string CheminsDuDossier = Environment.GetFolderPath(Dossier);
			return string.IsNullOrEmpty(CheminsDuDossier) ? null : new DossierReference(CheminsDuDossier);
		}

		/**
     * <summary>
     *   [FR] Combine plusieurs fragments avec un répertoire de base
     *        pour former un nouveau nom de répertoire.
     *   [EN] Combines several fragments with a base directory
     *        to form a new directory name.
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Le répertoire de base.
     *   [EN] The base directory.
     * </param>
     * <param name="Fragments">
     *   [FR] Fragments à combiner avec le répertoire de base.
     *   [EN] Fragments to combine with the base directory.
     * </param>
     * <returns>
     *   [FR] Nouvelle référence de répertoire.
     *   [EN] New directory reference.
     * </returns>
     **/
		public static DossierReference Combiner(DossierReference RepertoireDeBase, params string[] Fragments) {

			ArgumentNullException.ThrowIfNull(RepertoireDeBase);
			ArgumentNullException.ThrowIfNull(Fragments);

			string NomComplet = FichierSystemeReference.CombinerLesChaines(RepertoireDeBase, Fragments);
			return new DossierReference(NomComplet, Desinfecter.Aucun);
		}

		/**
     * <summary>
     *   [FR] Fonction d'aide à la création d'une référence de répertoire distant.
     *        Contrairement à un objet DossierReference normal, 
     *        ceux-ci ne sont pas convertis en un chemin complet dans le système de fichiers local.
     *   [EN] Helper function to create a remote directory reference.
     *        Unlike normal DossierReference objects,
     *        these aren't converted to a full path in the local filesystem.
     * </summary>
     * <param name="CheminAbsolu">
     *   [FR] Le chemin absolu dans le système de fichiers distant.
     *   [EN] The absolute path in the remote file system.
     * </param>
     * <returns>
     *   [FR] Nouvelle référence de répertoire.
     *   [EN] New directory reference.
     * </returns>
     **/
		public static DossierReference MarquerADistance(string CheminAbsolu) {

			ArgumentException.ThrowIfNullOrEmpty(CheminAbsolu, nameof(CheminAbsolu));
			return new DossierReference(CheminAbsolu, Desinfecter.Aucun);
		}

		/**
     * <summary>
     *   [FR] Trouve le répertoire courant.
     *   [EN] Finds the current directory.
     * </summary>
     * <returns>
     *   [FR] Le répertoire courant.
     *   [EN] The current directory.
     * </returns>
     **/
		public static DossierReference ObtenirLeRepertoireActuel() => new(Directory.GetCurrentDirectory());

		/**
     * <summary>
     *   [FR] Récupère le répertoire parent d'un fichier, ou retourne null si le fichier est null.
     *   [EN] Gets the parent directory for a file, or returns null if the file is null.
     * </summary>
     * <param name="Fichier">
     *   [FR] Le fichier pour créer une référence de répertoire.
     *   [EN] The file to create a directory reference for.
     * </param>
     * <returns>
     *   [FR] Le répertoire contenant le fichier, ou null.
     *   [EN] The directory containing the file, or null.
     * </returns>
     **/
		public static DossierReference? DepuisLeFichier(FichierReference Fichier) =>
			(Fichier == null) ? null : Fichier.Repertoire;

		/**
     * <summary>
     *   [FR] Énumère les fichiers d'un répertoire donné.
     *   [EN] Enumerates files from a given directory.
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base dans lequel chercher.
     *   [EN] Base directory to search in.
     * </param>
     * <returns>
     *   [FR] Séquence des références de fichiers.
     *   [EN] Sequence of file references.
     * </returns>
     **/
		public static IEnumerable<FichierReference> FichiersEnumerer(DossierReference RepertoireDeBase) {

			ArgumentNullException.ThrowIfNull(RepertoireDeBase);

			foreach(string NomDeFichier in Directory.EnumerateFiles(RepertoireDeBase.NomComplet)) {

				yield return new FichierReference(NomDeFichier, Aseptise.Aucun);
			}
		}

		/**
     * <summary>
     *   [FR] Énumère les fichiers d'un répertoire donné correspondant à un modèle.
     *   [EN] Enumerates files from a given directory matching a pattern.
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base dans lequel chercher.
     *   [EN] Base directory to search in.
     * </param>
     * <param name="Modele">
     *   [FR] Modèle pour les fichiers correspondants.
     *   [EN] Pattern for matching files.
     * </param>
     * <returns>
     *   [FR] Séquence des références de fichiers.
     *   [EN] Sequence of file references.
     * </returns>
     **/
		public static IEnumerable<FichierReference> FichiersEnumerer(DossierReference RepertoireDeBase, string Modele) {

			ArgumentNullException.ThrowIfNull(RepertoireDeBase);
			ArgumentException.ThrowIfNullOrEmpty(Modele, nameof(Modele));

			foreach(string NomDeFichier in Directory.EnumerateFiles(RepertoireDeBase.NomComplet, Modele)) {

				yield return new FichierReference(NomDeFichier, Aseptise.Aucun);
			}
		}

		/**
     * <summary>
     *   [FR] Énumère les fichiers d'un répertoire donné correspondant à un modèle, avec une option de recherche.
     *   [EN] Enumerates files from a given directory matching a pattern, with a search option.
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base dans lequel chercher.
     *   [EN] Base directory to search in.
     * </param>
     * <param name="Modele">
     *   [FR] Modèle pour les fichiers correspondants.
     *   [EN] Pattern for matching files.
     * </param>
     * <param name="Option">
     *   [FR] Option pour la recherche.
     *   [EN] Option for the search.
     * </param>
     * <returns>
     *   [FR] Séquence des références de fichiers.
     *   [EN] Sequence of file references.
     * </returns>
     **/
		public static IEnumerable<FichierReference> FichiersEnumerer(DossierReference RepertoireDeBase, string Modele, SearchOption Option) {

			ArgumentNullException.ThrowIfNull(RepertoireDeBase);
			ArgumentException.ThrowIfNullOrEmpty(Modele, nameof(Modele));

			foreach(string NomDeFichier in Directory.EnumerateFiles(RepertoireDeBase.NomComplet, Modele, Option)) {

				yield return new FichierReference(NomDeFichier, Aseptise.Aucun);
			}
		}

		/**
     * <summary>
     *   [FR] Énumère les sous-répertoires dans un répertoire donné.
     *   [EN] Enumerates subdirectories in a given directory.
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base dans lequel chercher.
     *   [EN] Base directory to search in.
     * </param>
     * <returns>
     *   [FR] Séquence des références de répertoires.
     *   [EN] Sequence of directory references.
     * </returns>
     **/
		public static IEnumerable<DossierReference> RepertoireEnumerer(DossierReference RepertoireDeBase) {

			ArgumentNullException.ThrowIfNull(RepertoireDeBase);

			foreach(string NomDeRepertoire in Directory.EnumerateDirectories(RepertoireDeBase.NomComplet)) {

				yield return new DossierReference(NomDeRepertoire, Desinfecter.Aucun);
			}
		}

		/**
     * <summary>
     *   [FR] Énumère les sous-répertoires dans un répertoire donné correspondant à un modèle.
     *   [EN] Enumerates subdirectories in a given directory matching a pattern.
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base dans lequel chercher.
     *   [EN] Base directory to search in.
     * </param>
     * <param name="Modele">
     *   [FR] Modèle pour les répertoires correspondants.
     *   [EN] Pattern for matching directories.
     * </param>
     * <returns>
     *   [FR] Séquence des références de répertoires.
     *   [EN] Sequence of directory references.
     * </returns>
     **/
		public static IEnumerable<DossierReference> RepertoireEnumerer(DossierReference RepertoireDeBase, string Modele) {

			ArgumentNullException.ThrowIfNull(RepertoireDeBase);
			ArgumentException.ThrowIfNullOrEmpty(Modele, nameof(Modele));

			foreach(string NomDeRepertoire in Directory.EnumerateDirectories(RepertoireDeBase.NomComplet, Modele)) {

				yield return new DossierReference(NomDeRepertoire, Desinfecter.Aucun);
			}
		}

		/**
     * <summary>
     *   [FR] Énumère les sous-répertoires dans un répertoire donné correspondant à un modèle, avec une option de recherche.
     *   [EN] Enumerates subdirectories in a given directory matching a pattern, with a search option.
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base dans lequel chercher.
     *   [EN] Base directory to search in.
     * </param>
     * <param name="Modele">
     *   [FR] Modèle pour les répertoires correspondants.
     *   [EN] Pattern for matching directories.
     * </param>
     * <param name="Option">
     *   [FR] Option pour la recherche.
     *   [EN] Option for the search.
     * </param>
     * <returns>
     *   [FR] Séquence des références de répertoires.
     *   [EN] Sequence of directory references.
     * </returns>
     **/
		public static IEnumerable<DossierReference> RepertoireEnumerer(DossierReference RepertoireDeBase, string Modele, SearchOption Option) {

			ArgumentNullException.ThrowIfNull(RepertoireDeBase);
			ArgumentException.ThrowIfNullOrEmpty(Modele, nameof(Modele));

			foreach(string NomDeRepertoire in Directory.EnumerateDirectories(RepertoireDeBase.NomComplet, Modele, Option)) {

				yield return new DossierReference(NomDeRepertoire, Desinfecter.Aucun);
			}
		}

		/**
     * <summary>
     *   [FR] Crée un répertoire.
     *   [EN] Creates a directory.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du répertoire.
     *   [EN] Location of the directory.
     * </param>
     **/
		public static void Creer(DossierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			Directory.CreateDirectory(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Supprime un répertoire.
     *   [EN] Deletes a directory.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du répertoire.
     *   [EN] Location of the directory.
     * </param>
     **/
		public static void Supprimer(DossierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			Directory.Delete(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Supprime un répertoire.
     *   [EN] Deletes a directory.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du répertoire.
     *   [EN] Location of the directory.
     * </param>
     * <param name="Recursive">
     *   [FR] Indique s'il faut supprimer récursivement les sous-répertoires.
     *   [EN] Indicates whether to remove directories recursively.
     * </param>
     **/
		public static void Supprimer(DossierReference Localisation, bool Recursive) {

			ArgumentNullException.ThrowIfNull(Localisation);
			Directory.Delete(Localisation.NomComplet, Recursive);
		}

		/**
     * <summary>
     *   [FR] Vérifie si le répertoire existe.
     *   [EN] Checks whether the directory exists.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du répertoire.
     *   [EN] Location of the directory.
     * </param>
     * <returns>
     *   [FR] Vrai si ce répertoire existe.
     *   [EN] True if this directory exists.
     * </returns>
     **/
		public static bool VerifieSiExiste(DossierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return Directory.Exists(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Définit le répertoire de travail actuel.
     *   [EN] Sets the current directory.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du nouveau répertoire actuel.
     *   [EN] Location of the new current directory.
     * </param>
     **/
		public static void DefinirLeRepertoireActuel(DossierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			Directory.SetCurrentDirectory(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Compare deux noms d'objets du système de fichiers pour l'égalité. 
     *        Utilise la représentation du nom canonique,
     *        pas la représentation du nom d'affichage.
     *   [EN] Compares two filesystem object names for equality. 
     *        Uses the canonical name representation,
     *        not the display name representation.
     * </summary>
     * <param name="ObjetA">
     *   [FR] Premier objet à comparer.
     *   [EN] First object to compare.
     * </param>
     * <param name="ObjetB">
     *   [FR] Deuxième objet à comparer.
     *   [EN] Second object to compare.
     * </param>
     * <returns>
     *   [FR] Vrai si les noms représentent le même objet, Faux sinon.
     *   [EN] True if the names represent the same object, False otherwise.
     * </returns>
     **/
		public static bool operator ==(DossierReference ObjetA, DossierReference ObjetB) {
#pragma warning disable IDE0041 // Utiliser la vérification 'is null'
			if((object)ObjetA == null) {

				return (object)ObjetB == null;
			}
			else {

				return (object)ObjetB != null && ObjetA.NomComplet.Equals(ObjetB.NomComplet, Comparaison);
			}
#pragma warning restore IDE0041 // Utiliser la vérification 'is null'
		}

		/**
     * <summary>
     *   [FR] Compare deux noms d'objets du système de fichiers pour l'inégalité. 
     *   [EN] Compares two filesystem object names for inequality.
     * </summary>
     * <param name="ObjetA">
     *   [FR] Premier objet à comparer.
     *   [EN] First object to compare.
     * </param>
     * <param name="ObjetB">
     *   [FR] Deuxième objet à comparer.
     *   [EN] Second object to compare.
     * </param>
     * <returns>
     *   [FR] Faux si les noms représentent le même objet, Vrai sinon.
     *   [EN] False if the names represent the same object, True otherwise.
     * </returns>
     **/
		public static bool operator !=(DossierReference ObjetA, DossierReference ObjetB) => !(ObjetA == ObjetB);

		/**
     * <summary>
     *   [FR] S'assure que le séparateur de chemin final correct est appliqué.
     *        Sous Windows, le répertoire racine (par exemple C:\) a toujours un séparateur de chemin de fin,
     *        mais aucun autre chemin ne le fait.
     *   [EN] Ensures that the correct trailing path separator is applied.
     *        Under Windows, the root directory (e.g. C:\) always has a trailing separator,
     *        but no other path does.
     * </summary>
     * <param name="NomDuChemin">
     *   [FR] Chemin absolu du répertoire.
     *   [EN] Absolute path to the directory.
     * </param>
     * <returns>
     *   [FR] Chemin du répertoire, avec le séparateur de chemin correct.
     *   [EN] Path to the directory, with the correct path separator.
     * </returns>
     **/
		private static string SeparateurDeVoieDeFuite(string NomDuChemin) {

			ArgumentException.ThrowIfNullOrEmpty(NomDuChemin, nameof(NomDuChemin));

			if(NomDuChemin.Length == 2 && NomDuChemin[1] == ':') {

				return NomDuChemin + Rs;
			}
			else if(NomDuChemin.Length == 3 && NomDuChemin[1] == ':' && NomDuChemin[2] == Rs) {

				return NomDuChemin;
			}
#if NET8_0_OR_GREATER
			else if(NomDuChemin.Length > 1 && NomDuChemin[^1] == Rs) {
#else
      else if (NomDuChemin.Length > 1 && NomDuChemin[NomDuChemin.Length - 1] == Rs) {
#endif

				return NomDuChemin.TrimEnd(Rs);
			}
			else {

				return NomDuChemin;
			}
		}

		/// <summary>
		///   [FR] Helper interne pour valider le chemin source et retourner un chemin complet.
		///   [EN] Internal helper to validate the source path and return a full path.
		/// </summary>
		private static string ObtenirCheminCompletValide(string Chemins) {

			ArgumentException.ThrowIfNullOrEmpty(Chemins, nameof(Chemins));
			return Path.GetFullPath(Chemins);
		}

		/// <summary>
		///   [FR] Helper interne pour obtenir le nom complet depuis un <see cref="DirectoryInfo"/> avec validation.
		///   [EN] Internal helper to get the full name from a <see cref="DirectoryInfo"/> with validation.
		/// </summary>
		private static string ObtenirNomCompletDepuisDirectoryInfo(DirectoryInfo InfoRepertoire) {

			ArgumentNullException.ThrowIfNull(InfoRepertoire);
			return InfoRepertoire.FullName;
		}
	}

	/**
   * <summary>
   *   [FR] Méthodes d'extension pour passer des arguments DossierReference.
   *   [EN] Extension methods for passing DossierReference arguments.
   * </summary>
   **/
	public static class DossierReferenceMethodeDextension {

		/**
     * <summary>
     *   [FR] Désérialisation manuelle d'une référence de répertoire à partir d'un flux binaire.
     *   [EN] Manually deserialize a directory reference from a binary stream.
     * </summary>
     * <param name="Lecteur">
     *   [FR] Lecteur binaire à partir duquel lire.
     *   [EN] Binary reader to read from.
     * </param>
     * <returns>
     *   [FR] Nouvel objet DossierReference, ou null.
     *   [EN] New DossierReference object, or null.
     * </returns>
     **/
		public static DossierReference? ConsulterLeDossierReference(this BinaryReader Lecteur) {

			ArgumentNullException.ThrowIfNull(Lecteur);

			string NomComplet = Lecteur.ReadString();
			return (NomComplet.Length == 0) ? null : new DossierReference(NomComplet, Desinfecter.Aucun);
		}

		/**
     * <summary>
     *   [FR] Sérialise manuellement une référence de répertoire vers un flux binaire.
     *   [EN] Manually serialize a directory reference to a binary stream.
     * </summary>
     * <param name="Redacteur">
     *   [FR] Rédacteur binaire sur lequel écrire.
     *   [EN] Binary writer to write to.
     * </param>
     * <param name="Repertoire">
     *   [FR] La référence du répertoire à écrire.
     *   [EN] The directory reference to write.
     * </param>
     **/
		public static void Ecrire(this BinaryWriter Redacteur, DossierReference Repertoire) {

			ArgumentNullException.ThrowIfNull(Redacteur);
			Redacteur.Write((Repertoire == null) ? string.Empty : Repertoire.NomComplet);
		}
	}
}
