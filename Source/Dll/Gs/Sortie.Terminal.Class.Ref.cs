/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using Gs.Enumeration;

namespace Gs.Terminal {

	public static class Sortie {

    /**
     * <summary>
     *   [FR] Écrit la valeur de la chaîne spécifiée sur le flux de sortie standard.
     *        Racourci du <code>System.Console.Write</code>
     *   [EN] Writes the specified string value to the standard output stream.
     *        Shortcut of the <code>System.Console.Write</code>
     * </summary>
     * <param name="Texte">
     *   [FR] Texte à renvoyer
     *   [EN] Text to return
     * </param>
     * <returns>
     *   chaîne
     * </returns>
     **/
    public static void Ecrire(string Texte) => Console.Write(Texte);

    /**
     * <summary>
     *   [FR] Écrit la valeur de la chaîne spécifiée sur le flux de sortie standard.
     *        Racourci du <code>System.Console.Write</code>
     *   [EN] Writes the specified string value to the standard output stream.
     *        Shortcut of the <code>System.Console.Write</code>
     * </summary>
     * <param name="Texte">
     *   [FR] Texte à renvoyer
     *   [EN] Text to return
     * </param>
     * </summary>
     * <param name="Argument">
     *   [FR] Argument à renvoyer
     *   [EN] Argument to return
     * </param>
     * <returns>
     *   Une chaîne plus l'argument
     * </returns>
     **/
    public static void Ecrire(string Texte, object Argument) => Console.Write(Texte, Argument);

    /**
     * <summary>
     *   [FR] Écrit la valeur de la chaîne de caractères spécifiée, suivie de la fin de la ligne en cours, dans le flux de sortie standard.
     *        Racourci du <code>System.Console.Write</code>
     *   [EN] Writes the specified string value, followed by the current line terminator, to the standard output stream.
     *        Shortcut of the <code>System.Console.Write</code>
     * </summary>
     * <param name="ReserveToutLaLigne">
     *   [FR] Indique si la ligne entière est prise ou non
     *   [EN] Indicates whether the entire line is taken or not
     * </param>
     * <param name="Texte">
     *   [FR] Texte à renvoyer
     *   [EN] Text to return
     * </param>
     * <returns>
     *   chaîne
     * </returns>
     **/
    public static void Ecrire(bool ReserveToutLaLigne, string Texte) {

      switch (ReserveToutLaLigne) {

        case true:

          int Dimension = Console.WindowWidth - 1;
          
          if (Dimension != Console.CursorLeft + 1) {

            Dimension = Dimension - Console.CursorLeft;
          }
          
          if (Dimension < Texte.Length) {

            Dimension = Console.WindowWidth + Dimension;
          }

          if (Dimension < 0) {

            Dimension = Texte.Length;
          }
          
          Console.WriteLine($"{Texte.PadRight(Dimension)}");
          break;

        default:

          Ecrire(Texte);
          break;
      }
    }

    /**
     * <summary>
     *   [FR] Écrit la valeur de la chaîne de caractères spécifiée, suivie de la fin de la ligne en cours, dans le flux de sortie standard.
     *        Racourci du <code>System.Console.Write</code>
     *   [EN] Writes the specified string value, followed by the current line terminator, to the standard output stream.
     *        Shortcut of the <code>System.Console.Write</code>
     * </summary>
     * <param name="ReserveToutLaLigne">
     *   [FR] Indique si la ligne entière est prise ou non
     *   [EN] Indicates whether the entire line is taken or not
     * </param>
     * <param name="Texte">
     *   [FR] Texte à renvoyer
     *   [EN] Text to return
     * </param>
     * * </summary>
     * <param name="Argument">
     *   [FR] Argument à renvoyer
     *   [EN] Argument to return
     * </param>
     * <returns>
     *   chaîne
     * </returns>
     **/
    public static void Ecrire(bool ReserveToutLaLigne, string Texte, object Argument) {

      switch(ReserveToutLaLigne) {

        case true:

          int Dimension = Console.WindowWidth - 1;

          if(Dimension != Console.CursorLeft + 1) {

            Dimension = Dimension - Console.CursorLeft;
          }

          if(Dimension < Texte.Length) {

            Dimension = Console.WindowWidth + Dimension;
          }

          if(Dimension < 0) {

            Dimension = Texte.Length;
          }

          Console.WriteLine($"{Texte.PadRight(Dimension)}", Argument);
          break;

        default:

          Ecrire(Texte, Argument);
          break;
      }
    }

    public static void Aligner(string Text) => Ecrire(true, $"{Text.PadRight(Console.WindowWidth - 1)}");

    public static void Aligner(string Text, object Argument) => Ecrire(true, $"{Text.PadRight(Console.WindowWidth - 1)}", Argument);

    public static void Aligner(Alignement Alignement, string Texte) {

      switch (Alignement) {

        case Alignement.Droite:
          Ecrire(true, $"{Texte.PadRight(Console.WindowWidth - 1)}");
          break;

        case Alignement.Gauche:

          Ecrire(true, $"{Texte.PadLeft(Console.WindowWidth - 1)}");
          break;

        case Alignement.Centre:

          decimal Taille = Console.WindowWidth - 1 - Texte.Length;
          int TailleDroite = (int)Math.Round(Taille / 2);
          int TailleGauche = (int)(Taille - TailleDroite);
          string MargeGauche = new String(' ', TailleGauche);
          string MargeDroite = new String(' ', TailleDroite);

          Ecrire(MargeGauche);
          Ecrire(Texte);
          Ecrire(true, MargeDroite);
          break;

        default:

          Ecrire(true, $"{Texte.PadRight(Console.WindowWidth - 1)}");
          break;
      }
    }

    public static void Aligner(Alignement Alignement, string Texte, object Argument) {

      switch(Alignement) {

        case Alignement.Droite:
          Ecrire(true, $"{Texte.PadRight(Console.WindowWidth - 1)}", Argument);
          break;

        case Alignement.Gauche:

          Ecrire(true, $"{Texte.PadLeft(Console.WindowWidth - 1)}", Argument);
          break;

        case Alignement.Centre:

          decimal Taille = Console.WindowWidth - 1 - Texte.Length;
          int TailleDroite = (int)Math.Round(Taille / 2);
          int TailleGauche = (int)(Taille - TailleDroite);
          string MargeGauche = new String(' ', TailleGauche);
          string MargeDroite = new String(' ', TailleDroite);

          Ecrire(MargeGauche);
          Ecrire(Texte, Argument);
          Ecrire(true, MargeDroite);
          break;

        default:

          Ecrire(true, $"{Texte.PadRight(Console.WindowWidth - 1)}");
          break;
      }
    }

    public static void LigneSeparatriceDecoree(char Caractere) {

      string Texte = new String(Caractere, Console.WindowWidth - 1);
      Ecrire(true, Texte);
    }

    public static void LigneSeparatrice(int? Lignes = 1) {

      for (int i = 0; i < Lignes; i++) {

        LigneSeparatriceDecoree(' ');
      }
    }

		public static void Extreme(string Gauche, string Droit) {

			decimal Taille  = Console.WindowWidth - 1;
			int MargeDroite = (int)Math.Round(Taille / 2);
			int MargeGauche = (int)(Taille - MargeDroite);

      Ecrire($"{Gauche}".PadRight(MargeDroite));
      Ecrire(true, $"{Droit}".PadLeft(MargeGauche));
    }
	}
}