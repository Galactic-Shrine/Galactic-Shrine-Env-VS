/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System.Collections;
using System.Collections.Generic;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Modele.Configuration.Ini {

  public class SectionCollection : ClonableInterface<SectionCollection>, IEnumerable<Section> {

    private readonly Dictionary<string, Section> Sections;

    private readonly IEqualityComparer<string> RechercherComparer;

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.SectionCollection"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.SectionCollection"/> class.
     * </summary>
     **/
    public SectionCollection(): this(RechercherComparer: EqualityComparer<string>.Default) { }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.SectionCollection"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.SectionCollection"/> class.
     * </summary>
     * <param name="RechercherComparer">
     *   [FR] StringComparer utilisé pour accéder aux noms de sections<br/>
     *   [EN] StringComparer used to access section names
     * </param>
     **/
    public SectionCollection(IEqualityComparer<string> RechercherComparer) {

      this.RechercherComparer = RechercherComparer;
      Sections = new Dictionary<string, Section>(comparer: this.RechercherComparer);
    }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.SectionCollection"/> à partir de l'instance précédente.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.SectionCollection"/> class from its previous instance.
     * </summary>
     * <remarks>
     *   [FR] Les données sont copiées en profondeur.<br/>
     *   [EN] Data is copied in depth.
     * </remarks>
     * <param name="SectionCollectionInstance">
     *   [FR] L'instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.SectionCollection"/> utilisée pour créer la nouvelle instance.<br/>
     *   [EN] The <see cref="GalacticShrine.Modele.Configuration.Ini.SectionCollection"/> class instance used to create the new instance.
     * </param>
     **/
    public SectionCollection(SectionCollection SectionCollectionInstance, IEqualityComparer<string> searchComparer) {

      RechercherComparer = searchComparer ?? EqualityComparer<string>.Default;
      Sections = new Dictionary<string, Section>(comparer: RechercherComparer);
      foreach(var sectionData in SectionCollectionInstance) {

        Sections.Add(key: sectionData.Nom, value: sectionData.CloneEnProfondeur());
      };
    }

    /**
     * <summary>
     *   [FR] Obtient les propriétés associées à un nom de section spécifié.<br/>
     *   [EN] Gets the properties associated with a specified section name.
     * </summary>
     * <value>
     *   [FR] Une instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> contenant les propriétés de la section donnée,<br/>
     *        ou une valeur <c>null</c> si la section n'existe pas.
     *   [EN] An instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> class containing the properties of the given section,<br/>
     *        or a <c>null</c> value if the section doesn't exist.
     * </value>
     **/
    public ProprieteCollection this[string NomDeLaSection] {

      get {

        if(Sections.ContainsKey(key: NomDeLaSection))
          return Sections[key: NomDeLaSection].Proprietes;

        return null;
      }
    }

    /**
     * <summary>
     *   [FR] Renvoie le nombre d'éléments de la section dans la collection.<br/>
     *   [EN] Returns the number of elements of the section in the collection.
     * </summary>
     **/
    public int Compter => Sections.Count;

    /**
     * <summary>
     *   [FR] Crée une nouvelle section avec des données vides.<br/>
     *   [EN] Creates a new section with empty data.
     * </summary>
     * <remarks>
     *   <para>
     *     [FR] Si une section portant le même nom existe, cette opération n'a aucun effet.<br/>
     *     [EN] If a section with the same name exists, this operation has no effect.
     *   </para>
     * </remarks>
     * <param name="NomDeLaSection">
     *   [FR] Nom de la section à créer.<br/>
     *   [EN] Name of section to be created.
     * </param>
     * <returns>
     *   [FR] Vrai (true) si une nouvelle section portant le nom spécifié a été ajoutée, sinon Faux (false).<br/>
     *   [EN] True if a new section with the specified name has been added, otherwise false.
     * </returns>
     **/
    public bool Ajouter(string NomDeLaSection) {

      if(!Contient(NomDeLaSection: NomDeLaSection)) {

        Sections.Add(key: NomDeLaSection, value: new Section(Nom: NomDeLaSection, RechercherComparer: RechercherComparer));
        return true;
      }

      return false;
    }

    /**
     * <summary>
     *   [FR] Ajoute une nouvelle instance de section à la collection.<br/>
     *   [EN] Adds a new section instance to the collection.
     * </summary>
     * <param name="SectionInstance">
     * </param>
     **/
    public void Ajouter(Section SectionInstance) {

      if(Contient(NomDeLaSection: SectionInstance.Nom)) {

        Sections[key: SectionInstance.Nom] = new Section(SectionInstance: SectionInstance, RechercherComparer: RechercherComparer);
      }
      else {

        Sections.Add(key: SectionInstance.Nom, value: new Section(SectionInstance: SectionInstance, RechercherComparer: RechercherComparer));
      }
    }

    /**
     * <summary>
     *   [FR] Supprime toutes les entrées de cette collection.<br/>
     *   [EN] Deletes all entries in this collection.
     * </summary>
     **/
    public void Effacer() => Sections.Clear();

    /**
     * <summary>
     *   [FR] Supprime la section avec le nom donné et toutes ses propriétés.<br/>
     *   [EN] Deletes the section with the given name and all its properties.
     * </summary>
     * <param name="NomDeLaSection">
     *   [FR] Nom de la section à supprimer.<br/>
     *   [EN] Name of section to be deleted.
     * </param>
     * <returns>
     *   [FR] Vrai (true) si la section portant le nom spécifié a été supprimée, sinon Faux (false).<br/>
     *   [EN] True if the section with the specified name has been deleted, otherwise false.
     * </returns>
     **/
    public bool Effacer(string NomDeLaSection) => Sections.Remove(key: NomDeLaSection);

    /**
    * <summary>
    *   [FR] Renvoie les données d'une section spécifiée par son nom.<br/>
    *   [EN] Returns data for a section specified by its name.
    * </summary>
    * <param name="NomDeLaSection">
    *   [FR] Nom de la section.<br/>
    *   [EN] Section name.
    * </param>
    * <returns>
    *   [FR] Une instance d'une classe <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> contenant les données de section pour les données INI actuelles.<br/>
    *   [EN] An instance of a <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> class containing the section data for the current INI data.
    * </returns>
    **/
    public Section RechercheParNom(string NomDeLaSection) {

      if(Sections.ContainsKey(key: NomDeLaSection))
        return Sections[key: NomDeLaSection];

      return null;
    }

    public void Fusionner(SectionCollection SectionsA_Fusionner) {

      foreach(var DonneesDeSectionA_Fusionner in SectionsA_Fusionner) {

        var sectionDataInThis = RechercheParNom(NomDeLaSection: DonneesDeSectionA_Fusionner.Nom);

        if(sectionDataInThis == null) {

          Ajouter(NomDeLaSection: DonneesDeSectionA_Fusionner.Nom);
        }

        this[NomDeLaSection: DonneesDeSectionA_Fusionner.Nom].Fusionner(ProprieteA_Fusionner: DonneesDeSectionA_Fusionner.Proprietes);
      }
    }

    /**
     * <summary>
     *   [FR] Permet de savoir si une section portant le nom spécifié existe dans la collection.<br/>
     *   [EN] Finds out whether a section with the specified name exists in the collection.
     * </summary>
     * <param name="NomDeLaSection">
     *   [FR] Nom de la section à rechercher.<br/>
     *   [EN] Name of section to search.
     * </param>
     *  <returns>
     *   [FR] Vrai(true) si une section portant le nom spécifié existe dans la collection, sinon faux(false).<br/>
     *   [EN] True if a section with the specified name exists in the collection, otherwise false.
     * </returns>
     **/
    public bool Contient(string NomDeLaSection) => Sections.ContainsKey(key: NomDeLaSection);

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
    public SectionCollection CloneEnProfondeur() => new (SectionCollectionInstance: this, searchComparer: RechercherComparer);

    /**
     * <summary>
     *   [FR] Renvoie un énumérateur qui parcourt la collection.<br/>
     *   [EN] Returns an enumerator that traverses the collection.
     * </summary>
     * <returns>
     *   [FR] Un <see cref="T:System.Collections.Generic.IEnumerator`1"/> qui peut être utilisé pour parcourir la collection.<br/>
     *   [EN] A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to browse the collection.
     * </returns>
     **/
    public IEnumerator<Section> GetEnumerator() {

      foreach(string NomDeLaSection in Sections.Keys)
        yield return Sections[key: NomDeLaSection];
    }

    /**
     * <summary>
     *   [FR] Renvoie un énumérateur qui parcourt une collection.<br/>
     *   [EN] Returns an enumerator that traverses a collection.
     * </summary>
     * <returns>
     *   [FR] Un objet <see cref="T:System.Collections.IEnumerator"/> qui peut être utilisé pour parcourir la collection.<br/>
     *   [EN] A <see cref="T:System.Collections.IEnumerator"/> object that can be used to browse the collection.
     * </returns>
     **/
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
