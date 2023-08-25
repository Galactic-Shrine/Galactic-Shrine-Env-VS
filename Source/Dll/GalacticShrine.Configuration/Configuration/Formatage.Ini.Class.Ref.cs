/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;

using GalacticShrine.Enumeration.Outils;
using GalacticShrine.Interface.Configuration;
using GalacticShrine.Outils;

namespace GalacticShrine.Configuration.Configuration {

  internal class FormatageIni : ClonableInterface<FormatageIni> {

    private uint NombreEspaceEntreLaCleEtAffectation;

    private uint NombreEspaceEntreAffectationEtLaValeur;

    public FormatageIni() {

      NombreEspacesEntreLaCleEtAffectation = 1;
      NombreEspacesEntreAffectationEtLaValeur = 1;
    }

    public string EspaceEntreLaCleEtAffectation { get; private set; }

    public string EspaceEntreAffectationEtLaValeur { get; private set; }

    public bool NouvelleLigneAvantLaSection { get; set; } = false;

    public bool NouvelleLigneApresLaSection { get; set; } = false;

    public bool NouvelleLigneAvantLaPropriete { get; set; } = false;

    public bool NouvelleLigneApresLaPropriete { get; set; } = false;

    public string NouvelleLigne {

      get {

        switch(OS.ObtenirIdCourantes) {

          case SystemeExploitation.Windows:
            return "\r\n";

          case SystemeExploitation.Linux:
          case SystemeExploitation.Mac:
          default:
            return "\n";
        }
      }
    }

    public uint NombreEspacesEntreLaCleEtAffectation {

      set {

        NombreEspaceEntreLaCleEtAffectation = value;
        EspaceEntreLaCleEtAffectation       = new string(' ', (int)value);
      }
    }

    public uint NombreEspacesEntreAffectationEtLaValeur {

      set {

        NombreEspaceEntreAffectationEtLaValeur = value;
        EspaceEntreAffectationEtLaValeur       = new string(' ', (int)value);
      }
    }

    public FormatageIni CloneEnProfondeur() => MemberwiseClone() as FormatageIni;
  }
}