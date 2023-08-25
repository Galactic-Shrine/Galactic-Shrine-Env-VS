/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using GalacticShrine.Configuration.Properties;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Modele.Configuration.Ini {

  /**
   * <summary>
   *   [FR] Informations associées à une propriété provenant d'une Configuration INI.<br/>
   *   [EN] Information associated with a property from an INI Configuration.
   * </summary>
   **/
  public class Propriete : ClonableInterface<Propriete> {

    /**
     * <summary>
     *   [FR] Liste des lignes de commentaires associées à cette propriété.<br/>
     *   [EN] List of comment lines associated with this property.
     * </summary>
     **/
    private List<string> Commentaires;

    /**
     * <summary>
     *   [FR] Obtient ou définit le nom de cette propriété.<br/>
     *   [EN] Gets or sets the name of this property.
     * </summary>
     **/
    public string Cle { get; set; }

    /**
     * <summary>
     *   [FR] Obtient ou définit la valeur associée à cette propriété.<br/>
     *   [EN] Gets or sets the value associated with this property.
     * </summary>
     **/
    public string Valeur { get; set; }

    /**
     * <summary>
     *   [FR] Liste des lignes de commentaires associées à cette propriété.<br/>
     *   [EN] List of comment lines associated with this property.
     * </summary>
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
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Propriete"/>.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Propriete"/> class.
     * </summary>
     **/
    public Propriete(string Cle, string Valeur = "") {

      if(string.IsNullOrEmpty(value: Cle))         
        throw new ArgumentException(message: Resources.LeNomDeLaCleNePeutPasEtreVide, paramName: nameof(Cle));

      this.Valeur = Valeur;
      this.Cle = Cle;
    }

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Propriete"/> à partir de l'instance précédente.<br/>
     *   [EN] Initializes a new instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Propriete"/> class from its previous instance.
     * </summary>
     * <remarks>
     *   [FR] Les données sont copiées en profondeur<br/>
     *   [EN] Data is copied in depth
     * </remarks>
     * <param name="ProprieteInstance">
     *   [FR] L'instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.Propriete"/> utilisée pour créer la nouvelle instance.<br/>
     *   [EN] The instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.Propriete"/> class used to create the new instance.
     * </param>
     **/
    public Propriete(Propriete ProprieteInstance) {

      Cle         = ProprieteInstance.Cle;
      Valeur      = ProprieteInstance.Valeur;
      Commentaire = ProprieteInstance.Commentaire;
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
    public Propriete CloneEnProfondeur() => new (ProprieteInstance: this);
  }
}
