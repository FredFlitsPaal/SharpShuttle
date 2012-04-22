using System;
using Client.Controls;
using Shared.Communication;
using Shared.Logging;
using Shared.Views;
using UserControls.AbstractWizard;
using System.ComponentModel;

namespace Client.Forms.ClientWizard.LoginWizard
{
	/// <summary>
	/// De wizard step voor het kiezen van de server
	/// </summary>
	public partial class SelectServerStep : GenericAbstractWizardStep<ServerView>
	{
		/// <summary>
		/// Business logica
		/// </summary>
		protected SelectServerControl control;

		/// <summary>
		/// Backgroundworker die de verbinding maakt
		/// </summary>
		private BackgroundWorker bgwConnecter;

		/// <summary>
		/// Bool die aangeeft of er verbinding is met de server
		/// </summary>
		private bool is_connected;

		/// <summary>
		/// Methode die aangeroepen wordt wanneer het verbinden met de server is afgerond
		/// </summary>
		/// <param name="succes"></param>
		/// <param name="server"></param>
		public delegate void ConnectorFinishedHandler(bool succes, string server);

		/// <summary>
		/// Event die aangeeft dat er verbinding is gemaakt
		/// </summary>
		public event ConnectorFinishedHandler ConnectorFinished;


		/// <summary>
		/// Default constructor
		/// </summary>
		public SelectServerStep()
		{
			init();
		}

		/// <summary>
		/// Constructor met een StepNavigationChanged
		/// </summary>
		/// <param name="step_nav_listener"></param>
		public SelectServerStep(StepNavigationChanged step_nav_listener)
			: base(step_nav_listener)
		{
			init();
		}

		/// <summary>
		/// Initialiseert de backgroundworker en een aantal wizard-gerelateerde
		/// dingen
		/// </summary>
		private void init()
		{
			InitializeComponent();
			bgwConnecter = new BackgroundWorker();
			bgwConnecter.DoWork += bgwConnecter_DoWork;
			bgwConnecter.RunWorkerCompleted += bgwConnecter_RunWorkerCompleted;

			lblCurrentAction.Text = "";

			Text = "Server Selecteren";
			previous_visible = false;
			next_visible = true;
			finish_visible = false;
			control = new SelectServerControl();
		}

		/// <summary>
		/// De backgroundworker begint met verbinding maken
		/// </summary>
		public void StartConnecting()
		{
			lblCurrentAction.Text = "Verbinden...";
			if (!bgwConnecter.IsBusy)
				bgwConnecter.RunWorkerAsync(ddlServers.Text);
		}

		/// <summary>
		/// De backgroundworker heeft verbinding gemaakt en het ConnectorFinished
		/// event wordt aangeroepen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void bgwConnecter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			lblCurrentAction.Text = is_connected ? "Verbonden" : "";
			if (ConnectorFinished != null)
				ConnectorFinished(is_connected, ddlServers.Text);
		}

		/// <summary>
		/// Laat de backgroundworker een taak uitvoeren
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void bgwConnecter_DoWork(object sender, DoWorkEventArgs e)
		{
			is_connected = false;
			try
			{
				is_connected = control.Connect(e.Argument as string);
			}
			catch(Exception exc)
			{
				Logger.Write("SelectServerStep.bgwConnecter_DoWork", exc.ToString());
			}
		}

		/// <summary>
		/// Laat een lijst van beschikbare servers in de combobox zien
		/// </summary>
		public void SetForm()
		{
			if (Communication.Instance.Connected)
				Communication.Instance.Disconnect();
			is_connected = false;
			lblCurrentAction.Text = "";
			ddlServers.DataSource = control.GetServers();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ddlServer_OnTextChanged(object sender, EventArgs e)
		{
			is_connected = false;
			FireStepsHaveChanged();
		}


		#region Overrides of AbstractWizardStep<ServerView>

		/// <summary>
		/// Returnt een serverview met het geselecteerde adres
		/// </summary>
		/// <returns> De serverview met het geselecteerde adres</returns>
		protected override ServerView readInput()
		{
			ServerView s = new ServerView();
			s.Address = ddlServers.Text;
			return s;
		}

		/// <summary>
		/// We mogen alleen door naar de volgende step als we verbonden zijn
		/// </summary>
		/// <returns></returns>
		protected override bool validateNextInt()
		{
			return is_connected;
		}

		/// <summary>
		/// Als we door mogen gaan, sla dan de gekozen server op in de business logica
		/// </summary>
		/// <param name="can_next"></param>
		/// <param name="input"> De gekozen server </param>
		public override void Next(out bool can_next, out ServerView input)
		{
			base.Next(out can_next, out input);
			if (can_next)
			{
				control.StoreServer(input);
			}
		}

		#endregion
	}
}