using System.Windows.Forms;

namespace UserControls.AbstractWizard
{
	/// <summary>
	/// Methode die aangeroepen wordt bij het veranderen van een stap in een wizard
	/// </summary>
	/// <param name="prev_visible"></param>
	/// <param name="prev_enabled"></param>
	/// <param name="next_visible"></param>
	/// <param name="next_enabled"></param>
	/// <param name="finish_visible"></param>
	/// <param name="finish_enabled"></param>
	public delegate void StepNavigationChanged(bool prev_visible, bool prev_enabled, bool next_visible, bool next_enabled, bool finish_visible, bool finish_enabled);

	/// <summary>
	/// Abstracte klasse voor alle WizardSteps
	/// </summary>
	public abstract class AbstractWizardStep : UserControl
	{
		/// <summary>
		/// Is de vorige stap knop zichtbar
		/// </summary>
		protected bool previous_visible = true;
		/// <summary>
		/// Is de vorige stap knop enabled
		/// </summary>
		protected bool previous_enabled = true;
		/// <summary>
		/// Is de volgende stap knop zichtbaar
		/// </summary>
		protected bool next_visible = true;
		/// <summary>
		/// Is de volgende stap knop enabled
		/// </summary>
		protected bool next_enabled = true;
		/// <summary>
		/// Is de finish knop zichtbaar
		/// </summary>
		protected bool finish_visible = true;
		/// <summary>
		/// Is de finish knop enabled
		/// </summary>
		protected bool finish_enabled = true;

		/// <summary>
		/// Default constructor
		/// </summary>
		public AbstractWizardStep() {}

		/// <summary>
		/// Constructor die een StepNavigationChanged delegate meekrijgt
		/// </summary>
		/// <param name="step_nav_listener"></param>
		public AbstractWizardStep(StepNavigationChanged step_nav_listener) : this()
		{
			if (step_nav_listener != null)
				StepNavigationChanged += step_nav_listener;
			FireStepsHaveChanged();
		}

		#region wizard nav-button influencing

		/// <summary>
		/// Zal worden afgevuurd wanneer knoppen veranderd moeten
		/// worden (in termen van visibility en enabled-ism) voor real-time validatie
		/// </summary>
		public event StepNavigationChanged StepNavigationChanged;

		/// <summary>
		/// triggert de executie van onStepNavigationChanged
		/// </summary>
		public void FireStepsHaveChanged()
		{
			if (StepNavigationChanged != null)
				StepNavigationChanged(PreviousVisible, PreviousEnabled, NextVisible, NextEnabled, FinishVisible, FinishEnabled);
		}

		/// <summary>
		/// Is de vorige stap knop zichtbaar
		/// </summary>
		public bool PreviousVisible
		{
			get { return previous_visible; }
		}

		/// <summary>
		/// Is de vorige stap knop enabled
		/// </summary>
		public bool PreviousEnabled
		{
			get { return previous_enabled; }
		}

		/// <summary>
		/// Is de volgende stap knop zichtbaar
		/// </summary>
		public bool NextVisible
		{
			get { return next_visible; }
		}

		/// <summary>
		/// Is de volgende stap knop enabled
		/// </summary>
		public bool NextEnabled
		{
			get { return next_enabled; }
		}

		/// <summary>
		/// Is de finish knop zichtbaar
		/// </summary>
		public bool FinishVisible
		{
			get { return finish_visible; }
		}

		/// <summary>
		/// Is de finish knop enabled
		/// </summary>
		public bool FinishEnabled
		{
			get { return finish_enabled; }
		}

		#endregion

		#region wizard navigation calls

		/// <summary>
		/// Deze methode NIET overriden! De Previous() van GenericAbstractWizardForm mag wel.
		/// 
		/// Zegt tegen wizardstep dat het tijd is om 1 stap terug te gaan
		/// Deze methode kan een stap terugdoen voorkomen door can_move op false te zetten
		/// Het verwerken van de input gebeurt buiten dit object
		/// </summary>
		/// <param name="can_previous"> if moving on is allowed</param>
		/// <param name="input"> Alle input van deze stap in 1 object</param>
		public virtual void Previous(out bool can_previous, out object input)
		{
			can_previous = validatePrevious(out input);
			if (!can_previous)
				input = null;
		}

		/// <summary>
		/// Deze methode NIET overriden! De Next() van GenericAbstractWizardForm mag wel.
		/// 
		/// Zegt tegen wizardstep dat het tijd is om naar de volgende te gaan
		/// Deze methode kan het doorgaan voorkomen door can_move op false te zetten
		/// Het verwerken van de input gebeurt buiten dit object
		/// </summary>
		/// <param name="can_next">if moving on is allowed</param>
		/// <param name="input"> Alle input van deze stap in 1 object</param>
		public virtual void Next(out bool can_next, out object input)
		{
			can_next = validateNext(out input);
			if (!can_next)
				input = null;
		}

		/// <summary>
		/// Deze methode NIET overriden! De Finish() van GenericAbstractWizardForm mag wel.
		/// 
		/// Zegt tegen wizardstep dat het tijd is om de wizard af te sluiten
		/// Deze methode kan het afsluiten voorkomen door can_move op false te zetten
		/// Het verwerken van de input gebeurt buiten dit object
		/// </summary>
		/// <param name="can_finish">if moving on is allowed</param>
		/// <param name="input">all the input from this step in one object</param>
		public virtual void Finish(out bool can_finish, out object input)
		{
			can_finish = validateFinish(out input);
			if (!can_finish)
				input = null;
		}

		#endregion

		#region validation
		#region general validation
		/// <summary>
		/// Roept interne en externe validators aan
		/// Doet ook het lezen van de input (voor externe validatie)
		/// </summary>
		/// <param name="input"> Data die van deze stap is ingelezen</param>
		/// <returns> Validatie resultaat</returns>
		protected virtual bool validatePrevious(out object input)
		{
			input = null;
			bool result = validatePreviousInt();
			if (result)
			{
				input = read_input();
				result = extValPrevious(input);
				if (!result)
					input = null;
			}
			return result;
		}

		/// <summary>
        /// Roept interne en externe validators aan
        /// Doet ook het lezen van de input (voor externe validatie)
		/// </summary>
        /// <param name="input"> Data die van deze stap is ingelezen</param>
        /// <returns> Validatie resultaat</returns>
		protected virtual bool validateNext(out object input)
		{
			input = null;
			bool result = validateNextInt();
			if (result)
			{
				input = read_input();
				result = extValNext(input);
				if (!result)
					input = null;
			}
			return result;
		}

        /// <summary>
        /// Roept interne en externe validators aan
        /// Doet ook het lezen van de input (voor externe validatie)
        /// </summary>
        /// <param name="input"> Data die van deze stap is ingelezen</param>
        /// <returns> Validatie resultaat</returns>
		protected virtual bool validateFinish(out object input)
		{
			input = null;
			bool result = validateFinishInt();
			if (result)
			{
				input = read_input();
				result = extValFinish(input);
				if (!result)
					input = null;
			}
			return result;
		}
		#endregion
		#region internal validation
		/// <summary>
		/// Valideert input op een intern niveau (zoals number formats)
		/// voordat hij doorgaat naar de vorige wizardstep
		/// </summary>
		/// <returns> Validatie resultaat</returns>
		protected virtual bool validatePreviousInt()
		{
			return true;
		}

		/// <summary>
        /// Valideert input op een intern niveau (zoals number formats)
        /// voordat hij doorgaat naar de volgende wizardstep
		/// </summary>
		/// <returns> Validatie resultaat</returns>
		protected virtual bool validateNextInt()
		{
			return true;
		}

        /// <summary>
        /// Valideert input op een intern niveau (zoals number formats)
        /// voordat hij de hele wizard afsluit
        /// </summary>
        /// <returns> Validatie resultaat</returns>
		protected virtual bool validateFinishInt()
		{
			return true;
		}
		#endregion
		#region external validation
		/// <summary>
		/// Deze methode NIET zelf implementeren!
		/// 
		/// Valideert input op een (wizard) globaal/extern niveau
		/// voordat hij doorgaat naar de vorige wizardstep
		/// </summary>
		protected abstract bool extValPrevious(object input);

        /// <summary>
		/// Deze methode NIET zelf implementeren!
		/// 
        /// Valideert input op een (wizard) globaal/extern niveau
        /// voordat hij doorgaat naar de volgende wizardstep
        /// </summary>
		protected abstract bool extValNext(object input);

        /// <summary>
		/// Deze methode NIET zelf implementeren!
		/// 
        /// Valideert input op een (wizard) globaal/extern niveau
        /// voordat hij de hele wizard afsluit
        /// </summary>
		protected abstract bool extValFinish(object input);
		#endregion
		#region save validation
		/// <summary>
		/// Laat een dialog zien die vraagt of je data van deze step wilt opslaan
		/// </summary>
		/// <param name="title">De titel van de dialog</param>
		/// <param name="text">De tekst in de dialog zelf</param>
		/// <returns>Of je door kan gaan of niet</returns>
		public bool saveDialog(string title, string text)
		{
			DialogResult result = MessageBox.Show(text, text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
			switch (result)
			{
				case DialogResult.Yes:
					Save();
					return true;
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				default:
					return true;
			}
		}

		/// <summary>
		/// Opslaan van data van een bepaalde step
		/// </summary>
		public virtual void Save()
		{
		}
		#endregion
		#endregion

		#region reading input
		/// <summary>
		/// Leest de wizardstep's input data in een object
		/// Hulpmethode, moet NIET aangeroepen worden van buiten de abstract wizard step classes
		/// Wordt gebruikt voor InputTpe typecasting
		/// Is used for InputType typecasting
		/// </summary>
		/// <returns> Input data van de user in deze stap</returns>
		protected abstract object read_input();
		#endregion
	}
}
