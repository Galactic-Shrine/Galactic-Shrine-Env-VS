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
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalacticShrine.Interface;
using GalacticShrine.IO;
using static GalacticShrine.DossierReference;
using static GalacticShrine.FichierReference;
using static GalacticShrine.GalacticShrine;

namespace GalacticShrine.Stockage {

	/**
   * <summary>
   *   [FR] Classe responsable du stockage sécurisé des données de session.
   *   [EN] Class responsible for secure session data storage.
   * </summary>
   * <remarks>
   *   [FR] Utilise AES (CBC/PKCS7) et exige une clé dont l'encodage UTF-8 produit 16, 24 ou 32 octets.
   *   [EN] Uses AES (CBC/PKCS7) and requires a key whose UTF-8 encoding yields 16, 24 or 32 bytes.
   * </remarks>
   **/
	public class Session : StockageSessionInterface {

    /**
     * <summary>
     *   [FR] Nom du dossier de stockage sécurisé de l'application.
     *        Par défaut : vide
     *   [EN] Name of the secure storage folder for the application.
     *        Default: empty 
     * </summary>
     **/
    public static string Apps { get; set; } = "";

    /**
     * <summary>
     *   [FR] Extension du fichier de session.
     *        Par défaut : dat
     *   [EN] File extension for the session.
     *        Default: dat
     * </summary>
     **/
    public static string Extension { get; set; } = Fichier.Extension["Scripts"][4];

    /**
     * <summary>
     *   [FR] Nom du fichier de session.
     *        Par défaut : Session
     *   [EN] Session file name.
     *        Default: Session
     * </summary>
     **/
    public static string NomDeFichier { get; set; } = "Session";

    /**
     * <summary>
     *   [FR] Obtient le chemin complet du fichier de session.
     *   [EN] Gets the full path of the session file.
     * </summary>
     * <returns>
     *   [FR] Le chemin complet du fichier de session.
     *   [EN] The full path of the session file.
     * </returns>
     **/
    private static string ObtenirLeCheminDuFichier() => Chemin.Combiner(Chemin1: $"{Repertoire["Roaming"]}", Chemin2: Apps, Chemin3: $"{NomDeFichier}{Extension}");

		/**
     * <summary>
     *   [FR] Crée la clé de chiffrement à partir de la chaîne fournie et valide sa longueur.
     *   [EN] Builds the encryption key from the provided string and validates its length.
     * </summary>
     * <param name="Cle">
     *   [FR] Clé de chiffrement en texte clair.
     *   [EN] Encryption key as plain text.
     * </param>
     * <returns>
     *   [FR] Tableau d'octets utilisable par AES.
     *   [EN] Byte array usable by AES.
     * </returns>
     **/
		private static byte[] ObtenirCleDeChiffrement(string Cle) {
			ArgumentNullException.ThrowIfNull(Cle);

			byte[] cleOctets = Encoding.UTF8.GetBytes(Cle);

			if(cleOctets.Length != 16 && cleOctets.Length != 24 && cleOctets.Length != 32) {
				throw new ArgumentException(
					"La clé de chiffrement doit produire 16, 24 ou 32 octets lorsqu'elle est encodée en UTF-8.",
					nameof(Cle));
			}

			return cleOctets;
		}

		/**
     * <summary>
     *   [FR] Sauvegarde les données de session dans un fichier sécurisé.
     *   [EN] Saves the session data in a secure file.
     * </summary>
     * <param name="Donnees">
     *   [FR] Les données de session à sauvegarder.
     *   [EN] The session data to save.
     * </param>
     * <param name="Cle">
     *   [FR] La clé de cryptage utilisée pour sécuriser les données.
     *   [EN] The encryption key used to secure the data.
     * </param>
     **/
		public void Sauvegarder(string Donnees, string Cle) {
			ArgumentNullException.ThrowIfNull(Donnees);

			byte[] cleOctets = ObtenirCleDeChiffrement(Cle);

			string cheminDossier = Chemin.Combiner(
				Chemin1: $"{Repertoire["Roaming"]}",
				Chemin2: Apps);

			if(!VerifieSiExiste(Localisation: new DossierReference(Chemins: cheminDossier))) {
				Creer(Localisation: new DossierReference(Chemins: cheminDossier));
			}

			using Aes aes = Aes.Create();
			aes.Key = cleOctets;
			aes.GenerateIV();

			string cheminFichier = ObtenirLeCheminDuFichier();

			using FileStream fluxFichier = new FileStream(
				cheminFichier,
				FileMode.Create,
				FileAccess.Write,
				FileShare.None);

			// Écrit l'IV au début du fichier.
			fluxFichier.Write(aes.IV, 0, aes.IV.Length);

			using CryptoStream fluxChiffre = new CryptoStream(
				fluxFichier,
				aes.CreateEncryptor(),
				CryptoStreamMode.Write);

			using StreamWriter redacteur = new StreamWriter(
				fluxChiffre,
				Encoding.UTF8,
				bufferSize: 1024,
				leaveOpen: false);

			redacteur.Write(Donnees);
		}

		/**
     * <summary>
     *   [FR] Sauvegarde de manière asynchrone les données de session dans un fichier sécurisé.
     *   [EN] Asynchronously saves session data in a secure file.
     * </summary>
     * <param name="Donnees">
     *   [FR] Les données de session à sauvegarder.
     *   [EN] The session data to save.
     * </param>
     * <param name="Cle">
     *   [FR] La clé de cryptage utilisée pour sécuriser les données.
     *   [EN] The encryption key used to secure the data.
     * </param>
     * <param name="JetonAnnulation">
     *   [FR] Jeton permettant d'annuler l'opération.
     *   [EN] Token used to cancel the operation.
     * </param>
     * <returns>
     *   [FR] Une tâche représentant l'opération asynchrone.
     *   [EN] A task representing the asynchronous operation.
     * </returns>
     **/
		public async Task SauvegarderAsync(string Donnees, string Cle, CancellationToken JetonAnnulation = default) {
			ArgumentNullException.ThrowIfNull(Donnees);

			byte[] cleOctets = ObtenirCleDeChiffrement(Cle);

			string cheminDossier = Chemin.Combiner(
				Chemin1: $"{Repertoire["Roaming"]}",
				Chemin2: Apps);

			if(!VerifieSiExiste(Localisation: new DossierReference(Chemins: cheminDossier))) {
				Creer(Localisation: new DossierReference(Chemins: cheminDossier));
			}

			JetonAnnulation.ThrowIfCancellationRequested();

			using Aes aes = Aes.Create();
			aes.Key = cleOctets;
			aes.GenerateIV();

			string cheminFichier = ObtenirLeCheminDuFichier();

			using FileStream fluxFichier = new FileStream(
				cheminFichier,
				FileMode.Create,
				FileAccess.Write,
				FileShare.None,
				bufferSize: 4096,
				useAsync: true);

			// Écrit l'IV au début du fichier.
			await fluxFichier
				.WriteAsync(aes.IV, 0, aes.IV.Length, JetonAnnulation)
				.ConfigureAwait(false);

			using CryptoStream fluxChiffre = new CryptoStream(
				fluxFichier,
				aes.CreateEncryptor(),
				CryptoStreamMode.Write);

			using StreamWriter redacteur = new StreamWriter(
				fluxChiffre,
				Encoding.UTF8,
				bufferSize: 1024,
				leaveOpen: false);

			await redacteur
				.WriteAsync(Donnees.AsMemory(), JetonAnnulation)
				.ConfigureAwait(false);

			await redacteur
				.FlushAsync()
				.ConfigureAwait(false);
		}

		/**
     * <summary>
     *   [FR] Charge les données de session depuis un fichier sécurisé.
     *   [EN] Loads session data from a secure file.
     * </summary>
     * <param name="Cle">
     *   [FR] La clé de cryptage utilisée pour sécuriser les données.
     *   [EN] The encryption key used to secure the data.
     * </param>
     * <returns>
     *   [FR] Les données de session déchiffrées, ou <c>null</c> si le fichier n'existe pas.
     *   [EN] The decrypted session data, or <c>null</c> if the file does not exist.
     * </returns>
     **/
		public string Charger(string Cle) {
			byte[] cleOctets = ObtenirCleDeChiffrement(Cle);

			string cheminFichier = ObtenirLeCheminDuFichier();

			// Vérifie si le fichier de session existe.
			if(!VerifieSiExiste(Localisation: new FichierReference(Chemins: cheminFichier))) {
				return null;
			}

			using FileStream fluxFichier = new FileStream(
				cheminFichier,
				FileMode.Open,
				FileAccess.Read,
				FileShare.Read);

			byte[] iv = new byte[16];
			int octetsLus = fluxFichier.Read(iv, 0, iv.Length);

			if(octetsLus != iv.Length) {
				throw new InvalidDataException("Le fichier de session est corrompu : impossible de lire l'IV complet.");
			}

			using Aes aes = Aes.Create();
			aes.Key = cleOctets;
			aes.IV = iv;

			using CryptoStream fluxChiffre = new CryptoStream(
				fluxFichier,
				aes.CreateDecryptor(),
				CryptoStreamMode.Read);

			using StreamReader lecteur = new StreamReader(
				fluxChiffre,
				Encoding.UTF8,
				detectEncodingFromByteOrderMarks: true,
				leaveOpen: false);

			return lecteur.ReadToEnd();
		}

		/**
     * <summary>
     *   [FR] Charge de manière asynchrone les données de session depuis un fichier sécurisé.
     *   [EN] Asynchronously loads session data from a secure file.
     * </summary>
     * <param name="Cle">
     *   [FR] La clé de cryptage utilisée pour sécuriser les données.
     *   [EN] The encryption key used to secure the data.
     * </param>
     * <param name="JetonAnnulation">
     *   [FR] Jeton permettant d'annuler l'opération.
     *   [EN] Token used to cancel the operation.
     * </param>
     * <returns>
     *   [FR] Les données de session déchiffrées, ou <c>null</c> si le fichier n'existe pas.
     *   [EN] The decrypted session data, or <c>null</c> if the file does not exist.
     * </returns>
     **/
		public async Task<string> ChargerAsync(string Cle, CancellationToken JetonAnnulation = default) {
			byte[] cleOctets = ObtenirCleDeChiffrement(Cle);

			string cheminFichier = ObtenirLeCheminDuFichier();

			// Vérifie si le fichier de session existe.
			if(!VerifieSiExiste(Localisation: new FichierReference(Chemins: cheminFichier))) {
				return null;
			}

			JetonAnnulation.ThrowIfCancellationRequested();

			using FileStream fluxFichier = new FileStream(
				cheminFichier,
				FileMode.Open,
				FileAccess.Read,
				FileShare.Read,
				bufferSize: 4096,
				useAsync: true);

			byte[] iv = new byte[16];
			int octetsLus = await fluxFichier
				.ReadAsync(iv, 0, iv.Length, JetonAnnulation)
				.ConfigureAwait(false);

			if(octetsLus != iv.Length) {
				throw new InvalidDataException("Le fichier de session est corrompu : impossible de lire l'IV complet.");
			}

			using Aes aes = Aes.Create();
			aes.Key = cleOctets;
			aes.IV = iv;

			using CryptoStream fluxChiffre = new CryptoStream(
				fluxFichier,
				aes.CreateDecryptor(),
				CryptoStreamMode.Read);

			using StreamReader lecteur = new StreamReader(
				fluxChiffre,
				Encoding.UTF8,
				detectEncodingFromByteOrderMarks: true,
				leaveOpen: false);

			string resultat = await lecteur
				.ReadToEndAsync()
				.ConfigureAwait(false);

			return resultat;
		}
	}
}
