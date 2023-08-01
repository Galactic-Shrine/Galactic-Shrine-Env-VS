/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

namespace Gs.Terminal {

	public static class Couleurs {
    
    public static class Txt {

      public const string Default       = "text.default";
      public const string Magenta       = "text.magenta";
      public const string Sourdine      = "text.sourdine";
      public const string Primaire      = "text.primaire";
      public const string Succes        = "text.succes";
      public const string Info          = "text.info";
      public const string Avertissement = "text.avertissement";
      public const string Danger        = "text.danger";
    }

    public static class Bg {
      
      public const string Default       = "bg.default";
      public const string Magenta       = "bg.magenta";
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
