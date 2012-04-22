using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// View die een gebruiker beschrijft
	/// </summary>
	public class UserView : AbstractView <User>
	{
		#region Constructors

		/// <summary>
		/// Default constructor, maakt een "leeg" gebruiker domeinobject
		/// </summary>
		public UserView()
        {
            data = new User(0,"","");
        }

		/// <summary>
		/// Maakt een UserView van een gebruiker domeinobject
		/// </summary>
		/// <param name="user"></param>
		public UserView(User user)
        {
			data = user;
		}

		#endregion

		/// <summary>
		/// Het gebruiker domeinobject
		/// </summary>
		public override User Domain
    	{
			get { return data; }
		}

		#region Eigenschappen UserView

		/// <summary>
		/// Het ID van de user
		/// </summary>
		public long Id
        {
			get { return data.UserID; }
			set { data.UserID = value; }
        }

		/// <summary>
		/// De naam van de gebruiker
		/// </summary>
        public string Name
        {
            get { return data.Name; } 
			set { data.Name = value; }
        }

		/// <summary>
		/// Commentaar bij de gebruiker
		/// </summary>
		public string Info
		{
			get { return data.Info; }
			set { data.Info = value; }
		}

		/// <summary>
		/// De rollen van de gebruiker
		/// </summary>
		public User.Roles Role
		{
			get { return data.Role; }
		}

		#endregion

		/// <summary>
		/// Is het ID van de gebruiker gelijk aan dit ID
		/// </summary>
		/// <param name="n"> Het ID waarmee vergelijkt wordt</param>
		/// <returns></returns>
		public bool idEquals(int n)
        {
			return data.UserID == n;
        }


    }
}
