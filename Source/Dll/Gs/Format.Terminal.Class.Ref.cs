﻿/**
 * Copyright © 2017-2023, Galactic-Shrine - All Rights Reserved.
 * Copyright © 2017-2023, Galactic-Shrine - Tous droits réservés.
 **/

using System;
using System.Collections.Generic;
using Gs.Interface.Terminal;
using Gs.Structure.Terminal;
using Gs.Enumeration;

namespace Gs.Terminal {

	public class Format {

		Dictionary<string, Couleur> VariationDuTheme { get; set; }
		private readonly Object VerrouillageDeCouleur = new Object();

		public Format(CouleurInterface Theme) {

			if(Theme == null) {

				throw new ArgumentException(nameof(Theme));
			}

			VariationDuTheme = Theme.Couleurs;
			CouleurParDefaut();
		}

		void CouleurParDefaut(string Couleur = Couleurs.Bg.Default) {

			var Theme = VariationDuTheme[Couleur];
			Console.BackgroundColor = Theme.ArrierePlan;
			Console.ForegroundColor = Theme.PremierPlan;
		}

		public void RetablirCouleur() {

			lock(VerrouillageDeCouleur) {

				Console.ResetColor();
			}
		}

		void DefinirCouleur(string Couleur) {

      if(VariationDuTheme.TryGetValue(Couleur, out Couleur Value)) {

        Console.BackgroundColor = Value.ArrierePlan;
        Console.ForegroundColor = Value.PremierPlan;
      }
      else {

        throw new KeyNotFoundException();
      }
    }

		public void Eclaircir() {

			lock(VerrouillageDeCouleur) {

				Console.Clear();
			}
		}

		public void Ecrire(string Texte, string Couleur = Couleurs.Txt.Default) {

			lock(VerrouillageDeCouleur) {

				DefinirCouleur(Couleur);
				Sortie.Ecrire(Texte);
				CouleurParDefaut();
			}
		}

		public void Ecrire(bool WriteLine, string Texte, string Couleur = Couleurs.Txt.Default) {

			lock(VerrouillageDeCouleur) {

				DefinirCouleur(Couleur);
				Sortie.Ecrire(WriteLine, Texte);
				CouleurParDefaut();
			}
		}

		public void Aligner(string Texte, string Couleur = Couleurs.Txt.Default) {

			lock(VerrouillageDeCouleur) {

				DefinirCouleur(Couleur);
				Sortie.Aligner(Texte);
				CouleurParDefaut();
			}
		}

		public void Aligner(Alignement Alignement, string Texte, string Couleur = Couleurs.Txt.Default) {

			lock(VerrouillageDeCouleur) {

				DefinirCouleur(Couleur);
				Sortie.Aligner(Alignement, Texte);
				CouleurParDefaut();
			}
		}

		public void AlignerLaDivision(string Texte, string Couleur = Couleurs.Txt.Default) {

			lock(VerrouillageDeCouleur) {

				DefinirCouleur(Couleur);
				string Gauche = Texte.Split('|')[0];
				string Droit = Texte.Split('|')[1];
				Sortie.Extreme(Gauche, Droit);
				CouleurParDefaut();
			}
		}

		public void Enveloppement(string Texte, string Couleur = Couleurs.Txt.Default) {
			
			lock(VerrouillageDeCouleur) {

				DefinirCouleur(Couleur);
        Decorateur.Texte(Texte);
				CouleurParDefaut();
			}
		}

		public void LigneSeparatriceDecoree(char Character, string Couleur) {

			lock(VerrouillageDeCouleur) {

				DefinirCouleur(Couleur);
        Sortie.LigneSeparatriceDecoree(Character);
				CouleurParDefaut();
			}
		}

		public void LigneSeparatriceDecoree(char Character) {

			LigneSeparatriceDecoree(Character, Couleurs.Txt.Default);
		}

		public void LigneSeparatrice(int Lignes = 1) {
			LigneSeparatrice(Lignes, Couleurs.Txt.Default);
		}

		public void LigneSeparatrice(string Couleur) {
			LigneSeparatrice(1, Couleur);
		}

		public void LigneSeparatrice(int Lignes, string Couleur = Couleurs.Txt.Default) {
			lock(VerrouillageDeCouleur) {
				DefinirCouleur(Couleur);
				Sortie.LigneSeparatrice(Lignes);
				CouleurParDefaut();
			}
		}
	}
}