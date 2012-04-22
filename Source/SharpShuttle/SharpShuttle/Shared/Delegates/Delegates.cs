namespace Shared.Delegates
{
	/// <summary>
	/// De Delegates class heeft een aantal delegate methodes gedefinieerd die gebruikt worden
	/// om custom events te maken.
	/// </summary>
	public class Delegates
	{
		/// <summary>
		/// Delegate voor wanneer verbinding met de server is verbroken
		/// </summary>
		public delegate void ServerDisconnectedDelegate();
		/// <summary>
		/// Delegate voor wanneer verbinding met de server tot stand is gekomen
		/// </summary>
		public delegate void ServerConnectedDelegate();
		/// <summary>
		/// Delegate voor wanneer de server opgestart is
		/// </summary>
		public delegate void ServerStartedDelegate();
		/// <summary>
		/// Delegate voor wanneer de server gestopt is
		/// </summary>
		public delegate void ServerStoppedDelegate();
		/// <summary>
		/// Delegate voor wanneer een client met een ID verbindt met de server
		/// </summary>
		/// <param name="ClientID"> Het ID van de client </param>
		public delegate void ServerConnectionDelegate(long ClientID);
	}
}
