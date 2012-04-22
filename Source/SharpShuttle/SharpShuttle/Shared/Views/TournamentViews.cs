using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Een lijst van TournamentViews
	/// </summary>
	public class TournamentViews : AbstractViews<TournamentView, Tournament>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public TournamentViews ()
		{
			SetViews();
		}
	}
}
