
namespace Server.Controllers
{
	/// <summary>
	/// Beheert een verbinding en kan deze opstarten of stoppen
	/// </summary>
	public class ConnectionController
	{
		/// <summary>
		/// Start de server op de port <paramref name="ServerPort"/>
		/// </summary>
		/// <param name="ServerPort">De port om op te luisteren</param>
		public bool Start(int ServerPort)
		{
			return Communication.Communication.Start(ServerPort);
		}

		/// <summary>
		/// Stopt de server
		/// </summary>
		public void Stop()
		{
			Communication.Communication.Stop();
		}

		/// <summary>
		/// Stopt de server automatisch als de controller niet meer gebruikt wordt
		/// </summary>
		~ConnectionController()
		{
			Communication.Communication.Stop();
		}
	}
}
