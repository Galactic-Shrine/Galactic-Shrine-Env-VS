/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.IO;
using static Gs.Gs;

namespace Gs.Test.Terminal {
  internal class AutoGenere {

    public AutoGenere() {

      string fileName = $"Config{Rs}.ini";
      DateTime date = DateTime.Now;

      try {

        // Check if file already exists. If yes, delete it.     
        if(File.Exists(fileName)) {
          //File.Delete(fileName);
        }
        else {
          // Create a new file     
          using(StreamWriter sw = File.CreateText(fileName)) {

            sw.WriteLine("; Copyright © 2018 - {0}, Galactic-Shrine - Tous droits réservés.", date.ToString("yyyy"));
            sw.WriteLine(";");
            sw.WriteLine("; Fichier Auto-généré le: {0} à {1}", date.ToString("dddd d MMMM yyyy"), date.ToString("HH:mm K UTC"));
            sw.WriteLine("");
            sw.WriteLine("[Terminal]");
            sw.WriteLine("");
            sw.WriteLine("DefaultTemplate = Sombre");
          }
        }
      }
      catch(Exception Ex) {

        Console.WriteLine(Ex.ToString());
      }
    }
  }
}
