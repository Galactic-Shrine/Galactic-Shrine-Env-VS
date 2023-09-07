/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

namespace GalacticShrine.Terminal {

	public static class Couleurs {
    
    public static class Txt {

      public const string Default       = "text.default";
      public const string Black         = "text.black";
      public const string DarkBlue      = "text.darkBlue";
      public const string DarkGreen     = "text.darkGreen";
      public const string DarkCyan      = "text.darkCyan";
      public const string DarkRed       = "text.darkRed";
      public const string DarkMagenta   = "text.darkMagenta";
      public const string DarkYellow    = "text.darkYellow";
      public const string DarkGray      = "text.darkGray";
      public const string Blue          = "text.blue";
      public const string Green         = "text.green";
      public const string Cyan          = "text.cyan";
      public const string Red           = "text.red";
      public const string Magenta       = "text.magenta";
      public const string Yellow        = "text.yellow";
      public const string Gray          = "text.gray";
      public const string Sourdine      = "text.sourdine";
      public const string Primaire      = "text.primaire";
      public const string Succes        = "text.succes";
      public const string Info          = "text.info";
      public const string Avertissement = "text.avertissement";
      public const string Danger        = "text.danger";
    }

    public static class Bg {
      
      public const string Default       = "bg.default";
      public const string Black         = "bg.black";
      public const string DarkBlue      = "bg.darkBlue";
      public const string DarkGreen     = "bg.darkGreen";
      public const string DarkCyan      = "bg.darkCyan";
      public const string DarkRed       = "bg.darkRed";
      public const string DarkMagenta   = "bg.darkMagenta";
      public const string DarkYellow    = "bg.darkYellow";
      public const string DarkGray      = "bg.darkGray";
      public const string Blue          = "bg.blue";
      public const string Green         = "bg.green";
      public const string Cyan          = "bg.cyan";
      public const string Red           = "bg.red";
      public const string Magenta       = "bg.magenta";
      public const string Yellow        = "bg.yellow";
      public const string Gray          = "bg.gray";
      public const string Sourdine      = "bg.sourdine";
      public const string Primaire      = "bg.primaire";
      public const string Succes        = "bg.succes";
      public const string Info          = "bg.info";
      public const string Avertissement = "bg.avertissement";
      public const string Danger        = "bg.danger";
    }

    public static string TxtStatus(bool isValid) {

      return (isValid ? Txt.Primaire : Txt.Sourdine);
    }
  }
}
