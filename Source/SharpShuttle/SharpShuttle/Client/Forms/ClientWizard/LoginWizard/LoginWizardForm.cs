using System;
using System.Windows.Forms;
using Client.Controls;
using Shared.Communication;
using Shared.Views;
using UserControls.AbstractWizard;

namespace Client.Forms.ClientWizard.LoginWizard
{
	/// <summary>
	/// Scherm waar de gebruiker kan inloggen op een server
	/// </summary>
	public class LoginWizardForm : AbstractWizardForm<object>
	{
		private SelectServerStep serverStep;
		private SelectUserStep userStep;

		/// <summary>
		/// Default constructor
		/// </summary>
		public LoginWizardForm() : base("Verbinding Maken") { }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Communication.Instance.Disconnect();
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Initialiseert de relevante WizardSteps, SelectServerStep wordt gestart
		/// </summary>
		public override void Init()
		{
			Resizable = false;
			AutoResize = false;
			serverStep = new SelectServerStep(WizardStep_StepNavigationChanged);
			userStep = new SelectUserStep();
			steps = new AbstractWizardStep[]
			        	{
			        		serverStep,
							userStep
			        	};
			serverStep.SetForm();
			serverStep.ConnectorFinished += serverStep_ConnectorFinished;
			btnNext.Click += btnNext_Click;
		}

		/// <summary>
		/// Kijkt of de verbinding met de server succesvol tot stand is gekomen
		/// </summary>
		/// <param name="succes"></param>
		/// <param name="server"></param>
		private void serverStep_ConnectorFinished(bool succes, string server)
		{
			btnNext.Enabled = true;
			if (succes)
			{
				btnNext.PerformClick();
			}
			else
				MessageBox.Show(this, String.Format("Er kan geen verbinding worden gemaakt met {0}\nProbeer opnieuw.", server), "Geen Verbinding", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		#region Event handlers

		/// <summary>
		/// Probeert te verbinden met de server en disablet tijdelijk de volgende knop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnNext_Click(object sender, System.EventArgs e)
		{
			switch (step)
			{
				case 1: //serverStep
					btnNext.Enabled = false;
					serverStep.StartConnecting();
					break;
				case 2: //userStep
					break;
			}
		}

		#endregion

		#region Overrides of AbstractWizardForm

		/// <summary>
		/// Gaat naar de UserSelectStep
		/// </summary>
		/// <param name="input"> </param>
		protected override void nextInt(object input)
		{
			//Step is altijd 1 bij "volgende"
			userStep.SetForm();
		}

		/// <summary>
		/// Opent de juiste wizardstep
		/// </summary>
		/// <param name="input"></param>
		protected override void prevInt(object input)
		{
			if (Step == 2)
				serverStep.SetForm();
		}

		/// <summary>
		/// Stelt de juiste functionaliteitet voor de geselecteerde user in
		/// </summary>
		/// <param name="input"></param>
		protected override void stepFinishInt(object input)
		{
			//Step is altijd 2 bij "voltooien"
			FunctionalityControl fctrl = FunctionalityControl.Instance;
			UserView u = input as UserView;
			fctrl.CurrentRole = u.Role;
		}

		/// <summary>
		/// Lege override van completeFinishInt
		/// </summary>
		/// <param name="complete_input"></param>
		protected override void completeFinishInt(object complete_input) { }

		/// <summary>
		/// Lege override van readCompleteInput
		/// </summary>
		/// <returns></returns>
		protected override object readCompleteInput()
		{
			return null;
		}

		#endregion
	}
}
