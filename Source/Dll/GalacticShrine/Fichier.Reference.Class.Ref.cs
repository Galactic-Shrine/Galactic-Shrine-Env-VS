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
using System.Text;
using System.Threading.Tasks;

using GalacticShrine.Enumeration;
using GalacticShrine.Properties;

namespace GalacticShrine {

	/**
   * <summary>
   *   [FR] Représentation d'un chemin de fichier absolu. 
   *        Permet des hachages et des comparaisons rapides. 
   *   [EN] Representation of an absolute file path. 
   *        Allows fast hashing and comparisons. 
   * </summary>
   **/
	[Serializable]
	public class FichierReference : FichierSystemeReference, IEquatable<FichierReference> {

		/**
     * <summary>
     *   [FR] Constructeur par défaut.
     *   [EN] Default constructor.
     * </summary>
     * <param name="Chemins">
     *   [FR] Chemin d'accès à ce fichier.
     *   [EN] Path to this file.
     * </param>
     **/
		public FichierReference(string Chemins)
			: base(ObtenirCheminCompletValide(Chemins)) {

			if(NomComplet[^1] == '\\' || NomComplet[^1] == '/') {
				throw new ArgumentException(Resources.FichierTermineInvalide, nameof(Chemins));
			}
		}

		/**
     * <summary>
     *   [FR] Construire une FichierReference à partir d'un objet FileInfo.
     *   [EN] Construct a FichierReference from a FileInfo object.
     * </summary>
     * <param name="InInfo">
     *   [FR] Chemin d'accès à ce fichier.
     *   [EN] Path to this file.
     * </param>
     **/
		public FichierReference(FileInfo InInfo)
			: base(ObtenirNomCompletDepuisFileInfo(InInfo)) {
		}

		/**
     * <summary>
     *   [FR] Constructeur par défaut.
     *   [EN] Default constructor.
     * </summary>
     * <param name="NomAuComplet">
     *   [FR] Le chemin aseptisé complet.
     *   [EN] The full sanitized path.
     * </param>
     * <param name="Assainisseur">
     *   [FR] Argument factice pour utiliser la surcharge aseptisée.
     *   [EN] Dummy argument to use the sanitized overload.
     * </param>
     **/
		public FichierReference(string NomAuComplet, Aseptise Assainisseur)
			: base(NomAuComplet) {
		}

		/**
     * <summary>
     *   [FR] Change l'extension du fichier.
     *   [EN] Changes the file's extension.
     * </summary>
     * <param name="Extension">
     *   [FR] La nouvelle extension.
     *   [EN] The new extension.
     * </param>
     * <returns>
     *   [FR] Un FichierReference avec le même chemin et le même nom, mais avec la nouvelle extension.
     *   [EN] A FichierReference with the same path and name, but with the new extension.
     * </returns>
     **/
		public FichierReference ModifierLextension(string Extension) {

			ArgumentNullException.ThrowIfNull(Extension);

			string NouveauNomComplet = Path.ChangeExtension(NomComplet, Extension);
			return new FichierReference(NouveauNomComplet, Aseptise.Aucun);
		}

		/**
     * <summary>
     *   [FR] Récupère le répertoire contenant ce fichier.
     *   [EN] Retrieves the directory containing this file.
     * </summary>
     * <returns>
     *   [FR] Un nouvel objet répertoire représentant le répertoire contenant cet objet.
     *   [EN] A new directory object representing the directory containing this object.
     * </returns>
     **/
		public DossierReference Repertoire => DossierReference.ObtenirLeRepertoireParrent(this);

		/**
     * <summary>
     *   [FR] Construire un objet FileInfo à partir de cette référence.
     *   [EN] Construct a FileInfo object from this reference.
     * </summary>
     * <returns>
     *   [FR] Un nouvel objet FileInfo.
     *   [EN] A new FileInfo object.
     * </returns>
     **/
		public FileInfo InformationsDuFichier() => new(NomComplet);

		/**
     * <summary>
     *   [FR] Récupère le nom du fichier sans les informations de chemin d'accès.
     *   [EN] Gets the file name without path information.
     * </summary>
     * <returns>
     *   [FR] Une chaîne contenant le nom du fichier.
     *   [EN] A string containing the file name.
     * </returns>
     **/
		public string ObtenirLeNomDeFichier() => Path.GetFileName(NomComplet);

		/**
     * <summary>
     *   [FR] Récupère le nom du fichier sans les informations de chemin d'accès ni l'extension.
     *   [EN] Gets the file name without path information or extension.
     * </summary>
     * <returns>
     *   [FR] Une chaîne contenant le nom du fichier sans extension.
     *   [EN] A string containing the file name without an extension.
     * </returns>
     **/
		public string ObtenirLeNomDeFichierSansExtension() => Path.GetFileNameWithoutExtension(NomComplet);

		/**
     * <summary>
     *   [FR] Obtient le nom du fichier sans chemin d'accès ni aucune extension.
     *   [EN] Gets the file name without path and without any extension.
     * </summary>
     * <returns>
     *   [FR] Une chaîne contenant le nom du fichier sans extension.
     *   [EN] A string containing the file name without an extension.
     * </returns>
     **/
		public string ObtenirLeNomDeFichierSansAucuneExtension() {

			int IndexDeDebut = NomComplet.LastIndexOf(DossierReference.Rs) + 1;
			int IndexDeFin = NomComplet.LastIndexOf('.', IndexDeDebut);

			if(IndexDeFin < IndexDeDebut) {
				return NomComplet.Substring(IndexDeDebut);
			}
			else {
				return NomComplet.Substring(IndexDeDebut, IndexDeFin - IndexDeDebut);
			}
		}

		/**
     * <summary>
     *   [FR] Obtient l'extension pour ce nom de fichier.
     *   [EN] Gets the extension for this file name.
     * </summary>
     * <returns>
     *   [FR] Une chaîne contenant l'extension de ce nom de fichier.
     *   [EN] A string containing the extension of this file name.
     * </returns>
     **/
		public string ObtenirLextension() => Path.GetExtension(NomComplet);

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
		public bool Equals(FichierReference Objet) => Objet == this;

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
		public override bool Equals(object Objet) => (Objet is FichierReference) && ((FichierReference)Objet) == this;

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
     *   [FR] Combine plusieurs fragments avec un répertoire de base,
     *        pour former un nouveau nom de fichier.
     *   [EN] Combines several fragments with a base directory
     *        to form a new file name.
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
     *   [FR] Le nouveau nom du fichier.
     *   [EN] The new file name.
     * </returns>
     **/
		public static FichierReference Combiner(DossierReference RepertoireDeBase, params string[] Fragments) {

			ArgumentNullException.ThrowIfNull(RepertoireDeBase);
			ArgumentNullException.ThrowIfNull(Fragments);

			string NomComplet = FichierSystemeReference.CombinerLesChaines(RepertoireDeBase, Fragments);
			return new FichierReference(NomComplet, Aseptise.Aucun);
		}

		/**
     * <summary>
     *   [FR] Ajoute une chaîne à la fin d'un nom de fichier.
     *   [EN] Appends a string to the end of a file name.
     * </summary>
     * <param name="FichierA">
     *   [FR] La référence du fichier de base.
     *   [EN] The base file reference.
     * </param>
     * <param name="FichierB">
     *   [FR] Suffixe à joindre.
     *   [EN] Suffix to append.
     * </param>
     **/
		public static FichierReference operator +(FichierReference FichierA, string FichierB) {

			ArgumentNullException.ThrowIfNull(FichierA);
			ArgumentNullException.ThrowIfNull(FichierB);

			return new FichierReference(FichierA.NomComplet + FichierB, Aseptise.Aucun);
		}

		/**
    * <summary>
    *   [FR] Fonction d'aide à la création d'une référence de fichier distant.
    *        Contrairement à un objet FichierReference normal, 
    *        ceux-ci ne sont pas convertis en un chemin complet dans le système de fichiers local.
    *   [EN] Helper function to create a remote file reference.
    *        Unlike normal FichierReference objects,
    *        these aren't converted to a full path in the local filesystem.
    * </summary>
    * <param name="CheminAbsolu">
    *   [FR] Le chemin absolu dans le système de fichiers distant.
    *   [EN] The absolute path in the remote file system.
    * </param>
    * <returns>
    *   [FR] Nouvelle référence de fichier.
    *   [EN] New file reference.
    * </returns>
    **/
		public static FichierReference MarquerADistance(string CheminAbsolu) {

			ArgumentException.ThrowIfNullOrEmpty(CheminAbsolu, nameof(CheminAbsolu));
			return new FichierReference(CheminAbsolu, Aseptise.Aucun);
		}

		/**
     * <summary>
     *   [FR] Récupère les attributs d'un fichier.
     *   [EN] Gets the attributes for a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Attributs du fichier.
     *   [EN] Attributes of the file.
     * </returns>
     **/
		public static FileAttributes ObtenirDesAttributs(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return File.GetAttributes(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Récupère les attributs d'un fichier de manière asynchrone.
     *   [EN] Gets the attributes for a file asynchronously.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Tâche retournant les attributs du fichier.
     *   [EN] Task returning the attributes of the file.
     * </returns>
     **/
		public static Task<FileAttributes> ObtenirDesAttributsAsync(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return Task.Run(() => File.GetAttributes(Localisation.NomComplet));
		}

		/**
     * <summary>
     *   [FR] Récupère l'heure à laquelle le fichier a été écrit pour la dernière fois.
     *   [EN] Gets the time that the file was last written.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Dernière heure d'écriture, en heure locale.
     *   [EN] Last write time, in local time.
     * </returns>
     **/
		public static DateTime ObtenirLaDerniereHeureDecriture(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return File.GetLastWriteTime(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Récupère l'heure à laquelle le fichier a été écrit pour la dernière fois (UTC).
     *   [EN] Gets the time that the file was last written (UTC).
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Dernière heure d'écriture, en heure UTC.
     *   [EN] Last write time, in UTC time.
     * </returns>
     **/
		public static DateTime ObtenirLaDerniereHeureDecritureUtc(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return File.GetLastWriteTimeUtc(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Crée un fichier texte.
     *   [EN] Creates a text file.
     * </summary>
     * <param name="Nom">
     *   [FR] Nom du nouveau fichier.
     *   [EN] Name of the new file.
     * </param>
     **/
		public static StreamWriter Creer(string Nom) {

			ArgumentException.ThrowIfNullOrEmpty(Nom, nameof(Nom));
			return File.CreateText(Nom);
		}

		/**
     * <summary>
     *   [FR] Ouvre un flux de fichier sur le chemin spécifié avec accès en lecture/écriture.
     *   [EN] Opens a FileStream on the specified path with read/write access.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Mode">
     *   [FR] Mode à utiliser lors de l'ouverture du fichier.
     *   [EN] Mode to use when opening the file.
     * </param>
     * <returns>
     *   [FR] Nouveau FileStream pour le fichier donné.
     *   [EN] New FileStream for the given file.
     * </returns>
     **/
		public static FileStream Ouvrir(FichierReference Localisation, FileMode Mode) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return File.Open(Localisation.NomComplet, Mode);
		}

		/**
     * <summary>
     *   [FR] Ouvre un flux de fichier sur le chemin spécifié avec accès en lecture/écriture.
     *   [EN] Opens a FileStream on the specified path with read/write access.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Mode">
     *   [FR] Mode à utiliser lors de l'ouverture du fichier.
     *   [EN] Mode to use when opening the file.
     * </param>
     * <param name="Acces">
     *   [FR] Mode d'accès pour le nouveau fichier.
     *   [EN] Access mode for the new file.
     * </param>
     * <returns>
     *   [FR] Nouveau FileStream pour le fichier donné.
     *   [EN] New FileStream for the given file.
     * </returns>
     **/
		public static FileStream Ouvrir(FichierReference Localisation, FileMode Mode, FileAccess Acces) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return File.Open(Localisation.NomComplet, Mode, Acces);
		}

		/**
     * <summary>
     *   [FR] Ouvre un flux de fichier sur le chemin spécifié avec accès en lecture/écriture.
     *   [EN] Opens a FileStream on the specified path with read/write access.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Mode">
     *   [FR] Mode à utiliser lors de l'ouverture du fichier.
     *   [EN] Mode to use when opening the file.
     * </param>
     * <param name="Acces">
     *   [FR] Mode d'accès pour le nouveau fichier.
     *   [EN] Access mode for the new file.
     * </param>
     * <param name="Partager">
     *   [FR] Mode de partage pour le fichier ouvert.
     *   [EN] Sharing mode for the open file.
     * </param>
     * <returns>
     *   [FR] Nouveau FileStream pour le fichier donné.
     *   [EN] New FileStream for the given file.
     * </returns>
     **/
		public static FileStream Ouvrir(FichierReference Localisation, FileMode Mode, FileAccess Acces, FileShare Partager) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return File.Open(Localisation.NomComplet, Mode, Acces, Partager);
		}

		/**
     * <summary>
     *   [FR] Lit le contenu d'un fichier et retourne tous ses octets.
     *   [EN] Reads the contents of a file and returns all its bytes.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Tableau d'octets contenant le contenu du fichier.
     *   [EN] Byte array containing the content of the file.
     * </returns>
     **/
		public static byte[] LireTousLesOctets(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return File.ReadAllBytes(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Lit le contenu d'un fichier de manière asynchrone et retourne tous ses octets.
     *   [EN] Asynchronously reads the content of a file and returns all its bytes.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Tâche retournant un tableau de bytes contenant le contenu du fichier.
     *   [EN] Task returning a byte array containing the content of the file.
     * </returns>
     **/
		public async Task<byte[]> LireTousLesOctetsAsync(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return await File.ReadAllBytesAsync(Localisation.NomComplet).ConfigureAwait(false);
		}

		/**
     * <summary>
     *   [FR] Lit le contenu d'un fichier et retourne le texte complet.
     *   [EN] Reads the contents of a file and returns the complete text.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Contenu du fichier sous la forme d'une seule chaîne de caractères.
     *   [EN] Contents of the file as a single string.
     * </returns>
     **/
		public static string LireToutLeTexte(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return File.ReadAllText(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Lit le contenu d'un fichier de manière asynchrone et retourne le texte complet du fichier.
     *   [EN] Asynchronously reads the content of a file and returns the complete text of the file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Tâche retournant une chaîne contenant le texte complet du fichier.
     *   [EN] Task returning a string containing the full text of the file.
     * </returns>
     **/
		public async Task<string> LireToutLeTexteAsync(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return await File.ReadAllTextAsync(Localisation.NomComplet).ConfigureAwait(false);
		}

		/**
     * <summary>
     *   [FR] Lit le contenu d'un fichier avec l'encodage spécifié et retourne le texte complet.
     *   [EN] Reads the contents of a file with the specified encoding and returns the complete text.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Encodage">
     *   [FR] Encodage du fichier.
     *   [EN] Encoding of the file.
     * </param>
     * <returns>
     *   [FR] Contenu du fichier sous la forme d'une seule chaîne de caractères.
     *   [EN] Contents of the file as a single string.
     * </returns>
     **/
		public static string LireToutLeTexte(FichierReference Localisation, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Encodage);
			return File.ReadAllText(Localisation.NomComplet, Encodage);
		}

		/**
     * <summary>
     *   [FR] Lit le contenu d'un fichier de manière asynchrone et retourne le texte complet du fichier en utilisant un encodage spécifié.
     *   [EN] Asynchronously reads the content of a file and returns the complete text of the file using the specified encoding.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de la lecture du fichier.
     *   [EN] The encoding to use when reading the file.
     * </param>
     * <returns>
     *   [FR] Tâche retournant une chaîne contenant le texte complet du fichier, décodé avec l'encodage spécifié.
     *   [EN] Task returning a string containing the full text of the file, decoded using the specified encoding.
     * </returns>
     **/
		public async Task<string> LireToutLeTexteAsync(FichierReference Localisation, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Encodage);
			return await File.ReadAllTextAsync(Localisation.NomComplet, Encodage).ConfigureAwait(false);
		}

		/**
     * <summary>
     *   [FR] Lit le contenu d'un fichier et retourne toutes les lignes.
     *   [EN] Reads the contents of a file and returns all lines.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Tableau de chaînes de caractères contenant le contenu du fichier.
     *   [EN] String array containing the contents of the file.
     * </returns>
     **/
		public static string[] LireToutLesLignes(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return File.ReadAllLines(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Lit le contenu d'un fichier de manière asynchrone et retourne toutes les lignes du fichier.
     *   [EN] Asynchronously reads the content of a file and returns all lines of the file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Tâche retournant un tableau de chaînes contenant toutes les lignes du fichier.
     *   [EN] Task returning an array of strings containing all lines of the file.
     * </returns>
     **/
		public async Task<string[]> LireToutLesLignesAsync(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return await File.ReadAllLinesAsync(Localisation.NomComplet).ConfigureAwait(false);
		}

		/**
     * <summary>
     *   [FR] Lit le contenu d'un fichier avec encodage et retourne toutes les lignes.
     *   [EN] Reads the contents of a file with encoding and returns all lines.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Encodage">
     *   [FR] Encodage du fichier.
     *   [EN] Encoding of the file.
     * </param>
     * <returns>
     *   [FR] Tableau de chaînes de caractères contenant le contenu du fichier.
     *   [EN] String array containing the contents of the file.
     * </returns>
     **/
		public static string[] LireToutLesLignes(FichierReference Localisation, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Encodage);
			return File.ReadAllLines(Localisation.NomComplet, Encodage);
		}

		/**
     * <summary>
     *   [FR] Lit le contenu d'un fichier de manière asynchrone et retourne toutes les lignes du fichier en utilisant un encodage spécifié.
     *   [EN] Asynchronously reads the content of a file and returns all lines of the file using the specified encoding.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de la lecture du fichier.
     *   [EN] The encoding to use when reading the file.
     * </param>
     * <returns>
     *   [FR] Tâche retournant un tableau de chaînes contenant toutes les lignes du fichier, décodé avec l'encodage spécifié.
     *   [EN] Task returning an array of strings containing all lines of the file, decoded using the specified encoding.
     * </returns>
     **/
		public async Task<string[]> LireToutLesLignesAsync(FichierReference Localisation, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Encodage);
			return await File.ReadAllLinesAsync(Localisation.NomComplet, Encodage).ConfigureAwait(false);
		}

		/**
     * <summary>
     *   [FR] Rend un emplacement de fichier inscriptible.
     *   [EN] Makes a file location writeable.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     **/
		public static void RendreInscriptibles(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);

			if(VerifieSiExiste(Localisation)) {

				FileAttributes Attributs = ObtenirDesAttributs(Localisation);
				if((Attributs & FileAttributes.ReadOnly) != 0) {

					// Supprimer uniquement le flag ReadOnly et conserver les autres attributs.
					DefinirDesAttributs(Localisation, Attributs & ~FileAttributes.ReadOnly);
				}
			}
		}

		/**
     * <summary>
     *   [FR] Rend un fichier inscriptible de manière asynchrone en vérifiant ses attributs.
     *        Si le fichier est en lecture seule, l'attribut "lecture seule" est supprimé.
     *   [EN] Makes a file writable asynchronously by checking its attributes.
     *        If the file is read-only, the "read-only" attribute is removed.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier à rendre inscriptible.
     *   [EN] Location of the file to make writable.
     * </param>
     **/
		public static async Task RendreInscriptiblesAsync(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);

			if(VerifieSiExiste(Localisation)) {

				FileAttributes Attributs = await ObtenirDesAttributsAsync(Localisation).ConfigureAwait(false);
				if((Attributs & FileAttributes.ReadOnly) != 0) {

					// Supprimer uniquement le flag ReadOnly et conserver les autres attributs.
					await DefinirDesAttributsAsync(Localisation, Attributs & ~FileAttributes.ReadOnly).ConfigureAwait(false);
				}
			}
		}

		/**
     * <summary>
     *   [FR] Copie un fichier d'un emplacement à un autre.
     *   [EN] Copies a file from one location to another.
     * </summary>
     * <param name="LocalisationDeDepart">
     *   [FR] Emplacement du fichier source.
     *   [EN] Location of the source file.
     * </param>
     * <param name="LocalisationDeDestination">
     *   [FR] Emplacement du fichier cible.
     *   [EN] Location of the target file.
     * </param>
     **/
		public static void Copier(FichierReference LocalisationDeDepart, FichierReference LocalisationDeDestination) {

			ArgumentNullException.ThrowIfNull(LocalisationDeDepart);
			ArgumentNullException.ThrowIfNull(LocalisationDeDestination);

			File.Copy(LocalisationDeDepart.NomComplet, LocalisationDeDestination.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Copie un fichier d'un emplacement à un autre.
     *   [EN] Copies a file from one location to another.
     * </summary>
     * <param name="LocalisationDeDepart">
     *   [FR] Emplacement du fichier source.
     *   [EN] Location of the source file.
     * </param>
     * <param name="LocalisationDeDestination">
     *   [FR] Emplacement du fichier cible.
     *   [EN] Location of the target file.
     * </param>
     * <param name="Ecraser">
     *   [FR] Indique s'il faut écraser le fichier dans l'emplacement cible.
     *   [EN] Indicates whether to overwrite the file in the target location.
     * </param>
     **/
		public static void Copier(FichierReference LocalisationDeDepart, FichierReference LocalisationDeDestination, bool Ecraser) {

			ArgumentNullException.ThrowIfNull(LocalisationDeDepart);
			ArgumentNullException.ThrowIfNull(LocalisationDeDestination);

			File.Copy(LocalisationDeDepart.NomComplet, LocalisationDeDestination.NomComplet, Ecraser);
		}

		/**
     * <summary>
     *   [FR] Déplace un fichier d'un emplacement à un autre.
     *   [EN] Moves a file from one location to another.
     * </summary>
     * <param name="LocalisationDeDepart">
     *   [FR] Emplacement du fichier source.
     *   [EN] Location of the source file.
     * </param>
     * <param name="LocalisationDeDestination">
     *   [FR] Emplacement du fichier cible.
     *   [EN] Location of the target file.
     * </param>
     **/
		public static void Deplacer(FichierReference LocalisationDeDepart, FichierReference LocalisationDeDestination) {

			ArgumentNullException.ThrowIfNull(LocalisationDeDepart);
			ArgumentNullException.ThrowIfNull(LocalisationDeDestination);

			File.Move(LocalisationDeDepart.NomComplet, LocalisationDeDestination.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Supprime ce fichier.
     *   [EN] Deletes this file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     **/
		public static void Supprimer(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			File.Delete(Localisation.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Définit les attributs d'un fichier.
     *   [EN] Sets the attributes for a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Attributs">
     *   [FR] Nouveaux attributs du fichier.
     *   [EN] New attributes of the file.
     * </param>
     **/
		public static void DefinirDesAttributs(FichierReference Localisation, FileAttributes Attributs) {

			ArgumentNullException.ThrowIfNull(Localisation);
			File.SetAttributes(Localisation.NomComplet, Attributs);
		}

		/**
     * <summary>
     *   [FR] Définit les attributs d'un fichier de manière asynchrone.
     *        Cette méthode applique les attributs spécifiés au fichier indiqué.
     *   [EN] Sets the attributes of a file asynchronously.
     *        This method applies the specified attributes to the given file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier auquel les attributs doivent être appliqués.
     *   [EN] Location of the file to which the attributes should be applied.
     * </param>
     * <param name="Attributs">
     *   [FR] Les attributs à définir pour le fichier.
     *   [EN] The attributes to set for the file.
     * </param>
     * <returns>
     *   [FR] Tâche représentant l'opération asynchrone de définition des attributs.
     *   [EN] Task representing the asynchronous operation of setting the attributes.
     * </returns>
     **/
		public static Task DefinirDesAttributsAsync(FichierReference Localisation, FileAttributes Attributs) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return Task.Run(() => File.SetAttributes(Localisation.NomComplet, Attributs));
		}

		/**
     * <summary>
     *   [FR] Définit l'heure à laquelle le fichier a été écrit pour la dernière fois.
     *   [EN] Sets the time that the file was last written.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="DerniereHeureDecriture">
     *   [FR] Dernière heure d'écriture, en heure locale.
     *   [EN] Last write time, in local time.
     * </param>
     **/
		public static void DefinirLaDerniereHeureDecriture(FichierReference Localisation, DateTime DerniereHeureDecriture) {

			ArgumentNullException.ThrowIfNull(Localisation);
			File.SetLastWriteTime(Localisation.NomComplet, DerniereHeureDecriture);
		}

		/**
     * <summary>
     *   [FR] Définit l'heure à laquelle le fichier a été écrit pour la dernière fois (UTC).
     *   [EN] Sets the time that the file was last written (UTC).
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="DerniereHeureDecritureUTC">
     *   [FR] Dernière heure d'écriture, en heure UTC.
     *   [EN] Last write time, in UTC time.
     * </param>
     **/
		public static void DefinirLaDerniereHeureDecritureUtc(FichierReference Localisation, DateTime DerniereHeureDecritureUTC) {

			ArgumentNullException.ThrowIfNull(Localisation);
			File.SetLastWriteTimeUtc(Localisation.NomComplet, DerniereHeureDecritureUTC);
		}

		/**
     * <summary>
     *   [FR] Écrit les octets dans un fichier.
     *   [EN] Writes the contents of a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     **/
		public static void EcrireTousLesOctets(FichierReference Localisation, byte[] Contenu) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			File.WriteAllBytes(Localisation.NomComplet, Contenu);
		}

		/**
     * <summary>
     *   [FR] Écrit les octets dans un fichier de manière asynchrone.
     *   [EN] Writes the contents of a file asynchronously.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     **/
		public static Task EcrireTousLesOctetsAsync(FichierReference Localisation, byte[] Contenu) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			return File.WriteAllBytesAsync(Localisation.NomComplet, Contenu);
		}

		/**
     * <summary>
     *   [FR] Écrit du texte dans un fichier.
     *   [EN] Writes the contents of a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     **/
		public static void EcrireToutLeTexte(FichierReference Localisation, string Contenu) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			File.WriteAllText(Localisation.NomComplet, Contenu);
		}

		/**
     * <summary>
     *   [FR] Écrit du texte dans un fichier de manière asynchrone.
     *   [EN] Writes the contents of a file asynchronously.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     **/
		public static Task EcrireToutLeTexteAsync(FichierReference Localisation, string Contenu) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			return File.WriteAllTextAsync(Localisation.NomComplet, Contenu);
		}

		/**
     * <summary>
     *   [FR] Écrit du texte dans un fichier avec encodage.
     *   [EN] Writes the contents of a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'écriture du fichier.
     *   [EN] The encoding to use when writing the file.
     * </param>
     **/
		public static void EcrireToutLeTexte(FichierReference Localisation, string Contenu, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			ArgumentNullException.ThrowIfNull(Encodage);
			File.WriteAllText(Localisation.NomComplet, Contenu, Encodage);
		}

		/**
     * <summary>
     *   [FR] Écrit du texte dans un fichier de manière asynchrone avec encodage.
     *   [EN] Writes the contents of a file asynchronously.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'écriture du fichier.
     *   [EN] The encoding to use when writing the file.
     * </param>
     **/
		public static Task EcrireToutLeTexteAsync(FichierReference Localisation, string Contenu, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			ArgumentNullException.ThrowIfNull(Encodage);
			return File.WriteAllTextAsync(Localisation.NomComplet, Contenu, Encodage);
		}

		/**
     * <summary>
     *   [FR] Écrit une séquence de lignes dans un fichier.
     *   [EN] Writes the contents of a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     **/
		public static void EcrireToutesLeslignes(FichierReference Localisation, IEnumerable<string> Contenu) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			File.WriteAllLines(Localisation.NomComplet, Contenu);
		}

		/**
     * <summary>
     *   [FR] Écrit une séquence de lignes dans un fichier de manière asynchrone.
     *   [EN] Writes the contents of a file asynchronously.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     **/
		public static Task EcrireToutesLeslignesAsync(FichierReference Localisation, IEnumerable<string> Contenu) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			return File.WriteAllLinesAsync(Localisation.NomComplet, Contenu);
		}

		/**
     * <summary>
     *   [FR] Écrit un tableau de lignes dans un fichier.
     *   [EN] Writes the contents of a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     **/
		public static void EcrireToutesLeslignes(FichierReference Localisation, string[] Contenu) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			File.WriteAllLines(Localisation.NomComplet, Contenu);
		}

		/**
     * <summary>
     *   [FR] Écrit un tableau de lignes dans un fichier de manière asynchrone.
     *   [EN] Writes the contents of a file asynchronously.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     **/
		public static Task EcrireToutesLeslignesAsync(FichierReference Localisation, string[] Contenu) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			return File.WriteAllLinesAsync(Localisation.NomComplet, Contenu);
		}

		/**
     * <summary>
     *   [FR] Écrit une séquence de lignes dans un fichier avec encodage.
     *   [EN] Writes the contents of a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'écriture du fichier.
     *   [EN] The encoding to use when writing the file.
     * </param>
     **/
		public static void EcrireToutesLeslignes(FichierReference Localisation, IEnumerable<string> Contenu, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			ArgumentNullException.ThrowIfNull(Encodage);
			File.WriteAllLines(Localisation.NomComplet, Contenu, Encodage);
		}

		/**
     * <summary>
     *   [FR] Écrit une séquence de lignes dans un fichier de manière asynchrone avec encodage.
     *   [EN] Writes the contents of a file asynchronously.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'écriture du fichier.
     *   [EN] The encoding to use when writing the file.
     * </param>
     **/
		public static Task EcrireToutesLeslignesAsync(FichierReference Localisation, IEnumerable<string> Contenu, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			ArgumentNullException.ThrowIfNull(Encodage);
			return File.WriteAllLinesAsync(Localisation.NomComplet, Contenu, Encodage);
		}

		/**
     * <summary>
     *   [FR] Écrit un tableau de lignes dans un fichier avec encodage.
     *   [EN] Writes the contents of a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'écriture du fichier.
     *   [EN] The encoding to use when writing the file.
     * </param>
     **/
		public static void EcrireToutesLeslignes(FichierReference Localisation, string[] Contenu, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			ArgumentNullException.ThrowIfNull(Encodage);
			File.WriteAllLines(Localisation.NomComplet, Contenu, Encodage);
		}

		/**
     * <summary>
     *   [FR] Écrit un tableau de lignes dans un fichier de manière asynchrone avec encodage.
     *   [EN] Writes the contents of a file asynchronously.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier.
     *   [EN] Contents of the file.
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'écriture du fichier.
     *   [EN] The encoding to use when writing the file.
     * </param>
     **/
		public static Task EcrireToutesLeslignesAsync(FichierReference Localisation, string[] Contenu, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			ArgumentNullException.ThrowIfNull(Encodage);
			return File.WriteAllLinesAsync(Localisation.NomComplet, Contenu, Encodage);
		}

		/**
     * <summary>
     *   [FR] Ajoute une ligne à la fin d'un fichier.
     *   [EN] Appends a line to the end of a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu de la ligne à ajouter.
     *   [EN] Contents of the line to append.
     * </param>
     **/
		public static void AjouterLigne(FichierReference Localisation, string Contenu) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			File.AppendAllText(Localisation.NomComplet, Contenu);
		}

		/**
     * <summary>
     *   [FR] Ajoute une ligne à la fin d'un fichier de manière asynchrone.
     *   [EN] Appends a line to the end of a file asynchronously.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu de la ligne à ajouter.
     *   [EN] Contents of the line to append.
     * </param>
     **/
		public static Task AjouterLigneAsync(FichierReference Localisation, string Contenu) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			return File.AppendAllTextAsync(Localisation.NomComplet, Contenu);
		}

		/**
     * <summary>
     *   [FR] Ajoute une ligne à la fin d'un fichier avec encodage.
     *   [EN] Appends a line to the end of a file.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu de la ligne à ajouter.
     *   [EN] Contents of the line to append.
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'écriture du fichier.
     *   [EN] The encoding to use when writing the file.
     * </param>
     **/
		public static void AjouterLigne(FichierReference Localisation, string Contenu, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			ArgumentNullException.ThrowIfNull(Encodage);
			File.AppendAllText(Localisation.NomComplet, Contenu, Encodage);
		}

		/**
     * <summary>
     *   [FR] Ajoute une ligne à la fin d'un fichier de manière asynchrone avec encodage.
     *   [EN] Appends a line to the end of a file asynchronously.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu de la ligne à ajouter.
     *   [EN] Contents of the line to append.
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'écriture du fichier.
     *   [EN] The encoding to use when writing the file.
     * </param>
     **/
		public static Task AjouterLigneAsync(FichierReference Localisation, string Contenu, Encoding Encodage) {

			ArgumentNullException.ThrowIfNull(Localisation);
			ArgumentNullException.ThrowIfNull(Contenu);
			ArgumentNullException.ThrowIfNull(Encodage);
			return File.AppendAllTextAsync(Localisation.NomComplet, Contenu, Encodage);
		}

		/**
     * <summary>
     *   [FR] Détermine si le nom de fichier donné existe ou non.
     *   [EN] Determines whether the given file name exists.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier.
     *   [EN] Location of the file.
     * </param>
     * <returns>
     *   [FR] Vrai s'il existe, sinon Faux.
     *   [EN] True if it exists, False otherwise.
     * </returns>
     **/
		public static bool VerifieSiExiste(FichierReference Localisation) {

			ArgumentNullException.ThrowIfNull(Localisation);
			return File.Exists(Localisation.NomComplet);
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
     *   [FR] Faux si les noms représentent le même objet, Vrai sinon.
     *   [EN] False if the names represent the same object, True otherwise.
     * </returns>
     **/
		public static bool operator !=(FichierReference ObjetA, FichierReference ObjetB) => !(ObjetA == ObjetB);

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
		public static bool operator ==(FichierReference ObjetA, FichierReference ObjetB) {

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
     *   [FR] Helper interne pour valider le chemin source et retourner un chemin complet.
		 *   [EN] Internal helper to validate the source path and return a full path.
		 * </summary>
     **/
		private static string ObtenirCheminCompletValide(string Chemins) {

			ArgumentException.ThrowIfNullOrEmpty(Chemins, nameof(Chemins));
			return Path.GetFullPath(Chemins);
		}

		/**
     * <summary>
     *   [FR] Helper interne pour obtenir le nom complet à partir d'un FileInfo.
     *   [EN] Internal helper to get the full name from a FileInfo.
     * </summary>
     **/
		private static string ObtenirNomCompletDepuisFileInfo(FileInfo InInfo) {

			ArgumentNullException.ThrowIfNull(InInfo);
			return InInfo.FullName;
		}
	}

	/**
   * <summary>
   *   [FR] Méthodes d'extension pour la fonctionnalité FichierReference.
   *   [EN] Extension methods for FichierReference functionality.
   * </summary>
   **/
	public static class FichierReferenceMethodeDextension {

		/**
     * <summary>
     *   [FR] Sérialise manuellement une référence de fichier vers un flux binaire.
     *   [EN] Manually serializes a file reference to a binary stream.
     * </summary>
     * <param name="Redacteur">
     *   [FR] Rédacteur binaire sur lequel écrire.
     *   [EN] Binary writer to write to.
     * </param>
     * <param name="Fichier">
     *   [FR] La référence du fichier à écrire.
     *   [EN] The file reference to write.
     * </param>
     **/
		public static void Ecrire(this BinaryWriter Redacteur, FichierReference Fichier) {

			ArgumentNullException.ThrowIfNull(Redacteur);
			Redacteur.Write((Fichier == null) ? string.Empty : Fichier.NomComplet);
		}

		/**
     * <summary>
     *   [FR] Sérialise manuellement une référence de fichier vers un flux binaire
     *        en utilisant une table d'identifiants uniques.
     *   [EN] Manually serializes a file reference to a binary stream
     *        using a lookup table of unique identifiers.
     * </summary>
     * <param name="Redacteur">
     *   [FR] Rédacteur binaire sur lequel écrire.
     *   [EN] Binary writer to write to.
     * </param>
     * <param name="Fichier">
     *   [FR] La référence du fichier à écrire.
     *   [EN] The file reference to write.
     * </param>
     * <param name="FichierAIdUnique">
     *   [FR] Table associant une référence de fichier à un identifiant unique.
     *   [EN] Table mapping a file reference to a unique identifier.
     * </param>
     **/
		public static void Ecrire(this BinaryWriter Redacteur, FichierReference Fichier, Dictionary<FichierReference, int> FichierAIdUnique) {

			ArgumentNullException.ThrowIfNull(Redacteur);
			ArgumentNullException.ThrowIfNull(FichierAIdUnique);

			if(Fichier == null) {

				Redacteur.Write(-1);
			}
			else if(FichierAIdUnique.TryGetValue(Fichier, out int IdUnique)) {

				Redacteur.Write(IdUnique);
			}
			else {

				IdUnique = FichierAIdUnique.Count;
				FichierAIdUnique[Fichier] = IdUnique;
				Redacteur.Write(IdUnique);
				Redacteur.Ecrire(Fichier);
			}
		}

		/**
     * <summary>
     *   [FR] Désérialise manuellement une référence de fichier à partir d'un flux binaire.
     *   [EN] Manually deserializes a file reference from a binary stream.
     * </summary>
     * <param name="Lecteur">
     *   [FR] Lecteur binaire à partir duquel lire.
     *   [EN] Binary reader to read from.
     * </param>
     * <returns>
     *   [FR] Nouvel objet FichierReference ou null.
     *   [EN] New FichierReference object or null.
     * </returns>
     **/
		public static FichierReference LireLaReferenceDuFichier(this BinaryReader Lecteur) {

			ArgumentNullException.ThrowIfNull(Lecteur);

			string NomComplet = Lecteur.ReadString();
			return (NomComplet.Length == 0) ? null : new FichierReference(NomComplet, Aseptise.Aucun);
		}

		/**
     * <summary>
     *   [FR] Désérialise une référence de fichier en utilisant une table de recherche
     *        pour éviter d'écrire le même nom plus d'une fois.
     *   [EN] Deserializes a file reference using a lookup table
     *        to avoid writing the same name more than once.
     * </summary>
     * <param name="Lecteur">
     *   [FR] La source à lire.
     *   [EN] The source to read from.
     * </param>
     * <param name="FichierUnique">
     *   [FR] Table des références de fichiers uniques.
     *   [EN] Table of unique file references.
     * </param>
     * <returns>
     *   [FR] Nouvel objet FichierReference.
     *   [EN] New FichierReference object.
     * </returns>
     **/
		public static FichierReference LireLaReferenceDuFichier(this BinaryReader Lecteur, List<FichierReference> FichierUnique) {

			ArgumentNullException.ThrowIfNull(Lecteur);
			ArgumentNullException.ThrowIfNull(FichierUnique);

			int IdUnique = Lecteur.ReadInt32();
			if(IdUnique == -1) {

				return null;
			}
			else if(IdUnique < FichierUnique.Count) {

				return FichierUnique[IdUnique];
			}
			else {

				FichierReference Resultat = Lecteur.LireLaReferenceDuFichier();
				FichierUnique.Add(Resultat);
				return Resultat;
			}
		}
	}
}
