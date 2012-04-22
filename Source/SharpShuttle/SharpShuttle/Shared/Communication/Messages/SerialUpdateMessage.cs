using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Communication.Serials;

namespace Shared.Communication.Messages
{
	/// <summary>
	/// Message die gestuurd wordt als er een SerialNumber geupdate moet worden
	/// </summary>
	[Serializable]
	public class SerialUpdateMessage : Message
	{
		#region Fields

		/// <summary>
		/// Lijst met SeirualUpdates die doorgevoerd moeten worden op de client
		/// </summary>
		public List<SerialUpdate> Serials = new List<SerialUpdate>();

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een SerialUpdateMessage zonder lijst met SerialUpdates
		/// </summary>
		public SerialUpdateMessage()
			: base(MessageTypes.SERIALUPDATE)
		{ }

		/// <summary>
		/// Maakt een SerialUpdateMessage met een lijst met SerialUpdates
		/// </summary>
		/// <param name="updatelist">De lijst met SerialUpdates die doorgevoerd moeten worden op de client</param>
		public SerialUpdateMessage(IEnumerable<SerialUpdate> updatelist)
			: this()
		{
			Serials = updatelist.ToList();
		}

		#endregion
	}

	/// <summary>
	/// Definieert een nieuwe SerialDefinition en een SerialEventType die daartoe geleidt heeft
	/// </summary>
	[Serializable]
	public class SerialUpdate
	{
		#region Fields

		/// <summary>
		/// De nieuwe SerialDefinition
		/// </summary>
		public readonly SerialDefinition Serial;
		/// <summary>
		/// Het SerialEventType
		/// </summary>
		public readonly SerialEventTypes SerialEventType;

		#endregion

		#region Constructors

		/// <summary>
		/// Maakt een nieuwe SerialUpdate
		/// </summary>
		public SerialUpdate(SerialDefinition Serial, SerialEventTypes SerialEventType)
		{
			this.Serial = Serial;
			this.SerialEventType = SerialEventType;
		}

		#endregion
	}
}
