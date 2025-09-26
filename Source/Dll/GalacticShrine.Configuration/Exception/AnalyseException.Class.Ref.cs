/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Reflection;
using GalacticShrine.Configuration.Properties;

namespace GalacticShrine.Exception.Configuration {

  /**
   * <summary>
   *   [FR] Représente une erreur survenue lors de l'analyse des données.
   *   [EN] Represents an error during data analysis.
   * </summary>
   **/
  public class AnalyseException : System.Exception {
    
    public Version VersionLib { get;}
    
    public uint NumeroDeLaLigne {get;}
    
    public string ContenuDeLaLigne { get;}
    
    public AnalyseException(string Message, uint NumeroDeLigne) : this(Message: Message, NumeroDeLigne: NumeroDeLigne, ContenuDeLigne: string.Empty, ExceptionInterne: null) { }

    public AnalyseException(string Message, System.Exception ExceptionInterne) : this(Message: Message, NumeroDeLigne: 0, ContenuDeLigne: string.Empty, ExceptionInterne: ExceptionInterne) { }
    
    public AnalyseException(string Message, uint NumeroDeLigne, string ContenuDeLigne) : this(Message: Message, NumeroDeLigne: NumeroDeLigne, ContenuDeLigne: ContenuDeLigne, ExceptionInterne: null) { }
    
    public AnalyseException(string Message, uint NumeroDeLigne, string ContenuDeLigne, System.Exception ExceptionInterne) : base(message: $"{Message} {Resources.MessageExceptionErreurAnalyse0} {NumeroDeLigne} {Resources.MessageExceptionErreurAnalyse1} '{ContenuDeLigne}'", innerException: ExceptionInterne) {//Resources.MessageExceptionErreurAnalyse,

      VersionLib = ObtenirLaVersionDeL_Assemblage();
      NumeroDeLaLigne = NumeroDeLigne;
      ContenuDeLaLigne = ContenuDeLigne;
    }

    private Version ObtenirLaVersionDeL_Assemblage() => Assembly.GetExecutingAssembly().GetName().Version;
  }
}
