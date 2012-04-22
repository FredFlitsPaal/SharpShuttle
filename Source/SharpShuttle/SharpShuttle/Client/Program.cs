using System;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			//Even wat anders proberen
			//Debug debug = new Debug();
			//debug.StartDebug();
			//Application.Exit();

			//MessageBox.Show("Client gaat nu verbinding maken met SharpShuttle server op: 127.0.0.1(localhost)");

			//if (Shared.Communication.Communication.Connect("127.0.0.1") == false)
			//{
			//    MessageBox.Show("Verbinding maken is mislukt!");
			//    Application.Exit();
			//    return;
			//}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainform = new MainForm();
            Application.Run(mainform);
        }
    }
}
