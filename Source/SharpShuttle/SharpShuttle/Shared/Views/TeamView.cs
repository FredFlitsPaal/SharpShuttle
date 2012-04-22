using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Beschrijft een team
	/// </summary>
	public class TeamView : AbstractView <Team>
	{
		#region Constructors

		/// <summary>
		/// Maakt een TeamView met een "leeg" Team domeinobject
		/// </summary>
		public TeamView()
		{
			data = new Team("", null, null);
		}

		/// <summary>
		/// Maakt een TeamView met een Team domeinobject
		/// </summary>
		/// <param name="team"></param>
		public TeamView(Team team)
		{
			data = team;
		}

		#endregion

		/// <summary>
		/// Het Team domeinobject
		/// </summary>
		public override Team Domain
		{
			get { return data; }
		}

		#region Eigenschappen TeamView

		/// <summary>
		/// Het ID van het team 
		/// </summary>
		public int TeamId
		{
			get { return data.TeamID; }
			set { data.TeamID = value; }
		}

		/// <summary>
		/// De naam van het team
		/// </summary>
		public string Name
		{
			get { return data.Name; }
			set { data.Name = value; }
		}

		/// <summary>
		/// Speler 1 van het team
		/// </summary>
		public Player Player1
		{
			get { return data.Player1; }
			set { data.Player1 = value; }
		}

		/// <summary>
		/// Speler 2 van het team
		/// </summary>
		public Player Player2
		{
			get { return data.Player2; }
			set { data.Player2 = value; }
		}

		/// <summary>
		/// De naam van speler 1 van het team
		/// </summary>
		public string Player1Name
		{
			get { return data.Player1.Name; }
		}
	
		/// <summary>
		/// De naam van speler 2 van het team
		/// </summary>
		public string Player2Name
		{
			get
			{
				if (Player2 == null)
					return "";
				return data.Player2.Name;
			}
		}

		#endregion

	}
}