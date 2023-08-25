/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticShrine.Enumeration.Configuration {

  public enum ComportementDesProprietesDupliquees {

    /**
     * <summary>
     *   [FR] Les clés dupliquées ne sont pas autorisées. Lorsqu'une clé dupliquée est trouvée, l'analyseur s'arrête avec une erreur.<br/>
     *   [EN] Duplicate keys are not allowed. When a duplicate key is found, the analyzer stops with an error.
     * </summary>
     **/
    DesactiverEtArretAvecErreur,

    /**
     * <summary>
     *   [FR] Les clés dupliquées sont autorisées. La valeur de la clé dupliquée sera la première valeur trouvée dans l'ensemble des noms de clés dupliquées.<br/>
     *   [EN] Duplicate keys are allowed. The value of the duplicated key will be the first value found in the set of duplicated key names.
     * </summary>
     **/
    AutoriserEtConserverLaPremiereValeur,

    /**
     * <summary>
     *   [FR] Les clés dupliquées sont autorisées. La valeur de la clé dupliquée sera la dernière valeur trouvée dans l'ensemble des noms de clés dupliquées.<br/>
     *   [EN] Duplicate keys are allowed. The value of the duplicated key will be the last value found in the set of duplicated key names.
     * </summary>
     **/
    AutoriserEtConserverLaDerniereValeur,

    /**
     * <summary>
     *   [FR] Les clés dupliquées sont autorisées. La valeur des clés dupliquées sera une chaîne qui résulte de la concaténation de toutes les valeurs dupliquées trouvées, séparées par le caractère.<br/>
     *   [EN] Duplicate keys are allowed. The value of duplicate keys will be a string resulting from the concatenation of all duplicate values found, separated by the character.
     * </summary>
     **/
    AutoriserEtConcatenerLesValeurs
  }
}
