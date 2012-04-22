using Shared.Domain;
using System.Text.RegularExpressions;

namespace Shared.Views
{
	/// <summary>
	/// Beschrijft een speler (1 persoon)
	/// </summary>
	public class PlayerView : AbstractView <Player>
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public PlayerView()
		{
			data = new Player("", "", "", "", "");
		}

		/// <summary>
		/// Maakt een PlayerView van een speler domeinobject
		/// </summary>
		/// <param name="player"></param>
		public PlayerView(Player player)
		{
			data = player;
		}

		#endregion

		/// <summary>
		/// Het speler domeinobject
		/// </summary>
		public override Player Domain
		{
			get { return data; }
		}

		#region Eigenschappen PlayerView

		/// <summary>
		/// Het ID van de speler
		/// </summary>
		public int Id
		{
			get { return data.PlayerID; }  
			set { data.PlayerID = value; }
		}

		/// <summary>
		/// De naam van de speler
		/// </summary>
		public string Name
		{
			get { return data.Name; }
			set { data.Name = value; }
		}
		
		/// <summary>
		/// Het geslacht van de speler
		/// </summary>
		public string Gender
		{
			get { return data.Gender; }
			set { data.Gender = value; }
		}

		/// <summary>
		/// De vereniging van de speler
		/// </summary>
		public string Club
		{
			get { return data.Club; }
			set { data.Club = value; }
		}

		/// <summary>
		/// De poulevoorkeuren van de speler
		/// </summary>
		public string Preferences
		{
			get { return data.Preferences; }
			set { data.Preferences = value; }
		}

		/// <summary>
		/// Een lijst van poulevoorkeuren van de speler
		/// </summary>
		public string[] PreferencesList
		{
			get
			{
				return Regex.Split(Preferences, ", ");
			}
			set 
			{
				string pref = value[0];
				for (int i = 1; i < value.Length; i++)
					pref += ", " + value[i];
			}
		}

		/// <summary>
		/// Maakt een poulevoorkeur string van een discipline en een niveau
		/// </summary>
		/// <param name="discipline"> De discipline van de poule </param>
		/// <param name="niveau"> Het niveau van de poule </param>
		/// <returns> Een string die de poulevoorkeur representeert</returns>
		public static string CreatePreference(string discipline, string niveau)
		{
			return discipline + ": " + niveau;
		}

		/// <summary>
		/// Maakt van een voorkeurenstring een lijst van voorkeurstrings
		/// </summary>
		/// <param name="preference"></param>
		/// <returns></returns>
		public static string[] SplitPreference(string preference)
		{
			return Regex.Split(preference, ": ");
		}

		/// <summary>
		/// De discipline van een voorkeur
		/// </summary>
		/// <param name="preference"></param>
		/// <returns></returns>
		public static string GetDiscipline(string preference)
		{
			return SplitPreference(preference)[0];
		}

		/// <summary>
		/// Het niveau van de voorkeur
		/// </summary>
		/// <param name="preference"></param>
		/// <returns></returns>
		public static string GetNiveau(string preference)
		{
			return SplitPreference(preference)[1];
		}

		/// <summary>
		/// Voeg een voorkeur toe aan een voorkeurenstring
		/// </summary>
		/// <param name="preferences"> Bestaande voorkeuren </param>
		/// <param name="newPref"> De nieuwe voorkeur</param>
		/// <returns></returns>
		private string addPreference(string preferences, string newPref)
		{
			if (preferences == "")
				return newPref;
			return preferences + ", " + newPref;
		}

		/// <summary>
		/// Voeg een voorkeur toe aan de poule voorkeuren van de speler
		/// </summary>
		/// <param name="newPref"></param>
		public void AddPreference(string newPref)
		{
			Preferences = addPreference(Preferences, newPref);
		}

		/// <summary>
		/// Verwijder een voorkeur uit de poule voorkeuren van de speler
		/// </summary>
		/// <param name="preference"></param>
		/// <returns></returns>
		public bool RemovePreference(string preference)
		{
			string[] prefList = PreferencesList;
			string newPrefs = "";
			bool removed = false;
			for (int i = 0; i < prefList.Length; i++)
			{
				if (prefList[i] != preference)
					newPrefs = addPreference(newPrefs, prefList[i]);
				else
					removed = true;
			}
			if (removed)
				Preferences = newPrefs;
			return removed;
		}

		/// <summary>
		/// Opmerkingen bij de speler
		/// </summary>
		public string Comment
		{
			get { return data.Comment; }
			set { data.Comment = value; }
		}

		#endregion

	}
}
