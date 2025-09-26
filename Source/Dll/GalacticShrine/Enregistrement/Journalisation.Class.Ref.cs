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

  public class Journalisation {

    private string _CheminDuDossier = $"{GalacticShrine.Repertoire["Log"]}";

    private string _NomDuFichier;

    private bool _UnParJour;

    private readonly string _DateDuJour = DateTime.Now.ToString("yyyyMMdd");

    //private string _Horodatage = $"[{DateTime.Now:dd-MM-yyyy HH:mm:ss}] ";
    private string _Horodatage = "dd-MM-yyyy HH:mm:ss";

    private string CheminComplet { get; set; }

    /**
     * <summary>
     *  Chemin du dossier de sortie
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
     *  Nom du fichier
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
     *  Affiche la date du jour dans le nom du fichier
     * </summary>
     **/
    public bool UnFichierParJour {
      
      get => _UnParJour; 
      
      set {

        _UnParJour = value;
        MettreAJourCheminComplet();
      }
    }

    public string Extension { get; set; } = "log";

    public string Horodatage {

      get => _Horodatage;

      set {

        _Horodatage = $"{value}";
      }
    }

    public Journalisation(string CheminDuDossier = null, string NomDuFichier = null, bool UnParJour = true) {

      if(CheminDuDossier != null)
        Chemin = CheminDuDossier;

      if(NomDuFichier != null)
        Nom = NomDuFichier;

      if(UnParJour != true)
        UnFichierParJour = UnParJour;
    }

    private void MettreAJourCheminComplet() {

      if(!string.IsNullOrWhiteSpace(value: _CheminDuDossier) && !string.IsNullOrWhiteSpace(value: _NomDuFichier)) {

        if(_UnParJour == true) {

          CheminComplet = IO.Chemin.Combiner(Chemin1: _CheminDuDossier, Chemin2: $"{_NomDuFichier}.{_DateDuJour}.{Extension}");
        }
        else {

          CheminComplet = IO.Chemin.Combiner(Chemin1: _CheminDuDossier, Chemin2: $"{_NomDuFichier}.{Extension}");
        }
      }
    }

    public void Message(Journalisations Niveau, string Message, string TypePersonnalise = null, string SousNiveau = null) => Enregistreur(Niveau: Niveau, Message: Message, TypePersonnalise: TypePersonnalise, SousNiveau: SousNiveau);

    public void Autre(string Message) => Enregistreur(Niveau: Journalisations.Autre, Message: Message);
    public void Avertissement(string Message) => Enregistreur(Niveau: Journalisations.Avertissement, Message: Message);
    public void Info(string Message) => Enregistreur(Niveau: Journalisations.Info, Message: Message);
    public void Erreur(string Message) => Enregistreur(Niveau: Journalisations.Erreur, Message: Message);
    public void Succes(string Message) => Enregistreur(Niveau: Journalisations.Succes, Message: Message);
    public void Personnalise(string TypePersonnalise, string Message) => Enregistreur(Niveau: Journalisations.Personnalise, Message: Message, TypePersonnalise: TypePersonnalise);
    public void Avertissement(string SousNiveau, string Message) => Enregistreur(Niveau: Journalisations.Avertissement, Message: Message, SousNiveau: SousNiveau);
    public void Info(string SousNiveau, string Message) => Enregistreur(Niveau: Journalisations.Info, Message: Message, SousNiveau: SousNiveau);
    public void Erreur(string SousNiveau, string Message) => Enregistreur(Niveau: Journalisations.Erreur, Message: Message, SousNiveau: SousNiveau);
    public void Succes(string SousNiveau, string Message) => Enregistreur(Niveau: Journalisations.Succes, Message: Message, SousNiveau: SousNiveau);
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

        Echelon = NiveauTexte == "Autre" 
          ? "" 
          : $"[{
            NiveauTexte
            .Replace(oldValue: "Info", newValue: Resources.INFO)
            .Replace(oldValue: "Succes", newValue: Resources.SUCCES)
            .Replace(oldValue: "Erreur", newValue: Resources.ERREUR)
            .Replace(oldValue: "Avertissement", newValue: Resources.AVERTISSEMENT)
            }] ";
      }
      else {

        Echelon = $"[{NiveauTexte}] ";
      }

      string journalMessage = SousNiveau == null 
        ? $"{Datage}{Echelon}{Message}{Environment.NewLine}" 
        : $"{Datage}{Echelon}[{SousNiveau}] {Message}{Environment.NewLine}";

      if(!VerifieSiExiste(Localisation: new FichierReference(Chemins: CheminComplet))) {

        EcrireToutesLeslignes(Localisation: new FichierReference(Chemins: CheminComplet), Contenu: [journalMessage]);
      }
      else {

        AjouterLigne(Localisation: new FichierReference(Chemins: CheminComplet), Contenu: journalMessage);
      }
    }
  }
}
