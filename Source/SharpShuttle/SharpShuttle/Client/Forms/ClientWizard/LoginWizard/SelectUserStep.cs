using System;
using System.ComponentModel;
using Client.Controls;
using Shared.Communication.Serials;
using Shared.Views;
using UserControls.AbstractWizard;

namespace Client.Forms.ClientWizard.LoginWizard
{
	/// <summary>
	/// De wizardstep voor het kiezen van een user
	/// </summary>
	public partial class SelectUserStep : GenericAbstractWizardStep<UserView>, ISerialFilter
	{
		/// <summary>
		/// De business logica
		/// </summary>
		private SelectUserControl control;

		/// <summary>
		/// De mogelijke users
		/// </summary>
		private UserViews items;

		/// <summary>
		/// Default constructor
		/// </summary>
		public SelectUserStep()
		{
			init();
		}

		/// <summary>
		/// Initialiseert de lijst van mogelijke users en een aantal wizard-gerelateerde
		/// dingen
		/// </summary>
		private void init()
		{
			InitializeComponent();
			control = new SelectUserControl();
			
			//haal users pas op als deze step geactiveert wordt

			Text = "Gebruiker Selecteren";
			previous_visible = true;
			next_visible = false;
			finish_visible = true;
			next_enabled = false;
		}

		/// <summary>
		/// Update de beschrijving van de gebruiker
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlUsers.SelectedIndex != -1 && items != null)
			{
				int id = ddlUsers.SelectedValue;
				lblInfo.Text = items.getUserViewId(id).Info;
			}
		}

		#region Overrides of GenericAbstractWizardStep<UserView>

		/// <summary>
		/// Returnt de geselecteerde user
		/// </summary>
		/// <returns> De geselecteerde user </returns>
		protected override UserView readInput()
		{
			int id = ddlUsers.SelectedValue;
			UserView u = control.GetUser(id);
			return u;
		}

		/// <summary>
		/// Kijkt of er een gebruiker met de geselecteerde id bestaat
		/// </summary>
		/// <returns></returns>
		protected override bool validateFinishInt()
		{
			int id = ddlUsers.SelectedValue;
			return control.UserExists(id);
		}

		#endregion

		/// <summary>
		/// Wordt elke keer aan geroepen als naar deze stap wordt gegaan
		/// De gebruikers worden opgehaald.
		/// </summary>
		public void SetForm()
		{
			BackgroundWorker bgw = new BackgroundWorker();
			bgw.DoWork += getUsers;
			bgw.RunWorkerCompleted += bindControls;
			bgw.RunWorkerAsync();
		}

		/// <summary>
		/// Haalt de gebruikers op voor de backgroundworker.
		/// </summary>
		/// <param name="send"></param>
		/// <param name="e"></param>
		private void getUsers(object send, DoWorkEventArgs e)
		{
			items = control.GetUsers();
		}

		/// <summary>
		/// Bindt de items aan de combobox.
		/// </summary>
		private void bindControls(object send, RunWorkerCompletedEventArgs e)
		{
			ddlUsers.DataSource = items;
		}

		#region Implementation of ISerialFilter

		/// <summary>
		/// Geen events zijn van belang
		/// </summary>
		public FilterFlags FilterFlags
		{
			get { return new FilterFlags(); }
		}


		//public bool UpdateAllPoulesEvent(SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		//public bool UpdateAllPlayersEvent(SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		//public bool UpdateAllMatchesEvent(SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		//public bool UpdateAllHistoryMatchesEvent(SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		//public bool UpdatePouleEvent(int PouleID, SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		//public bool UpdatePoulePlanningEvent(int PouleID, SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		//public bool UpdatePouleTeamsEvent(int PouleID, SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		//public bool UpdatePouleLadderEvent(int PouleID, SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		//public bool UpdatePouleMatchesEvent(int PouleID, SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		//public bool UpdateMatchEvent(int MatchID, SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		//public bool UpdateSettings(SerialEventTypes SerialEvent)
		//{
		//    return false;
		//}

		/// <summary>
		/// UpdateAllPoulesEvent is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllPoulesEvent(SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdateAllPlayersEvent is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllPlayersEvent(SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdateAllMatchesEvent is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllMatchesEvent(SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdateAllHistoryMatchesEvent is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateAllHistoryMatchesEvent(SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdatePouleEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdatePoulePlanningEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePoulePlanningEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdatePouleTeamsEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleTeamsEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdatePouleLadderEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleLadderEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdatePouleMatchesEvent is niet van belang
		/// </summary>
		/// <param name="PouleID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdatePouleMatchesEvent(int PouleID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdateMatchEvent is niet van belang
		/// </summary>
		/// <param name="MatchID"></param>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateMatchEvent(int MatchID, SerialEventTypes SerialEvent) { return false; }

		/// <summary>
		/// UpdateSettings is niet van belang
		/// </summary>
		/// <param name="SerialEvent"></param>
		/// <returns></returns>
		public bool UpdateSettings(SerialEventTypes SerialEvent) { return false; }

		#endregion

	}
}
