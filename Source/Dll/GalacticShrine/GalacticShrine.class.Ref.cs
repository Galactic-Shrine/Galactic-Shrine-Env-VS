/**
 * Copyright © 2023-2025, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2023-2025, Galactic-Shrine - Tous droits réservés.
 * 
 * Mozilla Public License 2.0 / Licence Publique Mozilla 2.0
 *
 * This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
 * If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.
 * Modifications to this file must be shared under the same Mozilla Public License, v. 2.0.
 *
 * Cette Forme de Code Source est soumise aux termes de la Licence Publique Mozilla, version 2.0.
 * Si une copie de la MPL ne vous a pas été distribuée avec ce fichier, vous pouvez en obtenir une à l'adresse suivante : https://mozilla.org/MPL/2.0/.
 * Les modifications apportées à ce fichier doivent être partagées sous la même Licence Publique Mozilla, v. 2.0.
 **/

using System;
using System.Collections.Generic;
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
        "Roaming",
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
      },
      {
        "Societe",
        new DossierReference(
          Chemins: Chemin.Combiner(
            Chemin1: ObtenirLeNomDuRepertoire(
              Chemin : Assembly.GetExecutingAssembly().ObtenirL_EmplacementD_Origine()
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
              Chemin: Assembly.GetExecutingAssembly().ObtenirL_EmplacementD_Origine()
            )
          )
        )
      },
      {
        "DLog",
        new DossierReference(
          Chemins: Chemin.Combiner(
            Chemin1: Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            Chemin2: Chemin.Combiner(
              Chemin1: AssemblageReference.ObtenirL_Entreprise(),
              Chemin2: "Logs"
            )
            
          )
        )
      },
      {
        "Config",
        new DossierReference(
          Chemins: Chemin.Combiner(
            Chemin1: ObtenirLeNomDuRepertoire(
              Chemin: Assembly.GetExecutingAssembly().ObtenirL_EmplacementD_Origine()
            ),
            Chemin2: "Config"
          )
        )
      },
      {
        "Log",
        new DossierReference(
          Chemins: Chemin.Combiner(
            Chemin1: ObtenirLeNomDuRepertoire(
              Chemin: Assembly.GetExecutingAssembly().ObtenirL_EmplacementD_Origine()
            ),
            Chemin2: "Logs"
          )
        )
      },
      {
        "Source",
        new DossierReference(
          Chemins: Chemin.Combiner(
            Chemin1: Chemin.Combiner(
              Chemin1: ObtenirLeNomDuRepertoire(
                Chemin: Assembly.GetExecutingAssembly().ObtenirL_EmplacementD_Origine()),
              Chemin2: ".."
            ),
            Chemin2: "Source"
          )
        )
      }
    };
  }
}
