/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.IO;

namespace Gs {

  /**
   * <summary>
   *   [FR] Classe de base pour les outil.
   *   [EN] Basic class for tools.
   * </summary>
   **/
  [Serializable]
  public abstract class Gs {

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

    /**
     * <summary>
     *   [FR] Fournit le chemin d'accès au dossier Program Files/Program Files (x86)
     *        Racourci du <code>System.Environment.GetEnvironmentVariable</code>
     *   [EN] Provides path to Program Files/Program Files folder (x86)
     *        Shortcut of the <code>System.Environment.GetEnvironmentVariable</code>
     * </summary>
     * <returns>
     *   string
     * </returns>
     **/
    public readonly string ProgramFiles = EstEn64Bit ? Environment.GetEnvironmentVariable("ProgramFiles") : Environment.GetEnvironmentVariable("ProgramFiles(x86)");
    //public Gs () => Chemin = EstEn64Bit ? Environment.GetEnvironmentVariable("ProgramFiles") : Environment.GetEnvironmentVariable("ProgramFiles(x86)");

    /**
     * <summary>
     *   [FR] Separateur de repertoire.
     *        Racourci du <code>System.IO.Path.DirectorySeparatorChar</code>
     *   [EN] Directory separator.
     *        Shortcut of the <code>System.IO.Path.DirectorySeparatorChar</code>
     * </summary>
     **/
    public static readonly char Rs = Path.DirectorySeparatorChar;

    /**
     * <summary>
     *   [FR] Separateur de repertoire.
     *        Racourci du <code>System.IO.Path.DirectorySeparatorChar</code>
     *   [EN] Directory separator.
     *        Shortcut of the <code>System.IO.Path.DirectorySeparatorChar</code>
     * </summary>
     **/
    public static readonly char RepertoireSeparateur = Path.DirectorySeparatorChar;
  }
}
