using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Een lijst van PouleViews
	/// </summary>
	public class PouleViews : AbstractViews<PouleView, Poule>
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public PouleViews ()
		{
			SetViews();
		}

		/// <summary>
		/// Maakt een PouleViews van een lijst van Poule domeinobjecten
		/// </summary>
		/// <param name="poules"></param>
		public PouleViews(ICollection<Poule> poules)
		{
			SetDomainList(poules);

			foreach (Poule p in poules)
				viewList.Add(new PouleView(p));
		}

		/// <summary>
		/// Haalt een poule op met een gegeven poule ID
		/// </summary>
		/// <param name="pouleID"> het poule ID</param>
		/// <returns> De poule met het poule ID</returns>
		public PouleView getPoule(int pouleID)
		{
			foreach (PouleView poule in this)
				if (poule.Id == pouleID)
					return poule;
			return null;
		}

		/// <summary>
		/// Kijkt of de poule met het gegeven pouleID bestaat
		/// </summary>
		/// <param name="pouleID"> Het poule ID</param>
		/// <returns></returns>
		public bool isPoule(int pouleID)
		{
			foreach (PouleView poule in this)
				if (poule.Id == pouleID)
					return true;
			return false;
		}

		/// <summary>
		/// Kijkt of er een poule met de opgegeven discipline en niveau bestaat
		/// </summary>
		/// <param name="discipline"></param>
		/// <param name="niveau"></param>
		/// <returns></returns>
		public bool ContainsPoule(string discipline, string niveau)
		{
			foreach (PouleView poule in this)
				if (poule.Discipline == discipline && poule.Niveau == niveau)
					return true;
			return false;
		}

	}

}
