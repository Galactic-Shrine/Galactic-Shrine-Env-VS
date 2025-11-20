# Changelog

> Nous sommes désolés : aucun changelog n’avait été maintenu jusqu’à présent.  
> Cet historique démarre avec la version ci-dessous ; les changements antérieurs (2023–2025) n’ont pas été consignés.

---

## [1.2.0.115] - 2025-11-20

### Modifié
- Terminal / Thèmes
  - `GalacticShrine.Structure.Terminal.Couleur`
    - Struct rendue immuable (`readonly struct`) avec propriétés en lecture seule.
    - Ajout de la documentation XML bilingue (FR/EN).
  - `GalacticShrine.UI.Terminal.ThemeParams`
    - Propriété `Couleurs` désormais non-nullable et initialisée avec un dictionnaire vide.
    - Documentation clarifiée sur le rôle de `ConsoleParDefault`, `ConsoleArrierePlan` et `ConsolePremierPlan`.
  - `GalacticShrine.UI.Terminal.Theme`, `ThemeLumineux`, `ThemeSombre`
    - Construction des thèmes inchangée, mais utilisation sécurisée de `ThemeParams.Couleurs`.
  - `GalacticShrine.Interface.Terminal.CouleurInterface`
    - Utilisation cohérente de la propriété `Couleurs` côté implémentations.

- Formatage du terminal
  - `GalacticShrine.Terminal.Format`
    - Validation du paramètre `Theme` via `ArgumentNullException.ThrowIfNull`.
    - Vérification explicite que `Theme.Couleurs` est initialisé, avec `InvalidOperationException` en cas de configuration invalide.
    - Champ `VariationDuTheme` rendu `private readonly` pour garantir l’immuabilité après construction.
    - Méthodes internes (`DefinirCouleur*`, `CouleurParDefaut`) rendues privées, avec messages d’erreur plus explicites lorsqu’une clé de couleur est introuvable.
    - Documentation XML FR/EN complétée pour l’ensemble de l’API publique.

- Utilitaires Terminal
  - `GalacticShrine.Terminal.Terminal`
    - Conversion en classe statique.
    - Propriété `Controleur` exposée en lecture seule, avec construction tolérante multi-plateforme :
      - utilisation de `Utilisateur@Domaine` lorsque disponible ;
      - repli sur `Environment.UserName` en cas d’environnement sans domaine.
  - `GalacticShrine.Terminal.Couleurs`
    - Paramètre de `TxtStatus` renommé en `EstValide` (français) pour respecter les conventions de nommage.
    - Documentation XML mise à jour pour refléter ce changement.

- Sortie et décoration de texte
  - `GalacticShrine.Terminal.Sortie`
    - Documentation XML nettoyée (balises cohérentes, suppression des redondances).
    - Alignement des textes droite/gauche/centre clarifié et rendu plus lisible.
  - `GalacticShrine.Terminal.Decorateur`
    - Ajout de la validation de l’argument `Texte` avec `ArgumentNullException.ThrowIfNull`.
    - Calcul de `LargeurEcran` rendu plus robuste (valeur minimale garantie).
    - Nettoyage du code (utilisation de `var`, `StringBuilder`, `TrimEnd`, etc.) sans changement fonctionnel visible.

### Corrigé
- `GalacticShrine.Terminal.Sortie`
  - Correction d’incohérences possibles entre les modes d’alignement (gauche/droite/centre) et l’utilisation de `PadLeft` / `PadRight`.
- `GalacticShrine.Terminal.Decorateur`
  - Protection contre des largeurs de console invalides pouvant conduire à des comportements inattendus lors du découpage des mots.


## [1.2.0.114] - 2025-11-19

### Ajouté
- `GalacticShrine.Interface.StockageSessionInterface`
  - Méthode asynchrone `Task SauvegarderAsync(string Donnees, string Cle, CancellationToken JetonAnnulation = default)`  
    pour la sauvegarde non bloquante des données de session.
  - Méthode asynchrone `Task<string> ChargerAsync(string Cle, CancellationToken JetonAnnulation = default)`  
    pour le chargement non bloquant des données de session.

### Modifié
- `GalacticShrine.Stockage.Session`
  - Ajout des implémentations asynchrones `SauvegarderAsync` et `ChargerAsync` basées sur `FileStream` asynchrone,
    `CryptoStream` et propagation du `CancellationToken`.
  - Introduction de la méthode interne `ObtenirCleDeChiffrement(string Cle)` :
    - validation de la clé AES en UTF-8 (longueur 16, 24 ou 32 octets),
    - levée d’`ArgumentException` en cas de clé invalide.
  - Harmonisation de la gestion de l’extension du fichier de session :
    - utilisation de `Fichier.Extension["Scripts"][4]` pour initialiser `Extension`,
    - concaténation du nom de fichier sous la forme `${NomDeFichier}{Extension}` sans point redondant.
  - Renforcement de la robustesse lors du chargement :
    - lecture et validation stricte de l’IV (16 octets),
    - levée d’`InvalidDataException` lorsque l’IV ne peut pas être lu intégralement (fichier corrompu).
  - Mise à jour et clarification de la documentation XML bilingue (FR/EN) sur l’ensemble des méthodes
    de stockage de session (synchrones et asynchrones).

## [1.2.0.113] - 2025-11-19

### Ajouté
- **SystemeExploitation.Inconnu**  
  (`\Source\Dll\GalacticShrine\Enumeration\SystemeExploitation.Enum.Ref.cs`)
  - Nouvelle valeur explicite `Inconnu = 0` pour représenter un système d’exploitation inconnu ou non supporté.
  - Utilisée comme valeur de retour par défaut par les méthodes de détection de l’OS.

### Modifié
- **GalacticShrine.Enumeration.SystemeExploitation**  
  (`\Source\Dll\GalacticShrine\Enumeration\SystemeExploitation.Enum.Ref.cs`)
  - Enum enrichie et documentation FR/EN clarifiée.
  - Les valeurs suivent désormais `Inconnu = 0` :
    - `Mac` = 1,
    - `Linux` = 2,
    - `Windows` = 3.

- **GalacticShrine.Outils.Outils**  
  (`\Source\Dll\GalacticShrine\Outils\Outils.Class.Ref.cs`)
  - Légère clarification des commentaires FR/EN (aucun changement d’API).

- **GalacticShrine.Outils.OS**  
  (`\Source\Dll\GalacticShrine\Outils\Os.Class.Ref.cs`)
  - `ObtenirNomCourantes` :
    - Ne renvoie plus jamais `null`, mais l’une des valeurs : `"Windows"`, `"macOS"`, `"Linux"` ou `"Inconnu"`.
    - Logique simplifiée pour limiter les branches imbriquées.
  - `ObtenirIdCourantes` :
    - Retourne désormais `SystemeExploitation.Inconnu` si aucun OS connu n’est détecté.
    - Le fallback implicite sur `SystemeExploitation.Windows` pour les plateformes inconnues est supprimé.
  - Harmonisation de la documentation XML FR/EN.

- **GalacticShrine.Enumeration.WindowsTheme**  
  (`\Source\Dll\Galactic-Shrine\Enumeration\WindowsTheme.Enum.Ref.cs`)
  - Normalisation mineure du formatage (espaces, alignement) sans modification des valeurs ni de l’API.

- **GalacticShrine.Outils.WindowsThemeAssistant**  
  (`\Source\Dll\GalacticShrine\Outils\WindowsTheme.Assistant.Class.Ref.cs`)
  - `ObtenirThemeApp` et `ObtenirThemeSystem` :
    - Vérifient désormais explicitement la plateforme : sur un système non Windows, retournent immédiatement `WindowsTheme.Inconnu` sans accès au Registre.
    - Les accès au Registre (`Registry.CurrentUser.OpenSubKey(...)`) sont encapsulés dans un `try/catch` pour éviter les exceptions non gérées.
    - En cas de valeur de registre invalide ou manquante, l’API retourne `WindowsTheme.Inconnu` de manière déterministe.
  - Nettoyage de quelques coquilles (stray `;`) et amélioration des commentaires XML FR/EN.

### Corrigé
- **Détection de l’OS courant**  
  (`GalacticShrine.Outils.OS`)
  - Cas des systèmes non Windows/Linux/macOS mieux pris en compte :
    - `ObtenirNomCourantes` ne renvoie plus `null` mais `"Inconnu"`.
    - `ObtenirIdCourantes` ne force plus `SystemeExploitation.Windows` par défaut lorsque l’OS n’est pas reconnu.

- **Compatibilité multi-plateforme de WindowsThemeAssistant**
  - L’utilisation de `WindowsThemeAssistant` sur Linux/macOS ne provoque plus d’accès au Registre ni de comportement indéfini :
    - Sur toute plateforme non Windows, les thèmes applicatif et système sont systématiquement retournés comme `WindowsTheme.Inconnu`.
  - Les erreurs d’accès au Registre (droits, clés manquantes, valeurs inattendues) n’entrainent plus d’exception : l’API revient à un état sûr (`Inconnu`).

### Ruptures
- **SystemeExploitation (valeurs numériques sous-jacentes)**
  - L’ajout de `Inconnu = 0` en tête de l’énumération modifie les valeurs numériques des autres membres :
    - `Mac` passe de `0` à `1`,
    - `Linux` passe de `1` à `2`,
    - `Windows` passe de `2` à `3`.
  - Impacts possibles :
    - Données sérialisées ou stockées qui s’appuyaient sur les valeurs numériques brutes de l’énumération.
    - Éventuels `switch` ou comparaisons externes qui utilisent explicitement ces valeurs numériques.
  - L’API publique reste inchangée, mais la sémantique de la valeur par défaut (`0`) correspond désormais explicitement à un OS inconnu (`Inconnu`).

  
## [1.2.0.112] - 2025-11-18

### Modifié
- `GalacticShrine.Enregistrement.Journalisation`
  - Renforcement de la validation des paramètres `Chemin`, `Nom`, `Extension` et `Horodatage`
    avec des exceptions localisées (`Resources`).
  - Ajout d’un verrou interne pour rendre l’écriture dans les fichiers de log thread-safe.
  - Centralisation de la construction du chemin complet et prise en compte de l’extension configurable.
  - Gestion plus robuste du format d’horodatage : rétablissement du format par défaut
    `dd-MM-yyyy HH:mm:ss` lorsqu’une valeur vide est fournie.
  - Amélioration de la génération des messages de log :
    - horodatage basé sur `CultureInfo.InvariantCulture`,
    - meilleure utilisation des libellés localisés (`INFO`, `SUCCES`, `AVERTISSEMENT`, `ERREUR`).

### Ajouté
- Aucun nouvel élément public.

### Corrigé
- Réduction des risques de conditions de course lors d’écritures concurrentes dans les journaux.
- Meilleure cohérence entre les niveaux de journalisation et les messages localisés.

### Ruptures
- Aucune rupture d’API attendue : la surface publique de `Journalisation` est conservée.


## [1.2.0.111] — 2025-11-18

### Ajouté
- **GalacticShrine (répertoires standards)** (`\Source\Dll\GalacticShrine\GalacticShrine.class.Ref.cs`)
  - Méthodes utilitaires :
    - `ObtenirLeDossier(string Nom)` : accès typé (`DossierReference`) aux entrées de `Repertoire`.
    - `ObtenirLeChemin(string Nom)` : accès direct au chemin brut (`string`) pour les clés de type chemin simple.
- **Fichier (catalogue d’extensions)** (`\Source\Dll\GalacticShrine\Fichier.Class.Ref.cs`)
  - Méthodes de confort autour du catalogue `Extension` :
    - `EstExtensionDansGroupe(string ExtensionDeFichier, string NomDuGroupe)` : vérifie l’appartenance d’une extension à un groupe logique (Images, Audio, Gs, etc.).
    - `TrouverPremierGroupePourExtension(string ExtensionDeFichier)` : retourne le premier groupe qui contient l’extension (ou `null`).
- **Stockage.Groupe** (`\Source\Dll\GalacticShrine\Stockage\Groupe.Class.Ref.cs`)
  - Méthode `Contient(string ElementRecherche)` pour tester la présence d’un élément dans le groupe (comparaison insensible à la casse).
- **Stockage.Catalogue** (`\Source\Dll\GalacticShrine\Stockage\Catalogue.Class.Ref.cs`)
  - Propriétés / méthodes supplémentaires :
    - `Groupes` : énumération des instances de `Groupe`.
    - `Compter` : nombre total de groupes.
    - `ContientLeGroupe(string NomDuGroupe)` : test d’existence.
    - `EssayerObtenirGroupe(string NomDuGroupe, out Groupe Groupe)` : accès sécurisé sans exception.

### Modifié
- **GalacticShrine.Repertoire**
  - Construction déplacée dans une factory privée `CreerTableDesRepertoires` pour clarifier la logique d’initialisation.
  - Dictionnaire désormais créé avec `StringComparer.OrdinalIgnoreCase` (accès insensible à la casse aux clés comme `"Config"`, `"Log"`, `"Source"`, etc.).
  - Détection de `ProgramFiles` rendue plus robuste :
    - Priorité à `Environment.SpecialFolder.ProgramFiles`.
    - Fallback propre sur les variables d’environnement `ProgramFiles` / `ProgramFiles(x86)`.
    - Renvoie simplement aucune entrée si aucun chemin pertinent n’est disponible sur la plateforme (Linux/macOS).
  - Chemins standards (`Societe`, `Racine`, `Config`, `Log`, `Source`, `DLog`) toujours basés sur l’emplacement de l’assembly mais avec vérifications supplémentaires sur les chaînes vides.
- **Stockage.Groupe**
  - Validation plus stricte des arguments (`Nom` et `Element` ne peuvent pas être nuls ou vides).
  - Copie défensive du tableau d’éléments et cache d’une vue en lecture seule pour exposer `Element` sans risque de modification externe.
  - Documentation FR/EN complétée et clarifiée.
- **Stockage.Catalogue**
  - Le constructeur recopie désormais le dictionnaire source en validant chaque entrée (nom non vide, groupe non null).
  - L’indexeur `this[string NomDuGroupe]` vérifie l’argument et documente explicitement la `KeyNotFoundException`.
  - Normalisation systématique sur `StringComparer.OrdinalIgnoreCase` pour l’ensemble des accès.

- **Références de fichiers et de dossiers**
  - **FichierReference** (`\Source\Dll\GalacticShrine\Fichier.Reference.Class.Ref.cs`)  
    - Renforcement de la validation d’arguments sur les méthodes publiques (chemins, contenus, etc.).
    - Harmonisation des commentaires XML FR/EN et corrections de formulations.
  - **DossierReference** (`\Source\Dll\GalacticShrine\Dossier.Reference.Class.Ref.cs`)  
    - Documentation complétée.
    - Passage à un style plus moderne (utilisation de l’indexeur ^, etc.) en conservant l’API publique.

### Corrigé
- **DossierReference.FichiersEnumerer** (`\Source\Dll\GalacticShrine\Dossier.Reference.Class.Ref.cs`)
  - Les trois surcharges `FichiersEnumerer(...)` utilisaient `Directory.EnumerateDirectories` au lieu de `Directory.EnumerateFiles`, ce qui empêchait de lister les fichiers correctement.  
    → Correction pour énumérer les fichiers comme attendu.
- **FichierReference.RendreInscriptibles / RendreInscriptiblesAsync**
  - Correction de la manipulation des attributs : l’attribut `ReadOnly` est désormais correctement retiré au lieu de reconstruire un ensemble d’attributs incorrect.
- **GalacticShrine.Repertoire["ProgramFiles"]**
  - Correction de la logique 32/64 bits et du fallback : évite de renvoyer un chemin incohérent lorsque les variables d’environnement ne sont pas définies comme attendu.

### Ruptures
- Aucune rupture majeure identifiée :
  - Les signatures publiques existantes des types centraux (`FichierReference`, `DossierReference`, `Fichier`, `Stockage.Groupe`, `Stockage.Catalogue`, `GalacticShrine`) sont conservées.
  - Les changements sont principalement additifs (nouvelles méthodes utilitaires) et des durcissements internes (validation, corrections de bugs).


## [1.2.0.110] — 2025-10-26

### Ajouté
- **Stockage.Groupe** (`\Source\Dll\GalacticShrine\Stockage\Groupe.Class.Ref.cs`)
  - Type simple pour regrouper des éléments (indexeurs, `Range`, `Count`, vue en lecture seule).
- **Stockage.Catalogue** (`\Source\Dll\GalacticShrine\Stockage\Catalogue.Class.Ref.cs`)
  - Dictionnaire case-insensitive de groupes par nom, accès direct via l’indexeur et énumération des noms.
- **Fichier (catalogue d’extensions)** (`\Source\Dll\GalacticShrine\Fichier.Class.Ref.cs`)
  - Classe statique exposant `Fichier.Extension` (catalogue prêt à l’emploi : Images, Audio, Vidéo, Documents, Scripts, Archives, Fonts, Config, Gs).

### Modifié
- **Renommages structurels**
  - `Dossier.Class.Ref.cs` ➜ `Dossier.Reference.Class.Ref.cs`
  - `Fichier.Class.Ref.cs` ➜ `Fichier.Reference.Class.Ref.cs`
  - `Fichier.Systeme.Class.Ref.cs` ➜ `Fichier.Systeme.Reference.Class.Ref.cs`
    > (Un nouveau `Fichier.Class.Ref.cs` coexiste désormais pour le **catalogue d’extensions**.)

### Corrigé
- N/A

### Ruptures
- Aucune (ajouts non-cassants, renommages conservant les types *Reference* existants).


## [1.2.0.109] — 2025-09-26

### Ajouté
- **Services.Http.VerificateurDeServeur**
  - Vérification périodique d’une URL via HttpClient.
  - Événement `ServeurVerifie` (online/offline).
  - Journalisation via la classe `Journalisation`.
- **API asynchrones dans `FichierReference`**
  - Lecture : `LireTousLesOctetsAsync`, `LireToutLeTexteAsync` (± `Encoding`), `LireToutesLesLignesAsync` (± `Encoding`).
  - Écriture : `EcrireTousLesOctetsAsync`, `EcrireToutLeTexteAsync` (± `Encoding`), `EcrireToutesLesLignesAsync` (`IEnumerable<string>`/`string[]` ± `Encoding`), `AjouterLigneAsync` (± `Encoding`).
  - Attributs : `ObtenirDesAttributsAsync`, `DefinirDesAttributsAsync`, `RendreInscriptiblesAsync`.

### Modifié
- Documentation FR/EN complétée pour les nouvelles API.
- Harmonisation mineure des noms (ex. `LireToutLeTexte`, `LireToutesLesLignes`).

### Corrigé
- **Services.Http.VerificateurDeServeur**
- Petites fautes dans les commentaires EN (ex. “Writres” → “Writes”, “containig” → “containing”, “frome” → “from”).

### Ruptures
- Aucune (toutes les API synchrones existantes restent disponibles).

## [1.1.8.108] — 2025-09-26

### Ajouté
- Documentation **FR/EN** sur la majorité de l’API publique.
- Entêtes de licence harmonisés **Mozilla Public License 2.0 (MPL-2.0)** sur les fichiers.
- Méthodes d’extensions de sérialisation pour `FichierReference` (lecture/écriture via `BinaryReader`/`BinaryWriter`).

### Modifié
- Nettoyage des commentaires XML et normalisation des noms (ex. `LireToutLeTexte`, `LireToutesLesLignes`).
- Amélioration des messages d’exception (ex. validations de chemin/nom de fichier).

### Corrigé
- Fautes d’orthographe récurrentes dans les docs EN (ex. “containig” → “containing”, “Writres” → “Writes”, etc.).
- Petits ajustements de balises XML (`<returns>` manquants, coquilles).

---

## Historique antérieur
- Non documenté (projet initial démarré en 2023).
