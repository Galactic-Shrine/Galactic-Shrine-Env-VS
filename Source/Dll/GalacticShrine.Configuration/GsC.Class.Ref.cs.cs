/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalacticShrine.Configuration.Configuration;

namespace GalacticShrine.Configuration {

  /**
    * <summary>
    *   [FR] Galactic-Shrine Config (GsC) est un fichier de configuration crypté.<br/>
    *   [EN] Galactic-Shrine Config (GsC) is an encrypted configuration file.
    * </summary>
    **/
  public class GsC {

    /**
    * <summary>
    *   [FR] Schéma qui définit la structure du fichier GsC à analyser.<br/>
    *   [EN] Schema that defines the structure of the GsC file to be analyzed.
    * </summary>
    **/
    public SchemaGsC Schema { get; protected set; }
  }
}
