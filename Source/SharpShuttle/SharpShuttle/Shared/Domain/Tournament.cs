
namespace Shared.Domain
{
	/// <summary>
	/// Een toernooi
	/// </summary>
	public class Tournament : Abstract
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public Tournament()
		{
			Id = 0;
			Name = "";
		}

		/// <summary>
		/// Volledige constructor
		/// </summary>
		/// <param name="id"> Het toernooi ID </param>
		/// <param name="name"> Naam van het toernooi</param>
		public Tournament(int id, string name)
		{
			Id = id;
			Name = name;
		}
		
		/// <summary>
		/// Het toernooi ID
		/// </summary>
		public int Id
		{
			get; set;
		}

		/// <summary>
		/// Naam van het toernooi
		/// </summary>
		public string Name
		{
			get; set;
		}


	}
}
