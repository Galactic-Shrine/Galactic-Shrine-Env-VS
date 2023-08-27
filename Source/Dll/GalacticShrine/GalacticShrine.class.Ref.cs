/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using GalacticShrine.IO;
using static GalacticShrine.DossierReference;

namespace GalacticShrine {

  [Serializable]
  public abstract class GalacticShrine {

    /**
     * <summary>
     *   [FR] Détermine si le système d'exploitation actuel est un système d'exploitation 64 bits.
     *        Racourci du <code>System.Environment.Is64BitOperatingSystem</code>
     *   [EN] Determines whether the current operating system is a 64-bit operating system.
     *        Shortcut of the <code>System.Environment.Is64BitOperatingSystem</code>
     * </summary>
     * <returns>
     *   [FR] <value>true</value> si le système d'exploitation est de type 64 bits ; sinon, <value>false</value>.
     *   [EN] <value>true</value> if the operating system is 64-bit; otherwise, <value>false</value>.
     * </returns>
     **/
    public static readonly bool EstEn64Bit = Environment.Is64BitOperatingSystem;

    public static readonly Dictionary<string, object> Repertoire = new(){
      {
        "ProgramFiles",
        /**
         * [FR] Fournit le chemin d'accès au dossier Program Files/Program Files (x86)
         *      Racourci du <code>System.Environment.GetEnvironmentVariable</code>
         * [EN] Provides path to Program Files/Program Files folder (x86)
         *      Shortcut of the <code>System.Environment.GetEnvironmentVariable</code>
         **/
        EstEn64Bit ? Environment.GetEnvironmentVariable("ProgramFiles") : Environment.GetEnvironmentVariable("ProgramFiles(x86)")
      },
      {
        "Documents",
        Environment.GetFolderPath(Environment.SpecialFolder.Personal)
      },
      {
        "Societe",
        new DossierReference(
          Chemins: Chemin.Combiner(
            Chemin1: ObtenirLeNomDuRepertoire(
              Chemin : Assembly.GetExecutingAssembly().ObtenirLemplacementDorigine()
            ),
            Chemin2: ".."
          )
        )
      },
      {
        "Racine",
        new DossierReference(
          Chemins: Chemin.Combiner(
            Chemin: ObtenirLeNomDuRepertoire(
              Chemin: Assembly.GetExecutingAssembly().ObtenirLemplacementDorigine()
            )
          )
        )
      },
      {
        "Config",
        new DossierReference(
          Chemins: Chemin.Combiner(
            Chemin1: ObtenirLeNomDuRepertoire(
              Chemin: Assembly.GetExecutingAssembly().ObtenirLemplacementDorigine()
            ),
            Chemin2: "Config"
          )
        )
      },
      {
        "Source",
        new DossierReference(
          Chemins: Chemin.Combiner(
            Chemin1: Chemin.Combiner(
              Chemin1: ObtenirLeNomDuRepertoire(
                Chemin: Assembly.GetExecutingAssembly().ObtenirLemplacementDorigine()),
              Chemin2: ".."
            ),
            Chemin2: "Source"
          )
        )
      }
    };
  }
}
