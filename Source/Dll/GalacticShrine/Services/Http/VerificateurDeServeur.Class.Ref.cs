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
#if GS_PREVIEW_1_2_0
using System;
using System.Timers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GalacticShrine.Properties;
using GalacticShrine.Enregistrement;

namespace GalacticShrine.Services.Http {

  /**
   * <summary>
   *   [FR] Vérifie périodiquement l'état d'un serveur donné.
   *   [EN] Periodically checks the status of a given server.
   * </summary>
   **/
#if NET8_0_OR_GREATER
  public class VerificateurDeServeur : IDisposable, IAsyncDisposable {
#else
  public class VerificateurDeServeur : IDisposable {
#endif

    /**
     * <summary>
     *   [FR] Minuteur utilisé pour déclencher la vérification périodique.
     *   [EN] Timer used to trigger periodic verification.
     * </summary>
     **/
    private readonly System.Timers.Timer _Minuteur;

    /**
     * <summary>
     *   [FR] URL du serveur à vérifier.
     *   [EN] Server URL to check.
     * </summary>
     **/
    private readonly string _AdresseDuServeur;

    /**
     * <summary>
     *   [FR] Client HTTP utilisé pour effectuer les requêtes.
     *   [EN] HTTP client used to make requests.
     * </summary>
     **/
    private readonly HttpClient _Client;

    /**
     * <summary>
     *   [FR] Classe de journalisation pour enregistrer les informations et erreurs.
     *   [EN] Logging class to record information and errors.
     * </summary>
     **/
    private readonly Journalisation? _Journal;

    /**
     * <summary>
     *   [FR] Indique si l'objet a déjà été supprimé.
     *   [EN] Indicates whether the object has already been disposed.
     * </summary>
     **/
    private bool _Elimine;

    /**
     * <summary>
     *   [FR] Indique si une vérification est déjà en cours pour éviter les exécutions concurrentes.
     *   [EN] Indicates if a verification is already in progress to prevent concurrent executions.
     * </summary>
     **/
    private bool _EstEnCours;

    /**
     * <summary>
     *   [FR] Événement déclenché après chaque vérification avec le résultat.
     *   [EN] Event raised after each check with the result.
     * </summary>
     **/
    public event EventHandler<bool>? ServeurVerifie;

    /**
     * <summary>
     *   [FR] Initialise une nouvelle instance de VerificateurDeServeur.
     *   [EN] Initializes a new instance of the server verifier.
     * </summary>
     * <param name="AdresseDuServeur">
     *   [FR] L'URL du serveur à vérifier.
     *   [EN] The URL of the server to check.
     * </param>
     * <param name="Client">
     *   [FR] Client HTTP optionnel pour les requêtes. Si null, un client par défaut est créé.
     *   [EN] Optional HTTP client for requests. If null, a default client is created.
     * </param>
     * <param name="Journal">
     *   [FR] Classe de journalisation optionnelle pour enregistrer infos et erreurs.
     *   [EN] Optional logging class to record information and errors.
     * </param>
     * <param name="Active">
     *   [FR] Indique si la vérification doit commencer immédiatement.
     *   [EN] Indicates whether the verification should start immediately.
     * </param>
     * <param name="Intervalle">
     *   [FR] Intervalle en millisecondes entre chaque vérification.
     *   [EN] Interval in milliseconds between each verification.
     * </param>
     * <param name="AutoReset">
     *   [FR] Indique si le timer doit se réinitialiser automatiquement.
     *   [EN] Indicates whether the timer should reset automatically.
     * </param>
     * <exception cref="ArgumentException">
     *   [FR] Levée si l'URL du serveur est vide ou invalide.
     *   [EN] Thrown if the server URL is empty or invalid.
     * </exception>
     **/
    public VerificateurDeServeur(string AdresseDuServeur, HttpClient? Client = null, Journalisation? Journal = null, bool Active = true, int Intervalle = 300000, bool AutoReset = true) {

      if(string.IsNullOrWhiteSpace(AdresseDuServeur)) {

        throw new ArgumentException(
          string.Format(Resources.LeParametreNePeutPasEtreVide, nameof(AdresseDuServeur)),
          nameof(AdresseDuServeur)
        );
      }

      _AdresseDuServeur = AdresseDuServeur;
      _Client = Client ?? new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
      _Journal = Journal;

      _Minuteur = new System.Timers.Timer(Intervalle) {

        AutoReset = AutoReset,
        Enabled = Active
      };

      _Minuteur.Elapsed += OnTimedEvent;
    }

    /**
     * <summary>
     *   [FR] Gestionnaire d'événement du timer qui déclenche la vérification.
     *   [EN] Timer event handler that triggers the verification.
     * </summary>
     **/
    private async void OnTimedEvent(object? sender, ElapsedEventArgs e) {

      if(_EstEnCours) {

        _Journal?.Avertissement("Une vérification est déjà en cours, la nouvelle demande est ignorée.");
        return;
      }

      _EstEnCours = true;
      try {

        using var cts = new CancellationTokenSource();
        await VerifierLEtatAsync(cts.Token);
      }
      finally {

        _EstEnCours = false;
      }
    }

    /**
     * <summary>
     *   [FR] Vérifie l'état du serveur en effectuant une requête HTTP.
     *   [EN] Checks the server status by performing an HTTP request.
     * </summary>
     * <param name="cancellationToken">
     *   [FR] Token d'annulation pour la vérification.
     *   [EN] Cancellation token for the verification.
     * </param>
     * <returns>
     *   [FR] Une tâche asynchrone effectuant la vérification.
     *   [EN] An asynchronous task performing the check.
     * </returns>
     **/
    private async Task VerifierLEtatAsync(CancellationToken CancellationToken) {

      try {

        var response = await _Client.GetAsync(_AdresseDuServeur, CancellationToken);
        bool estEnLigne = response.IsSuccessStatusCode;

        if(estEnLigne) {

          _Journal?.Info(Resources.ServeurEnLigne);
        }
        else {

          _Journal?.Avertissement(Resources.ServeurHorsLigne);
        }

        ServeurVerifie?.Invoke(this, estEnLigne);
      }
      catch(TaskCanceledException) {

        _Journal?.Erreur("La vérification du serveur a été annulée ou a expiré.");
        ServeurVerifie?.Invoke(this, false);
      }
      catch(System.Exception ex) {

        _Journal?.Erreur($"Erreur lors de la vérification du serveur : {ex.Message}");
        ServeurVerifie?.Invoke(this, false);
      }
    }

    /**
     * <summary>
     *   [FR] Libère les ressources utilisées par l'objet de manière synchrone.
     *   [EN] Releases the resources used by the object synchronously.
     * </summary>
     * <param name="disposing">
     *   [FR] Indique si l'appel provient de la méthode Dispose.
     *   [EN] Indicates whether the call comes from the Dispose method.
     * </param>
     **/
    protected virtual void Dispose(bool disposing) {

      if(!_Elimine) {

        if(disposing) {

          _Minuteur.Stop();
          _Minuteur.Dispose();
          _Client.Dispose();
        }

        _Elimine = true;
      }
    }

    /**
     * <summary>
     *   [FR] Libère les ressources utilisées par l'objet de manière synchrone.
     *   [EN] Releases the resources used by the object synchronously.
     * </summary>
     **/
    public void Dispose() {

      Dispose(true);
      GC.SuppressFinalize(this);
    }

#if NET8_0_OR_GREATER
    /**
     * <summary>
     *   [FR] Libère les ressources utilisées par l'objet de manière asynchrone.
     *   [EN] Asynchronously releases the resources used by the object.
     * </summary>
     * <returns>
     *   [FR] Une ValueTask représentant l'opération asynchrone.
     *   [EN] A ValueTask representing the asynchronous disposal operation.
     * </returns>
     **/
    public async ValueTask DisposeAsync() {

      Dispose(true);
      GC.SuppressFinalize(this);
      await Task.CompletedTask;
    }
#endif

    /**
     * <summary>
     *   [FR] Finaliseur pour s'assurer que Dispose est bien appelé.
     *   [EN] Finalizer to ensure Dispose is properly called.
     * </summary>
     **/
    ~VerificateurDeServeur() {

      Dispose(false);
    }
  }
}
#endif