using System.Collections.Generic;
using Shared.Views;
using UserControls.AbstractWizard;
using Shared.Algorithms;
using Shared.Domain;
using Client.Printers;
using Client.Controls;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Wizardstep voor het afronden van de toernooi initialisatie
	/// </summary>
	public partial class FinalInitStep : GenericAbstractWizardStep<object>
	{
		/// <summary>
		/// Business logica
		/// </summary>
		FinalTournamentWizardControl control;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="updateGUI"></param>
		public delegate void ReloadEvent(bool updateGUI);

		#region Contrstuctor & init methode

		/// <summary>
		/// Default constructor
		/// </summary>
		public FinalInitStep()
		{
			Init();
		}

		/// <summary>
		/// Initialiseert GUI en Wizard componenten
		/// </summary>
		public void Init()
		{
			control = new FinalTournamentWizardControl();
			
			control.ReloadEvent += reload;
			
			InitializeComponent();
			Text = "Wizard Voltooien";
			previous_visible = true;
			next_visible = false;
			finish_visible = true;
		}

		#endregion

		/// <summary>
		/// Begin met het genereren van rondes
		/// </summary>
		/// <param name="updateGUI"></param>
		private void reload (bool updateGUI)
		{
			generateMatches();
		}

		/// <summary>
		/// Genereert de eerste ronde van alle poules die niet leeg zijn,
		/// lege poules worden verwijderd.
		/// </summary>
		private void generateMatches()
		{
			PouleViews removeList = new PouleViews();
			IList<Team> teams;
			PouleViews poules = control.GetPoules();
			Dictionary<int, List<Match>> newMatchesDictionary = new Dictionary<int, List<Match>>();
 
			// Zoek lege poules en creeer de eerste wedstrijden voor de poules
			foreach (PouleView poule in poules)
			{
				teams = control.GetPouleTeams(poule.Id);
				if (teams.Count > 0)
				{
					Team notPlayingTeam;

					IList<Match> matches = control.GetPouleMatches(poule.Id);
					List<Match> newMatches = Algorithms.GenerateLadder(new List<Team>(teams), 1, matches, out notPlayingTeam);
					newMatchesDictionary.Add(poule.Id, newMatches);

					MatchNotePrinter.Printer.AddMatches(newMatches);
				}
				else
					removeList.Add(poule);
			}

			// Verwijder lege poules uit de lijst
			foreach (PouleView poule in removeList)
				poules.Remove(poule);

			if (removeList.Count > 0)
				control.SetAllPoules(poules);

			// Zet de wedstrijden van de eerste ronde
			foreach (KeyValuePair<int, List<Match>> key in newMatchesDictionary)
				control.SetPouleNextRound(key.Key, key.Value);
		}

		#region AbstractWizardStep<object> overschrijvende methodes

		/// <summary>
		/// Lege override
		/// </summary>
		/// <returns></returns>
		protected override object readInput()
		{
			return null;
		}

		/// <summary>
		/// Genereert de eerste ronde, returnt altijd true
		/// </summary>
		/// <returns></returns>
		protected override bool validateFinishInt()
		{
			generateMatches();
			return true;
		}

		#endregion
	}
}
