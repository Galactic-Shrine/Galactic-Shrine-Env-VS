/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Text;

namespace GalacticShrine.Terminal {

  /**
   * <summary>
   *   [FR] Classe statique Decorateur fournissant des méthodes pour formater et afficher du texte dans le terminal avec gestion de la largeur d'écran.
   *   [EN] Static Decorateur class providing methods to format and display text in the terminal with screen width management.
   * </summary>
   **/
  public static class Decorateur {
    /**
     * <summary>
     *   [FR] Largeur de l'écran du terminal, calculée en fonction de la largeur de la fenêtre de la console.
     *   [EN] Terminal screen width, calculated based on the console window width.
     * </summary>
     **/
    static int LargeurEcran { get; set; }

    /**
     * <summary>
     *   [FR] Affiche le texte fourni dans le terminal en gérant le retour à la ligne en fonction de la largeur de l'écran.
     *   [EN] Displays the provided text in the terminal by managing line breaks based on screen width.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à afficher dans le terminal.
     *   [EN] The text to display in the terminal.
     * </param>
     **/
    public static void Texte(string Texte) {

      StringBuilder Ligne = new StringBuilder();
      string[] Mots = Texte.Split(' ');
      LargeurEcran = (Console.WindowWidth - 3);

      foreach(var Element in Mots) {

        Lignes(ref Ligne, Element);
        Elements(ref Ligne, Element);
      }

      if(!String.IsNullOrEmpty(Ligne.ToString().Trim())) {

        Sortie.Ecrire(true, $"{Ligne.ToString().TrimEnd()}");
      }
    }

    /**
     * <summary>
     *   [FR] Gère l'écriture des lignes dans le terminal en fonction de la largeur de l'écran.
     *   [EN] Manages writing lines to the terminal based on screen width.
     * </summary>
     * <param name="Ligne">
     *   [FR] Référence au StringBuilder accumulant le contenu de la ligne actuelle.
     *   [EN] Reference to the StringBuilder accumulating the current line's content.
     * </param>
     * <param name="Element">
     *   [FR] Le mot actuel à ajouter à la ligne.
     *   [EN] The current word to add to the line.
     * </param>
     **/
    static void Lignes(ref StringBuilder Ligne, string Element) {

      if(((Ligne.Length + Element.Length) >= LargeurEcran) || (Ligne.ToString().Contains(Environment.NewLine))) {

        Sortie.Ecrire(true, $"{Ligne.ToString().TrimEnd()}");
        Ligne.Clear();
      }
    }

    /**
     * <summary>
     *   [FR] Gère l'ajout des éléments (mots) à la ligne en cours, en tenant compte de la largeur de l'écran.
     *   [EN] Manages adding elements (words) to the current line, considering screen width.
     * </summary>
     * <param name="Ligne">
     *   [FR] Référence au StringBuilder accumulant le contenu de la ligne actuelle.
     *   [EN] Reference to the StringBuilder accumulating the current line's content.
     * </param>
     * <param name="Element">
     *   [FR] Le mot actuel à ajouter à la ligne.
     *   [EN] The current word to add to the line.
     * </param>
     **/
    static void Elements(ref StringBuilder Ligne, string Element) {

      if(Element.Length >= LargeurEcran) {

        if(Ligne.Length > 0) {

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
