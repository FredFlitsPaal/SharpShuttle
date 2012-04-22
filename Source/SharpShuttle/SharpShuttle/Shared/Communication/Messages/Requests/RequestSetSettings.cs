using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Domain;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor SetSettings
	/// </summary>
	[Serializable]
	public class RequestSetSettings : RequestMessage
	{
		#region Fields

		/// <summary>
		/// De nieuwe TournamentSettings
		/// </summary>
		public readonly TournamentSettings Settings;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor SetSettings
		/// </summary>
		/// <param name="Settings">De nieuwe TournamentSettings</param>
		public RequestSetSettings(TournamentSettings Settings)
		{
			this.Settings = Settings;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.SetSettings; }
		}
	}
}
