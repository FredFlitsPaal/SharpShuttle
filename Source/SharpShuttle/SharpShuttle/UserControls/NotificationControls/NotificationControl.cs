using System.Windows.Forms;
using System.ComponentModel;
using System;
using Shared.Communication.Serials;
using System.Drawing;

namespace UserControls.NotificationControls
{
	/// <summary>
	/// De NotificationControl is de brug tussen de gebruiker en het ophalen van data van de server.
	/// Deze klassen implementeerd een button die naar gelang de update status van de data de refresh of save delegate aanroept.
	/// Tevens biedt deze klasse de functionaliteit om automatisch veranderingen van de server te 'zien'
	/// </summary>
	public partial class NotificationControl : Button
	{

		/// <summary>
		/// De Signature van een BackgroundWorker Delegate
		/// </summary>
		public delegate void BackgroundWorkerDelegate(Action action);

		/// <summary>
		/// De event die gevoked wordt als de backgroundworker start
		/// </summary>
		public event BackgroundWorkerDelegate WorkStart;
		/// <summary>
		/// De event die geinvoked wordt als de backgroundworker klaar is
		/// </summary>
		public event BackgroundWorkerDelegate WorkFinished;

		/// <summary>
		/// De status van de NotificationControl
		/// </summary>
		public enum State
		{
			/// <summary>
			/// Standaard state, geen wijzigingen.
			/// </summary>
			Default,
			/// <summary>
			/// Lokale data aan het opslaan.
			/// </summary>
			SavingData,
			/// <summary>
			/// Serverdata aan het ophalen.
			/// </summary>
			LoadingData, 
			/// <summary>
			/// Er zijn lokale wijzigingen die nog niet zijn opgeslagen.
			/// </summary>
			UnsavedChanges,
			/// <summary>
			/// De data is op de server gewijzigd. Deze moet opnieuw worden opgehaald.
			/// </summary>
			ExternallyChanged
		}

		/// <summary>
		/// De verschillende acties in de BackgroundWorkerDelegate
		/// </summary>
		public enum Action
		{
			/// <summary>
			/// Herladen van de serverdata.
			/// </summary>
			Reload,
			/// <summary>
			/// Opslaan van de lokale data.
			/// </summary>
			Save
		}

		private struct Work
		{
			public Action action;
			public BackgroundWorkerDelegate worker;

			public Work(Action action, BackgroundWorkerDelegate worker)
			{
				this.action = action;
				this.worker = worker;
			}
		}

		#region Fields

		private State state = State.Default;
		private ISerialFilter filter;
		private BackgroundWorkerDelegate workerdelegate;
		private FilterFlags filterflags;
		private bool _enabled = true;

		private Bitmap[] images;

		#endregion

		#region Constructors / Destructors

		/// <summary>
		/// Instantieer een nieuwe <see cref="NotificationControl"/>
		/// </summary>
		/// <param name="Worker">De BackgroundWorkerDelegate die moet worden aangeroepen in de background thread</param>
		/// <param name="SerialFilter">De SerialFilter die aangeeft welke events interessant zijn</param>
		public NotificationControl(BackgroundWorkerDelegate Worker, ISerialFilter SerialFilter)
		{
			this.filter = SerialFilter;
			this.filterflags = SerialFilter.FilterFlags;
			this.workerdelegate = Worker;
			this.state = State.Default;

			base.ImageAlign = ContentAlignment.MiddleLeft;
			base.TextImageRelation = TextImageRelation.ImageBeforeText;

			images = new Bitmap[]{
				Properties.Resources.DisabledRefresh,
				Properties.Resources.Refresh,
				Properties.Resources.SaveDisc};

			InitializeComponent();
			SubscribeEvents(this.filterflags);
			MouseUp += new MouseEventHandler(NotificationControl_MouseUp);
			base.Enabled = _enabled;
			UpdateControl();
		}

		/// <summary>
		/// De destructor van de NotificationControl die automatisch alle events deregistreerd
		/// </summary>
		~NotificationControl()
		{
			DeSubscribeEvents(this.filterflags);
		}

		#endregion

		/// <summary>
		/// De status waar de NotificationControl zich momenteel in bevindt
		/// </summary>
		/// <value>De status</value>
		public State Status
		{
			get { return state; }
			set { state = value; UpdateControl(); }
		}

		void NotificationControl_MouseUp(object sender, MouseEventArgs e)
		{
			switch (state)
			{
				case State.Default:
				case State.LoadingData:
				case State.SavingData:
					//niets
					break;
				case State.UnsavedChanges:
					//save
					//Controleer of het de rechtermuisknop was
					if (e.Button == MouseButtons.Right)
					{
						if (MessageBox.Show("Weet u zeker dat u alle veranderingen ongedaan wilt maken door de gegevens opnieuw op te halen?", "Sharp Shuttle - Gegevens herladen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							StartBackgroundWorker(Action.Reload);
						}
						break;
					}
					StartBackgroundWorker(Action.Save);
					break;
				case State.ExternallyChanged:
					//reload data
					StartBackgroundWorker(Action.Reload);
					break;
				default:
					break;
			}
		}

		private void StartBackgroundWorker(Action action)
		{
			//Controleer of we wel mogen klikken
			if (!_enabled)
				return;
			
			if (action == Action.Reload)
			{
				state = State.LoadingData;
			}
			else if (action == Action.Save)
			{
				state = State.SavingData;
			}

			bgwBackgroundWorker.RunWorkerAsync(new Work(action, this.workerdelegate));
		}

		/// <summary>
		/// Zorg ervoor dat de NotificationControl automatisch een Reload in de achtergrond aanroept
		/// </summary>
		public void Reload()
		{
			StartBackgroundWorker(Action.Reload);
		}

		/// <summary>
		/// Zorg ervoor dat de NotificationControl automatisch een Save in de achtergrond aanroept
		/// </summary>
		public void Save()
		{
			if (this.state == State.UnsavedChanges)
				StartBackgroundWorker(Action.Save);
		}

		/// <summary>
		/// Verander of de knop moet reageren op events of acties van de gebruiker
		/// </summary>
		/// <value></value>
		/// <returns>true als de knop actief is
		/// </returns>
		new public bool Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				this._enabled = value;
				SetEnable();
			}
		}

		private void SetEnable()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(SetEnable));
				return;
			}

			this.Enabled = this._enabled;
		}

		#region Button Update functions

		private void ExternallyChanged()
		{
			this.state = State.ExternallyChanged;
			UpdateControl();
		}

		/// <summary>
		/// Zeg tegen de NotificationControl dat hij lokaal veranderd is door de gebruiker
		/// </summary>
		public void Changed()
		{
			if (this.state == State.Default)
				this.state = State.UnsavedChanges;
			UpdateControl();
		}

		/// <summary>
		/// Deze methode moet de text en het icoontje van het knopje veranderen
		/// </summary>
		private void UpdateControl()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(UpdateControl));
				return;
			}
	
			switch (this.state)
			{
				case State.Default:
					base.Text = "Klaar";
					base.Image = images[0];
					break;
				case State.LoadingData:
					base.Text = "Loading…";
					base.Image = null;
					break;
				case State.SavingData:
					base.Text = "Saving…";
					base.Image = null;
					break;
				case State.UnsavedChanges:
					base.Text = "Opslaan";
					base.Image = images[2];
					break;
				case State.ExternallyChanged:
					base.Text = "Herladen";
					base.Image = images[1];
					break;
				default:
					break;
			}

			this.Invalidate();
		}

		#endregion

		#region Background Worker wrappers

		private void RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			//Kijken of we wel enabled zijn
			if (!_enabled)
				return;

			state = State.Default;
			UpdateControl();
			if (this.WorkFinished != null)
				this.WorkFinished((Action)e.Result);
		}

		private void DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			//Kijken of we wel enabled zijn
			if (!_enabled)
				return;

			e.Result = e.Argument;
			Work param = (Work)e.Argument;
	
			if (this.WorkStart != null)
				this.WorkStart(param.action);

			param.worker(param.action);

			e.Result = param.action;
		}

		#endregion

		#region (De)Subscribe of events

		private void SubscribeEvents(FilterFlags filter)
		{
			//We moeten nu controleren op welke events we allemaal moeten subscriben
			if ((filter | FilterFlags.UpdateAllHistoryMatchesEvent) == filter)
					SerialTracker.Instance.AllHistoryMatchesUpdated += new SerialTracker.AllUpdatedDelegate(Instance_AllHistoryMatchesUpdated);

			if ((filter | FilterFlags.UpdateAllMatchesEvent) == filter)
				SerialTracker.Instance.AllMatchesUpdated += new SerialTracker.AllUpdatedDelegate(Instance_AllMatchesUpdated);
			
			if ((filter | FilterFlags.UpdateAllPlayersEvent) == filter)
				SerialTracker.Instance.AllPlayersUpdated += new SerialTracker.AllUpdatedDelegate(Instance_AllPlayersUpdated);
		
			if ((filter | FilterFlags.UpdateAllPoulesEvent) == filter)
				SerialTracker.Instance.AllPoulesUpdated += new SerialTracker.AllUpdatedDelegate(Instance_AllPoulesUpdated);
		
			if ((filter | FilterFlags.UpdateMatchEvent) == filter)
				SerialTracker.Instance.MatchUpdated += new SerialTracker.MatchUpdatedDelegate(Instance_MatchUpdated);
		
			if ((filter | FilterFlags.UpdatePouleEvent) == filter)
				SerialTracker.Instance.PouleUpdated += new SerialTracker.PouleUpdatedDelegate(Instance_PouleUpdated);
		
			if ((filter | FilterFlags.UpdatePouleLadderEvent) == filter)
				SerialTracker.Instance.PouleLadderUpdated += new SerialTracker.PouleUpdatedDelegate(Instance_PouleLadderUpdated);
		
			if ((filter | FilterFlags.UpdatePouleMatchesEvent) == filter)
				SerialTracker.Instance.PouleMatchesUpdated += new SerialTracker.PouleUpdatedDelegate(Instance_PouleMatchesUpdated);
		
			if ((filter | FilterFlags.UpdatePoulePlanningEvent) == filter)
				SerialTracker.Instance.PoulePlanningUpdated += new SerialTracker.PouleUpdatedDelegate(Instance_PoulePlanningUpdated);
		
			if ((filter | FilterFlags.UpdatePouleTeamsEvent) == filter)
				SerialTracker.Instance.PouleTeamsUpdated += new SerialTracker.PouleUpdatedDelegate(Instance_PouleTeamsUpdated);

			if ((filter | FilterFlags.UpdateSettings) == filter)
				SerialTracker.Instance.SettingsUpdated += new SerialTracker.AllUpdatedDelegate(Instance_SettingsUpdated);
		
		}

		private void DeSubscribeEvents(FilterFlags filter)
		{
			//We moeten nu alle gesubscribde events desubscriben
			if ((filter | FilterFlags.UpdateAllHistoryMatchesEvent) == filter)
				SerialTracker.Instance.AllHistoryMatchesUpdated -= new SerialTracker.AllUpdatedDelegate(Instance_AllHistoryMatchesUpdated);

			if ((filter | FilterFlags.UpdateAllMatchesEvent) == filter)
				SerialTracker.Instance.AllMatchesUpdated -= new SerialTracker.AllUpdatedDelegate(Instance_AllMatchesUpdated);
			
			if ((filter | FilterFlags.UpdateAllPlayersEvent) == filter)
				SerialTracker.Instance.AllPlayersUpdated -= new SerialTracker.AllUpdatedDelegate(Instance_AllPlayersUpdated);
		
			if ((filter | FilterFlags.UpdateAllPoulesEvent) == filter)
				SerialTracker.Instance.AllPoulesUpdated -= new SerialTracker.AllUpdatedDelegate(Instance_AllPoulesUpdated);
		
			if ((filter | FilterFlags.UpdateMatchEvent) == filter)
				SerialTracker.Instance.MatchUpdated -= new SerialTracker.MatchUpdatedDelegate(Instance_MatchUpdated);
		
			if ((filter | FilterFlags.UpdatePouleEvent) == filter)
				SerialTracker.Instance.PouleUpdated -= new SerialTracker.PouleUpdatedDelegate(Instance_PouleUpdated);
		
			if ((filter | FilterFlags.UpdatePouleLadderEvent) == filter)
				SerialTracker.Instance.PouleLadderUpdated -= new SerialTracker.PouleUpdatedDelegate(Instance_PouleLadderUpdated);
		
			if ((filter | FilterFlags.UpdatePouleMatchesEvent) == filter)
				SerialTracker.Instance.PouleMatchesUpdated -= new SerialTracker.PouleUpdatedDelegate(Instance_PouleMatchesUpdated);
		
			if ((filter | FilterFlags.UpdatePoulePlanningEvent) == filter)
				SerialTracker.Instance.PoulePlanningUpdated -= new SerialTracker.PouleUpdatedDelegate(Instance_PoulePlanningUpdated);
		
			if ((filter | FilterFlags.UpdatePouleTeamsEvent) == filter)
				SerialTracker.Instance.PouleTeamsUpdated -= new SerialTracker.PouleUpdatedDelegate(Instance_PouleTeamsUpdated);

			if ((filter | FilterFlags.UpdateSettings) == filter)
				SerialTracker.Instance.SettingsUpdated -= new SerialTracker.AllUpdatedDelegate(Instance_SettingsUpdated);
			
		}

		#endregion

		#region SerialTracker events

		void Instance_SettingsUpdated(SerialEventTypes SerialEvent)
		{
			if (filter.UpdateSettings(SerialEvent))
				ExternallyChanged();
		}

		void Instance_PouleTeamsUpdated(int PouleID, SerialEventTypes SerialEvent)
		{
			if (filter.UpdatePouleTeamsEvent(PouleID,SerialEvent))
				ExternallyChanged();
		}

		void Instance_PoulePlanningUpdated(int PouleID, SerialEventTypes SerialEvent)
		{
			if (filter.UpdatePoulePlanningEvent(PouleID,SerialEvent))
				ExternallyChanged();
		}

		void Instance_PouleMatchesUpdated(int PouleID, SerialEventTypes SerialEvent)
		{
			if (filter.UpdatePouleMatchesEvent(PouleID, SerialEvent))
				ExternallyChanged();
		}

		void Instance_PouleLadderUpdated(int PouleID, SerialEventTypes SerialEvent)
		{
			if (filter.UpdatePouleLadderEvent(PouleID, SerialEvent))
				ExternallyChanged();
		}

		void Instance_PouleUpdated(int PouleID, SerialEventTypes SerialEvent)
		{
			if (filter.UpdatePouleEvent(PouleID, SerialEvent))
				ExternallyChanged();
		}

		void Instance_MatchUpdated(int MatchID, SerialEventTypes SerialEvent)
		{
			if (filter.UpdateMatchEvent(MatchID, SerialEvent))
				ExternallyChanged();
		}

		void Instance_AllPoulesUpdated(SerialEventTypes SerialEvent)
		{
			if (filter.UpdateAllPoulesEvent(SerialEvent))
				ExternallyChanged();
		}

		void Instance_AllPlayersUpdated(SerialEventTypes SerialEvent)
		{
			if (filter.UpdateAllPlayersEvent(SerialEvent))
				ExternallyChanged();
		}

		void Instance_AllMatchesUpdated(SerialEventTypes SerialEvent)
		{
			if (filter.UpdateAllMatchesEvent(SerialEvent))
				ExternallyChanged();
		}

		void Instance_AllHistoryMatchesUpdated(SerialEventTypes SerialEvent)
		{
			if (filter.UpdateAllHistoryMatchesEvent(SerialEvent))
				ExternallyChanged();
		}
		
		#endregion 
	}
}