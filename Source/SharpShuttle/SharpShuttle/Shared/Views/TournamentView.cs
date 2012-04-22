using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// View die een toernooi beschrijft
	/// </summary>
	public class TournamentView : AbstractView <Tournament>
	{
		#region Constructors

		/// <summary>
		/// Default constructor, maakt een "leeg" Toernooi domeinobject
		/// </summary>
		public TournamentView()
		{
			data = new Tournament(0, "");
		}

		/// <summary>
		/// Maakt een TournamentView met een toernooi domeinobject
		/// </summary>
		/// <param name="tournament"></param>
		public TournamentView(Tournament tournament)
		{
			data = tournament;
		}

		#endregion

		/// <summary>
		/// Het toernooi domeinobject
		/// </summary>
		public override Tournament Domain
		{
			get { return data; }
		}

		#region Eigenschappen TournamentView

		/// <summary>
		/// Het ID van het toernooi
		/// </summary>
		public int Id
		{
			get { return data.Id; }
			set { data.Id = value; }
		}

		/// <summary>
		/// De naam van het toernooi
		/// </summary>
		public string Name
		{
			get { return data.Name; }
			set { data.Name = value; }
		}

		#endregion
	}
}
