/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Configuration.Analyseur {

  /**
   * <summary>
   *   [FR] Cette structure définit le format du fichier INI en personnalisant les caractères utilisés
   *        pour définir les clés/valeurs des sections ou les commentaires.
   *   [EN] This structure defines the format of the INI file, customizing the characters used
   *        to define section keys/values or comments.
   * </summary>
   **/
  public class SchemaIni : ClonableInterface<SchemaIni> {

    /**
     * <summary>
     *   [FR] Chaîne de début de section
     *   [EN] Section start string
     * </summary>
     **/
    private string ChaineDeDebutDeSection          = "[";

    /**
     * <summary>
     *   [FR] Chaîne de fin de section
     *   [EN] Section end string
     * </summary>
     **/
    private string ChaineDeFinDeSection            = "]";

    /**
     * <summary>
     *   [FR] Chaîne d'attribution des propriétés
     *   [EN] Propertys attribution string
     * </summary>
     **/
    private string ChaineDattributionDesProprietes = "=";

    /**
     * <summary>
     *   [FR] Constructeur
     *   [EN] Constructor
     * </summary>
     **/
    public SchemaIni() { }

    /**
     * <summary>
     *   [FR] Analyseur du Constructeur
     *   [EN] Parse constructor
     * </summary>
     **/
    public SchemaIni(SchemaIni Schema) {

      DebutDeSection         = Schema.DebutDeSection;
      FinDeSection           = Schema.FinDeSection;
      AttributionDePropriete = Schema.AttributionDePropriete;
    }

    public string DebutDeSection {

      get => string.IsNullOrWhiteSpace(ChaineDeDebutDeSection) ? "[" : ChaineDeDebutDeSection;
      set => ChaineDeDebutDeSection = value?.Trim();
    }

    public string FinDeSection {

      get => string.IsNullOrWhiteSpace(ChaineDeFinDeSection) ? "]" : ChaineDeFinDeSection;
      set => ChaineDeFinDeSection = value?.Trim();
    }

    public string AttributionDePropriete {

      get => string.IsNullOrWhiteSpace(ChaineDattributionDesProprietes) ? "=" : ChaineDattributionDesProprietes;
      set => ChaineDattributionDesProprietes = value?.Trim();
    }

    public SchemaIni CloneEnProfondeur() => new SchemaIni(this);
  }
}
