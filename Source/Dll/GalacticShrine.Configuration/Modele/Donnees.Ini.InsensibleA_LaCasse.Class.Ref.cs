/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using GalacticShrine.Configuration;
using GalacticShrine.Configuration.Configuration;

namespace GalacticShrine.Modele.Configuration.Ini {

  /**
   * <summary>
   *   [FR] Représente toutes les données d'un fichier INI exactement comme la classe <see cref="GalacticShrine.Configuration.DonneesIni"/>,
   *        mais la recherche des sections et des noms de clés se fait avec une recherche insensible à la casse.<br/>
   *   [EN] Represents all the data in an INI file exactly like the <see cref="GalacticShrine.Configuration.DonneesIni"/> class,
   *        but sections and key names are searched using a case-insensitive search.
   * </summary>
   **/
  public class DonneesIniInsensibleA_LaCasse : DonneesIni {

    /**
     * <summary>
     *   [FR] Initialise une instance DonneesIni vide.<br/>
     *   [EN] Initializes an empty DonneesIni instance.
     * </summary>
     **/
    public DonneesIniInsensibleA_LaCasse() {

      Sections          = new SectionCollection(RechercherComparer: StringComparer.OrdinalIgnoreCase);
      ProprieteGlobales = new ProprieteCollection(RechercherComparer: StringComparer.OrdinalIgnoreCase);
      Schema            = new SchemaIni();
    }

    public DonneesIniInsensibleA_LaCasse(SchemaIni SchemaIniInstence) {

      Sections          = new SectionCollection(RechercherComparer: StringComparer.OrdinalIgnoreCase);
      ProprieteGlobales = new ProprieteCollection(RechercherComparer: StringComparer.OrdinalIgnoreCase);
      Schema            = SchemaIniInstence.CloneEnProfondeur();
    }

    /**
     * <summary>
     *   [FR] Copie une instance de la classe <see cref="GalacticShrine.Modele.Configuration.Ini.DonneesIniInsensibleA_LaCasse"/>.<br/>
     *   [EN] Copies an instance of the <see cref="GalacticShrine.Modele.Configuration.Ini.DonneesIniInsensibleA_LaCasse"/> class.
     * </summary>
     * <param name="DonneesIniInstence"></param>
     **/
    public DonneesIniInsensibleA_LaCasse(DonneesIni DonneesIniInstence): this() {

      ProprieteGlobales = DonneesIniInstence.ProprieteGlobales.CloneEnProfondeur();
      Configuration     = DonneesIniInstence.Configuration.CloneEnProfondeur();
      Sections          = new SectionCollection(SectionCollectionInstance: DonneesIniInstence.Sections, searchComparer: StringComparer.OrdinalIgnoreCase);
    }
  }
}
