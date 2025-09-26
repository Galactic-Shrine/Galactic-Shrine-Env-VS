/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

namespace GalacticShrine.Interface.Configuration {
  /**
   * <summary>
   * [FR] Crée une copie en profondeur du type <typeparamref name="T"/>, c'est-à-dire que tous les types 
   *      de référence sont également copiés au lieu de ne copier que la référence.
   * [EN] Creates a deep copy of type <typeparamref name="T"/>, meaning all reference types 
   *      are also copied instead of just copying the reference.
   * </summary>
   * <typeparam name="T">
   * [FR] Le type de classe à cloner en profondeur.
   * [EN] The class type to be deeply cloned.
   * </typeparam>
   **/
  public interface ClonableInterface<T> where T : class {
    /**
     * <summary>
     * [FR] Crée un nouvel objet qui est une copie de l'instance actuelle.
     * [EN] Creates a new object that is a copy of the current instance.
     * </summary>
     * <remarks>
     * [FR] Retourne un nouvel objet qui est une copie de cette instance.
     * [EN] Returns a new object that is a copy of this instance.
     * </remarks>
     **/
    T CloneEnProfondeur();
  }
}
