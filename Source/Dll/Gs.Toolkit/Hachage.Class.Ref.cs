/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System.IO;
using System.Text;
using System.Security.Cryptography;
using Gs.Outils.Enumeration;

namespace Gs.Outils {

  /**
   * <summary>
   *   [FR] Classe utilisée pour générer les sommes de hachage du fichier.
   *   [EN] Class used to generate hash sums of file.
   * </summary>
   **/
  static class Hachage {

    /**
     * <summary>
     *   [FR] Convertit les octets[] en chaînes de caractères.
     *   [EN] Converts byte[] to string.
     * </summary>
     * <param name="CheminDuFichier">
     *   [FR] Le fichier à hacher.
     *   [EN] The file to hash.
     * </param>
     * <param name="HachageType">
     *   [FR] Le type de hachage.
     *   [EN] The type of hash.
     * </param>
     * <returns>
     *   [FR] Le hachage calculé.
     *   [EN] The computed hash.
     * </returns>
     **/
    static string HachageDeFichier (string CheminDuFichier, HachageType HachageType) {

      FileStream Fichier = new FileStream(path: CheminDuFichier, mode: FileMode.Open);

      switch(HachageType) {

        case HachageType.Md5:
          return HachageOctetsVersHachageDeChaines(Hachures: MD5.Create().ComputeHash(inputStream: Fichier));

        case HachageType.Sha1:
          return HachageOctetsVersHachageDeChaines(Hachures: SHA1.Create().ComputeHash(inputStream: Fichier));

        case HachageType.Sha256:
          return HachageOctetsVersHachageDeChaines(Hachures: SHA256.Create().ComputeHash(inputStream: Fichier));

        case HachageType.Sha512:
          return HachageOctetsVersHachageDeChaines(Hachures: SHA512.Create().ComputeHash(inputStream: Fichier));

        default:
          return "";
      }

      /**
       * <summary>
       *   [FR] Convertit les octets[] en chaînes de caractères.
       *   [EN] Converts byte[] to string.
       * </summary>
       * <param name="Hachures">
       *   [FR] Chemin d'accès à ce répertoire.
       *   [EN] Path to this directory.
       * </param>
       **/
      string HachageOctetsVersHachageDeChaines (byte[] Hachures) {

        StringBuilder Constructeur = new StringBuilder(Hachures.Length * 2); // StringBuilder();

        foreach (byte OctetHachage in Hachures)
          Constructeur.Append(OctetHachage.ToString("X2").ToLower());

        return Constructeur.ToString();
      }
    }
  }
}
