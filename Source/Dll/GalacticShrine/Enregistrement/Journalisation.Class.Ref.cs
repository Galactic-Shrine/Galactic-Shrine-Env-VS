/**
 * Copyright © 2023-2025, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2023-2025, Galactic-Shrine - Tous droits réservés.
 * 
 * Mozilla Public License 2.0 / Licence Publique Mozilla 2.0
 *
 * This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
 * If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.
 * Modifications to this file must be shared under the same Mozilla Public License, v. 2.0.
 *
 * Cette Forme de Code Source est soumise aux termes de la Licence Publique Mozilla, version 2.0.
 * Si une copie de la MPL ne vous a pas été distribuée avec ce fichier, vous pouvez en obtenir une à l'adresse suivante : https://mozilla.org/MPL/2.0/.
 * Les modifications apportées à ce fichier doivent être partagées sous la même Licence Publique Mozilla, v. 2.0.
 **/
using System;
using System.Globalization;
using GalacticShrine.Enumeration;
using GalacticShrine.Properties;
using static GalacticShrine.DossierReference;
using static GalacticShrine.FichierReference;

namespace GalacticShrine.Enregistrement {

	/**
   * <summary>
   *  [FR] Fournit des méthodes pour journaliser des messages dans des fichiers texte.
	 *  [EN] Provides methods to log messages into text files.
	 * </summary>
	 * <remarks>
	 *  [FR] Le chemin et le nom du fichier doivent être définis avant le premier appel d'écriture.
	 *  [EN] Path and file name must be set before the first write call.
	 * </remarks>
   **/
	public class Journalisation {

		/**
     * <summary>
     *  [FR] Verrou pour la synchronisation des écritures dans le journal.
     *  [EN] Lock for synchronizing writes to the log.
     * </summary>
     **/
		private readonly object _VerrouEcriture = new();

		/**
     * <summary>
     *  [FR] Chemin du dossier de journalisation.
     *  [EN] Path to the logging folder.
     * </summary>
     **/
		private string _CheminDuDossier = $"{GalacticShrine.Repertoire["Log"]}";

		/**
     * <summary>
     *  [FR] Nom du fichier de journalisation sans extension.
     *  [EN] Log file name without extension.
     * </summary>
     **/
		private string _NomDuFichier;

		/**
     * <summary>
     *  [FR] Indique si un fichier de journalisation distinct doit être créé pour chaque jour.
     *  [EN] Indicates whether a separate log file should be created for each day.
     * </summary>
     **/
		private bool _UnParJour = true;

		/**
     * <summary>
     *  [FR] Date du jour au format « yyyyMMdd ».
     *  [EN] Current date in "yyyyMMdd" format.
     * </summary>
     **/
		private readonly string _DateDuJour = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

		/**
     * <summary>
     *  [FR] Format de la date/heure utilisée pour l'horodatage dans le journal.
     *  [EN] Date/time format used for timestamps in the log.
     * </summary>
     **/
		private string _Horodatage = "dd-MM-yyyy HH:mm:ss";

		/**
     * <summary>
     *  [FR] Extension du fichier de journalisation (par exemple « .log »).
     *  [EN] Log file extension (for example ".log").
     * </summary>
     **/
		private string _Extension = Fichier.Extension["Documents"][6];
		/**
     * <summary>
     *  [FR] Chemin complet du fichier de journalisation.
     *  [EN] Full path of the log file.
     * </summary>
     **/
		private string CheminComplet { get; set; }

		/**
     * <summary>
     *  [FR] Chemin du dossier de sortie
     *  [EN] Path to the output folder
     * </summary>
     **/
		public string Chemin {

      get => _CheminDuDossier;

      set {

        if(string.IsNullOrWhiteSpace(value: value))
          throw new ArgumentException(message: Resources.LeCheminNePeutPasEtreVide, paramName: nameof(value));

        // Vérifie et crée le dossier si nécessaire
        if(!VerifieSiExiste(Localisation: new DossierReference(value))) {
          Creer(Localisation: new DossierReference(value));
        }

        _CheminDuDossier = value;
        MettreAJourCheminComplet();
      }
    }

		/**
     * <summary>
     *  [FR] Nom du fichier de journalisation sans extension
     *  [EN] Log file name without extension
     * </summary>
     **/
		public string Nom {

      get => _NomDuFichier;

      set {

        if(string.IsNullOrWhiteSpace(value: value))
          throw new ArgumentException(message: Resources.LeNomDuFichierNePeutPasEtreVide, paramName: nameof(value));

        _NomDuFichier = value;
        MettreAJourCheminComplet();
      }
    }

		/**
     * <summary>
     *  [FR] Indique si un fichier de journalisation distinct doit être créé pour chaque jour.
     *  [EN] Indicates whether a separate log file should be created for each day.
     * </summary>
     **/
		public bool UnFichierParJour {
      
      get => _UnParJour; 
      
      set {

        _UnParJour = value;
        MettreAJourCheminComplet();
      }
    }

		/**
     * <summary>
     *  [FR] Extension du fichier de journalisation (par exemple « .log »).
     *  [EN] Log file extension (for example ".log").
     * </summary>
     **/
		public string Extension {

			get => _Extension;

			set {

				_Extension = string.IsNullOrWhiteSpace(value)
					? Fichier.Extension["Documents"][6]
					: value;

				MettreAJourCheminComplet();
			}
		}

		/**
     * <summary>
     *  [FR] Format de la date/heure utilisée pour l'horodatage dans le journal.
     *  [EN] Date/time format used for timestamps in the log.
     * </summary>
     * <remarks>
     *  [FR] Si la valeur fournie est vide, le format par défaut « dd-MM-yyyy HH:mm:ss » est rétabli.
     *  [EN] If the provided value is empty, the default format "dd-MM-yyyy HH:mm:ss" is restored.
     * </remarks>
     **/
		public string Horodatage {

			get => _Horodatage;

			set {

				_Horodatage = string.IsNullOrWhiteSpace(value)
					? "dd-MM-yyyy HH:mm:ss"
					: value;
			}
    }

		/**
     * <summary>
     *  [FR] Initialise une nouvelle instance de <see cref="Enregistrement.Journalisation"/>.
     *  [EN] Initializes a new instance of <see cref="Enregistrement.Journalisation"/>.
     * </summary>
     * <param name="CheminDuDossier">
     *  [FR] Chemin du dossier de journalisation (optionnel).
     *  [EN] Path to the logging folder (optional).
     * </param>
     * <param name="NomDuFichier">
     *  [FR] Nom du fichier de journalisation sans extension, ou <c>null</c> pour le définir plus tard.
     *  [EN] Log file name without extension, or <c>null</c> to set it later.
     * </param>
     * <param name="UnParJour">
     *  [FR] Indique si un fichier de journalisation distinct doit être créé pour chaque jour (par défaut : true).
     *  [EN] Indicates whether a separate log file should be created for each day (default: true).
     * </param>
     **/
		public Journalisation(string CheminDuDossier = null, string NomDuFichier = null, bool UnParJour = true) {

			if(!string.IsNullOrWhiteSpace(CheminDuDossier))
				Chemin = CheminDuDossier;

			if(!string.IsNullOrWhiteSpace(NomDuFichier))
				Nom = NomDuFichier;

			UnFichierParJour = UnParJour;
		}

		/**
     * <summary>
     *  [FR] Met à jour le chemin complet du fichier de journalisation en fonction des paramètres actuels.
     *  [EN] Updates the full path of the log file based on the current settings.
     * </summary>
     **/
		private void MettreAJourCheminComplet() {

      if(!string.IsNullOrWhiteSpace(value: _CheminDuDossier) && !string.IsNullOrWhiteSpace(value: _NomDuFichier)) {

        if(_UnParJour == true) {

          CheminComplet = IO.Chemin.Combiner(Chemin1: _CheminDuDossier, Chemin2: $"{_NomDuFichier}.{_DateDuJour}{Extension}");
        }
        else {

          CheminComplet = IO.Chemin.Combiner(Chemin1: _CheminDuDossier, Chemin2: $"{_NomDuFichier}{Extension}");
        }
      }
    }

		/**
     * <summary>
     *  [FR] Écrit un message dans le journal avec le niveau spécifié.
     *  [EN] Writes a message to the log with the specified level.
     * </summary>
     * <param name="Niveau">
     *  [FR] Niveau de journalisation.
     *  [EN] Logging level.
     * </param>
     * <param name="Message">
     *  [FR] Message à journaliser.
     *  [EN] Message to log.
     * </param>
     * <param name="TypePersonnalise">
     *  [FR] Type personnalisé optionnel pour le niveau personnalisé.
     *  [EN] Optional custom type for the custom level.
     * </param>
     * <param name="SousNiveau">
     *  [FR] Sous-niveau optionnel pour catégoriser davantage le message.
     *   [EN] Optional sub-level to further categorize the message.
     * </param>
     **/
		public void Message(Journalisations Niveau, string Message, string TypePersonnalise = null, string SousNiveau = null) => Enregistreur(Niveau: Niveau, Message: Message, TypePersonnalise: TypePersonnalise, SousNiveau: SousNiveau);

		/**
     * <summary>
     *  [FR] Journalise un message avec le niveau <see cref="Journalisations.Autre"/>.
     *  [EN] Logs a message with level <see cref="Journalisations.Autre"/>.
     * </summary>
     **/
		public void Autre(string Message) => Enregistreur(Niveau: Journalisations.Autre, Message: Message);

		/**
     * <summary>
     *  [FR] Journalise un message avec le niveau <see cref="Journalisations.Avertissement"/>.
     *  [EN] Logs a message with level <see cref="Journalisations.Avertissement"/>.
     * </summary>
     **/
		public void Avertissement(string Message) => Enregistreur(Niveau: Journalisations.Avertissement, Message: Message);

		/**
     * <summary>
     *  [FR] Journalise un message avec le niveau <see cref="Journalisations.Info"/>.
     *  [EN] Logs a message with level <see cref="Journalisations.Info"/>.
     * </summary>
     **/
		public void Info(string Message) => Enregistreur(Niveau: Journalisations.Info, Message: Message);

		/**
     * <summary>
     *  [FR] Journalise un message avec le niveau <see cref="Journalisations.Erreur"/>.
     *  [EN] Logs a message with level <see cref="Journalisations.Erreur"/>.
     * </summary>
     **/
		public void Erreur(string Message) => Enregistreur(Niveau: Journalisations.Erreur, Message: Message);

		/**
     * <summary>
     *  [FR] Journalise un message avec le niveau <see cref="Journalisations.Succes"/>.
     *  [EN] Logs a message with level <see cref="Journalisations.Succes"/>.
     * </summary>
     **/
		public void Succes(string Message) => Enregistreur(Niveau: Journalisations.Succes, Message: Message);

		/**
     * <summary>
     *  [FR] Journalise un message avec le niveau <see cref="Journalisations.Personnalise"/>.
     *  [EN] Logs a message with level <see cref="Journalisations.Personnalise"/>.
     * </summary>
     **/
		public void Personnalise(string TypePersonnalise, string Message) => Enregistreur(Niveau: Journalisations.Personnalise, Message: Message, TypePersonnalise: TypePersonnalise);

		/** 
		 * <summary>
     *  [FR] Écrit un message dans le journal avec le niveau <see cref="Journalisations.Avertissement"/> et un sous-niveau.
     *  [EN] Writes a message to the log with the level <see cref="Journalisations.Avertissement"/> and a sub-level.
     * </summary>
     * <param name="Message">
     *  [FR] Message à journaliser.
     *  [EN] Message to log.
     * </param>
     * <param name="SousNiveau">
     *  [FR] Sous-niveau pour catégoriser davantage le message.
     *  [EN] Sub-level to further categorize the message.
     * </param>
     **/
		public void Avertissement(string SousNiveau, string Message) => Enregistreur(Niveau: Journalisations.Avertissement, Message: Message, SousNiveau: SousNiveau);

		/** 
		 * <summary>
     *  [FR] Écrit un message dans le journal avec le niveau <see cref="Journalisations.Info"/> et un sous-niveau.
     *  [EN] Writes a message to the log with the level <see cref="Journalisations.Info"/> and a sub-level.
     * </summary>
     * <param name="Message">
     *  [FR] Message à journaliser.
     *  [EN] Message to log.
     * </param>
     * <param name="SousNiveau">
     *  [FR] Sous-niveau pour catégoriser davantage le message.
     *  [EN] Sub-level to further categorize the message.
     * </param>
     **/
		public void Info(string SousNiveau, string Message) => Enregistreur(Niveau: Journalisations.Info, Message: Message, SousNiveau: SousNiveau);

		/** 
		 * <summary>
     *  [FR] Écrit un message dans le journal avec le niveau <see cref="Journalisations.Erreur"/> et un sous-niveau.
     *  [EN] Writes a message to the log with the level <see cref="Journalisations.Erreur"/> and a sub-level.
     * </summary>
     * <param name="Message">
     *  [FR] Message à journaliser.
     *  [EN] Message to log.
     * </param>
     * <param name="SousNiveau">
     *  [FR] Sous-niveau pour catégoriser davantage le message.
     *  [EN] Sub-level to further categorize the message.
     * </param>
     **/
		public void Erreur(string SousNiveau, string Message) => Enregistreur(Niveau: Journalisations.Erreur, Message: Message, SousNiveau: SousNiveau);

		/** 
		 * <summary>
     *  [FR] Écrit un message dans le journal avec le niveau <see cref="Journalisations.Succes"/> et un sous-niveau.
     *  [EN] Writes a message to the log with the level <see cref="Journalisations.Succes"/> and a sub-level.
     * </summary>
     * <param name="Message">
     *  [FR] Message à journaliser.
     *  [EN] Message to log.
     * </param>
     * <param name="SousNiveau">
     *  [FR] Sous-niveau pour catégoriser davantage le message.
     *  [EN] Sub-level to further categorize the message.
     * </param>
     **/
		public void Succes(string SousNiveau, string Message) => Enregistreur(Niveau: Journalisations.Succes, Message: Message, SousNiveau: SousNiveau);

		/** 
		 * <summary>
     *  [FR] Écrit un message dans le journal avec le niveau <see cref="Journalisations.Personnalise"/>, un type personnalisé et un sous-niveau.
     *  [EN] Writes a message to the log with the level <see cref="Journalisations.Personnalise"/>, a custom type and a sub-level.
     * </summary>
     * <param name="TypePersonnalise">
     *  [FR] Type personnalisé pour le message.
     *  [EN] Custom type for the message.
     * </param>
     * <param name="Message">
     *  [FR] Message à journaliser.
     *  [EN] Message to log.
     * </param>
     * <param name="SousNiveau">
     *  [FR] Sous-niveau pour catégoriser davantage le message.
     *  [EN] Sub-level to further categorize the message.
     * </param>
     **/
		public void Personnalise(string TypePersonnalise, string SousNiveau, string Message) => Enregistreur(Niveau: Journalisations.Personnalise, Message: Message, TypePersonnalise: TypePersonnalise, SousNiveau: SousNiveau);

    private void Enregistreur(Journalisations Niveau, string Message, string TypePersonnalise = null, string SousNiveau = null) {

      if(string.IsNullOrWhiteSpace(value: Message))
        throw new ArgumentException(message: Resources.LeMessageNePeutPasEtreVide, paramName: nameof(Message));

      if(string.IsNullOrWhiteSpace(value: CheminComplet))
        throw new InvalidOperationException(message: Resources.CheminOuNomDuJournalPasDefini);

      string NiveauTexte = TypePersonnalise ?? Niveau.ToString();

      string Datage = $"[{DateTime.Now.ToString(format: _Horodatage, provider: CultureInfo.InvariantCulture)}] ";

      string Echelon;

			if(Niveau == Journalisations.Autre) {

				if(NiveauTexte == nameof(Journalisations.Autre)) {

					Echelon = string.Empty;
				}
				else {

					string NiveauLocalise = NiveauTexte
						.Replace(oldValue: "Info", newValue: Resources.INFO)
						.Replace(oldValue: "Succes", newValue: Resources.SUCCES)
						.Replace(oldValue: "Erreur", newValue: Resources.ERREUR)
						.Replace(oldValue: "Avertissement", newValue: Resources.AVERTISSEMENT);

					Echelon = $"[{NiveauLocalise}] ";
				}
			}
			else {

				Echelon = $"[{NiveauTexte}] ";
			}

			string journalMessage = SousNiveau == null 
        ? $"{Datage}{Echelon}{Message}{Environment.NewLine}" 
        : $"{Datage}{Echelon}[{SousNiveau}] {Message}{Environment.NewLine}";

      lock(_VerrouEcriture) {

        if(!VerifieSiExiste(Localisation: new FichierReference(Chemins: CheminComplet))) {

          EcrireToutesLeslignes(Localisation: new FichierReference(Chemins: CheminComplet), Contenu: [journalMessage]);
        }
        else {

          AjouterLigne(Localisation: new FichierReference(Chemins: CheminComplet), Contenu: journalMessage);
        }
      }
    }
  }
}
