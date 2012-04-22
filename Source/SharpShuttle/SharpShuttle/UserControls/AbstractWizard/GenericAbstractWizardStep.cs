
namespace UserControls.AbstractWizard
{

	/// <summary>
	/// Methode die aangeroepen wordt bij het veranderen van een stap in een wizard
	/// </summary>
	/// <typeparam name="InputType"></typeparam>
	/// <param name="input"></param>
	/// <returns></returns>
	public delegate bool GenericProcessStepInput<InputType>(InputType input);

	/// <summary>
	/// Alleen AbstractWizardStep, maar met een return type
	/// We hebben deze extra abstractielaag nodig, omdat we anders geen array 
	/// van GenericAbstractWizardStep met verschillende T's kunnen maken
	/// </summary>
	/// <typeparam name="InputType"> 
	/// Een class die je kunt gebruiken om ALLE input in op te slaan, 
	/// zoals een View of een Dictionary
	/// </typeparam>
	public abstract class GenericAbstractWizardStep<InputType> : AbstractWizardStep
		where InputType : class
	{
		/// <summary>
		/// Default construcotr
		/// </summary>
		public GenericAbstractWizardStep() {}

		/// <summary>
		/// Constructor die een StepNavigationChanged delegate meekrijgt
		/// </summary>
		/// <param name="step_nav_listener"></param>
		public GenericAbstractWizardStep(StepNavigationChanged step_nav_listener) : base(step_nav_listener) {}
		
		#region Overrides of AbstractWizardStep for generic typecasting

		/// <summary>
		/// Ga terug naar de vorige wizardstep
		/// </summary>
		/// <param name="can_previous"></param>
		/// <param name="input"></param>
		public override void Previous(out bool can_previous, out object input)
		{
			InputType inp;
			Previous(out can_previous, out inp);
			input = inp;
		}

		/// <summary>
		/// overload voor generiek type
		/// overriden toegestaan
		/// </summary>
		/// <param name="can_previous"></param>
		/// <param name="input"></param>
		public virtual void Previous(out bool can_previous, out InputType input)
		{
			object inp;
			base.Previous(out can_previous, out inp);
			input = (inp != null) ? (InputType)inp : null;
		}

		/// <summary>
		/// Ga door naar de volgende WizardStep
		/// </summary>
		/// <param name="can_previous"></param>
		/// <param name="input"></param>
		public override void Next(out bool can_previous, out object input)
		{
			InputType inp;
			Next(out can_previous, out inp);
			input = inp;
		}

		/// <summary>
		/// overload voor generiek type
		/// overriden toegestaan
		/// </summary>
		/// <param name="can_next"></param>
		/// <param name="input"></param>
		public virtual void Next(out bool can_next, out InputType input)
		{
			object inp;
			base.Next(out can_next, out inp);
			input = (inp != null) ? (InputType)inp : null;
		}

		/// <summary>
		/// Rondt de wizard af
		/// </summary>
		/// <param name="can_previous"></param>
		/// <param name="input"></param>
		public override void Finish(out bool can_previous, out object input)
		{
			InputType inp;
			Finish(out can_previous, out inp);
			input = inp;
		}

		/// <summary>
		/// overload voor generiek type
		/// overriden toegestaan
		/// </summary>
		/// <param name="can_finish"></param>
		/// <param name="input"></param>
		public virtual void Finish(out bool can_finish, out InputType input)
		{
			object inp;
			base.Finish(out can_finish, out inp);
			input = (inp != null) ? (InputType)inp : null;
		}

		/// <summary>
		/// Roept interne en externe validators aan.
		/// Doet ook het lezen van de input (voor externe validatie)
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		protected bool validatePrevious(out InputType input)
		{
			object inp;
			bool result = base.validatePrevious(out inp);
			input = (inp != null) ? (InputType)inp : null;
			return result;
		}

		/// <summary>
		/// Roept interne en externe validators aan.
		/// Doet ook het lezen van de input (voor externe validatie)
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		protected bool validateNext(out InputType input)
		{
			object inp;
			bool result = base.validateNext(out inp);
			input = (inp != null) ? (InputType)inp : null;
			return result;
		}

		/// <summary>
		/// Roept interne en externe validators aan.
		/// Doet ook het lezen van de input (voor externe validatie)
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		protected bool validateFinish(out InputType input)
		{
			object inp;
			bool result = base.validateFinish(out inp);
			input = (inp != null) ? (InputType)inp : null;
			return result;
		}

		/// <summary>
		/// Methode die aangeroepen wordt als de gebruiker een stap terug gaat
		/// </summary>
		public GenericProcessStepInput<InputType> validatePreviousExt; 

		/// <summary>
		/// Valideert input op een (wizard) globaal/extern niveau
		/// voordat hij doorgaat naar de vorige wizardstep
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		protected override bool extValPrevious(object input)
		{
			if (validatePreviousExt != null)
				return validatePreviousExt((InputType) input);
			return true;
		}

		/// <summary>
		/// Methode die aangeroepen wordt als de gebruiker een stap vooruit gaat
		/// </summary>
		public GenericProcessStepInput<InputType> validateNextExt; 

		/// <summary>
		/// Valideert input op een (wizard) globaal/extern niveau
		/// voordat hij doorgaat naar de volgende wizardstep
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		protected override bool extValNext(object input)
		{
			if (validateNextExt != null)
				return validateNextExt((InputType)input);
			return true;
		}

		/// <summary>
		/// Methode die aangeroepen wordt als de gebruiker de wizard afrondt
		/// </summary>
		public GenericProcessStepInput<InputType> validateFinishExt;

		/// <summary>
		/// Valideert input op een (wizard) globaal/extern niveau
		/// voordat hij de wizard afrondt
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		protected override bool extValFinish(object input)
		{
			if (validateFinishExt != null)
				return validateFinishExt((InputType)input);
			return true;
		}

		/// <summary>
		/// Leest de input data van de wizard step in 1 object
		/// </summary>
		/// <returns>Input data van de user in deze stap</returns>
		protected abstract InputType readInput();

		/// <summary>
		/// Leest de wizardstep's input data in een object
		/// </summary>
		/// <returns></returns>
		protected override object read_input()
		{
			return readInput();
		}

		#endregion
	}
}
