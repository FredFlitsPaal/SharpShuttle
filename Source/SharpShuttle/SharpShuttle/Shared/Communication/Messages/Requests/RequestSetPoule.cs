using System;
using Shared.Domain;

namespace Shared.Communication.Messages.Requests
{
	/// <summary>
	/// Request voor SetPoule
	/// </summary>
	[Serializable]
	public class RequestSetPoule : RequestMessage
	{
		#region Fields

		/// <summary>
		/// ID van de Poule die geset wordt
		/// </summary>
		public readonly int PouleID;
		/// <summary>
		/// De nieuwe Poule
		/// </summary>
		public readonly Poule Poule;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een Request voor SetPoule
		/// </summary>
		/// <param name="PouleID">ID van de Poule die geset wordt</param>
		/// <param name="Poule">De nieuwe Poule</param>
		public RequestSetPoule(int PouleID, Poule Poule)
		{
			this.PouleID = PouleID;
			this.Poule = Poule;
		}

		#endregion

		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public override RequestTypes RequestType
		{
			get { return RequestTypes.SetPoule; }
		}
	}
}
