namespace Server
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.notifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.showWindowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.ServerGroupBox = new System.Windows.Forms.GroupBox();
			this.tbConnectionEvents = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.lblActive = new System.Windows.Forms.Label();
			this.ttlActive = new System.Windows.Forms.Label();
			this.tournamentGroupBox = new System.Windows.Forms.GroupBox();
			this.btnNewTournament = new System.Windows.Forms.Button();
			this.btnBrowseFile = new System.Windows.Forms.Button();
			this.tbFile = new System.Windows.Forms.TextBox();
			this.ttlFile = new System.Windows.Forms.Label();
			this.tabNetwork = new System.Windows.Forms.TabPage();
			this.nwInfoGroupBox = new System.Windows.Forms.GroupBox();
			this.ipAddressesBox = new System.Windows.Forms.TextBox();
			this.tbPort = new System.Windows.Forms.TextBox();
			this.ttlPort = new System.Windows.Forms.Label();
			this.ttlIpAddress = new System.Windows.Forms.Label();
			this.btnMinimize = new System.Windows.Forms.Button();
			this.btnQuit = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.mnuMenu = new System.Windows.Forms.MenuStrip();
			this.bestandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuNewTournament = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpenTournament = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMinimalize = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuStartServer = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuStopServer = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.notifyIconContextMenu.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.ServerGroupBox.SuspendLayout();
			this.tournamentGroupBox.SuspendLayout();
			this.tabNetwork.SuspendLayout();
			this.nwInfoGroupBox.SuspendLayout();
			this.mnuMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.notifyIconContextMenu;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Sharp Shuttle Server";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// notifyIconContextMenu
			// 
			this.notifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showWindowMenuItem,
            this.toolStripMenuItem2,
            this.closeMenuItem});
			this.notifyIconContextMenu.Name = "notifyIconContextMenu";
			this.notifyIconContextMenu.Size = new System.Drawing.Size(145, 54);
			// 
			// showWindowMenuItem
			// 
			this.showWindowMenuItem.Name = "showWindowMenuItem";
			this.showWindowMenuItem.Size = new System.Drawing.Size(144, 22);
			this.showWindowMenuItem.Text = "Toon Venster";
			this.showWindowMenuItem.Click += new System.EventHandler(this.showWindowMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(141, 6);
			// 
			// closeMenuItem
			// 
			this.closeMenuItem.Name = "closeMenuItem";
			this.closeMenuItem.Size = new System.Drawing.Size(144, 22);
			this.closeMenuItem.Text = "Afsluiten";
			this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabGeneral);
			this.tabControl1.Controls.Add(this.tabNetwork);
			this.tabControl1.Location = new System.Drawing.Point(12, 32);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(484, 376);
			this.tabControl1.TabIndex = 0;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.ServerGroupBox);
			this.tabGeneral.Controls.Add(this.tournamentGroupBox);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tabGeneral.Size = new System.Drawing.Size(476, 350);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "Algemeen";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// ServerGroupBox
			// 
			this.ServerGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ServerGroupBox.Controls.Add(this.tbConnectionEvents);
			this.ServerGroupBox.Controls.Add(this.label1);
			this.ServerGroupBox.Controls.Add(this.btnStop);
			this.ServerGroupBox.Controls.Add(this.btnStart);
			this.ServerGroupBox.Controls.Add(this.lblActive);
			this.ServerGroupBox.Controls.Add(this.ttlActive);
			this.ServerGroupBox.Location = new System.Drawing.Point(7, 130);
			this.ServerGroupBox.Name = "ServerGroupBox";
			this.ServerGroupBox.Size = new System.Drawing.Size(463, 206);
			this.ServerGroupBox.TabIndex = 1;
			this.ServerGroupBox.TabStop = false;
			this.ServerGroupBox.Text = "Server";
			// 
			// tbConnectionEvents
			// 
			this.tbConnectionEvents.Location = new System.Drawing.Point(8, 80);
			this.tbConnectionEvents.Multiline = true;
			this.tbConnectionEvents.Name = "tbConnectionEvents";
			this.tbConnectionEvents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbConnectionEvents.Size = new System.Drawing.Size(448, 120);
			this.tbConnectionEvents.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Gebeurtenissen:";
			// 
			// btnStop
			// 
			this.btnStop.Enabled = false;
			this.btnStop.Location = new System.Drawing.Point(89, 35);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 4;
			this.btnStop.Text = "Stoppen";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(8, 35);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 2;
			this.btnStart.Text = "Starten";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// lblActive
			// 
			this.lblActive.AutoSize = true;
			this.lblActive.Location = new System.Drawing.Point(49, 18);
			this.lblActive.Name = "lblActive";
			this.lblActive.Size = new System.Drawing.Size(27, 13);
			this.lblActive.TabIndex = 1;
			this.lblActive.Text = "Nee";
			// 
			// ttlActive
			// 
			this.ttlActive.AutoSize = true;
			this.ttlActive.Location = new System.Drawing.Point(6, 18);
			this.ttlActive.Name = "ttlActive";
			this.ttlActive.Size = new System.Drawing.Size(37, 13);
			this.ttlActive.TabIndex = 0;
			this.ttlActive.Text = "Actief:";
			// 
			// tournamentGroupBox
			// 
			this.tournamentGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tournamentGroupBox.Controls.Add(this.btnNewTournament);
			this.tournamentGroupBox.Controls.Add(this.btnBrowseFile);
			this.tournamentGroupBox.Controls.Add(this.tbFile);
			this.tournamentGroupBox.Controls.Add(this.ttlFile);
			this.tournamentGroupBox.Location = new System.Drawing.Point(7, 7);
			this.tournamentGroupBox.Name = "tournamentGroupBox";
			this.tournamentGroupBox.Size = new System.Drawing.Size(463, 117);
			this.tournamentGroupBox.TabIndex = 0;
			this.tournamentGroupBox.TabStop = false;
			this.tournamentGroupBox.Text = "Toernooi";
			// 
			// btnNewTournament
			// 
			this.btnNewTournament.Location = new System.Drawing.Point(8, 72);
			this.btnNewTournament.Name = "btnNewTournament";
			this.btnNewTournament.Size = new System.Drawing.Size(75, 23);
			this.btnNewTournament.TabIndex = 3;
			this.btnNewTournament.Text = "Nieuw";
			this.btnNewTournament.UseVisualStyleBackColor = true;
			this.btnNewTournament.Click += new System.EventHandler(this.btnNewTournament_Click);
			// 
			// btnBrowseFile
			// 
			this.btnBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseFile.Location = new System.Drawing.Point(89, 72);
			this.btnBrowseFile.Name = "btnBrowseFile";
			this.btnBrowseFile.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseFile.TabIndex = 2;
			this.btnBrowseFile.Text = "Openen...";
			this.btnBrowseFile.UseVisualStyleBackColor = true;
			this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
			// 
			// tbFile
			// 
			this.tbFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbFile.Location = new System.Drawing.Point(8, 40);
			this.tbFile.Name = "tbFile";
			this.tbFile.ReadOnly = true;
			this.tbFile.Size = new System.Drawing.Size(448, 20);
			this.tbFile.TabIndex = 1;
			// 
			// ttlFile
			// 
			this.ttlFile.AutoSize = true;
			this.ttlFile.Location = new System.Drawing.Point(8, 24);
			this.ttlFile.Name = "ttlFile";
			this.ttlFile.Size = new System.Drawing.Size(49, 13);
			this.ttlFile.TabIndex = 0;
			this.ttlFile.Text = "Bestand:";
			// 
			// tabNetwork
			// 
			this.tabNetwork.Controls.Add(this.nwInfoGroupBox);
			this.tabNetwork.Location = new System.Drawing.Point(4, 22);
			this.tabNetwork.Name = "tabNetwork";
			this.tabNetwork.Padding = new System.Windows.Forms.Padding(3);
			this.tabNetwork.Size = new System.Drawing.Size(476, 350);
			this.tabNetwork.TabIndex = 1;
			this.tabNetwork.Text = "Netwerkinformatie";
			this.tabNetwork.UseVisualStyleBackColor = true;
			// 
			// nwInfoGroupBox
			// 
			this.nwInfoGroupBox.Controls.Add(this.ipAddressesBox);
			this.nwInfoGroupBox.Controls.Add(this.tbPort);
			this.nwInfoGroupBox.Controls.Add(this.ttlPort);
			this.nwInfoGroupBox.Controls.Add(this.ttlIpAddress);
			this.nwInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.nwInfoGroupBox.Location = new System.Drawing.Point(3, 3);
			this.nwInfoGroupBox.Name = "nwInfoGroupBox";
			this.nwInfoGroupBox.Size = new System.Drawing.Size(470, 341);
			this.nwInfoGroupBox.TabIndex = 0;
			this.nwInfoGroupBox.TabStop = false;
			this.nwInfoGroupBox.Text = "Informatie";
			// 
			// ipAddressesBox
			// 
			this.ipAddressesBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ipAddressesBox.Location = new System.Drawing.Point(3, 56);
			this.ipAddressesBox.Multiline = true;
			this.ipAddressesBox.Name = "ipAddressesBox";
			this.ipAddressesBox.ReadOnly = true;
			this.ipAddressesBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ipAddressesBox.Size = new System.Drawing.Size(464, 282);
			this.ipAddressesBox.TabIndex = 4;
			// 
			// tbPort
			// 
			this.tbPort.Location = new System.Drawing.Point(82, 14);
			this.tbPort.Name = "tbPort";
			this.tbPort.ReadOnly = true;
			this.tbPort.Size = new System.Drawing.Size(43, 20);
			this.tbPort.TabIndex = 3;
			this.tbPort.Text = "7015";
			this.tbPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tbPort.TextChanged += new System.EventHandler(this.tbPort_TextChanged);
			this.tbPort.Leave += new System.EventHandler(this.tbPort_Leave);
			// 
			// ttlPort
			// 
			this.ttlPort.AutoSize = true;
			this.ttlPort.Location = new System.Drawing.Point(6, 17);
			this.ttlPort.Name = "ttlPort";
			this.ttlPort.Size = new System.Drawing.Size(35, 13);
			this.ttlPort.TabIndex = 2;
			this.ttlPort.Text = "Poort:";
			// 
			// ttlIpAddress
			// 
			this.ttlIpAddress.AutoSize = true;
			this.ttlIpAddress.Location = new System.Drawing.Point(6, 35);
			this.ttlIpAddress.Name = "ttlIpAddress";
			this.ttlIpAddress.Size = new System.Drawing.Size(67, 13);
			this.ttlIpAddress.TabIndex = 0;
			this.ttlIpAddress.Text = "IP Adressen:";
			// 
			// btnMinimize
			// 
			this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMinimize.Location = new System.Drawing.Point(310, 418);
			this.btnMinimize.Name = "btnMinimize";
			this.btnMinimize.Size = new System.Drawing.Size(90, 23);
			this.btnMinimize.TabIndex = 2;
			this.btnMinimize.Text = "Minimaliseren";
			this.btnMinimize.UseVisualStyleBackColor = true;
			this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
			// 
			// btnQuit
			// 
			this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnQuit.Location = new System.Drawing.Point(406, 418);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(90, 23);
			this.btnQuit.TabIndex = 3;
			this.btnQuit.Text = "Afsluiten";
			this.btnQuit.UseVisualStyleBackColor = true;
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.CheckFileExists = false;
			this.openFileDialog.DefaultExt = "xml";
			this.openFileDialog.FileName = "ToernooiDatabase";
			this.openFileDialog.Filter = "Sharp Shuttle XML Bestanden|*.xml|Alle Bestanden|*.*";
			this.openFileDialog.Title = "Selecteer een bestaand toernooi…";
			// 
			// mnuMenu
			// 
			this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bestandToolStripMenuItem,
            this.serverToolStripMenuItem});
			this.mnuMenu.Location = new System.Drawing.Point(0, 0);
			this.mnuMenu.Name = "mnuMenu";
			this.mnuMenu.Size = new System.Drawing.Size(511, 24);
			this.mnuMenu.TabIndex = 4;
			this.mnuMenu.Text = "menuStrip1";
			// 
			// bestandToolStripMenuItem
			// 
			this.bestandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewTournament,
            this.mnuOpenTournament,
            this.toolStripMenuItem1,
            this.mnuMinimalize,
            this.mnuQuit});
			this.bestandToolStripMenuItem.Name = "bestandToolStripMenuItem";
			this.bestandToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.bestandToolStripMenuItem.Text = "Bestand";
			// 
			// mnuNewTournament
			// 
			this.mnuNewTournament.Name = "mnuNewTournament";
			this.mnuNewTournament.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.mnuNewTournament.Size = new System.Drawing.Size(168, 22);
			this.mnuNewTournament.Text = "Nieuw";
			this.mnuNewTournament.Click += new System.EventHandler(this.mnuNewTournament_Click);
			// 
			// mnuOpenTournament
			// 
			this.mnuOpenTournament.Name = "mnuOpenTournament";
			this.mnuOpenTournament.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mnuOpenTournament.Size = new System.Drawing.Size(168, 22);
			this.mnuOpenTournament.Text = "Openen…";
			this.mnuOpenTournament.Click += new System.EventHandler(this.mnuOpenTournament_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(165, 6);
			// 
			// mnuMinimalize
			// 
			this.mnuMinimalize.Name = "mnuMinimalize";
			this.mnuMinimalize.Size = new System.Drawing.Size(168, 22);
			this.mnuMinimalize.Text = "Minimaliseren";
			this.mnuMinimalize.Click += new System.EventHandler(this.mnuMinimalize_Click);
			// 
			// mnuQuit
			// 
			this.mnuQuit.Name = "mnuQuit";
			this.mnuQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.mnuQuit.Size = new System.Drawing.Size(168, 22);
			this.mnuQuit.Text = "Afsluiten";
			this.mnuQuit.Click += new System.EventHandler(this.mnuQuit_Click);
			// 
			// serverToolStripMenuItem
			// 
			this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStartServer,
            this.mnuStopServer});
			this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
			this.serverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			this.serverToolStripMenuItem.Text = "Server";
			// 
			// mnuStartServer
			// 
			this.mnuStartServer.Name = "mnuStartServer";
			this.mnuStartServer.Size = new System.Drawing.Size(152, 22);
			this.mnuStartServer.Text = "Starten";
			this.mnuStartServer.Click += new System.EventHandler(this.mnuStartServer_Click);
			// 
			// mnuStopServer
			// 
			this.mnuStopServer.Enabled = false;
			this.mnuStopServer.Name = "mnuStopServer";
			this.mnuStopServer.Size = new System.Drawing.Size(152, 22);
			this.mnuStopServer.Text = "Stoppen";
			this.mnuStopServer.Click += new System.EventHandler(this.mnuStopServer_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "xml";
			this.saveFileDialog.FileName = "ToernooiDatabase";
			this.saveFileDialog.Filter = "Sharp Shuttle XML Bestanden|*.xml|Alle Bestanden|*.*";
			this.saveFileDialog.Title = "Selecteer een bestand om een nieuwe toernooi te maken…";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(511, 449);
			this.Controls.Add(this.mnuMenu);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnQuit);
			this.Controls.Add(this.btnMinimize);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mnuMenu;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sharp Shuttle Server";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.notifyIconContextMenu.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.ServerGroupBox.ResumeLayout(false);
			this.ServerGroupBox.PerformLayout();
			this.tournamentGroupBox.ResumeLayout(false);
			this.tournamentGroupBox.PerformLayout();
			this.tabNetwork.ResumeLayout(false);
			this.nwInfoGroupBox.ResumeLayout(false);
			this.nwInfoGroupBox.PerformLayout();
			this.mnuMenu.ResumeLayout(false);
			this.mnuMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabNetwork;
		private System.Windows.Forms.Button btnMinimize;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.ContextMenuStrip notifyIconContextMenu;
		private System.Windows.Forms.ToolStripMenuItem showWindowMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
		private System.Windows.Forms.GroupBox tournamentGroupBox;
		private System.Windows.Forms.Label ttlFile;
		private System.Windows.Forms.GroupBox ServerGroupBox;
		private System.Windows.Forms.Button btnBrowseFile;
		private System.Windows.Forms.TextBox tbFile;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Label lblActive;
		private System.Windows.Forms.Label ttlActive;
		private System.Windows.Forms.GroupBox nwInfoGroupBox;
		private System.Windows.Forms.TextBox tbPort;
		private System.Windows.Forms.Label ttlPort;
		private System.Windows.Forms.Label ttlIpAddress;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbConnectionEvents;
		private System.Windows.Forms.TextBox ipAddressesBox;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button btnNewTournament;
		private System.Windows.Forms.MenuStrip mnuMenu;
		private System.Windows.Forms.ToolStripMenuItem bestandToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuQuit;
		private System.Windows.Forms.ToolStripMenuItem mnuOpenTournament;
		private System.Windows.Forms.ToolStripMenuItem mnuNewTournament;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuStartServer;
		private System.Windows.Forms.ToolStripMenuItem mnuStopServer;
		private System.Windows.Forms.ToolStripMenuItem mnuMinimalize;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
	}
}