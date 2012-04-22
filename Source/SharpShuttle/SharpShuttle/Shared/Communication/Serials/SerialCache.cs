using System.Collections.Generic;
using System.Threading;

namespace Shared.Communication.Serials
{

	/// <summary>
	/// De SerialCache heeft een lijst met alle serienummers die actief zijn binnen deze applicatie.
	/// Deze klasse biedt functionaliteit om te kijken wanneer een serienummer out of date is.
	/// </summary>
	public class SerialCache
	{
		/// <summary>
		///Houd een verwijzing naar een instantie van SerialCache bij zodat we een singleton krijgen
		/// </summary>
		private static readonly SerialCache singleton = new SerialCache();
		
		/// <summary>
		/// Alle constructoren goed gezet zodat er geen concurrencyproblemen ontstaan.
		/// </summary>
		private SerialCache() { }

		// DO NOT REMOVE!!
		// google BeforeFieldInit+singleton
		static SerialCache() { }

		/// <summary>
		/// Definieer een mutex zodat we de toegang tot de trackinglist kunnen regelen
		/// </summary>
		private Mutex mutex = new Mutex();

		/// <summary>
		/// Geef iedereen de mogelijkheid om de cache te benaderen
		/// </summary>
		public static SerialCache GetCache()
		{
			return singleton;
		}

		/// <summary>
		/// De singleton instance van de SerialCache
		/// </summary>
		public static SerialCache Instance
		{
			get { return singleton; }
		}

		/// <summary>
		/// In de serials dictionary houden we een lijst bij van alle serienummers die in de omloop zijn
		/// </summary>
		private Dictionary<long,Serial> serials = new Dictionary<long,Serial>();

		/// <summary>
		/// Voeg een nieuwe SerialDefinition toe of update een bestaande
		/// </summary>
		/// <param name="serial"></param>
		/// <returns></returns>
		public SerialDefinition AddOrUpdate(SerialDefinition serial)
		{
			//zorg dat er maar 1 thread tegelijkertijd in de lijst mag
			mutex.WaitOne();

			Serial s;
			if (serials.TryGetValue(serial.SerialNumber, out s))
			{
				s.SerialID.VersionNumber = serial.VersionNumber;
				s.Valid = true;
			}
			else
			{
				serials.Add(serial.SerialNumber, new Serial(serial, true));
			}

			//geef de toegang tot de lijst weer terug
			mutex.ReleaseMutex();

			return serial;
		}

		/// <summary>
		/// Haalt een bestaande SerialDefinition uit de cache of voegt een nieuwe toe
		/// </summary>
		/// <param name="serial"></param>
		/// <returns></returns>
		public SerialDefinition GetOrAdd(SerialDefinition serial)
		{
			//zorg dat er maar 1 thread tegelijkertijd in de lijst mag
			mutex.WaitOne();

			Serial s;

			if (serials.TryGetValue(serial.SerialNumber, out s))
			{
				serial = s.SerialID;
			}
			else
			{
				serials.Add(serial.SerialNumber, new Serial(serial, true));
			}

			//geef de toegang tot de lijst weer terug
			mutex.ReleaseMutex();

			return serial;
		}

		/// <summary>
		/// Maak de SerialCache leeg
		/// </summary>
		public void Clear()
		{
			//toegang tot de lijst beperken
			mutex.WaitOne();

			serials.Clear();

			//maak te toegang weer vrij
			mutex.ReleaseMutex();
		}

		/// <summary>
		/// Maak een SerialDefinition ongeldig
		/// </summary>
		/// <param name="sd"></param>
		public void Invalidate(SerialDefinition sd)
		{
			//toegang tot de lijst beperken
			mutex.WaitOne();

			Serial s;
			if (serials.TryGetValue(sd.SerialNumber, out s))
			{
				s.Valid = false;

				//Omdat de serial bestaat moeten we nu ervoor zorgen dat iedereen een serialupdatekrijgt
				//TODO
			}

			//maak de toegang weer vrij
			mutex.ReleaseMutex();
		}

		/// <summary>
		/// Is een SerialDefinition up to date
		/// </summary>
		/// <param name="sd"></param>
		/// <returns></returns>
		public bool IsUpToDate(SerialDefinition sd)
		{
			//weer de toegangbeperken
			mutex.WaitOne();

			Serial s;
			bool result;

			if (serials.TryGetValue(sd.SerialNumber, out s))
				result = s.Valid;
			else
				result = false;

			//geef de toegang weer vrij
			mutex.ReleaseMutex();

			return result;
		}

		/// <summary>
		/// Is een SerialDefinition out of date
		/// </summary>
		/// <param name="sd"></param>
		/// <returns></returns>
		public bool IsOutOfDate(SerialDefinition sd)
		{
			return !IsUpToDate(sd);
		}

		/// <summary>
		/// Handel het binnenkomen van een serienummer af en kijk of hij up to date is
		/// </summary>
		/// <param name="sd">De SerialDefinition die is binnengekomen</param>
		/// <returns>true als de serial definitie up to date is anders false</returns>
		public bool HandleSerialUpdate(SerialDefinition sd)
		{
			//weer de toegangbeperken
			mutex.WaitOne();

			//Eerst kijken of we een serialdefinitie hiervan hebben
			Serial s;
			bool result = false;

			if (serials.TryGetValue(sd.SerialNumber, out s))
			{
				if (s.CheckVersion(sd))
				{
					//Als onze serial up to date is, is het goed
					result = true;
				}
				else
				{
					//zetten op invalidated
					s.Valid = false;
				}
			}

			//geef de toegang weer vrij
			mutex.ReleaseMutex();

			return result;
		}

		/// <summary>
		/// Hoogt een serienummer van een bestaande SerialDefinition op
		/// of voegt een nieuwe toe
		/// </summary>
		/// <param name="sd"></param>
		/// <returns></returns>
		public SerialDefinition IncreaseVersion(SerialDefinition sd)
		{
			//toegang tot de lijst beperken
			mutex.WaitOne();

			SerialDefinition ret;

			Serial s;
			if (serials.TryGetValue(sd.SerialNumber, out s))
			{
				s.SerialID.VersionNumber++;
				s.Valid = true;
				ret = s.SerialID;
			}
			else
			{
				//Als de serial nog niet bestaan, dan begint hij op 0
				sd.VersionNumber = 0;
				serials.Add(sd.SerialNumber, new Serial(sd, true));
				ret = sd;
			}

			//maak de toegang weer vrij
			mutex.ReleaseMutex();

			return ret;
		}
	
		/// <summary>
		/// Voeg toe of update een lijst van SerialDefinitions
		/// </summary>
		/// <param name="SerialDefinitions"></param>
		public void HandleNewSerialDefinitions(IList<SerialDefinition> SerialDefinitions)
		{
			//Zorg dat wij alvast access krijgen op de lijst met serial
			//overerving van Mutexen gaat goed
			//zorg dat er maar 1 thread tegelijkertijd in de lijst mag
			mutex.WaitOne();


			foreach(SerialDefinition sd in SerialDefinitions)
			{
				AddOrUpdate(sd);
			}

			//zorg dat er maar 1 thread tegelijkertijd in de lijst mag
			mutex.ReleaseMutex();
		}

		/// <summary>
		/// Verhoog het serienummer van een Poule
		/// </summary>
		/// <param name="PouleID">De id van de poule om het serienummer op te hogen</param>
		/// <returns>De nieuwe SerialDefinition</returns>
		public SerialDefinition IncreasePoule(int PouleID)
		{
			return IncreaseVersion(new SerialDefinition(SerialTypes.Poule, PouleID));
		}

		/// <summary>
		/// Haal het serienummer van een poule op
		/// </summary>
		/// <param name="PouleID">De id van de poule om het serienummer op te vragen</param>
		/// <returns>De SerialDefinition van de poule</returns>
		public SerialDefinition GetPoule(int PouleID)
		{
			return GetOrAdd(new SerialDefinition(SerialTypes.Poule, PouleID));
		}
	}
}
