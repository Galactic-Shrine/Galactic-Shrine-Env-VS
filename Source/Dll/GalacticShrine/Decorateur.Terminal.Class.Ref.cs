/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Text;

namespace GalacticShrine.Terminal {

	public static class Decorateur {

    static int LargeurEcran { get; set; }

    public static void Texte(string Texte) {

      StringBuilder Ligne = new StringBuilder();
      string[] Mots = Texte.Split(' ');
      LargeurEcran = (Console.WindowWidth - 3);

      foreach (var Element in Mots) {

        Lignes(ref Ligne, Element);
        Elements(ref Ligne, Element);
      }

      if (!String.IsNullOrEmpty(Ligne.ToString().Trim())) {

        Sortie.Ecrire(true, $"{Ligne.ToString().TrimEnd()}");
      }
    }

    static void Lignes(ref StringBuilder Ligne, string Element) {

      if (((Ligne.Length + Element.Length) >= LargeurEcran) || (Ligne.ToString().Contains(Environment.NewLine))) {

        Sortie.Ecrire(true, $"{Ligne.ToString().TrimEnd()}");
        Ligne.Clear();
      }
    }

    static void Elements(ref StringBuilder Ligne, string Element) {

      if (Element.Length >= LargeurEcran) {

        if (Ligne.Length > 0) {

          Sortie.Ecrire(true, $" {Ligne.ToString().TrimEnd()}");
          Ligne.Clear();
        }

        int TailleDesMorceaux = Element.Length - LargeurEcran;
        string Morceau = Element.Substring(0, LargeurEcran);

        Ligne.Append($"{Morceau} ");
        Lignes(ref Ligne, Element);

        Element = Element.Substring(LargeurEcran, TailleDesMorceaux);
        Elements(ref Ligne, Element);
      }
      else {

        Ligne.Append($"{Element} ");
      }
    }
  }
}
