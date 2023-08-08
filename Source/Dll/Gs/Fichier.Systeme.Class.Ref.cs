/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Gs.Properties;

namespace Gs {

  /**
   * <summary>
   * [FR] Classe de base pour les objets du système de fichiers (fichiers ou répertoires).
   * [EN] Basic class for file system objects (files or directories).
   * </summary>
   **/
  [Serializable]
  public abstract class FichierSystemeReference {

    /**
     * <summary>
     * [FR] Le chemin vers cet objet.
     *      Stocké en tant que chemin absolu, avec des caractères de séparation préférés d'O/S, et pas de barre oblique pour les répertoires.
     * [EN] The path to this object.
     *      Stored as an absolute path, with preferred O/S separation characters, and no slash for directories.
     * </summary>
     **/
    public readonly string NomComplet;

    /**
     * <summary>
     * [FR] Le comparateur à utiliser pour les références de systèmes de fichiers
     * [EN] The comparator to use for file system references
     * </summary>
     **/
    public static readonly StringComparer Comparateur = StringComparer.OrdinalIgnoreCase;

    /**
     * <summary>
     * [FR] La comparaison à utiliser pour les références de systèmes de fichiers
     * [EN] The comparison to be used for file system references
     * </summary>
     **/
    public static readonly StringComparison Comparaison = StringComparison.OrdinalIgnoreCase;

    /**
     * <summary>
     * [FR] Vérifie si ce nom a l'extension donnée.
     * [EN] Checks if this name has the given extension.
     * </summary>
     * <param name="Extension">
     * [FR] L'extension à vérifier.
     * [EN] The extension to be checked.
     * </param>
     * <returns>
     * [FR] Vrai si ce nom a l'extension donnée, Sinon c'est faux.
     * [EN] True if this name has the given extension, otherwise it is false.
     * </returns>
     **/
    public bool PossedeLextension(string Extension) {

      if (Extension.Length > 0 && Extension[0] != '.') {

        return NomComplet.Length >= Extension.Length + 1 && NomComplet[NomComplet.Length - Extension.Length - 1] == '.' && NomComplet.EndsWith(Extension, Comparaison);
      }
      else {
        return NomComplet.EndsWith(Extension, Comparaison);
      }
    }

    /**
     * <summary>
     * [FR] Détermine si l'objet donné est dans ou sous le répertoire donné.
     * [EN] Determines if the given object is in or under the given directory.
     * </summary>
     * <param name="Autre">
     * [FR] Répertoire à vérifier par rapport à
     * [EN] Directory to be checked against
     * </param>
     * <returns>
     * [FR] Vrai si ce chemin est sous le répertoire donné.
     * [EN] True if this path is under the given directory.
     * </returns>
     **/
    public bool EstSousLeRepertoire(DossierReference Autre) => NomComplet.StartsWith(Autre.NomComplet, Comparaison) && (NomComplet.Length == Autre.NomComplet.Length 
      || NomComplet[Autre.NomComplet.Length] == DossierReference.Rs || Autre.EstLeRepertoireRacine());

    /**
     * <summary>
     * [FR] Recherche les fragments de chemin pour le nom donné.
     *      Seuls les fragments complets sont considérés comme une correspondance.
     * [EN] Searches the path fragments for the name given.
     *      Only complete fragments are considered a match.
     * </summary>
     * <param name="Nom">
     * [FR] Nom à vérifier pour
     * [EN] Name to check for
     * </param>
     * <param name="Offset">
     * [FR] Décalage à l'intérieur de la chaîne de caractères pour lancer la recherche.
     * [EN] Offset within the string to start the search.
     * </param>
     * <returns>
     * [FR] vrai si le nom donné se trouve dans le chemin d'accès
     * [EN] true if the name given is found wihtin the path.
     * </returns>
     **/
    public bool ContientLeNom(string Nom, int Offset) => ContientLeNom(Nom, Offset, NomComplet.Length - Offset);

    /**
     * <summary>
     * [FR] Recherche les fragments de chemin pour le nom donné.
     *      Seuls les fragments complets sont considérés comme une correspondance.
     * [EN] Searches the path fragments for the name given.
     *      Only complete fragments are considered a match.
     * </summary>
     * <param name="Nom">
     * [FR] Nom à vérifier pour
     * [EN] Name to check for
     * </param>
     * <param name="Offset">
     * [FR] Décalage à l'intérieur de la chaîne de caractères pour lancer la recherche.
     * [EN] Offset within the string to start the search.
     * </param>
     * <param name="Longueur">
     * [FR] Longueur de la sous-chaîne à rechercher.
     * [EN] Length of the substring to search.
     * </param>
     * <returns>
     * [FR] vrai si le nom donné se trouve dans le chemin d'accès
     * [EN] true if the name given is found wihtin the path.
     * </returns>
     **/
    public bool ContientLeNom(string Nom, int Offset, int Longueur) {

      /*
       * [FR] vérifiez que la chaîne de caractères à rechercher est au moins assez longue pour contenir une correspondance.
       * [EN] check that the character string to be searched is at least long enough to contain a match.
       */
      if (Longueur < Nom.Length) {

        return false;
      }

      /*
       * [FR] Trouver chaque occurence du nom dans la chaîne de caractères restante,
       *      puis testez s'il est entouré d'un séparateur de répertoire.
       * [EN] Find each occurence of the name within the remainig string,
       *      then test whether it's surrounded by directory separator. 
       */
      int IndexDeCorrespondance = Offset;
      /* 
       * [FR] Nous créons une boucle infinie.
       * [EN] We create an infinite loop. 
       */
      for (; ;) {

        /* 
         * [FR] Trouver le prochain événement.
         * [EN] Find the next occurrence. 
         */
        IndexDeCorrespondance = NomComplet.IndexOf(Nom, IndexDeCorrespondance, Offset + Longueur - IndexDeCorrespondance, Comparaison);
        if (IndexDeCorrespondance == -1) {

          return false;
        }

        /* 
         * [FR] vérifier si la sous-chaîne est un répertoire.
         * [EN] check if the substring is a directory. 
         */
        int IndexDeFinDeCorrespondance = IndexDeCorrespondance + Nom.Length;
        if (NomComplet[IndexDeCorrespondance - 1] == DossierReference.Rs && (IndexDeFinDeCorrespondance == NomComplet.Length || NomComplet[IndexDeFinDeCorrespondance] == DossierReference.Rs)) {

          return true;
        }

        /* 
         * [FR] Passez devant la chaîne qui ne correspond pas.
         * [EN] Move past the syring that didn't match. 
         */
        IndexDeCorrespondance += Nom.Length;
      }
    }

    /**
     * <summary>
     * [FR] Détermine si l'objet donné est sous le répertoire donné, dans un sous-dossier du nom donné.
     *      Utile pour masquer les répertoires par leur nom. 
     * [EN] Determines if the given object is under the given directory, within a subfolder of the given name.
     *      Useful for masking out directories by name.
     * </summary>
     * <param name="Nom">
     * [FR] Nom d'un sous-dossier à vérifier également pour
     * [EN] Name of a subfolder to also check for
     * </param>
     * <param name="RepertoireDeBase">
     * [FR] Répertoire de base à vérifier.
     * [EN] Base directory to check against.
     * </param>
     * <returns>
     * [FR] Vrai si le chemin est sous le répertoire donné.
     * [EN] True if the path is under the given directory.
     * </returns>
     **/
    public bool ContientLeNom(string Nom, DossierReference RepertoireDeBase) {

      /*
       * [FR] vérifiez que cela se trouve sous le répertoire de base
       * [EN] check that this is under the base directory
       */
      if (!EstSousLeRepertoire(RepertoireDeBase)) {

        return false;
      }
      else {

        return ContientLeNom(Nom, RepertoireDeBase.NomComplet.Length);
      }
    }

    /**
     * <summary>
     * [FR] Détermine si l'objet donné est sous le répertoire donné, dans un sous-dossier du nom donné.
     *      Utile pour masquer les répertoires par leur nom. 
     * [EN] Determines if the given object is under the given directory, within a subfolder of the given name.
     *      Useful for masking out directories by name.
     * </summary>
     * <param name="Noms">
     * [FR] Nom d'un sous-dossier à vérifier également pour
     * [EN] Name of a subfolder to also check for
     * </param>
     * <param name="RepertoireDeBase">
     * [FR] Répertoire de base à vérifier.
     * [EN] Base directory to check against.
     * </param>
     * <returns>
     * [FR] Vrai si le chemin est sous le répertoire donné.
     * [EN] True if the path is under the given directory.
     * </returns>
     **/
    public bool ContientTousLesNoms(IEnumerable<string> Noms, DossierReference RepertoireDeBase) {

      /*
       * [FR] vérifiez que cela se trouve sous le répertoire de base
       * [EN] check that this is under the base directory
       */
      if (!EstSousLeRepertoire(RepertoireDeBase)) {
        return false;
      }
      else {
        return Noms.Any(XObjet => ContientLeNom(XObjet, RepertoireDeBase.NomComplet.Length));
      }
    }

    /**
     * <summary>
     * [FR] crée un chemin relatif à partir du répertoire de base donné. 
     * [EN] creates a relative path from the given base directory.
     * </summary>
     * <param name="RepertoireDeBase">
     * [FR] Le répertoire pour créer un chemin relatif à partir de
     * [EN] The directory to create a relative path from
     * </param>
     * <returns>
     * [FR] Un chemin relatif à partir du répertoire donné.
     * [EN] A relative path from the given directory.
     * </returns>
     **/
    public string MarqueParRapportA(DossierReference RepertoireDeBase) {

      /*
       * [FR] Trouvez dans quelle mesure le chemin est commun aux deux chemins.
       *      Cette longueur n'inclut pas un caractère de séparateur de répertoire suiveur.
       * [EN] Find how much of the path is common between the two paths.
       *      This length does not include a trailing directory separator character.
       */
      int LongueurDuRepertoireCommun = -1;
      for (int Index = 0; ; Index++) {

        if (Index == NomComplet.Length) {

          /*
           * [FR] Les deux voies sont identiques. Renvoyez juste le caractère ".".
           * [EN] The two paths are identical. Just return the "." character.
           */
          if (Index == RepertoireDeBase.NomComplet.Length) {

            return ".";                                               //MLHIDE
          }

          /*
           * [FR] Vérifier si nous terminons sur un nom d'annuaire complet.
           * [EN] Check if we're finishing on a complete directory name.
           */
          if (RepertoireDeBase.NomComplet[Index] == DossierReference.Rs) {

            LongueurDuRepertoireCommun = Index;
          }
          break;
        }
        else if (Index == RepertoireDeBase.NomComplet.Length) {

          /*
           * [FR] Vérifiez si la fin du nom du répertoire coïncide avec une limite pour le nom actuel.
           * [EN] Check whether the end of the directory name coincides with a boundary for the current name.
           */
          if (NomComplet[Index] == DossierReference.Rs) {

            LongueurDuRepertoireCommun = Index;
          }
          break;
        }
        else {

          /*
           * [FR] Vérifiez que les deux chemins correspondent, et libérez les si ce n'est pas le cas.
           *      Augmenter la longueur du répertoire commun si nous avons atteint un séparateur.
           * [EN] Check the two paths match, and bail if they don't.
           *      Increase the common directory length if we've reached a separator.
           */
          if (String.Compare(NomComplet, Index, RepertoireDeBase.NomComplet, Index, 1, Comparaison) != 0) {

            break;
          }

          if (NomComplet[Index] == DossierReference.Rs) {

            LongueurDuRepertoireCommun = Index;
          }
        }
      }

      /*
       * [FR] S'il n'y a pas de chemin relatif, retournez simplement le chemin absolu.
       * [EN] If there's no relative path, just return the absolute path.
       */
      if (LongueurDuRepertoireCommun == -1) {

        return NomComplet;
      }

      /*
       * [FR] Ajoutez tous les séparateurs '..' pour revenir au répertoire commun, 
       *      Puis le reste de la chaîne de caractères pour atteindre l'élément cible.
       * [EN] Append all the '..' separators to get back to the common directory, 
       *      Then the rest of the string to reach the target item.
       */
      StringBuilder Resultat = new StringBuilder();
      for (int Index = LongueurDuRepertoireCommun + 1; Index < RepertoireDeBase.NomComplet.Length; Index++) {

        /*
         * [FR] Déplacer vers le haut d'un répertoire.
         * [EN] Move up a directory.
         */
        Resultat.Append("..");                                          //MLHIDE
        Resultat.Append(DossierReference.Rs);

        /*
         * [FR] Numériser vers le séparateur de répertoire suivant.
         * [EN] Scan to the next directory separator.
         */
        while (Index < RepertoireDeBase.NomComplet.Length && RepertoireDeBase.NomComplet[Index] != DossierReference.Rs) {

          Index++;
        }
      }

      if (LongueurDuRepertoireCommun + 1 < NomComplet.Length) {

        Resultat.Append(NomComplet, LongueurDuRepertoireCommun + 1, NomComplet.Length - LongueurDuRepertoireCommun - 1);
      }

      return Resultat.ToString();
    }

    /**
     * <summary>
     * [FR] Renvoie une représentation sous forme de chaîne de caractères de cet objet système de fichiers.
     * [EN] Returns a string representation of this filesystem object.
     * </summary>
     * <returns>
     * [FR] Chemin complet vers l'objet
     * [EN] Full path to the object
     * </returns>
     **/
    public override string ToString() => NomComplet;

    /**
     * <summary>
     * [FR] Constructeur direct pour un chemin.
     * [EN] Direct constructor for a path.
     * </summary>
     **/
    protected FichierSystemeReference(string NomAuComplet) => NomComplet = NomAuComplet;

    /**
     * <summary>
     * [FR] Constructeur direct pour un chemin.
     * [EN] Direct constructor for a path.
     * </summary>
     **/
    protected FichierSystemeReference(string NomAuComplet, string CanonicalName) => NomComplet = NomAuComplet;

    /**
     * <summary>
     * [FR] Créez un chemin complet en concaténant plusieurs chaînes de caractères.
     * [EN] Create a complete path by concatenating several strings.
     * </summary>
     * <returns></returns>
     **/
    protected static string CombinerLesChaines(DossierReference RepertoireDeBase, params string[] Fragments) {

      /*
       * [FR] Récupère la chaîne de caractères initiale à laquelle ajouter, 
       *      et supprimez le suffixe de n'importe quel répertoire racine. 
       * [EN] Retrieves the initial string to be added,
       *      and remove the suffix from any root directory.
       */
      StringBuilder NouveauNomComplet = new StringBuilder(RepertoireDeBase.NomComplet);

      if (NouveauNomComplet.Length > 0 && NouveauNomComplet[NouveauNomComplet.Length - 1] == DossierReference.Rs) {

        NouveauNomComplet.Remove(NouveauNomComplet.Length - 1, 1);
      }

      /*
       * [FR] Scanner les fragments pour les ajouter, 
       *      les ajouter à une chaîne de caractères et mettre à jour la longueur de la base au fur et à mesure
       * [EN] Scan the fragments to add them, 
       *      add them to a string and update the length of the database as you go along.
       */
      foreach (string Fragment in Fragments) {

        /*
         * [FR] Vérifier si ce fragment est un chemin absolu.
         * [EN] Check if this fragment is an absolute path.
         */
        if ((Fragment.Length >= 2 && Fragment[1] == ':') || (Fragment.Length >= 1 && (Fragment[0] == '\\' || Fragment[0] == '/'))) {

          /*
           * [FR] Ça l'est. Réinitialisez le nouveau nom à la version complète de ce chemin.
           * [EN] It is. Reset the new name to the full version of this path.
           */
          NouveauNomComplet.Clear();
          NouveauNomComplet.Append(Path.GetFullPath(Fragment).TrimEnd(DossierReference.Rs));
        } 
        else {

          /*
           * [FR] Ajoute toutes les parties de ce fragment à la fin du chemin existant.
           * [EN] Adds all parts of this fragment to the end of the existing path.
           */
          int IndexDeDepart = 0;
          while (IndexDeDepart < Fragment.Length) {

            /*
             * [FR] Trouvez la fin de ce fragment.
             *      Il se peut que nous ayons passé plusieurs chemins dans la même chaîne de caractères.
             * [EN] Find the end of this fragment.
             *      It is possible that we have passed several paths in the same string.
             */
            int IndexDeFin = IndexDeDepart;
            while (IndexDeFin < Fragment.Length && Fragment[IndexDeFin] != '\\' && Fragment[IndexDeFin] != '/') {

              IndexDeFin++;
            }

            /*
             * [FR] Ignorez les sections vides,
             *      comme les slashes de début ou de fin, et les références aux répertoires '.'.
             * [EN] Ignore empty sections,
             *      such as start or end slashes, and '.' directory references.
             */
            int Longueur = IndexDeFin - IndexDeDepart;
            if (Longueur == 0) {

              /*
               * [FR] Séparateurs de répertoires multiples dans une rangée; illégal.
               * [EN] Multiple directory separators in a row; illegal.
               */
              throw new ArgumentException(String.Format(Resources.RepertoiresSeparateursInvalides, Fragment));
            } 
            else if (Longueur == 2 && Fragment[IndexDeDepart] == '.' && Fragment[IndexDeDepart + 1] == '.') {

              /*
               * [FR] Supprimer le dernier nom de répertoire.
               * [EN] Delete the last directory name.
               */
              for (int SeparateurDindex = NouveauNomComplet.Length - 1; SeparateurDindex >= 0; SeparateurDindex--) {

                if (NouveauNomComplet[SeparateurDindex] == DossierReference.Rs) {

                  NouveauNomComplet.Remove(SeparateurDindex, NouveauNomComplet.Length - SeparateurDindex);
                  break;
                }
              }
            } 
            else if (Longueur != 1 || Fragment[IndexDeDepart] != '.') {
              /*
               * [FR] Ajouter ce fragment
               * [EN] Add this fragment
               */
              NouveauNomComplet.Append(DossierReference.Rs);
              NouveauNomComplet.Append(Fragment, IndexDeDepart, Longueur);
            }

            /*
             * [FR] Passer à la partie suivante.
             * [EN] Proceed to the next part
             */
            IndexDeDepart = IndexDeFin + 1;
          }
        }
      }

      if (NouveauNomComplet.Length == 0 || (NouveauNomComplet.Length == 2 && NouveauNomComplet[1] == ':')) {

        NouveauNomComplet.Append(DossierReference.Rs);
      }

      return NouveauNomComplet.ToString();
    }
  }
}