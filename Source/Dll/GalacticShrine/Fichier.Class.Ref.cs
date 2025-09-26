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
using System.Text;
using System.IO;
using System.Threading.Tasks;
using GalacticShrine.Enumeration;
using GalacticShrine.Properties;


namespace GalacticShrine {

  /**
   * <summary>
   *   [FR] Représentation d'un chemin de fichier absolu. 
   *        Permet des hachages et des comparaisons rapides. 
   *   [EN] Representation a file absolute path. 
   *        Allows fast hashings and comparisons. 
   * </summary>
   **/
  [Serializable]
  public class FichierReference: FichierSystemeReference, IEquatable<FichierReference> {

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
    public FichierReference(string Chemins) : base(Path.GetFullPath(Chemins)) {
#if NET8_0_OR_GREATER
        if(NomComplet[^1] == '\\' || NomComplet[^1] == '/') {
#else
        if(NomComplet[NomComplet.Length - 1] == '\\' || NomComplet[NomComplet.Length - 1] == '/') {
#endif

        throw new ArgumentException(Resources.FichierTermineInvalide);
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
    public FichierReference(FileInfo InInfo): base(InInfo.FullName) {
    }

    /**
     * <summary>
     *   [FR] Constructeur par défaut.
     *   [EN] Default constructor.
     * </summary>
     * <param name="NomAuComplet">
     *   [FR] Le chemin aseptisé complet
     *   [EN] The full Desinfecterd path
     * </param>
     * <param name="Assainisseur">
     *   [FR] Arguments fictifs pour utiliser la surcharge aseptisée.
     *   [EN] Dummary arguments to use the Desinfecterd overload.
     * </param>
     **/
    public FichierReference(string NomAuComplet, Aseptise Assainisseur): base(NomAuComplet) {
    }

    /**
     * <summary>
     *   [FR] changer l'extension du fichier à autre chose
     *   [EN] change the file's extension to something else
     * </summary>
     * <param name="Extension">
     *   [FR] La nouvelle extension
     *   [EN] The new extension
     * </param>
     * <returns>
     *   [FR] Un FichierReference avec le même chemin et le même nom, mais avec la nouvelle extension
     *   [EN] A FichierReference with the same path and name, but with the new extension
     * </returns>
     **/
    public FichierReference ModifierLextension(string Extension) {

      string NouveauNomComplet = Path.ChangeExtension(NomComplet, Extension);
      return new FichierReference(NouveauNomComplet, Aseptise.Aucun);
    }

    /**
     * <summary>
     *   [FR] Récupère le répertoire contenant ce fichier
     *   [EN] Retrieves the directory containing this file
     * </summary>
     * <returns>
     *   [FR] Un nouvel objet répertoire représentant le répertoire contenant cet objet
     *   [EN] A new directory object representing the directory containig this object
     * </returns>
     **/
    public DossierReference Repertoire => DossierReference.ObtenirLeRepertoireParrent(this);

    /**
     * <summary>
     *   [FR] Construire un objet FileInfo à partir de cette référence
     *   [EN] Construct a FileInfo object from this reference
     * </summary>
     * <returns>
     *   [FR] Un nouvel objet FileInfo
     *   [EN] A new FileInfo object
     * </returns>
     **/
    public FileInfo InformationsDuFichier() => new(NomComplet);

    /**
     * <summary>
     *   [FR] Récupère le nom du fichier sans les informations de chemin d'accès.
     *   [EN] Gets file name without path information.
     * </summary>
     * <returns>
     *   [FR] Une chaîne contenant le nom du fichier
     *   [EN] A string containing the file name
     * </returns>
     **/
    public string ObtenirLeNomDeFichier() => Path.GetFileName(NomComplet);

    /**
     * <summary>
     *   [FR] Récupère le nom du fichier sans l'information de chemin d'accès ou une extension.
     *   [EN] Gets file name without path information or an extension.
     * </summary>
     * <returns>
     *   [FR] Une chaîne contenant le nom du fichier sans extension.
     *   [EN] A string containing the file name without an extension.
     * </returns>
     **/
    public string ObtenirLeNomDeFichierSansExtension() => Path.GetFileNameWithoutExtension(NomComplet);

    /**
     * <summary>
     *   [FR] Obtient le nom du fichier sans chemin d'accès ou toute extension
     *   [EN] Gets the file name wihout path or any extension
     * </summary>
     * <returns>
     *   [FR] Une chaîne contenant le nom du fichier sans extension.
     *   [EN] A string containing the file name without an extension.
     * </returns>
     **/
    public string ObtenirLeNomDeFichierSansAucuneExtension()
    {

      int IndexDeDebut = NomComplet.LastIndexOf(DossierReference.Rs) + 1;
      int IndexDeFin = NomComplet.LastIndexOf('.', IndexDeDebut);

      if (IndexDeFin < IndexDeDebut)
      {

        return NomComplet.Substring(IndexDeDebut);
      }
      else
      {

        return NomComplet.Substring(IndexDeDebut, IndexDeFin - IndexDeDebut);
      }
    }

    /**
     * <summary>
     *   [FR] Obtient l'extension pour ce nom de fichier
     *   [EN] Gets the extension for this filename
     * </summary>
     * <returns>
     *   [FR] Une chaîne contenant l'extension de ce nom de fichier
     *   [EN] A string containig the extension of this filename
     * </returns>
     **/
    public string ObtenirLextension() => Path.GetExtension(NomComplet);

    /**
     * <summary>
     *   [FR] Comparaison par rapport à un autre objet pour l'égalité.
     *   [EN] Compares Against another object for equality.
     * </summary>
     * <param name="Objet">
     *   [FR] Autre exemple à comparer
     *   [EN] Other instance to compare
     * </param>
     * <returns>
     *   [FR] Vrai si les noms représentent le même objet, Faux sinon
     *   [EN] True if the names represent the same object, False otherwise
     * </returns>
     **/
    public bool Equals(FichierReference Objet) => Objet == this;

    /**
     * <summary>
     *   [FR] Comparaison par rapport à un autre objet pour l'égalité.
     *   [EN] Compares Against another object for equality.
     * </summary>
     * <param name="Objet">
     *   [FR] Autre exemple à comparer
     *   [EN] Other instance to compare
     * </param>
     * <returns>
     *   [FR] Vrai si les noms représentent le même objet, Faux sinon
     *   [EN] True if the names represent the same object, False otherwise
     * </returns>
     **/
    public override bool Equals(object Objet) => (Objet is FichierReference) && ((FichierReference)Objet) == this;

    /**
     * <summary>
     *   [FR] Retourne un code de hachage pour cet objet
     *   [EN] Returns a hash code for this object
     * </summary>
     * <returns>
     *   [FR] Retourne un code de hachage pour cet objet
     *   [EN] Returns a hash code for this object
     * </returns>
     **/
    public override int GetHashCode() => Comparateur.GetHashCode(NomComplet);

    /**
     * <summary>
     *   [FR] Combine several fragments with a base directory,
     *        à partir d'un nouveau nom de fichier
     *   [EN] Combine several fragments with a base directory,
     *        to from a new filename
     * </summary>
     * <param name = "RepertoireDeBase" >
     *   [FR] Le répertoire de base
     *   [EN] The basic directory
     * </param>
     * <param name = "Fragments" >
     *   [FR] Fragments à combiner avec le répertoire de base
     *   [EN] Fragments to combine with the base directory
     * </param>
     * <returns>
     *   [FR] Le nouveau nom du fichier
     *   [EN] The new file name
     * </returns>
     **/
    public static FichierReference Combiner(DossierReference RepertoireDeBase, params string[] Fragments) {

      string NomComplet = FichierSystemeReference.CombinerLesChaines(RepertoireDeBase, Fragments);
      return new FichierReference(NomComplet, Aseptise.Aucun);
    }

    /**
     * <summary>
     *   [FR] Ajouter une chaîne à la fin d'un nom de fichier
     *   [EN] Append a string to the end of a filename
     * </summary>
     * <param name="FichierA">
     *   [FR] La référence du fichier de base
     *   [EN] The base file reference
     * </param>
     * <param name="FichierB">
     *   [FR] Suffixe à joindre en annexe
     *   [EN] Suffix to be appended
     * </param>
     **/
    public static FichierReference operator +(FichierReference FichierA, string FichierB) => new(FichierA.NomComplet + FichierB, Aseptise.Aucun);

    /**
    * <summary>
    *   [FR] Fonction d'aide à la création d'une référence de répertoire distant.
    *        Contrairement à un objet DossierReference normal, 
    *        ceux-ci ne sont pas convertis en un chemin complet dans le système de fichiers local.
    *   [EN] Helper fonction to create a remote directory reference.
    *        Unlike normal DossierReference object,
    *        these aren't converted to a full path in the local filesystem.
    * </summary>
    * <param name="CheminAbsolu">
    *   [FR] Le chemin absolu dans le système de fichiers distant
    *   [EN] The absolutr path in the remote file system
    * </param>
    * <returns>
    *   [FR] Nouvelle référence au dossier
    *   [EN] New directory reference
    * </returns>
    **/
    public static FichierReference MarquerADistance(string CheminAbsolu) => new(CheminAbsolu, Aseptise.Aucun);

    /**
     * <summary>
     *   [FR] Récupère les attributs d'un fichier
     *   [EN] Gets the attributes for a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <returns>
     *   [FR] Attibutes du fichier
     *   [EN] Attibutes of the file
     * </returns>
     **/
    public static FileAttributes ObtenirDesAttributs(FichierReference Localisation) => File.GetAttributes(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] Récupère les attributs d'un fichier de manière asynchrone
     *   [EN] Gets the attributes for a file asynchronously
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <returns>
     *   [FR] Attibutes du fichier
     *   [EN] Attibutes of the file
     * </returns>
     **/
    public async static Task<FileAttributes> ObtenirDesAttributsAsync(FichierReference Localisation) => await Task.Run(() => File.GetAttributes(Localisation.NomComplet));

    /**
     * <summary>
     *   [FR] Récupère l'heure à laquelle le fichier a été écrit pour la dernière fois.
     *   [EN] Gets the time that the file was last written.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <returns>
     *   [FR] Dernière heure d'écriture, en heure locale
     *   [EN] Last write time, in local time
     * </returns>
     **/
    public static DateTime ObtenirLaDerniereHeureDecriture(FichierReference Localisation) => File.GetLastWriteTime(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] Récupère l'heure à laquelle le fichier a été écrit pour la dernière fois.
     *   [EN] Gets the time that the file was last written.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <returns>
     *   [FR] Dernière heure d'écriture, en heure UTC
     *   [EN] Last write time, in UTC time
     * </returns>
     **/
    public static DateTime ObtenirLaDerniereHeureDecritureUtc(FichierReference Localisation) => File.GetLastWriteTimeUtc(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] Créer un fichiers
     *   [EN] Create a file
     * </summary>
     * <param name="Nom">
     *   [FR] Nom du nouveau fichier
     *   [EN] Name of the new file
     * </param>
     **/
    public static StreamWriter Creer(string Nom) => File.CreateText(Nom);

    /**
     * <summary>
     *   [FR] Ouvre un flux de fichiers(FileStream) sur le chemin spécifié avec accès en lecture/écriture
     *   [EN] Opens a FileStream on the specified path with read/writer access
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Mode">
     *   [FR] Mode à utiliser lors de l'ouverture du fichier
     *   [EN] Mode to use when opening the file
     * </param>
     * <returns>
     *   [FR] Nouveau FileStream pour le fichier donné
     *   [EN] New FileStream for the given file
     * </returns>
     **/
    public static FileStream Ouvrir(FichierReference Localisation, FileMode Mode) => File.Open(Localisation.NomComplet, Mode);

    /**
     * <summary>
     *   [FR] Ouvre un flux de fichiers(FileStream) sur le chemin spécifié avec accès en lecture/écriture
     *   [EN] Opens a FileStream on the specified path with read/writer access
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Mode">
     *   [FR] Mode à utiliser lors de l'ouverture du fichier
     *   [EN] Mode to use when opening the file
     * </param>
     * <param name="Acces">
     *   [FR] Mode de partage pour le nouveau fichier
     *   [EN] Sharing mode for the new file
     * </param>
     * <returns>
     *   [FR] Nouveau FileStream pour le fichier donné
     *   [EN] New FileStream for the given file
     * </returns>
     **/
    public static FileStream Ouvrir(FichierReference Localisation, FileMode Mode, FileAccess Acces) => File.Open(Localisation.NomComplet, Mode, Acces);

    /**
     * <summary>
     *   [FR] Ouvre un flux de fichiers(FileStream) sur le chemin spécifié avec accès en lecture/écriture
     *   [EN] Opens a FileStream on the specified path with read/writer access
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Mode">
     *   [FR] Mode à utiliser lors de l'ouverture du fichier
     *   [EN] Mode to use when opening the file
     * </param>
     * <param name="Acces">
     *   [FR] Mode d'accès pour le nouveau fichier
     *   [EN] Access mode for the new file
     * </param>
     * <param name="Partager">
     *   [FR] Mode de partage pour le fichier ouvert
     *   [EN] Sharing mode for the open file
     * </param>
     * <returns>
     *   [FR] Nouveau FileStream pour le fichier donné
     *   [EN] New FileStream for the given file
     * </returns>
     **/
    public static FileStream Ouvrir(FichierReference Localisation, FileMode Mode, FileAccess Acces, FileShare Partager) => File.Open(Localisation.NomComplet, Mode, Acces, Partager);

    /**
     * <summary>
     *   [FR] Lit le contenu d'un fichier
     *   [EN] Reads the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <returns>
     *   [FR] Tableau d'octets contenant le contenu du fichier
     *   [EN] Byte array containig the content of the file
     * </returns>
     **/
    public static byte[] LireTousLesOctets(FichierReference Localisation) => File.ReadAllBytes(Localisation.NomComplet);

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
     *   [FR] Un tableau de bytes contenant le contenu du fichier.
     *   [EN] A byte array containing the content of the file.
     * </returns>
     **/
    public async Task<byte[]> LireTousLesOctetsAsync(FichierReference Localisation) => await File.ReadAllBytesAsync(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] Lit le contenu d'un fichier
     *   [EN] Reads the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <returns>
     *   [FR] Contenu du fichier sous la forme d'une seule chaîne de caractères
     *   [EN] Contents of the file as a single string
     * </returns>
     **/
    public static string LireToutLeTexte(FichierReference Localisation) => File.ReadAllText(Localisation.NomComplet);

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
     *   [FR] Une chaîne contenant le texte complet du fichier.
     *   [EN] A string containing the full text of the file.
     * </returns>
     **/
    public async Task<string> LireToutLeTexteAsync(FichierReference Localisation) => await File.ReadAllTextAsync(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] Lit le contenu d'un fichier
     *   [EN] Reads the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Encodage">
     *   [FR] Encodage du fichier
     *   [EN] Encoding of the file
     * </param>
     * <returns>
     *   [FR] Contenu du fichier sous la forme d'une seule chaîne de caractères
     *   [EN] Contents of the file as a single string
     * </returns>
     **/
    public static string LireToutLeTexte(FichierReference Localisation, Encoding Encodage) => File.ReadAllText(Localisation.NomComplet, Encodage);

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
     *   [FR] Une chaîne contenant le texte complet du fichier, décodé avec l'encodage spécifié.
     *   [EN] A string containing the full text of the file, decoded using the specified encoding.
     * </returns>
     **/
    public async Task<string> LireToutLeTexteAsync(FichierReference Localisation, Encoding Encodage) => await File.ReadAllTextAsync(Localisation.NomComplet, Encodage);
    
    /**
     * <summary>
     *   [FR] Lit le contenu d'un fichier
     *   [EN] Reads the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <returns>
     *   [FR] tableau de chaînes de caractères contenant le contenu du fichier
     *   [EN] string array containig the contents of the file
     * </returns>
     **/
    public static string[] LireToutLesLignes(FichierReference Localisation) => File.ReadAllLines(Localisation.NomComplet);

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
     *   [FR] Un tableau de chaînes contenant toutes les lignes du fichier.
     *   [EN] An array of strings containing all lines of the file.
     * </returns>
     **/
    public async Task<string[]> LireToutLesLignesAsync(FichierReference Localisation) => await File.ReadAllLinesAsync(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] Lit le contenu d'un fichier
     *   [EN] Reads the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Encodage">
     *   [FR] Encodage du fichier
     *   [EN] Encoding of the file
     * </param>
     * <returns>
     *   [FR] tableau de chaînes de caractères contenant le contenu du fichier
     *   [EN] string array containig the contents of the file
     * </returns>
     **/
    public static string[] LireToutLesLignes(FichierReference Localisation, Encoding Encodage) => File.ReadAllLines(Localisation.NomComplet, Encodage);

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
     *   [FR] Un tableau de chaînes contenant toutes les lignes du fichier, décodé avec l'encodage spécifié.
     *   [EN] An array of strings containing all lines of the file, decoded using the specified encoding.
     * </returns>
     **/
    public async Task<string[]> LireToutLesLignesAsync(FichierReference Localisation, Encoding Encodage) => await File.ReadAllLinesAsync(Localisation.NomComplet, Encodage);

    /**
     * <summary>
     *   [FR] Rend un emplacement de fichier inscriptible
     *   [EN] Makes a file location writeable
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     **/
    public static void RendreInscriptibles(FichierReference Localisation) {

      if (VerifieSiExiste(Localisation)) {

        FileAttributes Attributes = ObtenirDesAttributs(Localisation);
        if ((Attributes & FileAttributes.ReadOnly) != 0) {

          DefinirDesAttributs(Localisation, Attributes & FileAttributes.ReadOnly);
        }
      }
    }

    /**
     * <summary>
     *   [FR] Rende un fichier inscriptible de manière asynchrone en vérifiant ses attributs.
     *         Si le fichier est en lecture seule, l'attribut "lecture seule" est supprimé.
     *   [EN] Makes a file writable asynchronously by checking its attributes.
     *         If the file is read-only, the "read-only" attribute is removed.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier à rendre inscriptible.
     *   [EN] Location of the file to make writable.
     * </param>
     **/
    public async static Task RendreInscriptiblesAsync(FichierReference Localisation) {


      if (VerifieSiExiste(Localisation)) {

        FileAttributes Attributes = await ObtenirDesAttributsAsync(Localisation);
        if ((Attributes & FileAttributes.ReadOnly) != 0) {

          await DefinirDesAttributsAsync(Localisation, Attributes & FileAttributes.ReadOnly);
        }
      }
    }

    /**
     * <summary>
     *   [FR] Copie un fichier d'un emplacement à un autre
     *   [EN] Copies a file from one location to another
     * </summary>
     * <param name="LocalisationDeDepart">
     *   [FR] Emplacement du fichier source
     *   [EN] Location of the source file
     * </param>
     * <param name="LocalisationDeDestination">
     *   [FR] Emplacement du fichier cible
     *   [EN] Location of the target file
     * </param>
     **/
    public static void Copier(FichierReference LocalisationDeDepart, FichierReference LocalisationDeDestination) => File.Copy(LocalisationDeDepart.NomComplet, LocalisationDeDestination.NomComplet);

    /**
     * <summary>
     *   [FR] Copie un fichier d'un emplacement à un autre
     *   [EN] Copies a file from one location to another
     * </summary>
     * <param name="LocalisationDeDepart">
     *   [FR] Emplacement du fichier source
     *   [EN] Location of the source file
     * </param>
     * <param name="LocalisationDeDestination">
     *   [FR] Emplacement du fichier cible
     *   [EN] Location of the target file
     * </param>
     * <param name="Ecraser">
     *   [FR] Où écraser le fichier dans l'emplacement cible
     *   [EN] Whither to overwrite the file in the  target location
     * </param>
     **/
    public static void Copier(FichierReference LocalisationDeDepart, FichierReference LocalisationDeDestination, bool Ecraser) => File.Copy(LocalisationDeDepart.NomComplet, LocalisationDeDestination.NomComplet, Ecraser);

    /**
     * <summary>
     *   [FR] Déplace un fichier d'un emplacement à un autre
     *   [EN] Moves a file from one location to another
     * </summary>
     * <param name="LocalisationDeDepart">
     *   [FR] Emplacement du fichier source
     *   [EN] Location of the source file
     * </param>
     * <param name="LocalisationDeDestination">
     *   [FR] Emplacement du fichier cible
     *   [EN] Location of the target file
     * </param>
     **/
    public static void Deplacer(FichierReference LocalisationDeDepart, FichierReference LocalisationDeDestination) => File.Move(LocalisationDeDepart.NomComplet, LocalisationDeDestination.NomComplet);
    
    /**
     * <summary>
     *   [FR] Supprimer ce fichier
     *   [EN] Delete this file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     **/
    public static void Supprimer(FichierReference Localisation) => File.Delete(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] Définit les attributs d'un fichier
     *   [EN] Sets the attributes for a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Attributs">
     *   [FR] Nouveaux attributs du fichier
     *   [EN] New attributes of the file
     * </param>
     **/
    public static void DefinirDesAttributs(FichierReference Localisation, FileAttributes Attributs) => File.SetAttributes(Localisation.NomComplet, Attributs);

    /**
     * <summary>
     *   [FR] Définit les attributs d'un fichier de manière asynchrone.
     *         Cette méthode applique les attributs spécifiés au fichier indiqué.
     *   [EN] Sets the attributes of a file asynchronously.
     *         This method applies the specified attributes to the given file.
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
     *   [FR] Cette méthode retourne une tâche qui représente l'opération asynchrone de définition des attributs.
     *   [EN] This method returns a task representing the asynchronous operation of setting the attributes.
     * </returns>
     **/
    public async static Task DefinirDesAttributsAsync(FichierReference Localisation, FileAttributes Attributs) => await Task.Run(() => File.SetAttributes(Localisation.NomComplet, Attributs));

    /**
     * <summary>
     *   [FR] Définit l'heure à laquelle le fichier a été écrit pour la dernière fois.
     *   [EN] Sets the time that the file was last  written.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="DerniereHeureDecriture">
     *   [FR] Dernière heure d'écriture, en heure locale
     *   [EN] Last writre time, in local time
     * </param>
     **/
    public static void DefinirLaDerniereHeureDecriture(FichierReference Localisation, DateTime DerniereHeureDecriture) => File.SetLastWriteTime(Localisation.NomComplet, DerniereHeureDecriture);

    /**
     * <summary>
     *   [FR] Définit l'heure à laquelle le fichier a été écrit pour la dernière fois.
     *   [EN] Sets the time that the file was last  written.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="DerniereHeureDecritureUTC">
     *   [FR] Dernière heure d'écriture, en heure UTC
     *   [EN] Last writre time, in UTC time
     * </param>
     **/
    public static void DefinirLaDerniereHeureDecritureUtc(FichierReference Localisation, DateTime DerniereHeureDecritureUTC) => File.SetLastWriteTimeUtc(Localisation.NomComplet, DerniereHeureDecritureUTC);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier
     *   [EN] Writres the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     **/
    public static void EcrireTousLesOctets(FichierReference Localisation, byte[] Contenu) => File.WriteAllBytes(Localisation.NomComplet, Contenu);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier de manière asynchrone
     *   [EN] Writres the contents of a file asynchronously
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     **/
    public async static Task EcrireTousLesOctetsAsync(FichierReference Localisation, byte[] Contenu) => await File.WriteAllBytesAsync(Localisation.NomComplet, Contenu);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier
     *   [EN] Writres the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     **/
    public static void EcrireToutLeTexte(FichierReference Localisation, string Contenu) => File.WriteAllText(Localisation.NomComplet, Contenu);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier de manière asynchrone
     *   [EN] Writres the contents of a file asynchronously
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     **/
    public async static Task EcrireToutLeTexteAsync(FichierReference Localisation, string Contenu) => await File.WriteAllTextAsync(Localisation.NomComplet, Contenu);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier
     *   [EN] Writres the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'analyse du fichier
     *   [EN] The encoding to use when parsing the file
     * </param>
     **/
    public static void EcrireToutLeTexte(FichierReference Localisation, string Contenu, Encoding Encodage) => File.WriteAllText(Localisation.NomComplet, Contenu, Encodage);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier de manière asynchrone
     *   [EN] Writres the contents of a file asynchronously
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'analyse du fichier
     *   [EN] The encoding to use when parsing the file
     * </param>
     **/
    public async static Task EcrireToutLeTexteAsync(FichierReference Localisation, string Contenu, Encoding Encodage) => await File.WriteAllTextAsync(Localisation.NomComplet, Contenu, Encodage);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier
     *   [EN] Writres the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     **/
    public static void EcrireToutesLeslignes(FichierReference Localisation, IEnumerable<string> Contenu) => File.WriteAllLines(Localisation.NomComplet, Contenu);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier de manière asynchrone
     *   [EN] Writres the contents of a file asynchronously
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     **/
    public async static Task EcrireToutesLeslignesAsync(FichierReference Localisation, IEnumerable<string> Contenu) => await File.WriteAllLinesAsync(Localisation.NomComplet, Contenu);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier
     *   [EN] Writres the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     **/
    public static void EcrireToutesLeslignes(FichierReference Localisation, string[] Contenu) => File.WriteAllLines(Localisation.NomComplet, Contenu);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier de manière asynchrone
     *   [EN] Writres the contents of a file asynchronously
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     **/
    public async static Task EcrireToutesLeslignesAsync(FichierReference Localisation, string[] Contenu) => await File.WriteAllLinesAsync(Localisation.NomComplet, Contenu);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier
     *   [EN] Writres the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'analyse du fichier
     *   [EN] The encoding to use when parsing the file
     * </param>
     **/
    public static void EcrireToutesLeslignes(FichierReference Localisation, IEnumerable<string> Contenu, Encoding Encodage) => File.WriteAllLines(Localisation.NomComplet, Contenu, Encodage);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier de manière asynchrone
     *   [EN] Writres the contents of a file asynchronously
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'analyse du fichier
     *   [EN] The encoding to use when parsing the file
     * </param>
     **/
    public async static Task EcrireToutesLeslignesAsync(FichierReference Localisation, IEnumerable<string> Contenu, Encoding Encodage) => await File.WriteAllLinesAsync(Localisation.NomComplet, Contenu, Encodage);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier
     *   [EN] Writres the contents of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'analyse du fichier
     *   [EN] The encoding to use when parsing the file
     * </param>
     **/
    public static void EcrireToutesLeslignes(FichierReference Localisation, string[] Contenu, Encoding Encodage) => File.WriteAllLines(Localisation.NomComplet, Contenu, Encodage);

    /**
     * <summary>
     *   [FR] Écrit le contenu d'un fichier de manière asynchrone
     *   [EN] Writres the contents of a file asynchronously
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu du fichier
     *   [EN] Contents of the file
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'analyse du fichier
     *   [EN] The encoding to use when parsing the file
     * </param>
     **/
    public async static Task EcrireToutesLeslignesAsync(FichierReference Localisation, string[] Contenu, Encoding Encodage) => await File.WriteAllLinesAsync(Localisation.NomComplet, Contenu, Encodage);

    /**
     * <summary>
     *   [FR] Ajoute une ligne à la fin d'un fichier
     *   [EN] Appends a line to the end of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu de la ligne à ajouter
     *   [EN] Contents of the line to append
     * </param>
     **/
    public static void AjouterLigne(FichierReference Localisation, string Contenu) => File.AppendAllText(Localisation.NomComplet, Contenu);

    /**
     * <summary>
     *   [FR] Ajoute une ligne à la fin d'un fichier de manière asynchrone
     *   [EN] Appends a line to the end of a file asynchronously
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu de la ligne à ajouter
     *   [EN] Contents of the line to append
     * </param>
     **/
    public async static Task AjouterLigneAsync(FichierReference Localisation, string Contenu) => await File.AppendAllTextAsync(Localisation.NomComplet, Contenu);

    /**
     * <summary>
     *   [FR] Ajoute une ligne à la fin d'un fichier
     *   [EN] Appends a line to the end of a file
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu de la ligne à ajouter
     *   [EN] Contents of the line to append
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'analyse du fichier
     *   [EN] The encoding to use when parsing the file
     * </param>
     **/
    public static void AjouterLigne(FichierReference Localisation, string Contenu, Encoding Encodage) => File.AppendAllText(Localisation.NomComplet, Contenu, Encodage);

    /**
     * <summary>
     *   [FR] Ajoute une ligne à la fin d'un fichier de manière asynchrone
     *   [EN] Appends a line to the end of a file asynchronously
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <param name="Contenu">
     *   [FR] Contenu de la ligne à ajouter
     *   [EN] Contents of the line to append
     * </param>
     * <param name="Encodage">
     *   [FR] L'encodage à utiliser lors de l'analyse du fichier
     *   [EN] The encoding to use when parsing the file
     * </param>
     **/
    public async static Task AjouterLigneAsync(FichierReference Localisation, string Contenu, Encoding Encodage) => await File.AppendAllTextAsync(Localisation.NomComplet, Contenu, Encodage);

    /**
     * <summary>
     *   [FR] Détermine si le nom de fichier donné existe ou non.
     *   [EN] Determines  whether the given filename exists.
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du fichier
     *   [EN] Location of the file
     * </param>
     * <returns>
     *   [FR] Vrai s'il existe, Sinon faux
     *   [EN] True if it exists, False otherwise
     * </returns>
     **/
    public static bool VerifieSiExiste(FichierReference Localisation) => File.Exists(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] compare deux noms d'objets du système de fichiers pour l'égalité. 
     *        Utilise la représentation du nom canonique,
     *        pas la représentation du nom d'affichage.
     *   [EN] compares two filesystem object names for equality. 
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
     *   [FR] Faux si les noms représentent le même objet, Vrai sinon
     *   [EN] False if the names represent the same object, True otherwise
     * </returns>
     **/
    public static bool operator !=(FichierReference ObjetA, FichierReference ObjetB) => !(ObjetA == ObjetB);

    /**
     * <summary>
     *   [FR] compare deux noms d'objets du système de fichiers pour l'égalité. 
     *        Utilise la représentation du nom canonique,
     *        pas la représentation du nom d'affichage.
     *   [EN] compares two filesystem object names for equality. 
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
     *   [FR] Vrai si les noms représentent le même objet, faux sinon
     *   [EN] True if the names represent the same object, false otherwise
     * </returns>
     **/
    public static bool operator ==(FichierReference ObjetA, FichierReference ObjetB) {

    #pragma warning disable IDE0041 // Utiliser la vérification 'is null'
      if ((object) ObjetA == null) {

        return (object) ObjetB == null;
      }
      else {

        return (object) ObjetB != null && ObjetA.NomComplet.Equals(ObjetB.NomComplet, Comparaison);
      }
    #pragma warning restore IDE0041 // Utiliser la vérification 'is null'
    }
  }

  /**
   * <summary>
   *   [FR] Méthodes d'extension pour la fonctionnalité FichierReference
   *   [EN] Extension methods for FichierReference functionality
   * </summary>
   **/
  public static class FichierReferenceMethodeDextension {

    /**
     * <summary>
     *   [FR] Sérialiser manuellement une référence de fichier à un flux binaire.
     *   [EN] Manually serialize a file reference to a binary stream.
     * </summary>
     * <param name="Redacteur">
     *   [FR] Redacteur binaire à écrire sur
     *   [EN] Binary Writer to Write to
     * </param>
     * <param name="Fichier">
     *   [FR] La référence du fichier à écrire
     *   [EN] The file reference to write
     * </param>
     **/
    public static void Ecrire(this BinaryWriter Redacteur, FichierReference Fichier) => Redacteur.Write((Fichier == null) ? string.Empty : Fichier.NomComplet);

    /**
     * <summary>
     *   [FR] Sérialiser manuellement une référence de fichier à un flux binaire.
     *   [EN] Manually serialize a file reference to a binary stream.
     * </summary>
     * <param name="Redacteur">
     *   [FR] Redacteur binaire à écrire sur
     *   [EN] Binary Writer to Write to
     * </param>
     * <param name="Fichier">
     *   [FR] La référence du fichier à écrire
     *   [EN] The file reference to write
     * </param>
     * <param name="FichierAIdUnique">
     *   [FR] La référence du fichier à écrire
     *   [EN] The file reference to write
     * </param>
     **/
    public static void Ecrire(this BinaryWriter Redacteur, FichierReference Fichier, Dictionary<FichierReference, int> FichierAIdUnique) {

      //int IdUnique;
      if (Fichier == null) {

        Redacteur.Write(-1);
      }
      else if (FichierAIdUnique.TryGetValue(Fichier, out int IdUnique)) {

        Redacteur.Write(IdUnique);
      }
      else {

        Redacteur.Write(FichierAIdUnique.Count);
        Redacteur.Ecrire(Fichier);
      }
    }

    /**
     * <summary>
     *   [FR] Désérialisation manuelle d'une référence de fichier à partir d'un flux binaire.
     *   [EN] Manually deserialize a file reference frome a binary stream.
     * </summary>
     * <param name="Lecteur">
     *   [FR] Lecteur binaire à lire à partir de
     *   [EN] Binary reader to read from
     * </param>
     * <returns>
     *   [FR] Nouvel objet FichierReference
     *   [EN] New FichierReference object
     * </returns>
     **/
    public static FichierReference LireLaReferenceDuFichier(this BinaryReader Lecteur) {

      string NomComplet = Lecteur.ReadString();
      return (NomComplet.Length == 0) ? null : new FichierReference(NomComplet, Aseptise.Aucun);
    }

    /**
     * <summary>
     *   [FR] Désérialise une référence de fichier, en utilisant une table de recherche pour éviter d'écrire le même nom plus d'une fois.
     *   [EN] Deserializes a file reference, using a lookup table to avoid writing the same name more than once.
     * </summary>
     * <param name="Lecteur">
     *   [FR] La source à lire à partir de
     *   [EN] The source to read from
     * </param>
     * <param name="FichierUnique">
     *   [FR] La source à lire à partir de
     *   [EN] The source to read from
     * </param>
     * <returns>
     *   [FR] Nouvel objet FichierReference
     *   [EN] New FichierReference object
     * </returns>
     **/
    public static FichierReference LireLaReferenceDuFichier(this BinaryReader Lecteur, List<FichierReference> FichierUnique) {

      int IdUnique = Lecteur.ReadInt32();
      if (IdUnique == -1) {

        return null;
      }
      else if (IdUnique < FichierUnique.Count) {

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