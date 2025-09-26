/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using GalacticShrine.Interface.Terminal;
using GalacticShrine.Structure.Terminal;
using GalacticShrine.Enumeration;

namespace GalacticShrine.Terminal {

  /**
   * <summary>
   *   [FR] Classe Format gérant les opérations de formatage et d'affichage du texte dans le terminal avec gestion des couleurs.
   *   [EN] Format class managing text formatting and display operations in the terminal with color management.
   * </summary>
   **/
  public class Format {

    /**
     * <summary>
     *   [FR] Dictionnaire contenant les variations de thème associées à des clés de couleur.
     *   [EN] Dictionary containing theme variations associated with color keys.
     * </summary>
     **/
    Dictionary<string, Couleur> VariationDuTheme { get; set; }

    /**
     * <summary>
     *   [FR] Objet de verrouillage utilisé pour synchroniser l'accès aux opérations de couleur.
     *   [EN] Lock object used to synchronize access to color operations.
     * </summary>
     **/
    private readonly object VerrouillageDeCouleur = new();

    /**
     * <summary>
     *   [FR] Constructeur de la classe Format. Initialise les variations de thème et définit les couleurs par défaut.
     *   [EN] Constructor of the Format class. Initializes theme variations and sets default colors.
     * </summary>
     * <param name="Theme">
     *   [FR] Instance implémentant <see cref="CouleurInterface"/> représentant le thème à utiliser.
     *   [EN] Instance implementing <see cref="CouleurInterface"/> representing the theme to use.
     * </param>
     * <exception cref="ArgumentException">
     *   [FR] Lance une exception si l'argument <c>Theme</c> est <c>null</c>.
     *   [EN] Throws an exception if the <c>Theme</c> argument is <c>null</c>.
     * </exception>
     **/
    public Format(CouleurInterface Theme) {

      if(Theme == null) {

        throw new ArgumentException(message: nameof(Theme));
      }

      VariationDuTheme = Theme.Couleurs;
      CouleurParDefaut();
    }

    /**
     * <summary>
     *   [FR] Définit les couleurs par défaut de la console en utilisant une clé de couleur spécifique.
     *   [EN] Sets the console's default colors using a specific color key.
     * </summary>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour définir les couleurs par défaut. Par défaut, utilise <see cref="Couleurs.Bg.Default"/>.
     *   [EN] Color key to use for setting default colors. Defaults to <see cref="Couleurs.Bg.Default"/>.
     * </param>
     **/
    void CouleurParDefaut(string Couleur = Couleurs.Bg.Default) {

      var Theme = VariationDuTheme[Couleur];
      Console.BackgroundColor = Theme.ArrierePlan;
      Console.ForegroundColor = Theme.PremierPlan;
    }

    /**
     * <summary>
     *   [FR] Rétablit les couleurs par défaut de la console de manière thread-safe.
     *   [EN] Resets the console's default colors in a thread-safe manner.
     * </summary>
     **/
    public void RetablirCouleur() {

      lock(VerrouillageDeCouleur) {

        Console.ResetColor();
      }
    }

    /**
     * <summary>
     *   [FR] Définit la couleur de la console en fonction d'une clé de couleur spécifiée.
     *   [EN] Sets the console's color based on a specified color key.
     * </summary>
     * <param name="Couleur">
     *   [FR] Clé de couleur à définir.
     *   [EN] Color key to set.
     * </param>
     * <exception cref="KeyNotFoundException">
     *   [FR] Lance une exception si la clé de couleur n'est pas trouvée dans <see cref="VariationDuTheme"/>.
     *   [EN] Throws an exception if the color key is not found in <see cref="VariationDuTheme"/>.
     * </exception>
     **/
    void DefinirCouleur(string Couleur) {

      if(VariationDuTheme.TryGetValue(Couleur, out Couleur Value)) {

        Console.BackgroundColor = Value.ArrierePlan;
        Console.ForegroundColor = Value.PremierPlan;
      }
      else {

        throw new KeyNotFoundException();
      }
    }

    /**
     * <summary>
     *   [FR] Définit uniquement la couleur du texte de la console en fonction d'une clé de couleur spécifiée.
     *   [EN] Sets only the console's text color based on a specified color key.
     * </summary>
     * <param name="Couleur">
     *   [FR] Clé de couleur pour le texte.
     *   [EN] Color key for the text.
     * </param>
     * <exception cref="KeyNotFoundException">
     *   [FR] Lance une exception si la clé de couleur n'est pas trouvée dans <see cref="VariationDuTheme"/>.
     *   [EN] Throws an exception if the color key is not found in <see cref="VariationDuTheme"/>.
     * </exception>
     **/
    void DefinirCouleurTexte(string Couleur) {

      if(VariationDuTheme.TryGetValue(Couleur, out Couleur Value)) {

        Console.ForegroundColor = Value.PremierPlan;
      }
      else {

        throw new KeyNotFoundException();
      }
    }

    /**
     * <summary>
     *   [FR] Définit uniquement la couleur d'arrière-plan de la console en fonction d'une clé de couleur spécifiée.
     *   [EN] Sets only the console's background color based on a specified color key.
     * </summary>
     * <param name="Couleur">
     *   [FR] Clé de couleur pour l'arrière-plan.
     *   [EN] Color key for the background.
     * </param>
     * <exception cref="KeyNotFoundException">
     *   [FR] Lance une exception si la clé de couleur n'est pas trouvée dans <see cref="VariationDuTheme"/>.
     *   [EN] Throws an exception if the color key is not found in <see cref="VariationDuTheme"/>.
     * </exception>
     **/
    void DefinirCouleurArrierePlan(string Couleur) {

      if(VariationDuTheme.TryGetValue(Couleur, out Couleur Values)) {

        Console.BackgroundColor = Values.ArrierePlan;
      }
      else {

        throw new KeyNotFoundException();
      }
    }

    /**
     * <summary>
     *   [FR] Efface le contenu de la console de manière thread-safe.
     *   [EN] Clears the console content in a thread-safe manner.
     * </summary>
     **/
    public void Eclaircir() {

      lock(VerrouillageDeCouleur) {

        Console.Clear();
      }
    }

    /**
     * <summary>
     *   [FR] Écrit du texte dans la console avec une couleur spécifiée.
     *   [EN] Writes text to the console with a specified color.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à écrire.
     *   [EN] The text to write.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void Ecrire(string Texte, string Couleur = Couleurs.Txt.Default) {

      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Sortie.Ecrire(Texte);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Écrit du texte formaté dans la console avec une couleur spécifiée.
     *   [EN] Writes formatted text to the console with a specified color.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à écrire, pouvant contenir des formats.
     *   [EN] The text to write, potentially containing formats.
     * </param>
     * <param name="Argument">
     *   [FR] Argument à insérer dans le texte formaté.
     *   [EN] Argument to insert into the formatted text.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void Ecrire(string Texte, object Argument, string Couleur = Couleurs.Txt.Default) {

      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Sortie.Ecrire(Texte, Argument);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Écrit du texte dans la console en réservant toute la ligne avec une couleur spécifiée.
     *   [EN] Writes text to the console by reserving the entire line with a specified color.
     * </summary>
     * <param name="ReserveToutLaLigne">
     *   [FR] Indique si toute la ligne doit être réservée pour le texte.
     *   [EN] Indicates whether the entire line should be reserved for the text.
     * </param>
     * <param name="Texte">
     *   [FR] Le texte à écrire.
     *   [EN] The text to write.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void Ecrire(bool ReserveToutLaLigne, string Texte, string Couleur = Couleurs.Txt.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Sortie.Ecrire(ReserveToutLaLigne, Texte);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Écrit du texte formaté dans la console en réservant toute la ligne avec une couleur spécifiée.
     *   [EN] Writes formatted text to the console by reserving the entire line with a specified color.
     * </summary>
     * <param name="ReserveToutLaLigne">
     *   [FR] Indique si toute la ligne doit être réservée pour le texte.
     *   [EN] Indicates whether the entire line should be reserved for the text.
     * </param>
     * <param name="Texte">
     *   [FR] Le texte à écrire, pouvant contenir des formats.
     *   [EN] The text to write, potentially containing formats.
     * </param>
     * <param name="Argument">
     *   [FR] Argument à insérer dans le texte formaté.
     *   [EN] Argument to insert into the formatted text.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void Ecrire(bool ReserveToutLaLigne, string Texte, object Argument, string Couleur = Couleurs.Txt.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Sortie.Ecrire(ReserveToutLaLigne, Texte, Argument);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Écrit du texte avec deux couleurs distinctes pour le texte et l'arrière-plan.
     *   [EN] Writes text with two distinct colors for the text and background.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à écrire.
     *   [EN] The text to write.
     * </param>
     * <param name="CouleurTexte">
     *   [FR] Clé de couleur pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     * <param name="CouleurArrierePlan">
     *   [FR] Clé de couleur pour l'arrière-plan. Par défaut, utilise <see cref="Couleurs.Bg.Default"/>.
     *   [EN] Color key for the background. Defaults to <see cref="Couleurs.Bg.Default"/>.
     * </param>
     **/
    public void Ecrire2Couleur(string Texte, string CouleurTexte = Couleurs.Txt.Default, string CouleurArrierePlan = Couleurs.Bg.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleurTexte(CouleurTexte);
        DefinirCouleurArrierePlan(CouleurArrierePlan);
        Sortie.Ecrire(Texte);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Écrit du texte formaté avec deux couleurs distinctes pour le texte et l'arrière-plan.
     *   [EN] Writes formatted text with two distinct colors for the text and background.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à écrire, pouvant contenir des formats.
     *   [EN] The text to write, potentially containing formats.
     * </param>
     * <param name="Argument">
     *   [FR] Argument à insérer dans le texte formaté.
     *   [EN] Argument to insert into the formatted text.
     * </param>
     * <param name="CouleurTexte">
     *   [FR] Clé de couleur pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     * <param name="CouleurArrierePlan">
     *   [FR] Clé de couleur pour l'arrière-plan. Par défaut, utilise <see cref="Couleurs.Bg.Default"/>.
     *   [EN] Color key for the background. Defaults to <see cref="Couleurs.Bg.Default"/>.
     * </param>
     **/
    public void Ecrire2Couleur(string Texte, object Argument, string CouleurTexte = Couleurs.Txt.Default, string CouleurArrierePlan = Couleurs.Bg.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleurTexte(CouleurTexte);
        DefinirCouleurArrierePlan(CouleurArrierePlan);
        Sortie.Ecrire(Texte, Argument);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Écrit du texte en réservant toute la ligne avec deux couleurs distinctes pour le texte et l'arrière-plan.
     *   [EN] Writes text by reserving the entire line with two distinct colors for the text and background.
     * </summary>
     * <param name="ReserveToutLaLigne">
     *   [FR] Indique si toute la ligne doit être réservée pour le texte.
     *   [EN] Indicates whether the entire line should be reserved for the text.
     * </param>
     * <param name="Texte">
     *   [FR] Le texte à écrire.
     *   [EN] The text to write.
     * </param>
     * <param name="CouleurTexte">
     *   [FR] Clé de couleur pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     * <param name="CouleurArrierePlan">
     *   [FR] Clé de couleur pour l'arrière-plan. Par défaut, utilise <see cref="Couleurs.Bg.Default"/>.
     *   [EN] Color key for the background. Defaults to <see cref="Couleurs.Bg.Default"/>.
     * </param>
     **/
    public void Ecrire2Couleur(bool ReserveToutLaLigne, string Texte, string CouleurTexte = Couleurs.Txt.Default, string CouleurArrierePlan = Couleurs.Bg.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleurTexte(CouleurTexte);
        DefinirCouleurArrierePlan(CouleurArrierePlan);
        Sortie.Ecrire(ReserveToutLaLigne, Texte);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Aligne le texte spécifié avec une couleur donnée.
     *   [EN] Align the specified text with a given color.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à aligner.
     *   [EN] The text to align.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void Aligner(string Texte, string Couleur = Couleurs.Txt.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Sortie.Aligner(Texte);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Aligne le texte formaté avec une couleur donnée.
     *   [EN] Align the formatted text with a given color.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à aligner, pouvant contenir des formats.
     *   [EN] The text to align, potentially containing formats.
     * </param>
     * <param name="Argument">
     *   [FR] Argument à insérer dans le texte formaté.
     *   [EN] Argument to insert into the formatted text.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void Aligner(string Texte, object Argument, string Couleur = Couleurs.Txt.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Sortie.Aligner(Texte, Argument);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Aligne le texte avec un alignement spécifique et une couleur donnée.
     *   [EN] Align the text with a specific alignment and a given color.
     * </summary>
     * <param name="Alignement">
     *   [FR] Type d'alignement à appliquer (Droite, Gauche, Centre).
     *   [EN] Type of alignment to apply (Right, Left, Center).
     * </param>
     * <param name="Texte">
     *   [FR] Le texte à aligner.
     *   [EN] The text to align.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void Aligner(Alignement Alignement, string Texte, string Couleur = Couleurs.Txt.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Sortie.Aligner(Alignement, Texte);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Aligne le texte formaté avec un alignement spécifique et une couleur donnée.
     *   [EN] Align the formatted text with a specific alignment and a given color.
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
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void Aligner(Alignement Alignement, string Texte, object Argument, string Couleur = Couleurs.Txt.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Sortie.Aligner(Alignement, Texte, Argument);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Aligne le texte avec deux couleurs distinctes pour le texte et l'arrière-plan.
     *   [EN] Align the text with two distinct colors for the text and background.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à aligner.
     *   [EN] The text to align.
     * </param>
     * <param name="CouleurTexte">
     *   [FR] Clé de couleur pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     * <param name="CouleurArrierePlan">
     *   [FR] Clé de couleur pour l'arrière-plan. Par défaut, utilise <see cref="Couleurs.Bg.Default"/>.
     *   [EN] Color key for the background. Defaults to <see cref="Couleurs.Bg.Default"/>.
     * </param>
     **/
    public void Aligner2Couleur(string Texte, string CouleurTexte = Couleurs.Txt.Default, string CouleurArrierePlan = Couleurs.Bg.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleurTexte(CouleurTexte);
        DefinirCouleurArrierePlan(CouleurArrierePlan);
        Sortie.Aligner(Texte);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Aligne le texte formaté avec deux couleurs distinctes pour le texte et l'arrière-plan.
     *   [EN] Align the formatted text with two distinct colors for the text and background.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à aligner, pouvant contenir des formats.
     *   [EN] The text to align, potentially containing formats.
     * </param>
     * <param name="Argument">
     *   [FR] Argument à insérer dans le texte formaté.
     *   [EN] Argument to insert into the formatted text.
     * </param>
     * <param name="CouleurTexte">
     *   [FR] Clé de couleur pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     * <param name="CouleurArrierePlan">
     *   [FR] Clé de couleur pour l'arrière-plan. Par défaut, utilise <see cref="Couleurs.Bg.Default"/>.
     *   [EN] Color key for the background. Defaults to <see cref="Couleurs.Bg.Default"/>.
     * </param>
     **/
    public void Aligner2Couleur(string Texte, object Argument, string CouleurTexte = Couleurs.Txt.Default, string CouleurArrierePlan = Couleurs.Bg.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleurTexte(CouleurTexte);
        DefinirCouleurArrierePlan(CouleurArrierePlan);
        Sortie.Aligner(Texte, Argument);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Aligne le texte avec deux couleurs distinctes et un alignement spécifique.
     *   [EN] Align the text with two distinct colors and a specific alignment.
     * </summary>
     * <param name="Alignement">
     *   [FR] Type d'alignement à appliquer (Droite, Gauche, Centre).
     *   [EN] Type of alignment to apply (Right, Left, Center).
     * </param>
     * <param name="Texte">
     *   [FR] Le texte à aligner.
     *   [EN] The text to align.
     * </param>
     * <param name="CouleurTexte">
     *   [FR] Clé de couleur pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     * <param name="CouleurArrierePlan">
     *   [FR] Clé de couleur pour l'arrière-plan. Par défaut, utilise <see cref="Couleurs.Bg.Default"/>.
     *   [EN] Color key for the background. Defaults to <see cref="Couleurs.Bg.Default"/>.
     * </param>
     **/
    public void Aligner2Couleur(Alignement Alignement, string Texte, string CouleurTexte = Couleurs.Txt.Default, string CouleurArrierePlan = Couleurs.Bg.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleurTexte(CouleurTexte);
        DefinirCouleurArrierePlan(CouleurArrierePlan);
        Sortie.Aligner(Alignement, Texte);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Aligne le texte formaté avec deux couleurs distinctes et un alignement spécifique.
     *   [EN] Align the formatted text with two distinct colors and a specific alignment.
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
     * <param name="CouleurTexte">
     *   [FR] Clé de couleur pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     * <param name="CouleurArrierePlan">
     *   [FR] Clé de couleur pour l'arrière-plan. Par défaut, utilise <see cref="Couleurs.Bg.Default"/>.
     *   [EN] Color key for the background. Defaults to <see cref="Couleurs.Bg.Default"/>.
     * </param>
     **/
    public void Aligner2Couleur(Alignement Alignement, string Texte, object Argument, string CouleurTexte = Couleurs.Txt.Default, string CouleurArrierePlan = Couleurs.Bg.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleurTexte(CouleurTexte);
        DefinirCouleurArrierePlan(CouleurArrierePlan);
        Sortie.Aligner(Alignement, Texte, Argument);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Aligne une division de texte séparée par un caractère spécifique avec une couleur donnée.
     *   [EN] Align a text division separated by a specific character with a given color.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à aligner, contenant une division séparée par le caractère spécifié.
     *   [EN] The text to align, containing a division separated by the specified character.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void AlignerLaDivision(string Texte, string Couleur = Couleurs.Txt.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        string Gauche = Texte.Split('|')[0];
        string Droit = Texte.Split('|')[1];
        Sortie.Extreme(Gauche, Droit);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Enveloppe le texte fourni en utilisant le décorateur de texte.
     *   [EN] Wraps the provided text using the text decorator.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à envelopper.
     *   [EN] The text to wrap.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void Enveloppement(string Texte, string Couleur = Couleurs.Txt.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Decorateur.Texte(Texte);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Enveloppe le texte fourni avec deux couleurs distinctes pour le texte et l'arrière-plan.
     *   [EN] Wraps the provided text with two distinct colors for the text and background.
     * </summary>
     * <param name="Texte">
     *   [FR] Le texte à envelopper.
     *   [EN] The text to wrap.
     * </param>
     * <param name="CouleurTexte">
     *   [FR] Clé de couleur pour le texte. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key for the text. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     * <param name="CouleurArrierePlan">
     *   [FR] Clé de couleur pour l'arrière-plan. Par défaut, utilise <see cref="Couleurs.Bg.Default"/>.
     *   [EN] Color key for the background. Defaults to <see cref="Couleurs.Bg.Default"/>.
     * </param>
     **/
    public void Enveloppement2Couleur(string Texte, string CouleurTexte = Couleurs.Txt.Default, string CouleurArrierePlan = Couleurs.Bg.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleurTexte(CouleurTexte);
        DefinirCouleurArrierePlan(CouleurArrierePlan);
        Decorateur.Texte(Texte);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Dessine une ligne séparatrice décorée avec un caractère spécifique et une couleur donnée.
     *   [EN] Draws a decorated separator line with a specific character and a given color.
     * </summary>
     * <param name="Character">
     *   [FR] Le caractère à utiliser pour dessiner la ligne séparatrice.
     *   [EN] The character to use for drawing the separator line.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour la ligne séparatrice.
     *   [EN] Color key to use for the separator line.
     * </param>
     **/
    public void LigneSeparatriceDecoree(char Character, string Couleur) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Sortie.LigneSeparatriceDecoree(Character);
        CouleurParDefaut();
      }
    }

    /**
     * <summary>
     *   [FR] Dessine une ligne séparatrice décorée avec un caractère spécifique et une couleur par défaut.
     *   [EN] Draws a decorated separator line with a specific character and a default color.
     * </summary>
     * <param name="Character">
     *   [FR] Le caractère à utiliser pour dessiner la ligne séparatrice.
     *   [EN] The character to use for drawing the separator line.
     * </param>
     **/
    public void LigneSeparatriceDecoree(char Character) {

      LigneSeparatriceDecoree(Character, Couleurs.Txt.Default);
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
    public void LigneSeparatrice(int Lignes = 1) {

      LigneSeparatrice(Lignes, Couleurs.Txt.Default);
    }

    /**
     * <summary>
     *   [FR] Dessine une ligne séparatrice avec une couleur spécifiée.
     *   [EN] Draws a separator line with a specified color.
     * </summary>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour la ligne séparatrice.
     *   [EN] Color key to use for the separator line.
     * </param>
     **/
    public void LigneSeparatrice(string Couleur) {

      LigneSeparatrice(1, Couleur);
    }

    /**
     * <summary>
     *   [FR] Dessine une ou plusieurs lignes séparatrices avec une couleur spécifiée.
     *   [EN] Draws one or more separator lines with a specified color.
     * </summary>
     * <param name="Lignes">
     *   [FR] Nombre de lignes séparatrices à dessiner.
     *   [EN] Number of separator lines to draw.
     * </param>
     * <param name="Couleur">
     *   [FR] Clé de couleur à utiliser pour les lignes séparatrices. Par défaut, utilise <see cref="Couleurs.Txt.Default"/>.
     *   [EN] Color key to use for the separator lines. Defaults to <see cref="Couleurs.Txt.Default"/>.
     * </param>
     **/
    public void LigneSeparatrice(int Lignes, string Couleur = Couleurs.Txt.Default) {
      
      lock(VerrouillageDeCouleur) {

        DefinirCouleur(Couleur);
        Sortie.LigneSeparatrice(Lignes);
        CouleurParDefaut();
      }
    }
  }
}
