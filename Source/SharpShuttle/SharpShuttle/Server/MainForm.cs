using System;
using System.Windows.Forms;
using Server.Controllers;

namespace Server
{
	/// <summary>
	/// Het hoofdscherm van de server applicatie
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// De connection controller
		/// </summary>
		private ConnectionController connectioncontroller = new ConnectionController();
		/// <summary>
		/// De IP adres controller
		/// </summary>
		private IPAdressesController ipcontroller = new IPAdressesController();

		//note: port niet aanpasbaar omdat hij op de client niet invulbaar is
		// dat moet wel veranderen, wat als die port al in gebruik is?

		/// <summary>
		/// Default constructor
		/// </summary>
		public MainForm()
		{
			Communication.Communication.ServerStarted += Communication_ServerStarted;
			Communication.Communication.ServerStopped += Communication_ServerStopped;
			Communication.Communication.ClientConnected += Communication_ClientConnected;
			Communication.Communication.ClientDisconnected += Communication_ClientDisconnected;
			InitializeComponent();
			SetIPAddresses();
		}

		/// <summary>
		/// Vult de ipAddressesBox met de locale IP adressen
		/// </summary>
		void SetIPAddresses()
		{
			var iplist = ipcontroller.GetNamedLocalIPAddresses();
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			for (int i = 0; i < iplist.Count; i++)
			{
				sb.AppendFormat("{0,-15}\t({1})", iplist[i].Address, iplist[i].InterfaceName);
				if (i != iplist.Count - 1)
					sb.AppendLine();
			}

			ipAddressesBox.Text = sb.ToString();
		}

		/// <summary>
		/// Reactie op het disconnecten van een client
		/// </summary>
		/// <param name="ClientID">ID van de client</param>
		void Communication_ClientDisconnected(long ClientID)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(delegate(){Communication_ClientDisconnected(ClientID);}));
				return;
			}
			
			AddConnectionLog("Client " + ClientID + " verbroken");
		}

		/// <summary>
		/// Reactie op het connecten van een client
		/// </summary>
		/// <param name="ClientID">ID van de client</param>
		void Communication_ClientConnected(long ClientID)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(delegate() { Communication_ClientConnected(ClientID); }));
				return;
			}
			
			AddConnectionLog("Client " + ClientID + " verbonden");
		}

		/// <summary>
		/// Reactie op het stoppen van de messageloop van de server
		/// </summary>
		void Communication_ServerStopped()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(Communication_ServerStopped));
				return;
			}

			EnableGUI();
		}

		/// <summary>
		/// Reactie op het starten van de messageloop van de server
		/// </summary>
		void Communication_ServerStarted()
		{
			if (InvokeRequired){
				Invoke(new MethodInvoker(Communication_ServerStarted));
				return;
			}

			DisableGUI();
		}

		/// <summary>
		/// Voeg een message toe aan het log
		/// </summary>
		/// <param name="Message">De message om toe te voegen</param>
		public void AddConnectionLog(string Message)
		{
			if (InvokeRequired)
				Invoke(new MethodInvoker(() => AddConnectionLog(Message)));
			else
			{
				if (tbConnectionEvents.Text.Trim().Length == 0)
					tbConnectionEvents.Text = DateTime.Now.ToShortTimeString() + " - " + Message + "\r\n";
				else
					tbConnectionEvents.Text += DateTime.Now.ToShortTimeString() + " - " + Message + "\r\n";

				tbConnectionEvents.SelectionStart = tbConnectionEvents.Text.Length;
				tbConnectionEvents.ScrollToCaret();
			}
		}

		/// <summary>
		/// Reactie op het dubbelklikken van de notify-icon
		/// </summary>
		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			showWindowMenuItem.PerformClick();
		}

		/// <summary>
		/// Reactie op het closen van de form
		/// </summary>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Weet u zeker dat u de Sharp Shuttle Server volledig wilt afsluiten?", "Afsluiten", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				//terminate server functionality
				connectioncontroller.Stop();
				return;
			}

			e.Cancel = true;
		}

		/// <summary>
		/// Reactie op het klikken van Minimalizeren
		/// </summary>
		private void btnMinimize_Click(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
		}

		/// <summary>
		/// Reactie op het klikken van Show Window in het context menu
		/// </summary>
		private void showWindowMenuItem_Click(object sender, EventArgs e)
		{
			Show();
			Focus();
		}

		/// <summary>
		/// Reactie op het klikken van Close in het context menu
		/// </summary>
		private void closeMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Reactie op het klikken van de start knop
		/// </summary>
		private void btnStart_Click(object sender, EventArgs e)
		{
			if (tbFile.Text.Trim() == "")
			{
				MessageBox.Show("Er is geen locatie voor database opgegeven.\r\nGeef een locatie op voor de databaseserver", "Geen locatie opgegeven.", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			else if (!Database.Database.Instance.Open(tbFile.Text))
			{
				MessageBox.Show("Het toernooi kon niet geopend worden.\r\nRaadpleeg de Server Logboeken voor het probleem", "Fout in maken van toernooi database", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			else
			{
				int serverPort = int.Parse(tbPort.Text);
				if (connectioncontroller.Start(serverPort))
				{
					tbPort.Enabled = false;
					btnStart.Enabled = false;
				}
			}
		}

		/// <summary>
		/// Enable alle knopjes etc
		/// </summary>
		private void EnableGUI()
		{
			btnStop.Enabled = false;
			btnStart.Enabled = true;

			//Zorg ervoor dat men de database kan veranderen
			tournamentGroupBox.Enabled = true;

			lblActive.Text = "Nee";
			AddConnectionLog("Server gestopt");

			mnuNewTournament.Enabled = true;
			mnuOpenTournament.Enabled = true;
			mnuStartServer.Enabled = true;
			mnuStopServer.Enabled = false;
		}
		
		/// <summary>
		/// Disable alle knopjes (behalve stop)
		/// </summary>
		private void DisableGUI()
		{
			btnStop.Enabled = true;
			btnStart.Enabled = false;

			//Database mag niet meer worden veranderd
			tournamentGroupBox.Enabled = false;

			lblActive.Text = "Ja";
			AddConnectionLog("Server gestart");

			mnuNewTournament.Enabled = false;
			mnuOpenTournament.Enabled = false;
			mnuStartServer.Enabled = false;
			mnuStopServer.Enabled = true;
		}

		/// <summary>
		/// Reactie op het klikken van Stop
		/// </summary>
		private void btnStop_Click(object sender, EventArgs e)
		{
			connectioncontroller.Stop();
			tbPort.Enabled = true;
		}

		/// <summary>
		/// Reactie op het veranderen van de port, gooit niet-cijfers weg
		/// </summary>
		private void tbPort_TextChanged(object sender, EventArgs e)
		{
			int port;
			if (tbPort.Text.Length > 0 && 
				!int.TryParse(tbPort.Text, out port))
			{
				//verwijder niet-getallen
				tbPort.Text = System.Text.RegularExpressions.Regex.Replace(
					tbPort.Text, "\\D", "");
			}
		}

		/// <summary>
		/// Reactie op het stoppen van het editen van de port
		/// </summary>
		private void tbPort_Leave(object sender, EventArgs e)
		{
			if (tbPort.Text.Length == 0)
				tbPort.Text = "7015";
		}

		/// <summary>
		/// Reactie op het klikken op Browse
		/// </summary>
		private void btnBrowseFile_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
				tbFile.Text = openFileDialog.FileName;
		}

		/// <summary>
		/// Reactie op het resizen van het form, verstopt het form bij minimalizeren
		/// </summary>
		private void MainForm_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				Hide();
			}
		}

		/// <summary>
		/// Reactie op het klikken van Start
		/// </summary>
		private void mnuStartServer_Click(object sender, EventArgs e)
		{
			btnStart.PerformClick();
		}

		/// <summary>
		/// Reactie op het klikken van Stop
		/// </summary>
		private void mnuStopServer_Click(object sender, EventArgs e)
		{
			btnStop.PerformClick();
		}

		/// <summary>
		/// Reactie op het klikken van New Tournament
		/// </summary>
		private void mnuNewTournament_Click(object sender, EventArgs e)
		{
			btnNewTournament.PerformClick();
		}

		/// <summary>
		/// Reactie op het klikken van Open Tournament
		/// </summary>
		private void mnuOpenTournament_Click(object sender, EventArgs e)
		{
			btnBrowseFile.PerformClick();
		}

		/// <summary>
		/// Reactie op het klikken van Minimalize
		/// </summary>
		private void mnuMinimalize_Click(object sender, EventArgs e)
		{
			btnMinimize.PerformClick();
		}

		/// <summary>
		/// Reactie op het klikken van Quit
		/// </summary>
		private void mnuQuit_Click(object sender, EventArgs e)
		{
			btnQuit.PerformClick();
		}

		/// <summary>
		/// Reactie op het klikken van Quit
		/// </summary>
		private void btnQuit_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Reactie op het klikken van New Tournament
		/// </summary>
		private void btnNewTournament_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				tbFile.Text = saveFileDialog.FileName;
				if (!Database.Database.Instance.New(saveFileDialog.FileName))
					MessageBox.Show("Er kon geen nieuwe toernooi gemaakt worden.\r\nRaadpleeg de Server Logboeken voor het probleem","Fout in maken van toernooi database",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

	}
}
