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
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using GalacticShrine.Enregistrement;
using GalacticShrine.Properties;

namespace GalacticShrine.Services.Http {

	/**
   * <summary>
   *   [FR] Vérifie périodiquement l'état d'un serveur donné.
   *   [EN] Periodically checks the status of a given server.
   * </summary>
   **/
	public class VerificateurDeServeur : IDisposable, IAsyncDisposable {

		/**
     * <summary>
     *   [FR] Informations associées à l'événement de vérification du serveur.
     *   [EN] Information associated with the server verification event.
     * </summary>
     **/
		public sealed class ResultatVerificationServeurEventArgs : EventArgs {

			/**
       * <summary>
       *   [FR] Indique si le serveur est en ligne.
       *   [EN] Indicates whether the server is online.
       * </summary>
       **/
			public bool EstEnLigne { get; }

			/**
       * <summary>
       *   [FR] Initialise une nouvelle instance des arguments de l'événement.
       *   [EN] Initializes a new instance of the event arguments.
       * </summary>
       * <param name="ServeurEnLigne">
       *   [FR] Indique si le serveur est en ligne.
       *   [EN] Indicates whether the server is online.
       * </param>
       **/
			public ResultatVerificationServeurEventArgs(bool ServeurEnLigne) {

				EstEnLigne = ServeurEnLigne;
			}
		}

		/**
     * <summary>
     *   [FR] Minuteur utilisé pour déclencher la vérification périodique.
     *   [EN] Timer used to trigger periodic verification.
     * </summary>
     **/
		private readonly System.Timers.Timer _Minuteur;

		/**
     * <summary>
     *   [FR] URI du serveur à vérifier.
     *   [EN] Server URI to check.
     * </summary>
     **/
		private readonly Uri _AdresseDuServeur;

		/**
     * <summary>
     *   [FR] Client HTTP utilisé pour effectuer les requêtes.
     *   [EN] HTTP client used to make requests.
     * </summary>
     **/
		private readonly HttpClient _Client;

		/**
     * <summary>
     *   [FR] Indique si le client HTTP a été créé en interne et doit être disposé par cette instance.
     *   [EN] Indicates whether the HTTP client was created internally and must be disposed by this instance.
     * </summary>
     **/
		private readonly bool _ClientCreeEnInterne;

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
		private volatile bool _Elimine;

		/**
     * <summary>
     *   [FR] Indicateur atomique pour savoir si une vérification est en cours.
     *   [EN] Atomic flag indicating whether a verification is currently running.
     * </summary>
     **/
		private int _EtatVerificationEnCours;

		/**
     * <summary>
     *   [FR] Événement déclenché après chaque vérification avec le résultat.
     *   [EN] Event raised after each check with the result.
     * </summary>
     **/
		public event EventHandler<ResultatVerificationServeurEventArgs>? ServeurVerifie;

		/**
     * <summary>
     *   [FR] Obtient l'URI du serveur surveillé.
     *   [EN] Gets the URI of the monitored server.
     * </summary>
     **/
		public Uri AdresseDuServeur => _AdresseDuServeur;

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
     * <exception cref="ArgumentOutOfRangeException">
     *   [FR] Levée si l'intervalle est inférieur ou égal à zéro.
     *   [EN] Thrown if the interval is less than or equal to zero.
     * </exception>
     **/
		public VerificateurDeServeur(string AdresseDuServeur, HttpClient? Client = null, Journalisation? Journal = null, bool Active = true, int Intervalle = 300000, bool AutoReset = true) {

			if(string.IsNullOrWhiteSpace(AdresseDuServeur)) {

				throw new ArgumentException(
					string.Format(Resources.LeParametreNePeutPasEtreVide, nameof(AdresseDuServeur)),
					nameof(AdresseDuServeur)
				);
			}

			if(!Uri.TryCreate(AdresseDuServeur, UriKind.Absolute, out var AdresseUri) ||
				 (AdresseUri.Scheme != Uri.UriSchemeHttp && AdresseUri.Scheme != Uri.UriSchemeHttps)) {

				throw new ArgumentException(
					"L'URL du serveur est invalide. Une URL absolue HTTP ou HTTPS est attendue.",
					nameof(AdresseDuServeur)
				);
			}

			if(Intervalle <= 0) {

				throw new ArgumentOutOfRangeException(
					nameof(Intervalle),
					"L'intervalle doit être strictement supérieur à zéro."
				);
			}

			_AdresseDuServeur = AdresseUri;

			if(Client is null) {

				_Client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
				_ClientCreeEnInterne = true;
			}
			else {

				_Client = Client;
				_ClientCreeEnInterne = false;
			}

			_Journal = Journal;

			_Minuteur = new System.Timers.Timer(Intervalle)
			{

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
		private async void OnTimedEvent(object? Emetteur, ElapsedEventArgs Evenement) {

			if(_Elimine) {

				return;
			}

			if(Interlocked.Exchange(ref _EtatVerificationEnCours, 1) == 1) {

				_Journal?.Avertissement("Une vérification est déjà en cours, la nouvelle demande est ignorée.");
				return;
			}

			try {

				await VerifierLEtatAsync(CancellationToken.None).ConfigureAwait(false);
			}
			finally {

				Interlocked.Exchange(ref _EtatVerificationEnCours, 0);
			}
		}

		/**
     * <summary>
     *   [FR] Vérifie l'état du serveur en effectuant une requête HTTP.
     *   [EN] Checks the server status by performing an HTTP request.
     * </summary>
     * <param name="CancellationToken">
     *   [FR] Jeton d'annulation pour la vérification.
     *   [EN] Cancellation token for the verification.
     * </param>
     * <returns>
     *   [FR] True si le serveur répond avec un code HTTP de succès, sinon false.
     *   [EN] True if the server responds with a successful HTTP status code, otherwise false.
     * </returns>
     **/
		private async Task<bool> VerifierLEtatAsync(CancellationToken CancellationToken) {

			try {

				var Reponse = await _Client.GetAsync(_AdresseDuServeur, CancellationToken).ConfigureAwait(false);
				bool EstEnLigne = Reponse.IsSuccessStatusCode;

				if(EstEnLigne) {

					_Journal?.Info(Resources.ServeurEnLigne);
				}
				else {

					_Journal?.Avertissement(Resources.ServeurHorsLigne);
				}

				ServeurVerifie?.Invoke(this, new ResultatVerificationServeurEventArgs(EstEnLigne));

				return EstEnLigne;
			}
			catch(TaskCanceledException) {

				_Journal?.Erreur("La vérification du serveur a été annulée ou a expiré.");
				ServeurVerifie?.Invoke(this, new ResultatVerificationServeurEventArgs(false));
				return false;
			}
			catch(System.Exception Exception) {

				_Journal?.Erreur($"Erreur lors de la vérification du serveur : {Exception.Message}");
				ServeurVerifie?.Invoke(this, new ResultatVerificationServeurEventArgs(false));
				return false;
			}
		}

		/**
     * <summary>
     *   [FR] Lance immédiatement une vérification de l'état du serveur.
     *   [EN] Immediately triggers a server status check.
     * </summary>
     * <param name="CancellationToken">
     *   [FR] Jeton d'annulation optionnel pour la vérification.
     *   [EN] Optional cancellation token for the verification.
     * </param>
     * <returns>
     *   [FR] Une tâche retournant true si le serveur répond avec un code de succès HTTP.
     *   [EN] A task returning true if the server responds with a successful HTTP status code.
     * </returns>
     * <exception cref="ObjectDisposedException">
     *   [FR] Levée si l'instance a déjà été libérée.
     *   [EN] Thrown if the instance has already been disposed.
     * </exception>
     **/
		public Task<bool> VerifierMaintenantAsync(CancellationToken CancellationToken = default) {

			if(_Elimine) {

				throw new ObjectDisposedException(nameof(VerificateurDeServeur));
			}

			return VerifierLEtatAsync(CancellationToken);
		}

		/**
     * <summary>
     *   [FR] Démarre le minuteur de vérification périodique.
     *   [EN] Starts the periodic verification timer.
     * </summary>
     * <exception cref="ObjectDisposedException">
     *   [FR] Levée si l'instance a déjà été libérée.
     *   [EN] Thrown if the instance has already been disposed.
     * </exception>
     **/
		public void Demarrer() {

			if(_Elimine) {

				throw new ObjectDisposedException(nameof(VerificateurDeServeur));
			}

			_Minuteur.Start();
		}

		/**
     * <summary>
     *   [FR] Arrête le minuteur de vérification périodique.
     *   [EN] Stops the periodic verification timer.
     * </summary>
     * <exception cref="ObjectDisposedException">
     *   [FR] Levée si l'instance a déjà été libérée.
     *   [EN] Thrown if the instance has already been disposed.
     * </exception>
     **/
		public void Arreter() {

			if(_Elimine) {

				throw new ObjectDisposedException(nameof(VerificateurDeServeur));
			}

			_Minuteur.Stop();
		}

		/**
     * <summary>
     *   [FR] Libère les ressources utilisées par l'objet de manière synchrone.
     *   [EN] Releases the resources used by the object synchronously.
     * </summary>
     * <param name="Disposing">
     *   [FR] Indique si l'appel provient de la méthode Dispose.
     *   [EN] Indicates whether the call comes from the Dispose method.
     * </param>
     **/
		protected virtual void Dispose(bool Disposing) {

			if(_Elimine) {

				return;
			}

			if(Disposing) {

				_Minuteur.Stop();
				_Minuteur.Elapsed -= OnTimedEvent;
				_Minuteur.Dispose();

				if(_ClientCreeEnInterne) {

					_Client.Dispose();
				}
			}

			_Elimine = true;
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
		public ValueTask DisposeAsync() {

			Dispose();
			return ValueTask.CompletedTask;
		}

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
