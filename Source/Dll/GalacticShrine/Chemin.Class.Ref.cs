/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System.IO;

namespace GalacticShrine.IO {

  public static partial class Chemin {

    public static string Combiner(params string[] Chemin) => Path.Combine(paths: Chemin);

    public static string Combiner(string Chemin1, string Chemin2) => Path.Combine(path1: Chemin1, path2: Chemin2);

    public static string Combiner(string Chemin1, string Chemin2, string Chemin3) => Path.Combine(path1: Chemin1, path2: Chemin2, path3: Chemin3);

    public static string Combiner(string Chemin1, string Chemin2, string Chemin3, string Chemin4) => Path.Combine(path1: Chemin1, path2: Chemin2, path3: Chemin3, path4: Chemin4);

  }
}
