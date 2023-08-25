/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Configuration.Configuration {

  /**
   * <summary>
   *   [FR] Cette structure définit le format du fichier INI en personnalisant les caractères utilisés<br/>
   *        pour définir les clés/valeurs des sections ou les commentaires.<br/>
   *   [EN] This structure defines the format of the INI file, customizing the characters used<br/>
   *        to define section keys/values or comments.
   * </summary>
   **/
  public class SchemaIni : ClonableInterface<SchemaIni> {

    /**
     * <summary>
     *   [FR] Chaîne de début de section<br/>
     *   [EN] Section start string
     * </summary>
     **/
    private string ChaineDeDebutDeSection          = "[";

    /**
     * <summary>
     *   [FR] Chaîne de fin de section<br/>
     *   [EN] Section end string
     * </summary>
     **/
    private string ChaineDeFinDeSection            = "]";

    /**
     * <summary>
     *   [FR] Chaîne d'attribution des propriétés<br/>
     *   [EN] Property attribution string
     * </summary>
     **/
    private string ChaineDattributionDesProprietes = "=";

    /**
     * <summary>
     *   [FR] Chaîne d'attribution du commentaire<br/>
     *   [EN] Comment attribution string
     * </summary>
     **/
    private string ChaineDattributionDuCommentaire = ";";

    /**
     * <summary>
     *   [FR] Constructeur<br/>
     *   [EN] Constructor
     * </summary>
     **/
    public SchemaIni() { }

    /**
     * <summary>
     *   [FR] Analyseur du Constructeur<br/>
     *   [EN] Parse constructor
     * </summary>
     **/
    public SchemaIni(SchemaIni SchemaIniInstance) {

      DebutDeSection           = SchemaIniInstance.DebutDeSection;
      FinDeSection             = SchemaIniInstance.FinDeSection;
      AttributionDePropriete   = SchemaIniInstance.AttributionDePropriete;
      AttributionDuCommentaire = SchemaIniInstance.AttributionDuCommentaire;
    }

    /**
     * <summary>
     *   [FR] Définit le caractère qui définit le début d'un nom de section.<br/>
     *   [EN] Sets the character that defines the start of a section name.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est le caractère '['<br/>
     *        La chaîne renvoyée sera également coupée<br/>
     *   [EN] The default value is the '['<br/>
     *        The returned channel will also be muted
     * </remarks>
     **/
    public string DebutDeSection {

      get => string.IsNullOrWhiteSpace(value: ChaineDeDebutDeSection) ? "[" : ChaineDeDebutDeSection;
      set => ChaineDeDebutDeSection = value?.Trim();
    }

    /**
     * <summary>
     *   [FR] Définit le caractère qui définit la fin d'un nom de section.<br/>
     *   [EN] Sets the character that defines the end of a section name.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est le caractère ']'<br/>
     *        La chaîne renvoyée sera également coupée<br/>
     *   [EN] The default value is the ']'<br/>
     *        The returned channel will also be muted
     * </remarks>
     **/
    public string FinDeSection {

      get => string.IsNullOrWhiteSpace(value: ChaineDeFinDeSection) ? "]" : ChaineDeFinDeSection;
      set => ChaineDeFinDeSection = value?.Trim();
    }

    /**
     * <summary>
     *   [FR] Définit la chaîne utilisée dans le fichier ini pour indiquer une affectation clé/valeur.<br/>
     *   [EN] Defines the string used in the ini file to indicate a key/value assignment.
     * </summary>
     * <remarks>
     *   [FR] La valeur par défaut est le caractère '='<br/>
     *        La chaîne renvoyée sera également coupée<br/>
     *   [EN] The default value is the '='<br/>
     *        The returned channel will also be muted
     * </remarks>
     **/
    public string AttributionDePropriete {

      get => string.IsNullOrWhiteSpace(value: ChaineDattributionDesProprietes) ? "=" : ChaineDattributionDesProprietes;
      set => ChaineDattributionDesProprietes = value?.Trim();
    }

    /**
     * <summary>
     *   [FR] Définit la chaîne qui définit le début d'un commentaire.<br/>
     *        Un commentaire s'étend de la première chaîne de commentaire correspondante à la fin de la ligne.<br/>
     *   [EN] Defines the string that defines the beginning of a comment.<br/>
     *        A comment extends from the first corresponding comment string to the end of the line.
     * </summary>
     * <remarks>
     *   [FR] La chaîne par défaut est le caractère ';'<br/>
     *        La chaîne renvoyée sera également coupée.<br/>
     *   [EN] The default string is the ';'<br/>
     *        The returned channel will also be cut.
     * </remarks>
     **/
    public string AttributionDuCommentaire {

      get => string.IsNullOrWhiteSpace(value: ChaineDattributionDuCommentaire) ? ";" : ChaineDattributionDuCommentaire;
      set => ChaineDattributionDuCommentaire = value?.Trim();
    }

    /**
     * <summary>
     *   [FR] Crée un nouvel objet qui est une copie de l'instance actuelle.
     *   [EN] Creates a new object that is a copy of the current instance.
     * </summary>
     * <remarks>
     *   [FR] Un nouvel objet qui est une copie de cette instance.
     *   [EN] A new object that is a copy of this instance.
     * </remarks>
     **/
    public SchemaIni CloneEnProfondeur() => new (SchemaIniInstance: this);
  }
}
