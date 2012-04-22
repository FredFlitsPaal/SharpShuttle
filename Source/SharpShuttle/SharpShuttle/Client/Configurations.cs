using System.Collections.Specialized;
using Client.Properties;
using Shared.Data;
using Shared.Domain;
using Shared.Communication.Exceptions;

namespace Client
{
	/// <summary>
	/// Bewaart alle instellingen aan de client kant.
	/// </summary>
	public static class Configurations
	{
		/* default waardes */
		/// <summary>
		/// Het aantal banen van het toernooi.
		/// </summary>
		private volatile static int numberOfCourts = 100;
		/// <summary>
		/// Het aantal sets per wedstrijd dit toernooi.
		/// </summary>
		private volatile static int numberOfSets = 2;
		/// <summary>
		/// De naam van de gebruikte printer op deze client.
		/// </summary>
		private static string printerName = "none";
		/// <summary>
		/// De array van servers waarmee tijdens eerdere instanties verbonden is.
		/// </summary>
		private static string[] previousServers = new string[0];
		/// <summary>
		/// De huidige versie van de toernooi instellingen.
		/// </summary>
		private static TournamentSettings tournamentSettings = new TournamentSettings();
		/// <summary>
		/// Delegate voor het afhandelen van een veranderd aantal velden.
		/// </summary>
		public delegate void TournamentSettingsChangedHandler();
		/// <summary>
		/// Lokaal event die gegooid word bij gewijzigde toernooi-instellingen.
		/// </summary>
		private static event TournamentSettingsChangedHandler _tournamentSettingsChanged;
		/// <summary>
		/// Object om te kunnen locken.
		/// </summary>
		public static object locker = new object();
		/// <summary>
		/// Public event waarop gesubscribed kan worden.
		/// </summary>
		public static event TournamentSettingsChangedHandler TournamentSettingsChangedEvent 
		{
			add
			{
				lock (locker)
				{
					_tournamentSettingsChanged += value;
				}
			}
			remove
			{
				lock (locker)
				{
					_tournamentSettingsChanged -= value;
				}
			}
		}

		/// <summary>
		/// Static constructor. Verbind met events.
		/// </summary>
		static Configurations()
		{
			Shared.Communication.Serials.SerialTracker.Instance.SettingsUpdated += Instance_SettingsUpdated;
			Shared.Communication.Communication.ServerConnected += Communication_ServerConnected;
		}

		/// <summary>
		/// Luistert naar het veranderen van instellingen op de server en haalt deze op.
		/// </summary>
		/// <param name="SerialEvent">Het SerialEvent dat dit event veroorzaakte.</param>
		static void Instance_SettingsUpdated(Shared.Communication.Serials.SerialEventTypes SerialEvent)
		{
			//zit in tijdelijk thread
			ReloadSettings();
		}

		/// <summary>
		/// Luistert naar het verbinden met een server en haalt daarna de instellingen op.
		/// </summary>
		static void Communication_ServerConnected()
		{
			// zit in een tijdelijk thread
			ReloadSettings();
		}

		/// <summary>
		/// Haalt de laatste settings op vanaf de server.
		/// </summary>
		private static void ReloadSettings()
		{
			try
			{
				var newsettings = DataCache.Instance.GetSettings();
				tournamentSettings = newsettings;
				numberOfCourts = newsettings.Fields;
				numberOfSets = newsettings.Sets;
				//Gooi een event dat er wijzigingen zijn.
				lock (locker)
				{
					if (_tournamentSettingsChanged != null)
						_tournamentSettingsChanged();
				}
			}
			catch { } // ignore exception
		}

		/// <summary>
		/// Slaat de huidige toernooisettings op op de server.
		/// </summary>
		static void SaveGlobalSettingsAsync()
		{
			int oldSets = tournamentSettings.Sets;
			int oldFields = tournamentSettings.Fields;
			int updateSets = numberOfSets;
			int updateFields = numberOfCourts;
			tournamentSettings.Sets = numberOfSets;
			tournamentSettings.Fields = numberOfCourts;
			System.Threading.ThreadPool.QueueUserWorkItem(o =>
			{
				try
				{
					//Probeer weg te schrijven.
						CommunicationException ex;
						DataCache.Instance.SetSettings(tournamentSettings, out ex);
						if (ex != null)
						{
							//Als de data out of date is, probeer dan alsnog de verandering door te voeren.
							if (ex is DataOutOfDateException)
							{
								ReloadSettings();
								if (tournamentSettings.Fields != updateFields || tournamentSettings.Sets != updateSets)
									//Sets moet nog veranderd worden.
									if (tournamentSettings.Sets == oldSets && oldSets != updateSets)
									{
										CommunicationException ex2;
										tournamentSettings.Sets = updateSets;
										DataCache.Instance.SetSettings(tournamentSettings, out ex2);
									}
									//Fields moet nog veranderd worden.
									else if (tournamentSettings.Fields == oldFields && oldFields != updateFields)
									{
										CommunicationException ex2;
										tournamentSettings.Fields = updateFields;
										DataCache.Instance.SetSettings(tournamentSettings, out ex2);
									}
							}
						}
					//Anders laten we de verandering voor wat het is.
				}
				catch { } //ignore exception als er perongeluk een is (zou niet moeten)
			});
		}

		/// <summary>
		/// Het aantal velden in het toernooi.
		/// </summary>
		public static int NumberOfCourts
		{
			get { return numberOfCourts; }
			set
			{
				if (numberOfCourts != value && value > 0)
				{
					numberOfCourts = value;
					SaveGlobalSettingsAsync();
				}
			}
		}
		
		/// <summary>
		/// Het aantal sets in een wedstrijd dit toernooi.
		/// </summary>
		public static int NumberOfSets
		{
			get { return numberOfSets; }
			set
			{
				if (numberOfSets != value && value > 0 && value < 4)
				{
					numberOfSets = value;
					SaveGlobalSettingsAsync();
				}
			}
		}

		/// <summary>
		/// De naam van de printer die door deze client gebruikt word.
		/// </summary>
		public static string PrinterName
		{
			get { return printerName; }
			set
			{
				lock (Settings.Default)
				{
					printerName = value;
					Settings.Default.NameOfPrinter = value;
					Settings.Default.Save();
				}
			}
		}

		/// <summary>
		/// Array van vorig bezochte servers.
		/// </summary>
		public static string[] PreviousServers
		{
			get { return previousServers; }
			set
			{
				lock (Settings.Default)
				{
					previousServers = value;
					StringCollection collection = new StringCollection();
					collection.AddRange(value);
					Settings.Default.PreviousServers = collection;
					Settings.Default.Save();
				}
			}
		}

		/// <summary>
		/// Leest de lokale instellingen in vanaf de schijf.
		/// </summary>
		public static void UpdateLocal()
		{
			lock (Settings.Default)
			{
				printerName = Settings.Default.NameOfPrinter;
				StringCollection collection = Settings.Default.PreviousServers;
				if (collection == null)
					previousServers = new string[0];
				else
				{
					string[] newPrevServers = new string[collection.Count];
					collection.CopyTo(newPrevServers, 0);
					previousServers = newPrevServers;
				}
			}
		}

	}
}