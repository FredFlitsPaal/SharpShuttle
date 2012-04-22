using Client.Forms.PopUpWindows;
using Shared.Communication.Exceptions;
using Shared.Domain;
using Shared.Views;
using Shared.Data;
using System.Collections.Generic;

namespace Client.Controls
{

	/// <summary>
	/// Business logica voor het kiezen van een gebruikerstype
	/// </summary>
	class SelectUserControl
	{
		/// <summary>
		/// De datacache
		/// </summary>
		private IDataCache cache;

		/// <summary>
		/// Lijst van mogelijke gebruikers
		/// </summary>
		private List<User> users;

		/// <summary>
		/// Default constructor
		/// </summary>
		public SelectUserControl()
		{
			cache = DataCache.Instance;
		}

		/// <summary>
		/// Haalt alles users van de server op
		/// </summary>
		/// <returns></returns>
		public UserViews GetUsers()
		{
			users = new List<User>();
			
			try
			{
				users = cache.GetAllUsers();
			}

			catch (CommunicationException exc)
			{
				CatchCommunicationExceptions.Show(exc);
			}

			UserViews result = new UserViews(users);

			return result;
		}

		/// <summary>
		/// Geeft de UserView terug van een gegeven userid
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public UserView GetUser(int id)
		{
			foreach (User user in users)
			{
				if (user.UserID == id)
					return new UserView(user);
			}

			return null;
		}

		/// <summary>
		/// Geeft een boolean terug of er een user betaat met het gegeven userid
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool UserExists(int id)
		{
			foreach (User user in users)
			{
				if (user.UserID == id)
					return true;
			}

			return false;
		}
	}
}
