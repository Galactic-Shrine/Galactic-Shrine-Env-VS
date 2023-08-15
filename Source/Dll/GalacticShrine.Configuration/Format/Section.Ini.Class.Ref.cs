/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticShrine.Interface.Configuration;

namespace GalacticShrine.Configuration.Format {
  public class Section : ClonableInterface<Section> {

    private string Nom;

    public Section CloneEnProfondeur() => new Section();
  }
}
