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

namespace GalacticShrine.Configuration.Securite {
  class Crypt : ClonableInterface<Crypt> {

    public static byte[] Cle { get; set; }

    public Crypt CloneEnProfondeur() => MemberwiseClone() as Crypt;
  }
}
