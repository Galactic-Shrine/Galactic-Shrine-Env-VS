/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/


namespace GalacticShrine.Configuration.Configuration {

  /**
   * <summary>
   *   [FR] Cette structure définit le format du fichier GsC en personnalisant les caractères utilisés<br/>
   *        pour définir les clés/valeurs des sections ou les commentaires.<br/>
   *   [EN] This structure defines the format of the GsC file, customizing the characters used<br/>
   *        to define section keys/values or comments.
   * </summary>
   **/
  public class SchemaGsC {


    /**
     * <summary>
     *   [FR] Chaîne de début de section<br/>
     *   [EN] Section start string
     * </summary>
     **/
    private string ChaineDeDebutDeSection          = "<{";

    /**
     * <summary>
     *   [FR] Chaîne de fin de section<br/>
     *   [EN] Section end string
     * </summary>
     **/
    private string ChaineDeFinDeSection            = "}>";

    /**
     * <summary>
     *   [FR] Chaîne d'attribution des propriétés<br/>
     *   [EN] Property attribution string
     * </summary>
     **/
    private string ChaineDattributionDesProprietes = "~>";

    /**
     * <summary>
     *   [FR] Chaîne d'attribution du commentaire<br/>
     *   [EN] Comment attribution string
     * </summary>
     **/
    private string ChaineDattributionDuCommentaire = "#";

  }
}
