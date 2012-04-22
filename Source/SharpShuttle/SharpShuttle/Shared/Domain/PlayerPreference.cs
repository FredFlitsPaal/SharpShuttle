
namespace Shared.Domain
{
	/// <summary>
	/// Poulevoorkeur van een speler
	/// </summary>
	public class PlayerPreference : Abstract
	{
		/// <summary>
		/// Default constructor, geen voorkeur
		/// </summary>
		public PlayerPreference()
		{

		}

		/// <summary>
		/// Maakt een voorkeur voor een speler voor een discipline/niveau combinatie
		/// </summary>
		/// <param name="id"> ID van de speler </param>
		/// <param name="discipline"> De discipline </param>
		/// <param name="niveau"> Het niveau</param>
		public PlayerPreference(int id, Poule.Disciplines discipline, string niveau)
		{
			PlayerId = id;
			Discipline = discipline;
			Niveau = niveau;
		}

		/// <summary>
		/// Het ID van de speler
		/// </summary>
		public int PlayerId
		{
			get; set;
		}

		/// <summary>
		/// De discipline
		/// </summary>
		public Poule.Disciplines Discipline
		{
			get; set;
		}

		/// <summary>
		/// Het niveau
		/// </summary>
		public string Niveau
		{
			get; set;
		}
	}
}
