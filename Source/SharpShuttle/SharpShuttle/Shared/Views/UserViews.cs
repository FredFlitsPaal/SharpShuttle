using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Views
{

	/// <summary>
	/// Een lijst van UserViews
	/// </summary>
    public class UserViews : AbstractViews<UserView, User>
    {
		/// <summary>
		/// Default constructor
		/// </summary>
		public UserViews()
		{
			SetViews();
		}

		/// <summary>
		/// Maakt een UserViews met een list van User domeinobjecten
		/// </summary>
		/// <param name="users"></param>
		public UserViews(IList<User> users)
		{
			SetDomainList(users);

			foreach (User u in users)
				viewList.Add(new UserView(u));
		}

		/// <summary>
		/// Haalt de userview op met een gegeven id
		/// </summary>
		/// <param name="n"> Het ID van de userview </param>
		/// <returns> De bijbehorende userview</returns>
    	public UserView getUserViewId(int n)
        {
            foreach (UserView uv in this)
            {
				if (uv.idEquals(n))
					return uv;
            }

			return null;
        }
		
    }

}
