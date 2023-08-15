using GalacticShrine.Configuration.Analyseur;

namespace GalacticShrine.Configuration {

  public class Ini {

    /**
    * <summary>
    *   [FR] Schéma qui définit la structure du fichier ini à analyser.
    *   [EN] Schema that defines the structure of the ini file to be analyzed.
    * </summary>
    **/
    public SchemaIni Schema { get; protected set; }

    /**
     * <summary>
     *   [FR] Constructeur
     *   [EN] Constructor
     * </summary>
     **/
    public Ini() {
    
      Schema = new SchemaIni();
    }

  }
}