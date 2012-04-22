using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Communication;
using Server.Database;
using Shared.Communication.Exceptions;
using Shared.Communication.Messages.Requests;
using Shared.Communication.Messages.Responses;
using Shared.Communication.Serials;
using System.Windows.Forms;

namespace Server
{

	/// <summary>
	/// Test klasse waarin post start dingen uitgevoerd kunnen worden
	/// </summary>
	public class Debug
	{

		/// <summary>
		/// Initialiseer een nieuwe instantiets van de <see cref="Debug"/> class.
		/// </summary>
		public Debug() { }


		/// <summary>
		/// Start het debug process
		/// </summary>
		public void StartDebug()
		{
			if (!Database.Database.Instance.Open(@Application.StartupPath + @"\Database\SharpShuttleTemplate.xml"))
			{
				MessageBox.Show("Database niet geopend");
				return;
			}

			//var poule = Database.Database.Instance.GetPoule(0);
			//Database.Database.Instance.SetPouleTeamInOperative(ref poule, 17); 

			return;



			//System.Windows.Forms.MessageBox.Show("Starting Server Debug");

			//if (!Server.Database.Database.Instance.Open(@Application.StartupPath + @"\Database\SharpShuttleTemplate.xml"))
			//{
			//    System.Windows.Forms.MessageBox.Show("Database niet geopend");
			//    return;
			//}

			//if (!Server.Communication.Communication.Start(7015))
			//    System.Windows.Forms.MessageBox.Show("Server niet gestart");

			//System.Windows.Forms.MessageBox.Show("Sever Started");

			////RequestHandler.GetAllHistoryMatches(0, new RequestGetAllHistoryMatches());

			//Application.Run();
		}
	}
}
