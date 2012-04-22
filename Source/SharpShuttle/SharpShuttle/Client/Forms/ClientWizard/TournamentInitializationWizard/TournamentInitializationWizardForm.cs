using UserControls.AbstractWizard;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Wizard om het toernooi mee te initialiseren
	/// </summary>
	public class TournamentInitializationWizardForm : AbstractWizardForm<object>
	{
		/// <summary>
		/// De stap waarin spelers worden toegevoegd
		/// </summary>
		private PlayersStep contestantsStep;
		/// <summary>
		/// De stap waarin niveaus en poules gemaakt worden
		/// </summary>
		private NiveausPoulesStep nivPouleStep;
		/// <summary>
		/// De stap waarin spelers in poules gezet worden
		/// </summary>
		private PoulesLayoutStep poulesStep;
		/// <summary>
		/// De stap waarin teams gemaakt worden
		/// </summary>
		private ArrangeTeamsStep teamsStep;
		/// <summary>
		/// De stap waarin het aantal velden en sets geconfigureerd worden
		/// </summary>
		private DefineConfigurationStep configStep;
		/// <summary>
		/// De stap waarin het initialiseren wordt afgerond
		/// </summary>
		private FinalInitStep finalStep;

		#region Constructor & Initmethode

		/// <summary>
		/// Default constructor
		/// </summary>
		public TournamentInitializationWizardForm() : base("Toernooi Initialisatie")
		{
			Resizable = true;
		}

		/// <summary>
		/// Initialiseert alle WizardSteps
		/// </summary>
		public override void Init()
		{
			nivPouleStep = new NiveausPoulesStep();
			contestantsStep = new PlayersStep();
			poulesStep = new PoulesLayoutStep();
			teamsStep = new ArrangeTeamsStep();
			configStep = new DefineConfigurationStep();
			finalStep = new FinalInitStep();
			steps = new AbstractWizardStep[]
		        	{
		        		nivPouleStep,
						contestantsStep,
						poulesStep,
						teamsStep,
						configStep,
						finalStep
		        	};
			nivPouleStep.SetForm();
		}

		#endregion

		#region AbstractWizardForm overschrijvende methodes

		/// <summary>
		/// Opent de justie wizardstep
		/// </summary>
		/// <param name="input"></param>
		protected override void nextInt(object input)
		{
			if (Step == 1)
				contestantsStep.SetForm();

			else if (Step == 2)
				poulesStep.SetForm();

			else if (Step == 3)
				teamsStep.SetForm();

			else if (Step == 4)
			{
				//Laatste 2 stappen niet resizable.
				Resizable = false;
				configStep.SetForm();
			}

		}

		/// <summary>
		/// Opent de juiste wizardstep
		/// </summary>
		/// <param name="input"></param>
		protected override void prevInt(object input)
		{
			if (Step == 2)
				nivPouleStep.SetForm();

			else if (Step == 3)
				contestantsStep.SetForm();
			
			else if (Step == 4)
				poulesStep.SetForm();

			else if (Step == 5)
				teamsStep.SetForm();
		}

		/// <summary>
		/// Lege override
		/// </summary>
		/// <param name="input"></param>
		protected override void stepFinishInt(object input)
		{}

		/// <summary>
		/// Lege override
		/// </summary>
		/// <param name="complete_input"></param>
		protected override void completeFinishInt(object complete_input)
		{}

		/// <summary>
		/// Lege override
		/// </summary>
		/// <returns></returns>
		protected override object readCompleteInput()
		{
			return null;
		}

		#endregion
	}
}


