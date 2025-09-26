/**
 * Copyright © 2017-2024, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2024, Galactic-Shrine - Tous droits réservés.
 **/

using System.IO;
using System.Text;
using System.Security.Cryptography;
using GalacticShrine.Interface;
using GalacticShrine.IO;
using static GalacticShrine.GalacticShrine;
using static GalacticShrine.DossierReference;
using static GalacticShrine.FichierReference;

namespace GalacticShrine.Stockage {

  /**
   * <summary>
   *   [FR] Classe responsable du stockage sécurisé des données de session.
   *   [EN] Class responsible for secure session data storage.
   * </summary>
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
    public static string Extension { get; set; } = "dat";

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
    private static string ObtenirLeCheminDuFichier() => Chemin.Combiner(Chemin1: $"{Repertoire["Roaming"]}", Chemin2: Apps, Chemin3: $"{NomDeFichier}.{Extension}");

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

      // Vérifie si le dossier de stockage existe.
      if(!VerifieSiExiste(Localisation: new DossierReference(Chemins: Chemin.Combiner(Chemin1: $"{Repertoire["Roaming"]}", Chemin2: Apps)))) {

        // Crée un nouveau dossier de stockage.
        Creer(Localisation: new DossierReference(Chemins: Chemin.Combiner(Chemin1: $"{Repertoire["Roaming"]}", Chemin2: Apps)));
      }

      using(Aes aes = Aes.Create()) {
        
        byte[] key = Encoding.UTF8.GetBytes(Cle);
        aes.Key = key;
        aes.GenerateIV();

        using(FileStream fs = new FileStream(ObtenirLeCheminDuFichier(), FileMode.Create)) {
          
          // Écrit l'IV au début du fichier.
          fs.Write(aes.IV, 0, aes.IV.Length);

          // Crypteur
          using(CryptoStream cs = new CryptoStream(fs, aes.CreateEncryptor(), CryptoStreamMode.Write))
          using(StreamWriter writer = new StreamWriter(cs)) {
            writer.Write(Donnees);
          }
        }
      }
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
     *   [FR] Les données de session déchiffrées, ou null si le fichier n'existe pas.
     *   [EN] The decrypted session data, or null if the file does not exist.s.
     * </returns>
     **/
    public string Charger(string Cle) {

      // Vérifie si le fichier de session existe.
      if(!VerifieSiExiste(Localisation: new FichierReference(Chemins: ObtenirLeCheminDuFichier())))
        return null;

      using(FileStream fs = new FileStream(ObtenirLeCheminDuFichier(), FileMode.Open)) {
        byte[] iv = new byte[16];
        fs.Read(iv, 0, iv.Length);

        using(Aes aes = Aes.Create()) {
          byte[] key = Encoding.UTF8.GetBytes(Cle);
          aes.Key = key;
          aes.IV = iv;

          using(CryptoStream cs = new CryptoStream(fs, aes.CreateDecryptor(), CryptoStreamMode.Read))
          using(StreamReader reader = new StreamReader(cs)) {
            return reader.ReadToEnd();
          }
        }
      }
    }
  }
}