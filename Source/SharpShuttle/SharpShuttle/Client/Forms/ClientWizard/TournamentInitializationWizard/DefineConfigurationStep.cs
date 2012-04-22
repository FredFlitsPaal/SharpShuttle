using System;
using UserControls.AbstractWizard;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// WizardStep waarin het aantal velden en sets per game worden ingesteld
	/// </summary>
	public partial class DefineConfigurationStep : GenericAbstractWizardStep<object>
	{

		#region Constructors & init methode

		/// <summary>
		/// Default constructor
		/// </summary>
		public DefineConfigurationStep()
		{
			init();
		}

		/// <summary>
		/// Constructor die een StepNavigationChanged meekrijgt
		/// </summary>
		/// <param name="step_nav_listener"></param>
		public DefineConfigurationStep(StepNavigationChanged step_nav_listener) : base(step_nav_listener)
		{
			init();
		}

		/// <summary>
		/// Initialiseert GUI en Wizard componenten
		/// </summary>
		private void init()
		{
			InitializeComponent();
			Text = "Aantal Velden";
			previous_visible = true;
			next_visible = true;
			finish_visible = false;
		}

		#endregion

		//NOTE Lege methode
		/// <summary>
		/// 
		/// </summary>
		public void SetForm()
		{}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ddlServer_OnTextChanged(object sender, EventArgs e)
		{
			next_enabled = validateNextInt();
			FireStepsHaveChanged();
		}

		#region AbstractWizardStep<object> overschrijvende methodes

		/// <summary>
		/// Lege override van readInput
		/// </summary>
		/// <returns></returns>
		protected override object readInput()
		{
			return null;
		}

		/// <summary>
		/// Stelt de sets en velden configuratie in en returnt altijd true
		/// </summary>
		/// <returns></returns>
		protected override bool validateNextInt()
		{
			Configurations.NumberOfCourts = defineCourtsForm.Value;
			Configurations.NumberOfSets = (int)nudAmountSets.Value;
			return true;
		}

		#endregion
	}
}
