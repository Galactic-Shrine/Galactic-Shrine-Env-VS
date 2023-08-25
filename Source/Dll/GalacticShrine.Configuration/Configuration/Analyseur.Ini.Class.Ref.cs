/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalacticShrine.Enumeration.Configuration;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Configuration.Configuration {

  public class AnalyseurIni : ClonableInterface<AnalyseurIni> {

    /**
     * <summary>
     *   [FR] L'extraction des sections/clés par nom se fait par une recherche insensible à la casse.<br/>
     *   [EN] Extraction of sections/keys by name is performed using a case-insensitive search.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est false (recherche sensible à la casse).<br/>
     *   [EN] Default value is false (case-sensitive search).
     * </remarks>
     **/
    public bool InsensibleA_LaCasse { get; set; } = false;

    /**
     * <summary>
     *   [FR] Si la valeur est "true", les commentaires sont extraits du texte analysé et ajoutés à la structure de données.<br/>
     *        S'il vaut false, il ignorera tous les commentaires lors de l'analyse et ne les stockera pas, ce qui permettra d'économiser de la mémoire et des allocations.<br/>
     *   [EN] If set to true, comments are extracted from the parsed text and added to the data structure.<br/>
     *        If set to false, it will ignore all comments during parsing and not store them, thus saving memory and allocations.
     * </summary>
     **/
    public bool AnalyseDesCommentaires { get; set; } = true;

    /**
     * <summary>
     *   [FR] Si true, l'instance <see cref="Ini"/> lèvera une exception si une erreur est trouvée.<br/>
     *        Si false, l'analyseur s'arrête et renvoie une valeur nulle.<br/>
     *   [EN] If true, the <see cref="Ini"/> instance will raise an exception if an error is found.<br/>
     *        If false, the parser stops and returns a null value.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est true.<br/>
     *   [EN] The default value is true.
     * </remarks>
     **/
    public bool LancerDesExceptionsEnCasDerreur { get; set; } = true;

    /**
     * <summary>
     *   [FR] S'il vaut true, il continue d'analyser le fichier même si une ligne mal formée est trouvée, <br/>
     *        mais ne compte pas comme une erreur trouvée (c'est-à-dire que <see cref="Ini.A_DesErreurs"/> renverra false). <br/>
     *        S'il a la valeur false, il lèvera une exception ou suivra une erreur, selon la valeur de <see cref="LancerDesExceptionsEnCasDerreur"/>,<br/>
     *        lorsque l'analyseur rencontrera une ligne mal formée.<br/>
     *   [EN] If set to true, it continues to analyze the file even if a malformed line is found, <br/>
     *        but does not count as an error found (i.e. <see cref="Ini.A_DesErrors"/> will return false). <br/>
     *        If set to false, it will throw an exception or follow an error, depending on the value of <see cref="LancerDesExceptionsEnCasDerreur"/>, <br/>
     *        when the parser encounters a malformed line.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est false.<br/>
     *   [EN] The default value is false.
     * </remarks>
     **/
    public bool SauterLesLignesInvalides { get; set; } = false;

    /**
     * <summary>
     *   [FR] S'il est défini sur true, il coupera l'espace blanc de la propriété lors de l'analyse. <br/>
     *        S'il vaut false, il considérera tous les espaces de la ligne comme faisant partie de la propriété lors de l'extraction de la clé et des valeurs.<br/>
     *   [EN] If set to true, it will cut the white space of the property during analysis. <br/>
     *        If set to false, it will consider all spaces on the line as part of the property when extracting the key and values.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est true.<br/>
     *   [EN] The default value is true.
     * </remarks>
     **/
    public bool HabillagePropriete { get; set; } = true;

    /**
     * <summary>
     *   [FR] Si la valeur est "true", le nom de la section sera coupé des espaces blancs lors de l'analyse.<br/>
     *        S'il vaut false, il considérera tous les espaces de la ligne comme faisant partie du nom de la section.<br/>
     *   [EN] If set to "true", the section name will be cut from the white spaces when parsed.<br/>
     *        If false, it will consider all the spaces on the line as part of the section name.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est true.<br/>
     *   [EN] The default value is true.
     * </remarks>
     **/
    public bool HabillageSections { get; set; } = true;

    /**
     * <summary>
     *   [FR] Si la valeur est "true", les espaces blancs seront supprimés des commentaires lors de l'analyse.<br/>
     *        S'il vaut false, il considérera tous les espaces blancs de la ligne comme faisant partie du commentaire.<br/>
     *   [EN] If set to true, white spaces will be removed from comments during parsing.<br/>
     *        If set to false, it will consider all white spaces on the line as part of the comment.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est true.<br/>
     *   [EN] The default value is true.
     * </remarks>
     **/
    public bool HabillageCommentaires { get; set; } = true;

    /**
     * <summary>
     *   [FR] Si la valeur est fixée à false et que le <see cref="GalacticShrine.Configuration.Ini"/> trouve une section dupliquée,<br/>
     *        l'analyseur s'arrêtera avec une erreur. Si la valeur est fixée à true, les sections dupliquées sont autorisées dans le fichier,<br/>
     *        mais seul un élément <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> sera créé dans la collection <see cref="GalacticShrine.Configuration.DonneesIni.Sections"/>.<br/>
     *   [EN] If the value is set to false and the <see cref="GalacticShrine.Configuration.Ini"/> finds a duplicated section,
     *        the parser will stop with an error. If the value is set to true, duplicated sections are allowed in the file, <br/>
     *        but only a <see cref="GalacticShrine.Modele.Configuration.Ini.Section"/> element will be created in the <see cref="GalacticShrine.Configuration.DonneesIni.Sections"/> collection.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est false.<br/>
     *   [EN] The default value is false.
     * </remarks>
     **/
    public bool AutoriserLesSectionsDupliquees  { get; set; } = false;

    /**
     * <summary>
     *   [FR] Permet d'avoir des clés au début du fichier, avant qu'une section ne soit définie.<br/>
     *        Ces clés n'appartiennent à aucune section et sont stockées dans le champ spécial <see cref="GalacticShrine.Configuration.DonneesIni.ProprieteGlobales"/>.<br/>
     *        S'il est défini à false et que le fichier ini contient des clés en dehors d'une section, l'analyseur s'arrêtera avec une erreur.<br/>
     *   [EN] Allows you to have keys at the beginning of the file, before a section is defined.<br/>
     *        These keys do not belong to any section and are stored in the special field <see cref="GalacticShrine.Configuration.DonneesIni.ProprieteGlobales"/>.<br/>
     *        If set to false and the ini file contains keys outside a section, the parser will stop with an error.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est true.<br/>
     *   [EN] The default value is true.
     * </remarks>
     **/
    public bool AutoriserLesClesSansSection { get; set; } = true;

    /**
     * <summary>
     *   [FR] Définit la politique à utiliser lorsque deux propriétés ou plus portant le même nom de clé sont trouvées sur la même section.<br/>
     *   [EN] Defines the policy to be used when two or more properties with the same key name are found on the same section.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est <see cref="ComportementDesProprietesDupliquees.DesactiverEtArretAvecErreur"/>.<br/>
     *   [EN] The default value is <see cref="ComportementDesProprietesDupliquees.DesactiverEtArretAvecErreur"/>.
     * </remarks>
     **/
    public ComportementDesProprietesDupliquees ComportementDesProprietesDupliquees { get; set; } = ComportementDesProprietesDupliquees.DesactiverEtArretAvecErreur;

    /**
     * <summary>
     *   [FR] Obtient ou définit la chaîne utilisée pour concaténer les clés dupliquées.<br/>
     *   [EN] Gets or sets the string used to concatenate duplicate keys.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est ";".<br/>
     *   [EN] The default value is ";".
     * </remarks>
     **/
    public string ConcatenerLesChainesDeProprietesDupliquees { get; set; } = ";";

    public AnalyseurIni() { }

    public AnalyseurIni(AnalyseurIni AnalyseurIniInstance) {

      AutoriserLesClesSansSection = AnalyseurIniInstance.AutoriserLesClesSansSection;
      AutoriserLesSectionsDupliquees = AnalyseurIniInstance.AutoriserLesSectionsDupliquees;
      ComportementDesProprietesDupliquees = AnalyseurIniInstance.ComportementDesProprietesDupliquees;
      ConcatenerLesChainesDeProprietesDupliquees = AnalyseurIniInstance.ConcatenerLesChainesDeProprietesDupliquees;
      HabillagePropriete = AnalyseurIniInstance.HabillagePropriete;
      HabillageSections = AnalyseurIniInstance.HabillageSections;
      LancerDesExceptionsEnCasDerreur = AnalyseurIniInstance.LancerDesExceptionsEnCasDerreur;
      SauterLesLignesInvalides = AnalyseurIniInstance.SauterLesLignesInvalides;
    }

    public AnalyseurIni CloneEnProfondeur() => new (AnalyseurIniInstance: this);
  }
}
