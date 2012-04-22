using WeifenLuo.WinFormsUI.Docking;

namespace Client
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
			WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
			WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.fileLoginMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.filePrintMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filePrintAllMatchPapersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filePrintFullPageMatchPapersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.editInitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editAddPouleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editManagePlayersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.viewStatusBarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.windowsScoreInputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.windowsPouleInformationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.windowsCourtInformationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsAmountFieldsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.printerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.sharpShuttleHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutSharpShuttleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.fileLogoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.windowsMenu,
            this.settingsMenu,
            this.helpMenu});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.MdiWindowListItem = this.windowsMenu;
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(632, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "MenuStrip";
			// 
			// fileMenu
			// 
			this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileLoginMenuItem,
            this.fileLogoutMenuItem,
            this.fileSeparator1,
            this.filePrintMenuItem,
            this.fileSeparator2,
            this.fileExitMenuItem});
			this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
			this.fileMenu.Name = "fileMenu";
			this.fileMenu.Size = new System.Drawing.Size(61, 20);
			this.fileMenu.Text = "&Bestand";
			// 
			// fileLoginMenuItem
			// 
			this.fileLoginMenuItem.Name = "fileLoginMenuItem";
			this.fileLoginMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.fileLoginMenuItem.Size = new System.Drawing.Size(163, 22);
			this.fileLoginMenuItem.Text = "Log&in";
			this.fileLoginMenuItem.Click += new System.EventHandler(this.fileLoginMenuItem_Click);
			// 
			// fileSeparator1
			// 
			this.fileSeparator1.Name = "fileSeparator1";
			this.fileSeparator1.Size = new System.Drawing.Size(160, 6);
			// 
			// filePrintMenuItem
			// 
			this.filePrintMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filePrintAllMatchPapersMenuItem,
            this.filePrintFullPageMatchPapersMenuItem});
			this.filePrintMenuItem.Enabled = false;
			this.filePrintMenuItem.Name = "filePrintMenuItem";
			this.filePrintMenuItem.Size = new System.Drawing.Size(163, 22);
			this.filePrintMenuItem.Text = "&Print...";
			// 
			// filePrintAllMatchPapersMenuItem
			// 
			this.filePrintAllMatchPapersMenuItem.Name = "filePrintAllMatchPapersMenuItem";
			this.filePrintAllMatchPapersMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.filePrintAllMatchPapersMenuItem.Size = new System.Drawing.Size(226, 22);
			this.filePrintAllMatchPapersMenuItem.Text = "&Alle Wedstrijdbriefjes";
			this.filePrintAllMatchPapersMenuItem.Click += new System.EventHandler(this.allMatchPapersMenuItem_Click);
			// 
			// filePrintFullPageMatchPapersMenuItem
			// 
			this.filePrintFullPageMatchPapersMenuItem.Name = "filePrintFullPageMatchPapersMenuItem";
			this.filePrintFullPageMatchPapersMenuItem.Size = new System.Drawing.Size(226, 22);
			this.filePrintFullPageMatchPapersMenuItem.Text = "&Volle Pagina\'s";
			this.filePrintFullPageMatchPapersMenuItem.Click += new System.EventHandler(this.fullPagesMenuItem_Click);
			// 
			// fileSeparator2
			// 
			this.fileSeparator2.Name = "fileSeparator2";
			this.fileSeparator2.Size = new System.Drawing.Size(160, 6);
			// 
			// fileExitMenuItem
			// 
			this.fileExitMenuItem.Name = "fileExitMenuItem";
			this.fileExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.fileExitMenuItem.Size = new System.Drawing.Size(163, 22);
			this.fileExitMenuItem.Text = "&Afsluiten";
			this.fileExitMenuItem.Click += new System.EventHandler(this.fileExitMenuItem_Click);
			// 
			// editMenu
			// 
			this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editInitMenuItem,
            this.editAddPouleMenuItem,
            this.editManagePlayersMenuItem});
			this.editMenu.Name = "editMenu";
			this.editMenu.Size = new System.Drawing.Size(70, 20);
			this.editMenu.Text = "Be&werken";
			// 
			// editInitMenuItem
			// 
			this.editInitMenuItem.Enabled = false;
			this.editInitMenuItem.Name = "editInitMenuItem";
			this.editInitMenuItem.Size = new System.Drawing.Size(185, 22);
			this.editInitMenuItem.Text = "&Toernooi Initialiseren";
			this.editInitMenuItem.Click += new System.EventHandler(this.editInitMenuItem_Click);
			// 
			// editAddPouleMenuItem
			// 
			this.editAddPouleMenuItem.Enabled = false;
			this.editAddPouleMenuItem.Name = "editAddPouleMenuItem";
			this.editAddPouleMenuItem.Size = new System.Drawing.Size(185, 22);
			this.editAddPouleMenuItem.Text = "&Poule Toevoegen";
			this.editAddPouleMenuItem.Click += new System.EventHandler(this.editAddPouleMenuItem_Click);
			// 
			// editManagePlayersMenuItem
			// 
			this.editManagePlayersMenuItem.Enabled = false;
			this.editManagePlayersMenuItem.Name = "editManagePlayersMenuItem";
			this.editManagePlayersMenuItem.Size = new System.Drawing.Size(185, 22);
			this.editManagePlayersMenuItem.Text = "&Spelers Beheren";
			this.editManagePlayersMenuItem.Click += new System.EventHandler(this.editManagePlayersMenuItem_Click);
			// 
			// viewMenu
			// 
			this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewStatusBarMenuItem});
			this.viewMenu.Name = "viewMenu";
			this.viewMenu.Size = new System.Drawing.Size(48, 20);
			this.viewMenu.Text = "B&eeld";
			// 
			// viewStatusBarMenuItem
			// 
			this.viewStatusBarMenuItem.Checked = true;
			this.viewStatusBarMenuItem.CheckOnClick = true;
			this.viewStatusBarMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.viewStatusBarMenuItem.Name = "viewStatusBarMenuItem";
			this.viewStatusBarMenuItem.Size = new System.Drawing.Size(152, 22);
			this.viewStatusBarMenuItem.Text = "&Statusbalk";
			this.viewStatusBarMenuItem.Click += new System.EventHandler(this.viewStatusBarMenuItem_Click);
			// 
			// windowsMenu
			// 
			this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsScoreInputMenuItem,
            this.windowsPouleInformationMenuItem,
            this.windowsCourtInformationMenuItem});
			this.windowsMenu.Name = "windowsMenu";
			this.windowsMenu.Size = new System.Drawing.Size(63, 20);
			this.windowsMenu.Text = "Ve&nsters";
			// 
			// windowsScoreInputMenuItem
			// 
			this.windowsScoreInputMenuItem.Enabled = false;
			this.windowsScoreInputMenuItem.Name = "windowsScoreInputMenuItem";
			this.windowsScoreInputMenuItem.Size = new System.Drawing.Size(162, 22);
			this.windowsScoreInputMenuItem.Text = "&Wedstrijdscores";
			this.windowsScoreInputMenuItem.Click += new System.EventHandler(this.windowsScoreInputMenuItem_Click);
			// 
			// windowsPouleInformationMenuItem
			// 
			this.windowsPouleInformationMenuItem.Enabled = false;
			this.windowsPouleInformationMenuItem.Name = "windowsPouleInformationMenuItem";
			this.windowsPouleInformationMenuItem.Size = new System.Drawing.Size(162, 22);
			this.windowsPouleInformationMenuItem.Text = "&Poule informatie";
			this.windowsPouleInformationMenuItem.Click += new System.EventHandler(this.windowsPouleInformationMenuItem_Click);
			// 
			// windowsCourtInformationMenuItem
			// 
			this.windowsCourtInformationMenuItem.Enabled = false;
			this.windowsCourtInformationMenuItem.Name = "windowsCourtInformationMenuItem";
			this.windowsCourtInformationMenuItem.Size = new System.Drawing.Size(162, 22);
			this.windowsCourtInformationMenuItem.Text = "&Veld informatie";
			this.windowsCourtInformationMenuItem.Click += new System.EventHandler(this.windowsCourtInformationMenuItem_Click);
			// 
			// settingsMenu
			// 
			this.settingsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsAmountFieldsMenuItem,
            this.printerMenuItem});
			this.settingsMenu.Name = "settingsMenu";
			this.settingsMenu.Size = new System.Drawing.Size(80, 20);
			this.settingsMenu.Text = "&Instellingen";
			// 
			// settingsAmountFieldsMenuItem
			// 
			this.settingsAmountFieldsMenuItem.Enabled = false;
			this.settingsAmountFieldsMenuItem.Name = "settingsAmountFieldsMenuItem";
			this.settingsAmountFieldsMenuItem.Size = new System.Drawing.Size(155, 22);
			this.settingsAmountFieldsMenuItem.Text = "&Aantal velden...";
			this.settingsAmountFieldsMenuItem.Click += new System.EventHandler(this.settingsAmountFieldsMenuItem_Click);
			// 
			// printerMenuItem
			// 
			this.printerMenuItem.Name = "printerMenuItem";
			this.printerMenuItem.Size = new System.Drawing.Size(155, 22);
			this.printerMenuItem.Text = "&Printer...";
			this.printerMenuItem.Click += new System.EventHandler(this.printerMenuItem_Click);
			// 
			// helpMenu
			// 
			this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sharpShuttleHelpMenuItem,
            this.toolStripSeparator1,
            this.aboutSharpShuttleMenuItem});
			this.helpMenu.Name = "helpMenu";
			this.helpMenu.Size = new System.Drawing.Size(44, 20);
			this.helpMenu.Text = "&Help";
			// 
			// sharpShuttleHelpMenuItem
			// 
			this.sharpShuttleHelpMenuItem.Name = "sharpShuttleHelpMenuItem";
			this.sharpShuttleHelpMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.sharpShuttleHelpMenuItem.Size = new System.Drawing.Size(191, 22);
			this.sharpShuttleHelpMenuItem.Text = "Sharp Shuttle &Help";
			this.sharpShuttleHelpMenuItem.Click += new System.EventHandler(this.sharpShuttleHelpMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
			// 
			// aboutSharpShuttleMenuItem
			// 
			this.aboutSharpShuttleMenuItem.Name = "aboutSharpShuttleMenuItem";
			this.aboutSharpShuttleMenuItem.Size = new System.Drawing.Size(191, 22);
			this.aboutSharpShuttleMenuItem.Text = "&Over Sharp Shuttle...";
			this.aboutSharpShuttleMenuItem.Click += new System.EventHandler(this.aboutSharpShuttleMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 431);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(632, 22);
			this.statusStrip.TabIndex = 999;
			this.statusStrip.Text = "StatusStrip";
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
			this.toolStripStatusLabel.Text = "Status";
			// 
			// dockPanel
			// 
			this.dockPanel.ActiveAutoHideContent = null;
			this.dockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dockPanel.DockBackColor = System.Drawing.SystemColors.ControlDark;
			this.dockPanel.Location = new System.Drawing.Point(0, 24);
			this.dockPanel.Margin = new System.Windows.Forms.Padding(0);
			this.dockPanel.Name = "dockPanel";
			this.dockPanel.Size = new System.Drawing.Size(632, 407);
			dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
			dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
			autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
			tabGradient1.EndColor = System.Drawing.SystemColors.Control;
			tabGradient1.StartColor = System.Drawing.SystemColors.Control;
			tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
			autoHideStripSkin1.TabGradient = tabGradient1;
			dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
			tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
			tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
			tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
			dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
			dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
			dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
			tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
			tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
			tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
			dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
			tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
			tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
			tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
			dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
			tabGradient5.EndColor = System.Drawing.SystemColors.Control;
			tabGradient5.StartColor = System.Drawing.SystemColors.Control;
			tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
			dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
			dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
			dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
			tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
			tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
			tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
			tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
			dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
			tabGradient7.EndColor = System.Drawing.Color.Transparent;
			tabGradient7.StartColor = System.Drawing.Color.Transparent;
			tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
			dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
			dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
			dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
			this.dockPanel.Skin = dockPanelSkin1;
			this.dockPanel.TabIndex = 1;
			// 
			// fileLogoutMenuItem
			// 
			this.fileLogoutMenuItem.Enabled = false;
			this.fileLogoutMenuItem.Name = "fileLogoutMenuItem";
			this.fileLogoutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
			this.fileLogoutMenuItem.Size = new System.Drawing.Size(163, 22);
			this.fileLogoutMenuItem.Text = "Log &uit";
			this.fileLogoutMenuItem.Click += new System.EventHandler(this.fileLogoutMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(632, 453);
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "Sharp Shuttle";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }
        #endregion


		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem fileMenu;
		private System.Windows.Forms.ToolStripMenuItem editMenu;
		private System.Windows.Forms.ToolStripMenuItem viewMenu;
		private System.Windows.Forms.ToolStripMenuItem viewStatusBarMenuItem;
		private System.Windows.Forms.ToolStripMenuItem windowsMenu;
		private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolTip toolTip;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
		private System.Windows.Forms.ToolStripMenuItem settingsMenu;
		private System.Windows.Forms.ToolStripMenuItem fileExitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsAmountFieldsMenuItem;
		private System.Windows.Forms.ToolStripSeparator fileSeparator1;
		private System.Windows.Forms.ToolStripMenuItem aboutSharpShuttleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsScoreInputMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printerMenuItem;
		private System.Windows.Forms.ToolStripMenuItem windowsPouleInformationMenuItem;
        private System.Windows.Forms.ToolStripSeparator fileSeparator2;
		private System.Windows.Forms.ToolStripMenuItem filePrintMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filePrintAllMatchPapersMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filePrintFullPageMatchPapersMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileLoginMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editInitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem windowsCourtInformationMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editAddPouleMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editManagePlayersMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sharpShuttleHelpMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem fileLogoutMenuItem;
    }
}



