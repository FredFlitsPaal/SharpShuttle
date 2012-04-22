using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Poule-informatie van een speler
	/// </summary>
	public class PlayerPoulesView : PlayerView
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public PlayerPoulesView()
		{
			
		}

		/// <summary>
		/// Maakt een PlayerPoulesView van een speler domeinobject
		/// </summary>
		/// <param name="player"></param>
		public PlayerPoulesView(Player player) : base(player)
		{
			AssignedPoules = new PouleViews();
			PreferencesPoules = new PouleViews();
		}

		#endregion

		#region Eigenschappen PlayerPoulesView

		/// <summary>
		/// De poules die toegekend zijn aan de speler
		/// </summary>
		public PouleViews AssignedPoules 
		{
			get; set;
		}

		/// <summary>
		/// De voorkeurspoule van de speler
		/// </summary>
		public PouleViews PreferencesPoules 
		{
			get; set;
		}
		
#endregion

	}

}
