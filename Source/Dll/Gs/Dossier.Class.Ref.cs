/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using System.IO;
using Gs.Enumeration;

namespace Gs {

  /**
   * <summary>
   *   [FR] Représentation d'un chemin de répertoire absolu.
   *        Permet un hachage et des comparaisons rapides.
   *   [EN] Representation of an absolute directory path. 
   *        Allows fast hashing and comparisons.
   * </summary>
   **/
  [Serializable]
  public class DossierReference: FichierSystemeReference, IEquatable<DossierReference> {

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
    public DossierReference(string Chemins) : base (SeparateurDeVoieDeFuite(Path.GetFullPath(Chemins))) {
    }

    /**
     * <summary>
     *   [FR] Construire une DossierReference à partir d'un objet DirectoryInfo.
     *   [EN] Construct a DossierReference from a DirectoryInfo object.
     * </summary>
     * <param name="InfoRepertoire">
     *   [FR] Chemin vers ce fichier.
     *   [EN] Path to this file.
     * </param>
     **/
    public DossierReference(DirectoryInfo InfoRepertoire) : base (SeparateurDeVoieDeFuite(InfoRepertoire.FullName)) {
    }

    /**
     * <summary>
     *   [FR] Constructeur pour créer un objet répertoire directement à partir de deux chaînes de caractères.
     *   [EN] Constructor for createing a directory object directly from two strings.
     * </summary>
     * <param name="NomAuComplet">
     *   [FR] Le nom complet et aseptisé le chemin d'accès.
     *   [EN] The full, Desinfecterd path name.
     * </param>
     * <param name="Assainisseur">
     *   [FR] L'argument factice utilisé pour résoudre cette surcharge.
     *   [EN] Dummy argument used to resolve this overload.
     * </param>
     **/
    public DossierReference(string NomAuComplet, Desinfecter Assainisseur) : base (NomAuComplet) {
    }

    /**
     * <summary>
     *   [FR] Récupère le répertoire contenant cet objet.
     *   [EN] Gets the directory containing this object.
     * </summary>
     * <returns>
     *   [FR] Un nouvel objet répertoire représentant le répertoire contenant cet objet.
     *   [EN] A new directory object representing the directory containing this object.
     * </returns>
     **/
    public DossierReference RepertoireParrent {

      get {

        if (EstLeRepertoireRacine()) {

          return null;
        }

        int LongueurDuParent = NomComplet.LastIndexOf(RepertoireSeparateur);
        if (LongueurDuParent == 2 && NomComplet[1] == ':') {

          LongueurDuParent++;
        }

        return new DossierReference(NomComplet.Substring(0, LongueurDuParent), Desinfecter.Aucun);
      }
    }

    /**
     * <summary>
     *   [FR] Obtient le nom du répertoire des niveaux supérieurs.
     *   [EN] Gets the top levels directory name.
     * </summary>
     * <returns>
     *   [FR] Le nom du répertoire.
     *   [EN] The name of the directory.
     * </returns>
     **/
    public string ObtenirLeNomDuRepertoire() => Path.GetFileName(NomComplet);

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
    public bool EstLeRepertoireRacine() => NomComplet[NomComplet.Length - 1] == RepertoireSeparateur;

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
    public bool Equals(DossierReference Objet) => Objet == this;

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
    public override bool Equals(object Objet) => (Objet is DossierReference) && ((DossierReference)Objet) == this;

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
     *   [FR] Récupère le répertoire parent d'un fichier
     *   [EN] Gets the parent directory for a file
     * </summary>
     * <param name="Fichier">
     *   [FR] Le fichier à récupérer dans le répertoire pour
     *   [EN] The file to get directory for
     * </param>
     * <returns>
     *   [FR] Le nom complet du répertoire contenant le fichier donné
     *   [EN] The full directory name containig the given file
     * </returns>
     **/
    public static DossierReference ObtenirLeRepertoireParrent(FichierReference Fichier) {
      
      int LongueurDuParent = Fichier.NomComplet.LastIndexOf(RepertoireSeparateur);
      //int ParrentLength = Fichier.NomComplet.LastIndexOf(DirSepChar);
      if (LongueurDuParent == 2 && Fichier.NomComplet[1] == ':') {

        LongueurDuParent++;
      }

      return new DossierReference(Fichier.NomComplet.Substring(0, LongueurDuParent), Desinfecter.Aucun);
    }

    /**
     * <summary>
     *   [FR] Récupère le chemin d'accès d'un dossier spécial
     *   [EN] Gets the path for a special folder
     * </summary>
     * <param name = "Dossier" >
     *   [FR] Le dossier pour recevoir le chemin d'accès pour
     *   [EN] The folder to receive the path for
     * </param>
     * <returns>
     *   [FR] Référence du répertoire pour le dossier donné, ou nulle si elle n'est pas disponible
     *   [EN] Directory reference for the given folder, or null if it is not available
     * </returns>
     **/
    public static DossierReference ObtenirLeDossierSpecial(Environment.SpecialFolder Dossier) {

      string CheminsDuDossier = Environment.GetFolderPath(Dossier);
      return String.IsNullOrEmpty(CheminsDuDossier) ? null : new DossierReference(CheminsDuDossier);
    }

    /**
     * <summary>
     *   [FR] Combiner plusieurs fragments avec un répertoire de base,
     *        à partir d'un nouveau nom de répertoire
     *   [EN] Combine several fragments with a base directory,
     *        to from a new directory name
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
     *   [FR] Le nouveau nom du répertoire
     *   [EN] The new name of the directory
     * </returns>
     **/
    public static DossierReference Combiner(DossierReference RepertoireDeBase, params string[] Fragments) {

      string NomComplet = FichierSystemeReference.CombinerLesChaines(RepertoireDeBase, Fragments);
      return new DossierReference(NomComplet, Desinfecter.Aucun);
    }

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
    public static DossierReference MarquerADistance(string CheminAbsolu) => new DossierReference(CheminAbsolu, Desinfecter.Aucun);

    /**
     * <summary>
     *   [FR] Trouve le répertoire courant
     *   [EN] Finds the current directory
     * </summary>
     * <returns>
     *   [FR] Le répertoire courant
     *   [EN] The current directory
     * </returns>
     **/
    public static DossierReference ObtenirLeRepertoireActuel() => new DossierReference(Directory.GetCurrentDirectory());

    /**
     * <summary>
     *   [FR] Récupère le répertoire parent d'un fichier, ou retourne null s'il est nul.
     *   [EN] Gets the parent directory for a file, or returns null if it's null.
     * </summary>
     * <param name="Fichier">
     *   [FR] Le fichier pour créer une référence de répertoire pour
     *   [EN] The file to create a directory reference for
     * </param>
     * <returns>
     *   [FR] Le répertoire contenant le fichier
     *   [EN] The directory containig the file
     * </returns>
     **/
    #pragma warning disable IDE0031 // Utiliser la propagation de valeurs null
    public static DossierReference DepuisLeFichier(FichierReference Fichier) => (Fichier == null) ? null : Fichier.Repertoire;
    #pragma warning restore IDE0031 // Utiliser la propagation de valeurs null

    /**
     * <summary>
     *   [FR] Énumérer les fichiers d'un répertoire donné
     *   [EN] Enumerate files from a given directory
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base pour la recherche dans
     *   [EN] Base directory to search in
     * </param>
     * <returns>
     *   [FR] Séquence des références des fichiers
     *   [EN] Sequence of file references
     * </returns>
     **/
    public static IEnumerable<FichierReference> FichiersEnumerer(DossierReference RepertoireDeBase)
    {

      foreach (string NomDeFichier in Directory.EnumerateDirectories(RepertoireDeBase.NomComplet))
      {

        yield return new FichierReference(NomDeFichier, Aseptise.Aucun);
      }
    }

    /**
     * <summary>
     *   [FR] Énumérer les fichiers d'un répertoire donné
     *   [EN] Enumerate files from a given directory
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base pour la recherche dans
     *   [EN] Base directory to search in
     * </param>
     * <param name="Modele">
     *   [FR] Modèle pour les fichiers correspondants
     *   [EN] Pattern for matching files
     * </param>
     * <returns>
     *   [FR] Séquence des références des fichiers
     *   [EN] Sequence of file references
     * </returns>
     **/
    public static IEnumerable<FichierReference> FichiersEnumerer(DossierReference RepertoireDeBase, string Modele)
    {

      foreach (string NomDeFichier in Directory.EnumerateDirectories(RepertoireDeBase.NomComplet, Modele))
      {

        yield return new FichierReference(NomDeFichier, Aseptise.Aucun);
      }
    }

    /**
     * <summary>
     *   [FR] Énumérer les fichiers d'un répertoire donné
     *   [EN] Enumerate files from a given directory
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base pour la recherche dans
     *   [EN] Base directory to search in
     * </param>
     * <param name="Modele">
     *   [FR] Modèle pour les fichiers correspondants
     *   [EN] Pattern for matching files
     * </param>
     * <param name="Option">
     *   [FR] Option pour la recherche
     *   [EN] Option for the search
     * </param>
     * <returns>
     *   [FR] Séquence des références des fichiers
     *   [EN] Sequence of file references
     * </returns>
     **/
    public static IEnumerable<FichierReference> FichiersEnumerer(DossierReference RepertoireDeBase, string Modele, SearchOption Option)
    {

      foreach (string NomDeFichier in Directory.EnumerateDirectories(RepertoireDeBase.NomComplet, Modele, Option))
      {

        yield return new FichierReference(NomDeFichier, Aseptise.Aucun);
      }
    }

    /**
     * <summary>
     *   [FR] Énumérer un sous-répertoire dans un répertoire donné
     *   [EN] Enumerate subdirectory in a given directory
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base pour la recherche dans
     *   [EN] Base directory to search in
     * </param>
     * <returns>
     *   [FR] Séquence des références de répertoire
     *   [EN] Sequence of directory references 
     * </returns>
     **/
    public static IEnumerable<DossierReference> RepertoireEnumerer(DossierReference RepertoireDeBase)
    {

      foreach (string NomDeRepertoire in Directory.EnumerateDirectories(RepertoireDeBase.NomComplet))
      {

        yield return new DossierReference(NomDeRepertoire, Desinfecter.Aucun);
      }
    }

    /**
     * <summary>
     *   [FR] Énumérer un sous-répertoire dans un répertoire donné
     *   [EN] Enumerate subdirectory in a given directory
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base pour la recherche dans
     *   [EN] Base directory to search in
     * </param>
     * <param name="Modele">
     *   [FR] Modèle pour les répertoire correspondants
     *   [EN] Pattern for matching directory
     * </param>
     * <returns>
     *   [FR] Séquence des références de répertoire
     *   [EN] Sequence of directory references 
     * </returns>
     **/
    public static IEnumerable<DossierReference> RepertoireEnumerer(DossierReference RepertoireDeBase, string Modele)
    {

      foreach (string NomDeRepertoire in Directory.EnumerateDirectories(RepertoireDeBase.NomComplet, Modele))
      {

        yield return new DossierReference(NomDeRepertoire, Desinfecter.Aucun);
      }
    }

    /**
     * <summary>
     *   [FR] Énumérer un sous-répertoire dans un répertoire donné
     *   [EN] Enumerate subdirectory in a given directory
     * </summary>
     * <param name="RepertoireDeBase">
     *   [FR] Répertoire de base pour la recherche dans
     *   [EN] Base directory to search in
     * </param>
     * <param name="Modele">
     *   [FR] Modèle pour les fichiers correspondants
     *   [EN] Pattern for matching files
     * </param>
     * * <param name="Option">
     *   [FR] Option pour la recherche
     *   [EN] Option for the search
     * </param>
     * <returns>
     *   [FR] Séquence des références de répertoire
     *   [EN] Sequence of directory references 
     * </returns>
     **/
    public static IEnumerable<DossierReference> RepertoireEnumerer(DossierReference RepertoireDeBase, string Modele, SearchOption Option)
    {

      foreach (string NomDeRepertoire in Directory.EnumerateDirectories(RepertoireDeBase.NomComplet, Modele, Option))
      {

        yield return new DossierReference(NomDeRepertoire, Desinfecter.Aucun);
      }
    }

    /**
     * <summary>
     *   [FR] Créer un répertoire
     *   [EN] Create a directory
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du répertoire
     *   [EN] Location of the directory
     * </param>
     **/
    public static void CreerLeRepertoire(DossierReference Localisation) => Directory.CreateDirectory(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] Supprimer un répertoire
     *   [EN] Delete a directory
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du répertoire
     *   [EN] Location of the directory
     * </param>
     **/
    public static void Supprimer(DossierReference Localisation) => Directory.Delete(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] Supprimer un répertoire
     *   [EN] Delete a directory
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du répertoire
     *   [EN] Location of the directory
     * </param>
     * <param name="Recursive">
     *   [FR] si aux répertoires distants récursivement
     *   [EN] whether to remote directories recursively
     * </param>
     **/
    public static void Supprimer(DossierReference Localisation, bool Recursive) => Directory.Delete(Localisation.NomComplet, Recursive);

    /**
     * <summary>
     *   [FR] Vérifie si le répertoire existe
     *   [EN] Checks wether the directory exists
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du répertoire
     *   [EN] Location of the directory
     * </param>
     * <retruns>
     *   [FR] Vrai si ce répertoire existe
     *   [EN] True if this directory exists
     * </retruns>
     **/
    public static void VerifieSiExiste(DossierReference Localisation) => Directory.Exists(Localisation.NomComplet);

    /**
     * <summary>
     *   [FR] Définit le répertoire actuel
     *   [EN] Sets the Current Directory
     * </summary>
     * <param name="Localisation">
     *   [FR] Emplacement du nouveau répertoire actuel
     *   [EN] Location of the new current directory
     * </param>
     **/
    public static void DefinirLeRepertoireActuel(DossierReference Localisation) => Directory.SetCurrentDirectory(Localisation.NomComplet);

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
    public static bool operator ==(DossierReference ObjetA, DossierReference ObjetB) {
    #pragma warning disable IDE0041 // Utiliser la vérification 'is null'
      if ((object) ObjetA == null) {

        return (object) ObjetB == null;
      } 
      else {

        return (object) ObjetB != null && ObjetA.NomComplet.Equals(ObjetB.NomComplet, Comparaison);
      }
    #pragma warning restore IDE0041 // Utiliser la vérification 'is null'
    }

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
    public static bool operator !=(DossierReference ObjetA, DossierReference ObjetB) => !(ObjetA == ObjetB);

    /**
     * <summary>
     *   [FR] S'assure que le séparateur de chemin de fuite correct est ajouté. Sous Windows, le répertoire racine (par exemple C:\) a toujours un séparateur de chemin de fuite,
     *        mais aucun autre chemin ne le fait.
     *   [EN] Ensure that the correct escape route separator is added. Under Windows, the root directory (e. g. C:\) still has a escape path separator,
     *        but no other path does.
     * </summary>
     * <param name="NomDuChemin">
     *   [FR] Chemin d'accès absolu au répertoire.
     *   [EN] Absolute path to the directory.
     * </param>
     * <returns>
     *   [FR] Chemin d'accès au répertoire, avec le séparateur de chemin correct.
     *   [EN] Path to the directory, with the correct path separator.
     * </returns>
     **/
    private static string SeparateurDeVoieDeFuite(string NomDuChemin) {

      if (NomDuChemin.Length == 2 && NomDuChemin[1] == ':') {

        return NomDuChemin + RepertoireSeparateur;
      }
      else if (NomDuChemin.Length == 3 && NomDuChemin[1] == ':' && NomDuChemin[2] == RepertoireSeparateur) {

        return NomDuChemin;
      }
      else if (NomDuChemin.Length > 1 && NomDuChemin[NomDuChemin.Length - 1] == RepertoireSeparateur) {

        return NomDuChemin.TrimEnd(RepertoireSeparateur);
      }
      else {

        return NomDuChemin;
      }
    }
  }

  /**
   * <summary>
   *   [FR] méthodes d'extension pour passer des arguments DossierReference
   *   [EN] extension methods for passing  DossierReference arguments
   * </summary>
   **/
  public static class DossierReferenceMethodeDextension {

    /**
     * <summary>
     *   [FR] Désérialisation manuelle d'une référence de répertoire à partir d'un flux binaire.
     *   [EN] Manualy deserialize a directory reference from a binary stream.
     * </summary>
     * <param name="Lecteur">
     *   [FR] Lecteur binaire à lire à partir de
     *   [EN] Binary reader to read from
     * </param>
     * <returns>
     *   [FR] Nouvel objet DossierReference
     *   [EN] New DossierReference object
     * </returns>
     **/
    public static DossierReference ConsulterLeDossierReference(this BinaryReader Lecteur) {

      string NomComplet = Lecteur.ReadString();
      return (NomComplet.Length == 0) ? null : new DossierReference(NomComplet, Desinfecter.Aucun);
    }

    /**
     * <summary>
     *   [FR] Sérialiser manuellement une référence de fichier à un flux binaire.
     *   [EN] Manually serialize a file reference to a binary stream.
     * </summary>
     * <param name="Redacteur">
     *   [FR] Redacteur binaire à écrire sur
     *   [EN] Binary Writer to Write to
     * </param>
     * <param name="Repertoire">
     *   [FR] la référence du répertoire à écrire
     *   [EN] The directory reference  to write
     * </param>
     **/
    public static void Ecrire(this BinaryWriter Redacteur, DossierReference Repertoire) => Redacteur.Write((Repertoire == null) ? string.Empty : Repertoire.NomComplet);
  }
}