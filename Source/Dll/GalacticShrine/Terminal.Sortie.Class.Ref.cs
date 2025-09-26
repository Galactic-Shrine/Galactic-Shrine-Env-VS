/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using GalacticShrine.Enumeration;

namespace GalacticShrine.Terminal {

  /**
   * <summary>
   *   [FR] Classe statique Sortie fournissant des méthodes simplifiées pour écrire et aligner du texte dans le terminal.
   *   [EN] Static Sortie class providing simplified methods to write and align text in the terminal.
   * </summary>
   **/
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

    /**
     * <summary>
     *   [FR] Aligne le texte spécifié avec une couleur donnée.
     *   [EN] Aligns the specified text with a given color.
     * </summary>
     * <param name="Text">
     *   [FR] Le texte à aligner.
     *   [EN] The text to align.
     * </param>
     **/
    public static void Aligner(string Text) => Ecrire(true, $"{Text.PadRight(Console.WindowWidth - 1)}");

    /**
     * <summary>
     *   [FR] Aligne le texte spécifié avec une couleur donnée et un argument formaté.
     *   [EN] Aligns the specified text with a given color and a formatted argument.
     * </summary>
     * <param name="Text">
     *   [FR] Le texte à aligner.
     *   [EN] The text to align.
     * </param>
     * <param name="Argument">
     *   [FR] Argument à insérer dans le texte formaté.
     *   [EN] Argument to insert into the formatted text.
     * </param>
     **/
    public static void Aligner(string Text, object Argument) => Ecrire(true, $"{Text.PadRight(Console.WindowWidth - 1)}", Argument);

    /**
     * <summary>
     *   [FR] Aligne le texte avec un alignement spécifique.
     *   [EN] Aligns the text with a specific alignment.
     * </summary>
     * <param name="Alignement">
     *   [FR] Type d'alignement à appliquer (Droite, Gauche, Centre).
     *   [EN] Type of alignment to apply (Right, Left, Center).
     * </param>
     * <param name="Texte">
     *   [FR] Le texte à aligner.
     *   [EN] The text to align.
     * </param>
     **/
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

    /**
     * <summary>
     *   [FR] Aligne le texte formaté avec un alignement spécifique et un argument.
     *   [EN] Aligns the formatted text with a specific alignment and an argument.
     * </summary>
     * <param name="Alignement">
     *   [FR] Type d'alignement à appliquer (Droite, Gauche, Centre).
     *   [EN] Type of alignment to apply (Right, Left, Center).
     * </param>
     * <param name="Texte">
     *   [FR] Le texte à aligner, pouvant contenir des formats.
     *   [EN] The text to align, potentially containing formats.
     * </param>
     * <param name="Argument">
     *   [FR] Argument à insérer dans le texte formaté.
     *   [EN] Argument to insert into the formatted text.
     * </param>
     **/
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

    /**
     * <summary>
     *   [FR] Dessine une ligne séparatrice décorée avec un caractère spécifique et une couleur donnée.
     *   [EN] Draws a decorated separator line with a specific character and a given color.
     * </summary>
     * <param name="Caractere">
     *   [FR] Le caractère à utiliser pour dessiner la ligne séparatrice.
     *   [EN] The character to use for drawing the separator line.
     * </param>
     **/
    public static void LigneSeparatriceDecoree(char Caractere) {

      string Texte = new String(Caractere, Console.WindowWidth - 1);
      Ecrire(true, Texte);
    }

    /**
     * <summary>
     *   [FR] Dessine une ou plusieurs lignes séparatrices avec une couleur par défaut.
     *   [EN] Draws one or more separator lines with a default color.
     * </summary>
     * <param name="Lignes">
     *   [FR] Nombre de lignes séparatrices à dessiner. Par défaut, 1.
     *   [EN] Number of separator lines to draw. Defaults to 1.
     * </param>
     **/
    public static void LigneSeparatrice(int? Lignes = 1) {

      for (int i = 0; i < Lignes; i++) {

        LigneSeparatriceDecoree(' ');
      }
    }

    /**
     * <summary>
     *   [FR] Aligne une division de texte séparée par un caractère spécifique avec une couleur donnée.
     *   [EN] Align a text division separated by a specific character with a given color.
     * </summary>
     * <param name="Gauche">
     *   [FR] Texte à aligner à gauche de la division.
     *   [EN] Text to align to the left of the division.
     * </param>
     * <param name="Droit">
     *   [FR] Texte à aligner à droite de la division.
     *   [EN] Text to align to the right of the division.
     * </param>
     **/
    public static void Extreme(string Gauche, string Droit) {

			decimal Taille  = Console.WindowWidth - 1;
			int MargeDroite = (int)Math.Round(Taille / 2);
			int MargeGauche = (int)(Taille - MargeDroite);

      Ecrire($"{Gauche}".PadRight(MargeDroite));
      Ecrire(true, $"{Droit}".PadLeft(MargeGauche));
    }
	}
}