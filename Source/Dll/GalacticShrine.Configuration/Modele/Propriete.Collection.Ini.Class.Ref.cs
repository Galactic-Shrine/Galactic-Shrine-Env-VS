/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System.Collections;
using System.Collections.Generic;
using GalacticShrine.Enumeration.Configuration;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Modele.Configuration.Ini {

  /**
   * <summary>
   *   [FR] Représente une collection de données de clé.<br/>
   *   [EN] Represents a collection of key data.
   * </summary>
   **/
  public class ProprieteCollection : ClonableInterface<ProprieteCollection>, IEnumerable<Propriete> {

    /**
     * <summary>
     *   [FR] Collection de propriétés pour une section donnée<br/>
     *   [EN] Collection of property for a given section
     * </summary>
     **/
    private readonly Dictionary<string, Propriete> Proprietes;

    private readonly IEqualityComparer<string> RechercherComparer;

    private Propriete DernierAjout;

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> class.
     * </summary>
     **/
    public ProprieteCollection() : this(RechercherComparer: EqualityComparer<string>.Default) { }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> avec un comparateur de recherche donné.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> class with a given search comparator.
     * </summary>
     * <param name="RechercherComparer">
     *   [FR] Comparateur de recherche utilisé pour trouver la clé par nom dans la collection.<br/>
     *   [EN] Search comparator used to find the key by name in the collection.
     * </param>
     **/
    public ProprieteCollection(IEqualityComparer<string> RechercherComparer) {

      this.RechercherComparer = RechercherComparer;
      Proprietes = new Dictionary<string, Propriete>(comparer: this.RechercherComparer);
    }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> à partir de l'instance précédente.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> class from its previous instance.
     * </summary>
     * <remarks>
     *   [FR] Les données de l'instance originale de collecte des données clés sont copiées en profondeur.<br/>
     *   [EN] Data from the original key data collection instance is copied in depth.
     * </remarks>
     * <param name="ProprieteCollectionInstance">
     *   [FR] L'instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> utilisée pour créer la nouvelle instance.<br/>
     *   [EN] The <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> class instance used to create the new instance.
     * </param>
     **/
    public ProprieteCollection(ProprieteCollection ProprieteCollectionInstance, IEqualityComparer<string> RechercherComparer) : this(RechercherComparer: RechercherComparer) {

      foreach(Propriete Propriete in ProprieteCollectionInstance) {

        if(Proprietes.ContainsKey(key: Propriete.Cle)) {

          Proprietes[key: Propriete.Cle] = Propriete.CloneEnProfondeur();
        }
        else {

          Proprietes.Add(key: Propriete.Cle, value: Propriete.CloneEnProfondeur());
        }
      }
    }

    /**
     * <summary>
     *   [FR] Obtient ou définit la valeur d'une propriété.<br/>
     *   [EN] Gets or sets the value of a property.
     * </summary>
     * <remarks>
     *   [FR] Si nous essayons d'assigner la valeur d'une propriété qui n'existe pas, une nouvelle propriété est ajoutée avec le nom et la valeur spécifiés.<br/>
     *   [EN] If we try to assign the value of a property that doesn't exist, a new property is added with the specified name and value.
     * </remarks>
     * <param name="NomDeLaCle">
     *   [FR] Clé de la propriété.<br/>
     *   [EN] key of the property.
     * </param>
     **/
    public string this[string NomDeLaCle] {

      get {

        if(Proprietes.ContainsKey(key: NomDeLaCle)) {

          return Proprietes[key: NomDeLaCle].Valeur;
        }

        return null;
      }

      set {

        if(!Proprietes.ContainsKey(key: NomDeLaCle)) {

          Ajouter(Cle: NomDeLaCle);
        }

        Proprietes[key: NomDeLaCle].Valeur = value;
      }
    }

    /**
     * <summary>
     *   [FR] Renvoie le nombre de clés dans la collection.<br/>
     *   [EN] Returns the number of keys in the collection.
     * </summary>
     **/
    public int Compter => Proprietes.Count;

    /**
     * <summary>
     *   [FR] Ajoute une propriété sans vérifier si elle est déjà contenue dans le dictionnaire.<br/>
     *   [EN] Adds a property without checking whether it is already contained in the dictionary.
     * </summary>
     **/
    internal void AjouterUneProprieteInterne(Propriete Proprietes) {

      DernierAjout = Proprietes;
      this.Proprietes.Add(key: Proprietes.Cle, value: Proprietes);
    }

    /**
     * <summary>
     *   [FR] Ajoute une nouvelle clé avec le nom spécifié, une valeur vide et des commentaires.<br/>
     *   [EN] Adds a new key with the specified name, an empty value and comments.
     * </summary>
     * <param name="Cle">
     *   [FR] Nouvelle clé à ajouter.<br/>
     *   [EN] New key to be added.
     * </param>
     * <returns>
     *   [FR] Vrai (true) si la clé a été ajoutée false si une clé portant le même nom existe déjà dans la collection.<br/>
     *   [EN] true if the key has been added false if a key with the same name already exists in the collection.
     * </returns>
     **/
    public bool Ajouter(string Cle) {

      if(!Proprietes.ContainsKey(Cle)) {

        AjouterUneProprieteInterne(Proprietes: new Propriete(Cle: Cle, Valeur: string.Empty));
        return true;
      }

      return false;
    }

    /**
     * <summary>
     *   [FR] Ajoute une nouvelle propriété à la collection avec la clé et la valeur spécifiées.<br/>
     *   [EN] Adds a new property to the collection with the specified key and value.
     * </summary>
     * <param name="Cle">
     *   [FR] clé de la nouvelle propriété à ajouter.<br/>
     *   [EN] key of the new property to be added.
     * </param>
     * <param name="Valeur">
     *   [FR] clé de la nouvelle propriété à ajouter.<br/>
     *   [EN] key of the new property to be added.
     * </param>
     * <returns>
     *   [FR] Vrai (true) si la propriété a été ajoutée, false si une clé portant le même nom existe déjà dans la collection.<br/>
     *   [EN] True if the property has been added, false if a key with the same name already exists in the collection.
     * </returns>
     **/
    public bool Ajouter(string Cle, string Valeur) {

      if(!Proprietes.ContainsKey(key: Cle)) {

        AjouterUneProprieteInterne(Proprietes: new Propriete(Cle: Cle, Valeur: Valeur));
        return true;
      }

      return false;
    }

    /**
     * <summary>
     *   [FR] Ajoute une nouvelle propriété à la collection.<br/>
     *   [EN] Adds a new property to the collection.
     * </summary>
     * <param name="Propriete">
     *   [FR] Instance de propriété.<br/>
     *   [EN] Property instance.
     * </param>
     * <returns>
     *   [FR] Vrai (true) si la propriété a été ajoutée false si une propriété avec la même clé existe déjà dans la collection.<br/>
     *   [EN] True if the property has been added false if a property with the same key already exists in the collection.
     * </returns>
     **/
    public bool Ajouter(Propriete Propriete) {

      if(!Proprietes.ContainsKey(key: Propriete.Cle)) {

        AjouterUneProprieteInterne(Proprietes: Propriete);
        return true;
      }

      return false;
    }
    /*public bool Ajouter(Ajout Ajout, string Cle = null, string Valeur = null, Propriete Propriete = null) {

      switch(Ajout) {

        case Ajout.Cle:

          if(!string.IsNullOrEmpty(Valeur)) {

            if(!Proprietes.ContainsKey(Cle)) {

              AjouterUneProprieteInterne(new Propriete(Cle, string.Empty));
              return true;
            }
          }

          return false;

        case Ajout.CleEtValeur:

          if(!string.IsNullOrEmpty(Valeur)) {

            if(!Proprietes.ContainsKey(Cle)) {

              AjouterUneProprieteInterne(new Propriete(Cle, Valeur));
              return true;
            }
          }

          return false;

        case Ajout.Propriete:

          if(!Proprietes.ContainsKey(Propriete.Cle)) {

            AjouterUneProprieteInterne(Propriete);
            return true;
          }

          return false;

        default: return false;
      }
    }*/

    /**
   * <summary>
   *   [FR] Fonction de suppression.<br/>
   *   [EN] Deletion function.
   * </summary>
   * <remarks>
   *   [FR] Utiliser par défaut: <see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>.<br/>
   *   [EN] Use by default: <see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>.
   * </remarks>
   * <param name="Effacement">
   *   [FR] Permet de passer au type de suppression souhaité.<br/>
   *   [EN] Switches to the desired type of deletion.
   * </param>
   * <value>
   *   [FR] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>: Efface toutes les propriétés de cette collection.</para>
   *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/>: Efface tous les commentaires de cette section.</para>
   *   [EN] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>: Deletes all properties in this collection.</para>
   *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/>: Deletes all comments in this section.</para>
   * </value>
   **/
    public void Effacer(Effacement Effacement = Effacement.Proprietes) {

      switch(Effacement) {

        case Effacement.Commentaires:

          foreach(Propriete DonneesDeCle in this)
            DonneesDeCle.Commentaire.Clear();
          break;


        case Effacement.Proprietes:

          Proprietes.Clear();
          break;
      }
    }

    /**
     * <summary>
     *   [FR] Efface une clé existante, ainsi que les données qui y sont associées.<br/>
     *   [EN] Deletes an existing key and its associated data.
     * </summary>
     * <param name="NomDeLaCle">
     *   [FR] La clé à retirer.<br/>
     *   [EN] The key to remove.
     * </param>
     * <returns>
     *   [FR] Vrai(true) si une clé portant le nom spécifié a été supprimée, sinon faux(false).<br/>
     *   [EN] True if a key with the specified name has been deleted, otherwise false.
     * </returns>
     **/
    public bool Effacer(string NomDeLaCle) => Proprietes.Remove(key: NomDeLaCle);

    /**
     * <summary>
     *   [FR] Obtient si une propriété spécifiée avec le nom de clé donné existe dans la collection.<br/>
     *   [EN] Gets whether a property specified with the given key name exists in the collection.
     * </summary>
     * <param name="NomDeLaCle">
     *   [FR] La clé pour la recherche.<br/>
     *   [EN] The key to search.
     * </param>
     * <returns>
     *   [FR] Vrai(true) si un bien avec le nom donné existe dans la collection, sinon faux(false).<br/>
     *   [EN] True if a good with the given name exists in the collection, otherwise false.
     * </returns>
     **/
    public bool Contient(string NomDeLaCle) => Proprietes.ContainsKey(key: NomDeLaCle);

    /**
     * <summary>
     *   [FR] Récupère les données d'une clé spécifiée en fonction de son nom.<br/>
     *   [EN] Retrieves data from a specified key based on its name.
     * </summary>
     * <param name="NomDeLaCle">
     *   [FR] Nom de la clé à récupérer.<br/>
     *   [EN] Name of the key to be recovered.
     * </param>
     * <returns>
     *   [FR] Une instance <see cref="GalacticShrine.Modele.Configuration.Ini.Propriete"/> contenant les informations relatives à la clé ou <c>null</c> si la clé n'a pas été trouvée.<br/>
     *   [EN] A <see cref="GalacticShrine.Modele.Configuration.Ini.Propriete"/> instance containing information about the key or <c>null</c> if the key was not found.
     * </returns>
     **/
    public Propriete RechercheParCle(string NomDeLaCle) {

      if(Proprietes.ContainsKey(key: NomDeLaCle))
        return Proprietes[key: NomDeLaCle];

      return null;
    }

    /**
     * <summary>
     *   [FR] Fusionne les autres propriétés,<br/>
     *        en ajoutant de nouvelles propriétés si elles n'existent pas déjà,<br/>
     *        ou en remplaçant les valeurs si elles existent déjà.<br/>
     *   [EN] Merges other properties into it,<br/>
     *        adding new properties if they didn't already exist,<br/>
     *        or replacing values if they did.
     * </summary>
     * <remarks>
     *   [FR] Les commentaires sont également fusionnés, mais ils sont toujours ajoutés et non écrasés..<br/>
     *   [EN] Comments are also merged, but are still added and not overwritten.
     * </remarks>
     **/
    public void Fusionner(ProprieteCollection ProprieteA_Fusionner) {

      foreach(var donneesDeCles in ProprieteA_Fusionner) {

        Ajouter(Cle: donneesDeCles.Cle);
        this[NomDeLaCle: donneesDeCles.Cle] = donneesDeCles.Valeur;
        RechercheParCle(NomDeLaCle: donneesDeCles.Cle).Commentaire.AddRange(collection: donneesDeCles.Commentaire);
      }
    }

    internal Propriete ObtenirLeDernier() => DernierAjout;

    /**
     * <summary>
     *   [FR] Crée un nouvel objet qui est une copie de l'instance actuelle.<br/>
     *   [EN] Creates a new object that is a copy of the current instance.
     * </summary>
     * <remarks>
     *   [FR] Un nouvel objet qui est une copie de cette instance.<br/>
     *   [EN] A new object that is a copy of this instance.
     * </remarks>
     **/
    public ProprieteCollection CloneEnProfondeur() => new (ProprieteCollectionInstance: this, RechercherComparer: RechercherComparer);

    /**
     * <summary>
     *   [FR] Permet l'itération dans la collection.<br/>
     *   [EN] Allows iteration througt the collection.
     * </summary>
     * <remarks>
     *   [FR] Un IEnumerator à type fort.<br/>
     *   [EN] A strong-typed IEnumerator.
     * </remarks>
     **/
    public IEnumerator<Propriete> GetEnumerator() {

      foreach(string Cle in Proprietes.Keys)
        yield return Proprietes[key: Cle];
    }

    /**
     * <summary>
     *   [FR] Mise en œuvre nécessaire.<br/>
     *   [EN] Implementation required.
     * </summary>
     * <remarks>
     *   [FR] Un IEnumerator de type faible.<br/>
     *   [EN] An IEnumerator of weak type.
     * </remarks>
     **/
    IEnumerator IEnumerable.GetEnumerator() => Proprietes.GetEnumerator();
  }
}
