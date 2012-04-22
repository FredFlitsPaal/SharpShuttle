using System;
using System.Windows.Forms;

namespace Server
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			//A.U.B. laten staan in verband met optimale debugging.
			//Standaard database: Server.Database.SharpShuttleTemplate.xml heeft als build actie copy if newer,
			//Lokale testdata dus aanpassen in SharpShuttle/Server/bin/Debug/Database/SharpShuttleTemplate.xml
			//Debug debug = new Debug();
			//debug.StartDebug();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
