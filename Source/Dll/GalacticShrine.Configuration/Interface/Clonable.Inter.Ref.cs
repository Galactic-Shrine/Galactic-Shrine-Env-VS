/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

namespace GalacticShrine.Interface.Configuration {

  /**
   * <summary>
   *   [FR] Crée une copie en profondeur du type <typeparamref name="T"/>, ce qui signifie que tous les types
   *        de référence sont également copiés au lieu de copier la référence.
   *   [EN] Creates a deep copy of type <typeparamref name="T"/>, which means that all reference types
   *        are also copied instead of copying the reference.
   * </summary>
   **/
  public interface ClonableInterface<T> where T : class {

    T CloneEnProfondeur();
  }
}
