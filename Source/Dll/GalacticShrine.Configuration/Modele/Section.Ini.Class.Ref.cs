/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using GalacticShrine.Configuration.Properties;
using GalacticShrine.Enumeration.Configuration;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Modele.Configuration.Ini {

  /**
   * <summary>
   *   [FR] Informations associées à une propriété provenant d'une Configuration INI.<br/>
   *   [EN] Information associated with a property from an INI Configuration.
   * </summary>
   **/
  public class Section : ClonableInterface<Section> {

    private string Noms;

    /**
     * <summary>
     *   [FR] Liste des lignes de commentaires associées à cette propriété.<br/>
     *   [EN] List of comment lines associated with this property.
     * </summary>
     **/
    private List<string> Commentaires;

    private readonly IEqualityComparer<string> RechercherComparer;

    /**
     * <summary>
     *   [FR] Obtient ou définit le nom de la section.<br/>
     *   [EN] Gets or sets the section name.
     * </summary>
     * <value>
     *   [FR] Le nom de la section<br/>
     *   [EN] Section name
     * </value>
     **/
    public string Nom {

      get => Noms;
    
      set {

        if(!string.IsNullOrEmpty(value: value))
          Noms = value;
      }
    }

    /**
     * <summary>
     *   [FR] Obtient ou définit la liste de commentaires associée à cette section.<br/>
     *   [EN] Gets or sets the list of comments associated with this section.
     * </summary>
     * <value>
     *   [FR] Une liste de chaînes.<br/>
     *   [EN] A list of strings.
     * </value>
     **/
    public List<string> Commentaire {

      get {

        Commentaires ??= new List<string>();
        return Commentaires;
      }

      set {

        Commentaires ??= new List<string>();
        Commentaires.Clear();
        Commentaires.AddRange(value);
      }
    }

    /**
     * <summary>
     *   [FR] Obtient ou définit les propriétés associées à cette section.<br/>
     *   [EN] Gets or sets properties associated with this section.
     * </summary>
     * <value>
     *   [FR] Une collection d'objets de propriété.<br/>
     *   [EN] A collection of Property objects.
     * </value>
     **/
    public ProprieteCollection Proprietes { get; set; }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> class.
     * </summary>
     **/
    public Section(string Nom) : this(Nom: Nom, RechercherComparer: EqualityComparer<string>.Default) { }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> class.
     * </summary>
     **/
    public Section(string Nom, IEqualityComparer<string> RechercherComparer) {

      this.RechercherComparer = RechercherComparer;

      if(string.IsNullOrEmpty(value: Nom)) {

        throw new ArgumentException(message: Resources.LeNomDeLaSectionNePeutPasEtreVide, paramName: nameof(Nom));
      }

      Proprietes = new ProprieteCollection(RechercherComparer: RechercherComparer);
      this.Nom = Nom;
    }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> à partir de l'instance précédente.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> class from its previous instance.
     * </summary>
     * <remarks>
     *   [FR] Les données sont copiées en profondeur<br/>
     *   [EN] Data is copied in depth
     * </remarks>
     * <param name="SectionInstance">
     *   [FR] L'instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> utilisée pour créer la nouvelle instance.<br/>
     *   [EN] The instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> class used to create the new instance.
     * </param>
     * <param name="RechercherComparer">
     *   [FR] Rechercher et comparer.<br/>
     *   [EN] Search comparer.
     * </param>
     **/
    public Section(Section SectionInstance, IEqualityComparer<string> RechercherComparer = null) {

      Nom = SectionInstance.Nom;
      Commentaire = SectionInstance.Commentaire;
      Proprietes = new ProprieteCollection(ProprieteCollectionInstance: SectionInstance.Proprietes, RechercherComparer: RechercherComparer ?? SectionInstance.RechercherComparer);
      this.RechercherComparer = RechercherComparer;
    }

    /**
     * <summary>
     *   [FR] Fonction de suppression.<br/>
     *   [EN] Deletion function.
     * </summary>
     * <param name="Effacement">
     *   [FR] Permet de passer au type de suppression souhaité.<br/>
     *   [EN] Switches to the desired type of deletion.
     * </param>
     * <remarks>
     *   [FR] Utiliser par défaut: <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/></para>
     *   [EN] Use by default: <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/></para>
     * </remarks>
     * <value>
     *   [FR] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>: Efface toutes les paires de propriétés de cette section.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/>: Efface tous les commentaires de cette section et de toutes les paires de propriétés qu'elle contient.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/>: Efface tous les commentaires et toutes les propriétés de cette section.</para>
     *   [EN] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>: Deletes all property pairs in this section.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/>: Deletes all comments from this section and all property pairs it contains.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Tout"/>: Deletes all comments and properties from this Section.</para>
     * </value>
     **/
    public void Effacer(Effacement Effacement = Effacement.Tout) {

      switch (Effacement) {

        case Effacement.Commentaires:

          Commentaire.Clear();
          Proprietes.Effacer(Effacement: Effacement.Commentaires);
          break;

        case Effacement.Proprietes:

          Proprietes.Effacer();
          break;

        case Effacement.Tout:

          Proprietes.Effacer();
          Proprietes.Effacer(Effacement: Effacement.Commentaires);
          Commentaire.Clear();
          break;
      }
    }

    /**
     * <summary>
     *   [FR] Fusionne d'autres sections dans celle-ci,<br/>
     *        en ajoutant de nouvelles propriétés si elles n'existaient pas,<br/>
     *        ou en remplaçant des valeurs si les propriétés existaient déjà.<br/>
     *   [EN] Merges other sections into this one,<br/>
     *        adding new properties if they didn't exist,<br/>
     *        or replacing values if the properties already existed.
     * </summary>
     * <param name="FusionnerLaSection"></param>
     * <remarks>
     *   [FR] Les commentaires sont également fusionnés, mais ils sont toujours ajoutés et ne sont pas écrasés.
     *   [EN] Comments are also merged, but are still added and not overwritten.
     * </remarks>
     **/
    public void Fusionner(Section FusionnerLaSection) {

      Proprietes.Fusionner(ProprieteA_Fusionner: FusionnerLaSection.Proprietes);

      foreach(var Commentaires in FusionnerLaSection.Commentaire)
        Commentaire.Add(item: Commentaires);
    }

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
    public Section CloneEnProfondeur() => new (SectionInstance: this);
  }
}
