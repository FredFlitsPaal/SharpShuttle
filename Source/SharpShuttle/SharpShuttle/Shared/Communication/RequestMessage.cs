using System;

namespace Shared.Communication
{
	/// <summary>
	/// Abstract class waarvan alle RequestMessages moeten inheriten
	/// </summary>
	[Serializable]
	public abstract class RequestMessage : Message
	{
		#region Properties

		/// <summary>
		/// ID van deze message
		/// </summary>
		public long MessageID
		{
			get;
			set;
		}
		/// <summary>
		/// RequestType van deze message
		/// </summary>
		public abstract RequestTypes RequestType { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructr
		/// </summary>
		public RequestMessage()
		{
			Type = MessageTypes.REQUEST;
		}

		#endregion
	}
}
