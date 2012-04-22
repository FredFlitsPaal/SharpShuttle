namespace Client.Forms.PouleInformation
{
    partial class PouleInformationForm
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
			this.cmsMatches = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmsItemPrintAgain = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsItemSetScore = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsTeams = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmsItemEditTeam = new System.Windows.Forms.ToolStripMenuItem();
			this.btnDetail = new System.Windows.Forms.Button();
			this.spcTeamsMatches = new System.Windows.Forms.SplitContainer();
			this.btnPrintLadder = new System.Windows.Forms.Button();
			this.lvwTeamsSimple = new UserControls.TeamControls.SimpleTeamListView();
			this.lvwTeams = new UserControls.TeamControls.TeamListView();
			this.lblRound = new System.Windows.Forms.Label();
			this.cboPoules = new UserControls.PouleControls.PouleComboBox();
			this.lblTeams = new System.Windows.Forms.Label();
			this.cb_allmatches = new System.Windows.Forms.CheckBox();
			this.lvwMatches = new UserControls.MatchControls.MatchListView();
			this.btnRemoveMatches = new System.Windows.Forms.Button();
			this.lblMatches = new System.Windows.Forms.Label();
			this.btnNextRound = new System.Windows.Forms.Button();
			this.spcPouleInformation = new System.Windows.Forms.SplitContainer();
			this.cmsMatches.SuspendLayout();
			this.cmsTeams.SuspendLayout();
			this.spcTeamsMatches.Panel1.SuspendLayout();
			this.spcTeamsMatches.Panel2.SuspendLayout();
			this.spcTeamsMatches.SuspendLayout();
			this.spcPouleInformation.Panel1.SuspendLayout();
			this.spcPouleInformation.Panel2.SuspendLayout();
			this.spcPouleInformation.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsMatches
			// 
			this.cmsMatches.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemPrintAgain,
            this.cmsItemSetScore});
			this.cmsMatches.Name = "contextMenuStrip1";
			this.cmsMatches.Size = new System.Drawing.Size(233, 48);
			// 
			// cmsItemPrintAgain
			// 
			this.cmsItemPrintAgain.Name = "cmsItemPrintAgain";
			this.cmsItemPrintAgain.Size = new System.Drawing.Size(232, 22);
			this.cmsItemPrintAgain.Text = "Print wedstrijdbriefje opnieuw";
			this.cmsItemPrintAgain.Click += new System.EventHandler(this.printWedstrijdbriefjeOpnieuwToolStripMenuItem_Click);
			// 
			// cmsItemSetScore
			// 
			this.cmsItemSetScore.Name = "cmsItemSetScore";
			this.cmsItemSetScore.Size = new System.Drawing.Size(232, 22);
			this.cmsItemSetScore.Text = "Vul score in";
			this.cmsItemSetScore.Click += new System.EventHandler(this.vulScoreInToolStripMenuItem_Click);
			// 
			// cmsTeams
			// 
			this.cmsTeams.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemEditTeam});
			this.cmsTeams.Name = "cmsTeam";
			this.cmsTeams.Size = new System.Drawing.Size(187, 26);
			// 
			// cmsItemEditTeam
			// 
			this.cmsItemEditTeam.Name = "cmsItemEditTeam";
			this.cmsItemEditTeam.Size = new System.Drawing.Size(186, 22);
			this.cmsItemEditTeam.Text = "Wijzig teamgegevens";
			this.cmsItemEditTeam.Click += new System.EventHandler(this.wijzigTeamgegevensToolStripMenuItem_Click);
			// 
			// btnDetail
			// 
			this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDetail.Location = new System.Drawing.Point(790, 2);
			this.btnDetail.Name = "btnDetail";
			this.btnDetail.Size = new System.Drawing.Size(91, 23);
			this.btnDetail.TabIndex = 4;
			this.btnDetail.Text = "Detail";
			this.btnDetail.UseVisualStyleBackColor = true;
			this.btnDetail.Click += new System.EventHandler(this.button_Details_Click);
			// 
			// spcTeamsMatches
			// 
			this.spcTeamsMatches.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcTeamsMatches.Location = new System.Drawing.Point(0, 0);
			this.spcTeamsMatches.Name = "spcTeamsMatches";
			this.spcTeamsMatches.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spcTeamsMatches.Panel1
			// 
			this.spcTeamsMatches.Panel1.Controls.Add(this.btnPrintLadder);
			this.spcTeamsMatches.Panel1.Controls.Add(this.lvwTeamsSimple);
			this.spcTeamsMatches.Panel1.Controls.Add(this.lvwTeams);
			this.spcTeamsMatches.Panel1.Controls.Add(this.lblRound);
			this.spcTeamsMatches.Panel1.Controls.Add(this.cboPoules);
			this.spcTeamsMatches.Panel1.Controls.Add(this.lblTeams);
			// 
			// spcTeamsMatches.Panel2
			// 
			this.spcTeamsMatches.Panel2.Controls.Add(this.cb_allmatches);
			this.spcTeamsMatches.Panel2.Controls.Add(this.lvwMatches);
			this.spcTeamsMatches.Panel2.Controls.Add(this.btnRemoveMatches);
			this.spcTeamsMatches.Panel2.Controls.Add(this.lblMatches);
			this.spcTeamsMatches.Panel2.Controls.Add(this.btnNextRound);
			this.spcTeamsMatches.Size = new System.Drawing.Size(888, 643);
			this.spcTeamsMatches.SplitterDistance = 339;
			this.spcTeamsMatches.TabIndex = 3;
			// 
			// btnPrintLadder
			// 
			this.btnPrintLadder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrintLadder.Location = new System.Drawing.Point(780, 313);
			this.btnPrintLadder.Name = "btnPrintLadder";
			this.btnPrintLadder.Size = new System.Drawing.Size(103, 23);
			this.btnPrintLadder.TabIndex = 3;
			this.btnPrintLadder.Text = "Print Ladder";
			this.btnPrintLadder.UseVisualStyleBackColor = true;
			this.btnPrintLadder.Click += new System.EventHandler(this.btnPrintLadder_Click);
			// 
			// lvwTeamsSimple
			// 
			this.lvwTeamsSimple.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwTeamsSimple.DataSource = null;
			this.lvwTeamsSimple.FullRowSelect = true;
			this.lvwTeamsSimple.GridLines = true;
			this.lvwTeamsSimple.HideSelection = false;
			this.lvwTeamsSimple.Location = new System.Drawing.Point(5, 28);
			this.lvwTeamsSimple.MultiSelect = false;
			this.lvwTeamsSimple.Name = "lvwTeamsSimple";
			this.lvwTeamsSimple.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lvwTeamsSimple.ScrollPosition = 0;
			this.lvwTeamsSimple.Size = new System.Drawing.Size(876, 279);
			this.lvwTeamsSimple.TabIndex = 2;
			this.lvwTeamsSimple.UseCompatibleStateImageBehavior = false;
			this.lvwTeamsSimple.View = System.Windows.Forms.View.Details;
			this.lvwTeamsSimple.Visible = false;
			this.lvwTeamsSimple.MouseClick += new System.Windows.Forms.MouseEventHandler(this.simpleTeamListView_Spelers_MouseClick);
			// 
			// lvwTeams
			// 
			this.lvwTeams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwTeams.DataSource = null;
			this.lvwTeams.FullRowSelect = true;
			this.lvwTeams.GridLines = true;
			this.lvwTeams.HideSelection = false;
			this.lvwTeams.Location = new System.Drawing.Point(5, 28);
			this.lvwTeams.MultiSelect = false;
			this.lvwTeams.Name = "lvwTeams";
			this.lvwTeams.ScrollPosition = 0;
			this.lvwTeams.Size = new System.Drawing.Size(876, 277);
			this.lvwTeams.TabIndex = 12;
			this.lvwTeams.UseCompatibleStateImageBehavior = false;
			this.lvwTeams.View = System.Windows.Forms.View.Details;
			this.lvwTeams.MouseClick += new System.Windows.Forms.MouseEventHandler(this.simpleTeamListView_Spelers_MouseClick);
			// 
			// lblRound
			// 
			this.lblRound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblRound.AutoSize = true;
			this.lblRound.Location = new System.Drawing.Point(4, 313);
			this.lblRound.Name = "lblRound";
			this.lblRound.Size = new System.Drawing.Size(51, 13);
			this.lblRound.TabIndex = 16;
			this.lblRound.Text = "Ronde: 1";
			// 
			// cboPoules
			// 
			this.cboPoules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboPoules.DataSource = null;
			this.cboPoules.DisplayMember = "Name";
			this.cboPoules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPoules.FormattingEnabled = true;
			this.cboPoules.Location = new System.Drawing.Point(760, 6);
			this.cboPoules.Name = "cboPoules";
			this.cboPoules.SelectedValue = 0;
			this.cboPoules.Size = new System.Drawing.Size(121, 21);
			this.cboPoules.TabIndex = 1;
			this.cboPoules.ValueMember = "Id";
			this.cboPoules.SelectedIndexChanged += new System.EventHandler(this.pouleComboBox_SelectedIndexChanged);
			// 
			// lblTeams
			// 
			this.lblTeams.AutoSize = true;
			this.lblTeams.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTeams.Location = new System.Drawing.Point(4, 7);
			this.lblTeams.Name = "lblTeams";
			this.lblTeams.Size = new System.Drawing.Size(61, 17);
			this.lblTeams.TabIndex = 8;
			this.lblTeams.Text = "Teams:";
			// 
			// cb_allmatches
			// 
			this.cb_allmatches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cb_allmatches.AutoSize = true;
			this.cb_allmatches.Checked = true;
			this.cb_allmatches.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cb_allmatches.Location = new System.Drawing.Point(7, 278);
			this.cb_allmatches.Name = "cb_allmatches";
			this.cb_allmatches.Size = new System.Drawing.Size(188, 17);
			this.cb_allmatches.TabIndex = 17;
			this.cb_allmatches.Text = "Alleen Huidige Ronde Weergeven";
			this.cb_allmatches.UseVisualStyleBackColor = true;
			this.cb_allmatches.CheckedChanged += new System.EventHandler(this.cb_allmatches_CheckedChanged);
			// 
			// lvwMatches
			// 
			this.lvwMatches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwMatches.DataSource = null;
			this.lvwMatches.FullRowSelect = true;
			this.lvwMatches.GridLines = true;
			this.lvwMatches.HideSelection = false;
			this.lvwMatches.Location = new System.Drawing.Point(5, 23);
			this.lvwMatches.Name = "lvwMatches";
			this.lvwMatches.ScrollPosition = 0;
			this.lvwMatches.Size = new System.Drawing.Size(876, 245);
			this.lvwMatches.TabIndex = 5;
			this.lvwMatches.UseCompatibleStateImageBehavior = false;
			this.lvwMatches.View = System.Windows.Forms.View.Details;
			this.lvwMatches.MouseClick += new System.Windows.Forms.MouseEventHandler(this.matchListView_wedstrijden_MouseClick);
			this.lvwMatches.DoubleClick += new System.EventHandler(this.matchListView_wedstrijden_DoubleClick);
			// 
			// btnRemoveMatches
			// 
			this.btnRemoveMatches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveMatches.Location = new System.Drawing.Point(655, 274);
			this.btnRemoveMatches.Name = "btnRemoveMatches";
			this.btnRemoveMatches.Size = new System.Drawing.Size(110, 23);
			this.btnRemoveMatches.TabIndex = 6;
			this.btnRemoveMatches.Text = "Ronde Verwijderen";
			this.btnRemoveMatches.UseVisualStyleBackColor = true;
			this.btnRemoveMatches.Click += new System.EventHandler(this.button_RemoveWedstrijden_Click);
			// 
			// lblMatches
			// 
			this.lblMatches.AutoSize = true;
			this.lblMatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMatches.Location = new System.Drawing.Point(4, 3);
			this.lblMatches.Name = "lblMatches";
			this.lblMatches.Size = new System.Drawing.Size(99, 17);
			this.lblMatches.TabIndex = 9;
			this.lblMatches.Text = "Wedstrijden:";
			// 
			// btnNextRound
			// 
			this.btnNextRound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNextRound.Location = new System.Drawing.Point(771, 274);
			this.btnNextRound.Name = "btnNextRound";
			this.btnNextRound.Size = new System.Drawing.Size(110, 23);
			this.btnNextRound.TabIndex = 7;
			this.btnNextRound.Text = "Volgende Ronde";
			this.btnNextRound.UseVisualStyleBackColor = true;
			this.btnNextRound.Click += new System.EventHandler(this.button_GenWedstrijden_Click);
			// 
			// spcPouleInformation
			// 
			this.spcPouleInformation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcPouleInformation.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.spcPouleInformation.IsSplitterFixed = true;
			this.spcPouleInformation.Location = new System.Drawing.Point(0, 0);
			this.spcPouleInformation.Name = "spcPouleInformation";
			this.spcPouleInformation.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spcPouleInformation.Panel1
			// 
			this.spcPouleInformation.Panel1.Controls.Add(this.spcTeamsMatches);
			// 
			// spcPouleInformation.Panel2
			// 
			this.spcPouleInformation.Panel2.Controls.Add(this.btnDetail);
			this.spcPouleInformation.Panel2MinSize = 30;
			this.spcPouleInformation.Size = new System.Drawing.Size(888, 679);
			this.spcPouleInformation.SplitterDistance = 643;
			this.spcPouleInformation.TabIndex = 5;
			// 
			// PouleInformationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(888, 679);
			this.Controls.Add(this.spcPouleInformation);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "PouleInformationForm";
			this.Text = "Poule Informatie";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PouleInformationForm_FormClosing);
			this.cmsMatches.ResumeLayout(false);
			this.cmsTeams.ResumeLayout(false);
			this.spcTeamsMatches.Panel1.ResumeLayout(false);
			this.spcTeamsMatches.Panel1.PerformLayout();
			this.spcTeamsMatches.Panel2.ResumeLayout(false);
			this.spcTeamsMatches.Panel2.PerformLayout();
			this.spcTeamsMatches.ResumeLayout(false);
			this.spcPouleInformation.Panel1.ResumeLayout(false);
			this.spcPouleInformation.Panel2.ResumeLayout(false);
			this.spcPouleInformation.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.ContextMenuStrip cmsMatches;
		private System.Windows.Forms.ToolStripMenuItem cmsItemPrintAgain;
		private System.Windows.Forms.ContextMenuStrip cmsTeams;
		private System.Windows.Forms.ToolStripMenuItem cmsItemEditTeam;
		private System.Windows.Forms.ToolStripMenuItem cmsItemSetScore;
		private System.Windows.Forms.Button btnDetail;
		internal System.Windows.Forms.SplitContainer spcTeamsMatches;
		private System.Windows.Forms.Button btnPrintLadder;
		private UserControls.TeamControls.SimpleTeamListView lvwTeamsSimple;
		private UserControls.TeamControls.TeamListView lvwTeams;
		private System.Windows.Forms.Label lblRound;
		private UserControls.PouleControls.PouleComboBox cboPoules;
		private System.Windows.Forms.Label lblTeams;
		private UserControls.MatchControls.MatchListView lvwMatches;
		private System.Windows.Forms.Button btnRemoveMatches;
		private System.Windows.Forms.Label lblMatches;
		private System.Windows.Forms.Button btnNextRound;
		private System.Windows.Forms.SplitContainer spcPouleInformation;
        private System.Windows.Forms.CheckBox cb_allmatches;


    }
}