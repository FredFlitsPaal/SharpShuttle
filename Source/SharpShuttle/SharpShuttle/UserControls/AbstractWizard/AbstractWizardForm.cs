using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Shared.Logging;

namespace UserControls.AbstractWizard
{
	/// <summary>
	/// Methode die aangeroepen wordt om de volledige input te verwerken
	/// </summary>
	/// <typeparam name="CompleteInputType"></typeparam>
	/// <param name="sender"></param>
	/// <param name="complete_input"></param>
	public delegate void ProcessCompleteInput<CompleteInputType>(AbstractWizardForm<CompleteInputType> sender, CompleteInputType complete_input)
		where CompleteInputType : class;

	/// <summary>
	/// Abstracte klasse voor alle wizards
	/// </summary>
	/// <typeparam name="CompleteInputType"></typeparam>
    public abstract partial class AbstractWizardForm<CompleteInputType> : Form
		where CompleteInputType : class
    {

		/// <summary>
		/// De titel van het wizardscherm
		/// </summary>
    	protected string title;

		/// <summary>
		/// Huidige stap in de wizard
		/// </summary>
    	protected int step = 1;

		/// <summary>
		/// Is de wizard resizable
		/// </summary>
    	private bool resizable;

		/// <summary>
		/// Resizet de wizard automatischh
		/// </summary>
    	private bool auto_resize;

		/// <summary>
		/// Hierin staan alle step-controls
		/// </summary>
    	protected AbstractWizardStep[] steps;

		/// <summary>
		/// Als this.steps[i] geen waarde heeft, kan hij aangemaakt worden als zijn type in deze
		/// property staat
		/// </summary>
    	protected Type[] stepTypes;

		/// <summary>
		/// De schermgroottes die bij alle stappen horen
		/// </summary>
    	protected Dictionary<int, Size> stepSizes;

		/// <summary>
		/// Maakt de wizard en initialiseert hem
		/// </summary>
		/// <param name="name"></param>
		public AbstractWizardForm(string name)
        {
			InitializeComponent();

			title = name;
			stepSizes = new Dictionary<int, Size>();
			Resizable = false;
			auto_resize = true;
			Init();
			gotoStep(step);
			
        }

		/// <summary>
		/// Initialiseert de wizard
		/// </summary>
    	public abstract void Init();

		/// <summary>
		/// De titel van het wizardscherm
		/// </summary>
    	public string Title
    	{
			get { return title; }
			set {
				title = value;
				setForm();
			}
    	}

		/// <summary>
		/// Is de wizard resizable
		/// </summary>
    	public bool Resizable
    	{
			get { return resizable; }
    		set
			{
				resizable = value;
				FormBorderStyle = value ? FormBorderStyle.SizableToolWindow : FormBorderStyle.FixedToolWindow;
				SizeGripStyle = value ? SizeGripStyle.Show : SizeGripStyle.Hide;
    		}
    	}

		/// <summary>
		/// Resizet de wizard automatisch
		/// </summary>
    	public bool AutoResize
    	{
			get { return auto_resize; }
			set
			{
				auto_resize = value;
				if (value)
					wizardResize();
			}
    	}

		/// <summary>
		/// Het nummer van de huidige stap. Range 1..n
		/// </summary>
    	public int Step
    	{
			get { return step; }
			set
			{
				gotoStep(value);
			}
    	}

		/// <summary>
		/// Het totaal aantal stappen in de wizard
		/// </summary>
		/// <returns></returns>
    	public virtual int GetStepCount()
    	{
    		return steps.Count();
    	}

		/// <summary>
		/// De huidige stap in de wizard
		/// </summary>
    	protected AbstractWizardStep currentStep
    	{
			get { return steps[step-1]; }
    	}

		/// <summary>
		/// Ga na een bepaalde stap van de wizard
		/// </summary>
		/// <param name="new_step"> De stap waar de wizard naar toe moet </param>
		protected void gotoStep(int new_step)
		{
			AbstractWizardStep oldstep = currentStep;
			//update index
			step = new_step;
			AbstractWizardStep cur = currentStep;

			if (cur == null)
			{
				Logger.Write("AbstractWizardForm", "step obj. not found. going to generate one.");
                //Probeer een WizardStep te maken
				if (stepTypes == null)
				{
					Logger.Write("AbstractWizardForm", "no step type found for generating.");
					throw new NullReferenceException("Cannot find any step types for automatic step generation");
				}
				Type curtyp;
				try { curtyp = stepTypes[new_step]; }
				catch (IndexOutOfRangeException)
				{
					Logger.Write("AbstractWizardForm", "specific step type for generating not found in array.");
					throw new IndexOutOfRangeException(string.Format("Cannot find step type for new_step {0}", new_step));
				}
				if (curtyp == null)
				{
					Logger.Write("AbstractWizardForm", "specific step type for generating not set.");
					throw new NullReferenceException(string.Format("Cannot find or generate new_step {0}", new_step));
				}
				ConstructorInfo cinfo = curtyp.GetConstructor(Type.EmptyTypes);
				cur = (AbstractWizardStep) cinfo.Invoke(null);
				steps[new_step] = cur;
			}

			//Voeg een listener toe
			oldstep.StepNavigationChanged -= WizardStep_StepNavigationChanged;
			cur.StepNavigationChanged += WizardStep_StepNavigationChanged;

			//bepaal "originele grootte"
			Size s;
			if (!stepSizes.TryGetValue(new_step, out s))
			{
				s = cur.Size;
				stepSizes.Add(new_step, s);
			}

			//Voeg aan de container toe
			stepPanel.Controls.Remove(oldstep);
			stepPanel.Controls.Add(cur);

			//vul scherm
			cur.SetBounds(0, 0, stepPanel.Width, stepPanel.Height);
			cur.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
			//resize
			if (AutoResize)
				wizardResize();
			
			cur.FireStepsHaveChanged();
			cur.Focus();
			cur.Visible = true; //Even een visible aanroepen zodat we zeker weten dat de control getekend wordt.
			setForm();
		}

		/// <summary>
		/// Stelt de titel van de wizard in
		/// </summary>
		protected virtual void setForm()
		{
			string stepname = "";
			if (!string.IsNullOrEmpty(currentStep.Text))
				stepname = string.Concat(": ", currentStep.Text);
			string count = "";
			int c = GetStepCount();
			if (c > 0)
				count = string.Format("/{0}", c);
			Text = string.Format("{0} - Stap {1}{2}{3}", Title, Step, count, stepname);
		}

		/// <summary>
		/// resizet de wizard
		/// </summary>
    	protected virtual void wizardResize()
    	{
    		Size s;
			if (stepSizes.TryGetValue(step, out s))
			{
				Size = s + (Size - stepPanel.Size);
			}

            CenterToParent();
    	}

		/// <summary>
		/// Wordt aangeroepen wanneer van stap veranderd is
		/// </summary>
		/// <param name="prev_visible"></param>
		/// <param name="prev_enabled"></param>
		/// <param name="next_visible"></param>
		/// <param name="next_enabled"></param>
		/// <param name="finish_visible"></param>
		/// <param name="finish_enabled"></param>
		protected void WizardStep_StepNavigationChanged(bool prev_visible, bool prev_enabled, bool next_visible, bool next_enabled, bool finish_visible, bool finish_enabled)
		{
			btnPrevious.Visible = prev_visible;
			btnPrevious.Enabled = prev_enabled;
			if (prev_visible)
				AcceptButton = btnPrevious;

			btnNext.Visible = next_visible;
			btnNext.Enabled = next_enabled;
			if (next_visible)
				AcceptButton = btnNext;

			btnFinish.Visible = finish_visible;
			btnFinish.Enabled = finish_enabled;
			if (finish_visible)
				AcceptButton = btnFinish;
		}

		#region navigation button events
		/// <summary>
		/// Wordt aangeroepen wanneer op de "volgende" knop is geklikt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNext_Click(object sender, EventArgs e)
		{
			Focus();
			AbstractWizardStep cur = currentStep;
			bool canNext;
			object input;
			cur.Next(out canNext, out input); //Zorgt ook voor volledige validatie
			if (canNext)
				next(input);
		}

		/// <summary>
		/// Wordt aangeroepen wanneer op de "vorige" knop is geklikt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrevious_Click(object sender, EventArgs e)
		{
			Focus();
			AbstractWizardStep cur = currentStep;
			bool canPrev;
			object input;
			cur.Previous(out canPrev, out input); //Zorgt ook voor volledige validatie
			if (canPrev)
				previous(input);
		}

		/// <summary>
		/// Wordt aangeroepen wanneer de wizard afgerond is
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFinish_Click(object sender, EventArgs e)
		{
			Focus();
			AbstractWizardStep cur = currentStep;
			bool canFinish;
			object input;
            cur.Finish(out canFinish, out input); //Zorgt ook voor volledige validatie
			if (canFinish)
				finish(input);
		}
		#endregion

		#region step handling
		#region complete step handling
		/// <summary>
		/// Handelt het gaan naar de volgende stap af
		/// </summary>
		/// <param name="input"> De input data van de oude stap</param>
		private void next(object input)
    	{
			nextInt(input);
			Step++;
    	}

		/// <summary>
		/// Handelt het gaan naar de vorige stap af
		/// </summary>
		/// <param name="input"> De input data van de oude stap</param>
		private void previous(object input)
		{
			prevInt(input);
			Step--;
		}

		/// <summary>
		/// Handelt het finishen van de wizard af
		/// </summary>
		/// <param name="input">input data van de (laatste) stap</param>
		private void finish(object input)
		{
			stepFinishInt(input);
			CompleteInputType completeInput = readCompleteInput();
			completeFinishInt(completeInput);
			Finish(this, completeInput);
			Close();
			
		}
		#endregion
		#region internal step handling
		/// <summary>
		/// Zal worden aangeroepen na validatie van de oude (huidige) stap en voor de nieuwe stap.
		/// Handig wanneer de volgende stap afhankelijk is van het afmaken van de huidige stap.
		/// </summary>
		/// <param name="input">Input van de huidige stap</param>
		protected virtual void nextInt(object input) {}

		/// <summary>
		/// Zal worden aangeroepen na validatie van de oude (huidige) stap en voor de nieuwe stap.
		/// </summary>
		/// <param name="input">Input van de huidige stap</param>
		protected virtual void prevInt(object input) {}

		/// <summary>
        /// Zal worden aangeroepen na validatie van de oude (huidige) stap en voor de nieuwe stap.
        /// Kan worden gebruikt voor een cleanup
		/// </summary>
		/// <param name="input">Input van de huidige stap</param>
    	protected virtual void stepFinishInt(object input) {}

		/// <summary>
		/// Handelt het wizard-internal verwerken van de input van alle stappen af
		/// </summary>
		/// <param name="complete_input">De input van alle stappen</param>
		protected abstract void completeFinishInt(CompleteInputType complete_input);
    	#endregion
		#region external step handling
		/// <summary>
		/// Wordt getriggerd net voordat de wizard gesloten wordt.
		/// Handig voor het verwijderen van sluitende listeners of om iets met de input te doen
		/// </summary>
    	public ProcessCompleteInput<CompleteInputType> Finish;
		#endregion
		#region reading input
		/// <summary>
		/// Wordt getriggerd als de input volledig gelezen is
		/// </summary>
		/// <returns></returns>
		protected abstract CompleteInputType readCompleteInput();
		#endregion
		#endregion
	}
}