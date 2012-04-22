using System;
using Shared.Domain;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor SetPouleTeams
	/// </summary>
	[Serializable]
	public class RequestSetPouleTeams : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule waarvan de Teams geset worden
		/// </summary>
		public readonly int PouleID;
		/// <summary>
		/// De nieuwe Teams voor de Poule
		/// </summary>
		public readonly ChangeTrackingList<Team> Teams;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor SetPouleTeams
		/// </summary>
		/// <param name="PouleID">ID van de Poule waarvan de Teams geset worden</param>
		/// <param name="Teams">De nieuwe Teams voor de Poule</param>
		public RequestSetPouleTeams(int PouleID, ChangeTrackingList<Team> Teams)
		{
			this.PouleID = PouleID;
			this.Teams = Teams;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.SetPouleTeams; }
		}
	}
}
