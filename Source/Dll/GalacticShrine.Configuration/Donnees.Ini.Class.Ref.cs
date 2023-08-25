/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using GalacticShrine.Configuration.Configuration;
using GalacticShrine.Enumeration.Configuration;
using GalacticShrine.Interface.Configuration;
using GalacticShrine.Modele.Configuration.Ini;

namespace GalacticShrine.Configuration {

  public class DonneesIni : ClonableInterface<DonneesIni> {

    /**
     * <summary>
     *   [FR] Représente toutes les sections d'un fichier INI.<br/>
     *   [EN] Represents all sections of an INI file.
     * </summary>
     **/
    protected SchemaIni SchemaInstance;

    /**
     * <summary>
     *   [FR] Voir la propriété <see cref="Configuration"/> pour plus d'informations.<br/>
     *   [EN] See property <see cref="Configuration"/> for more information. 
     * </summary>
     **/
    private AnalyseurIni Configurations;

    /**
     * <summary>
     *   [FR] Si la valeur est fixée à true, une section sera automatiquement créée lorsque vous
     *        utilisez l'accès indexé avec un nom de section qui n'existe pas.
     *        Si la valeur est fixée à false, une exception sera levée si vous essayez d'accéder
     *        à une section qui n'existe pas à l'aide de l'opérateur d'indexation.
     *   [EN] If set to true, a section will be automatically created when you
     *        use indexed access with a section name that doesn't exist.
     *        If the value is set to false, an exception will be thrown if you try to access
     *        to a non-existent section using the indexing operator.
     * </summary>
     **/
    public bool CreerDesSectionsSilsNexistentPas { get; set; } = false;

    /**
     * <summary>
     *   [FR] Obtient ou définit toutes les <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> pour cette instance.<br/>
     *   [EN] Gets or sets all <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> for this instance.
     * </summary>
     **/
    public SectionCollection Sections { get; set; }

    /**
     * <summary>
     *   [FR] Propriete globales. Contient les propriétés qui ne sont incluses dans aucune section<br/>
     *        (c'est-à-dire qu'elles sont définies au début du fichier, avant toute section).
     *   [EN] Global property. Contains properties that are not included in any section<br/>
     *        (i.e. they are defined at the beginning of the file, before any section).
     * </summary>
     **/
    public ProprieteCollection ProprieteGlobales { get; protected set; }

    /**
     * <summary>
     *   [FR] Obtient l'instance <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> avec le nom de section spécifié.<br/>
     *   [EN] Gets the <see cref="GalacticShrine.Modele.Configuration.Ini.ProprieteCollection"/> instance with the specified section name.
     * </summary>
     **/
    public ProprieteCollection this[string sectionName] {

      get {

        if(!Sections.Contient(NomDeLaSection: sectionName)) { 

          if(CreerDesSectionsSilsNexistentPas)
            Sections.Ajouter(NomDeLaSection: sectionName);
          else
            return null;
        }

        return Sections[NomDeLaSection: sectionName];
      }
    }

    public SchemaIni Schema {

      get {

        // Lazy initialization
        if(SchemaInstance == null) {

          SchemaInstance = new SchemaIni();
        }

        return SchemaInstance;
      }

      set {
        SchemaInstance = value.CloneEnProfondeur();
      }
    }

    /**
     * <summary>
     *   [FR] Configuration utilisée pour écrire un fichier ini avec les caractères de délimitation et les données appropriés.<br/>
     *   [EN] Configuration used to write an ini file with the appropriate delimiting characters and data.
     * </summary>
     * <remarks>
     *   [FR] Si l'instance <see cref="GalacticShrine.Configuration.DonneesIni"/> a été créée par un analyseur,
     *        cette instance est une copie de l'instance <see cref="GalacticShrine.Configuration.Configuration.AnalyseurIni"/> utilisée par l'analyseur.
     *        par l'analyseur (c'est-à-dire des instances d'objets différents)
     *        Si cette instance est créée par programme sans utiliser d'analyseur,
     *        cette propriété renvoie une instance de <see cref="GalacticShrine.Configuration.Configuration.AnalyseurIni"/><br/>
     *   [EN] If the <see cref="GalacticShrine.Configuration.DonneesIni"/> instance was created by an analyzer,
     *        this instance is a copy of the <see cref="GalacticShrine.Configuration.Configuration.AnalyseurIni"/> instance used by the analyzer.
     *        by the analyzer (i.e., different object instances).
     *        If this instance is created programatically without using a parser,
     *        this property returns an instance of <see cref="GalacticShrine.Configuration.Configuration.AnalyseurIni"/>.
     * </remarks>
     **/
    public AnalyseurIni Configuration {

      get {

        if(Configurations == null) {

          Configurations = new AnalyseurIni();
        }

        return Configurations;
      }

      set {

        Configurations = value.CloneEnProfondeur();
      }
    }

    /**
     * <summary>
     *   [FR] Initialise une instance DonneesIni.<br/>
     *   [EN] Initializes an IniData instance.
     * </summary> 
     **/
    public DonneesIni() {

      SchemaInstance    = new SchemaIni();
      Sections          = new SectionCollection();
      ProprieteGlobales = new ProprieteCollection();
    }

    /**
     * <summary>
     *   [FR] Initialise une instance avec un schéma donné.<br/>
     *   [EN] Initializes an instance with a given schema.
     * </summary>
     * <param name="SchemaIniInstance"></param>
     **/
    public DonneesIni(SchemaIni SchemaIniInstance) : this() {

      SchemaInstance = SchemaIniInstance.CloneEnProfondeur();
    }

    /**
     * <summary>
     *   [FR] Initialise une instance DonneesIni.<br/>
     *   [EN] Initializes an IniData instance.
     * </summary> 
     * <param name="DonneesIniInstance"></param>
     **/
    public DonneesIni(DonneesIni DonneesIniInstance) : this() {

      SchemaInstance = DonneesIniInstance.SchemaInstance.CloneEnProfondeur();
      Sections       = DonneesIniInstance.Sections.CloneEnProfondeur();
      Configuration  = DonneesIniInstance.Configuration.CloneEnProfondeur();
    }

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
     *   [FR] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>: Supprime toutes les données.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/>: Supprime tous les commentaires dans toutes les sections et valeurs de propriétés.</para>
     *   [EN] <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Proprietes"/>: Deletes all data.</para>
     *        <para><see cref="GalacticShrine.Enumeration.Configuration.Effacement.Commentaires"/>: Deletes all comments in all sections and properties values.</para>
     * </value>
     **/
    public void Effacer(Effacement Effacement = Effacement.Proprietes) {

      switch(Effacement) {

        case Effacement.Commentaires:

          ProprieteGlobales.Effacer(Effacement: Effacement.Commentaires);

          foreach(var section in Sections) {

            section.Effacer(Effacement: Effacement.Commentaires);
            section.Proprietes.Effacer(Effacement: Effacement.Commentaires);
          }
          break;

        case Effacement.Proprietes:

          ProprieteGlobales.Effacer();
          Sections.Effacer();
          break;
      }
    }

    /**
     * <summary>
     *   [FR] Fusionne les autres iniData dans celle-ci en écrasant les valeurs existantes.<br/>
     *        Les commentaires sont ajoutés.<br/>
     *   [EN] Merges other iniData into this one, overwriting existing values.<br/>
     *        Comments are added.
     * </summary>
     * <param name="PourFusionnerLesDonneesIni">
     *   [FR] Permet de passer au type de suppression souhaité.<br/>
     *   [EN] Switches to the desired type of deletion.
     * </param>
     **/
    public void Fusionner(DonneesIni PourFusionnerLesDonneesIni) {

      if(PourFusionnerLesDonneesIni == null)
        return;

      ProprieteGlobales.Fusionner(ProprieteA_Fusionner: PourFusionnerLesDonneesIni.ProprieteGlobales);
      Sections.Fusionner(SectionsA_Fusionner: PourFusionnerLesDonneesIni.Sections);
    }

    public DonneesIni CloneEnProfondeur() => new (DonneesIniInstance: this);
  }
}
