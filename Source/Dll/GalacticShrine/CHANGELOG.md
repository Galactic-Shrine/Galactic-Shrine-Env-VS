# Changelog

> Nous sommes désolés : aucun changelog n’avait été maintenu jusqu’à présent.  
> Cet historique démarre avec la version ci-dessous ; les changements antérieurs (2023–2025) n’ont pas été consignés.

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

### Non inclus / réservés pour **1.2.0**
> Ces éléments existent en local mais **ne sont pas publiés** dans cette version.
- `Services.Http.VerificateurDeServeur` (vérification HTTP périodique, journalisation).
- API **asynchrones** de `FichierReference` (lectures/écritures async, attributs/dates async, `AjouterLigneAsync`, etc.).

### À venir
- Publication de `VerificateurDeServeur` et des API async de `FichierReference` dans **1.2.0**.
- Eventuel passage de la journalisation vers une file d’écriture asynchrone.

---

## Historique antérieur
- Non documenté (projet initial démarré en 2023).
