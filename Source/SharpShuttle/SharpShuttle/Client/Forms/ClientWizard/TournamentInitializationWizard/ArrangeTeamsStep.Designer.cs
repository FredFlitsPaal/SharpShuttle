namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
    partial class ArrangeTeamsStep
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
			this.pnlSelectPool = new System.Windows.Forms.Panel();
			this.lblInfo = new System.Windows.Forms.Label();
			this.lblPoule = new System.Windows.Forms.Label();
			this.cboPoules = new UserControls.PouleControls.PouleComboBox();
			this.pnlConfirmAll = new System.Windows.Forms.Panel();
			this.pnlTeamPools = new System.Windows.Forms.Panel();
			this.pnlArrange = new System.Windows.Forms.Panel();
			this.spcPlayers = new System.Windows.Forms.SplitContainer();
			this.lvwPlayer1 = new UserControls.TeamControls.TeamPlayerListView();
			this.lblPlayer1 = new System.Windows.Forms.Label();
			this.lvwPlayer2 = new UserControls.TeamControls.TeamPlayerListView();
			this.lblPlayer2 = new System.Windows.Forms.Label();
			this.ttSelectPoule = new System.Windows.Forms.ToolTip(this.components);
			this.pnlSelectPool.SuspendLayout();
			this.pnlTeamPools.SuspendLayout();
			this.pnlArrange.SuspendLayout();
			this.spcPlayers.Panel1.SuspendLayout();
			this.spcPlayers.Panel2.SuspendLayout();
			this.spcPlayers.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlSelectPool
			// 
			this.pnlSelectPool.BackColor = System.Drawing.SystemColors.Control;
			this.pnlSelectPool.Controls.Add(this.lblInfo);
			this.pnlSelectPool.Controls.Add(this.lblPoule);
			this.pnlSelectPool.Controls.Add(this.cboPoules);
			this.pnlSelectPool.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSelectPool.Location = new System.Drawing.Point(0, 0);
			this.pnlSelectPool.Name = "pnlSelectPool";
			this.pnlSelectPool.Size = new System.Drawing.Size(700, 30);
			this.pnlSelectPool.TabIndex = 0;
			// 
			// lblInfo
			// 
			this.lblInfo.AutoSize = true;
			this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInfo.Location = new System.Drawing.Point(216, 6);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(168, 12);
			this.lblInfo.TabIndex = 4;
			this.lblInfo.Text = "*Enkelpoules worden niet weergegeven";
			// 
			// lblPoule
			// 
			this.lblPoule.AutoSize = true;
			this.lblPoule.Location = new System.Drawing.Point(3, 6);
			this.lblPoule.Name = "lblPoule";
			this.lblPoule.Size = new System.Drawing.Size(37, 13);
			this.lblPoule.TabIndex = 3;
			this.lblPoule.Text = "Poule:";
			// 
			// cboPoules
			// 
			this.cboPoules.DataSource = null;
			this.cboPoules.DisplayMember = "Name";
			this.cboPoules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPoules.FormattingEnabled = true;
			this.cboPoules.Location = new System.Drawing.Point(45, 3);
			this.cboPoules.Name = "cboPoules";
			this.cboPoules.SelectedValue = 0;
			this.cboPoules.Size = new System.Drawing.Size(150, 21);
			this.cboPoules.TabIndex = 1;
			this.ttSelectPoule.SetToolTip(this.cboPoules, "Enkelpoules worden niet weergegeven omdat teams daarin niet te wijzigen zijn.");
			this.cboPoules.ValueMember = "Id";
			this.cboPoules.SelectedIndexChanged += new System.EventHandler(this.cmbPoules_SelectedIndexChanged);
			// 
			// pnlConfirmAll
			// 
			this.pnlConfirmAll.BackColor = System.Drawing.SystemColors.Control;
			this.pnlConfirmAll.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlConfirmAll.Location = new System.Drawing.Point(0, 471);
			this.pnlConfirmAll.Name = "pnlConfirmAll";
			this.pnlConfirmAll.Size = new System.Drawing.Size(700, 29);
			this.pnlConfirmAll.TabIndex = 3;
			// 
			// pnlTeamPools
			// 
			this.pnlTeamPools.AllowDrop = true;
			this.pnlTeamPools.Controls.Add(this.pnlArrange);
			this.pnlTeamPools.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTeamPools.Location = new System.Drawing.Point(0, 30);
			this.pnlTeamPools.Name = "pnlTeamPools";
			this.pnlTeamPools.Size = new System.Drawing.Size(700, 441);
			this.pnlTeamPools.TabIndex = 4;
			// 
			// pnlArrange
			// 
			this.pnlArrange.Controls.Add(this.spcPlayers);
			this.pnlArrange.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlArrange.Location = new System.Drawing.Point(0, 0);
			this.pnlArrange.Name = "pnlArrange";
			this.pnlArrange.Size = new System.Drawing.Size(700, 441);
			this.pnlArrange.TabIndex = 20;
			// 
			// spcPlayers
			// 
			this.spcPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcPlayers.IsSplitterFixed = true;
			this.spcPlayers.Location = new System.Drawing.Point(0, 0);
			this.spcPlayers.Name = "spcPlayers";
			// 
			// spcPlayers.Panel1
			// 
			this.spcPlayers.Panel1.Controls.Add(this.lvwPlayer1);
			this.spcPlayers.Panel1.Controls.Add(this.lblPlayer1);
			// 
			// spcPlayers.Panel2
			// 
			this.spcPlayers.Panel2.Controls.Add(this.lvwPlayer2);
			this.spcPlayers.Panel2.Controls.Add(this.lblPlayer2);
			this.spcPlayers.Size = new System.Drawing.Size(700, 441);
			this.spcPlayers.SplitterDistance = 350;
			this.spcPlayers.SplitterWidth = 2;
			this.spcPlayers.TabIndex = 5;
			// 
			// lvwPlayer1
			// 
			this.lvwPlayer1.AllowDrop = true;
			this.lvwPlayer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwPlayer1.DataSource = null;
			this.lvwPlayer1.FullRowSelect = true;
			this.lvwPlayer1.GridLines = true;
			this.lvwPlayer1.Location = new System.Drawing.Point(18, 41);
			this.lvwPlayer1.MultiSelect = false;
			this.lvwPlayer1.Name = "lvwPlayer1";
			this.lvwPlayer1.ScrollPosition = 0;
			this.lvwPlayer1.Size = new System.Drawing.Size(315, 368);
			this.lvwPlayer1.TabIndex = 2;
			this.lvwPlayer1.UseCompatibleStateImageBehavior = false;
			this.lvwPlayer1.View = System.Windows.Forms.View.Details;
			// 
			// lblPlayer1
			// 
			this.lblPlayer1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblPlayer1.AutoSize = true;
			this.lblPlayer1.Location = new System.Drawing.Point(141, 15);
			this.lblPlayer1.Name = "lblPlayer1";
			this.lblPlayer1.Size = new System.Drawing.Size(49, 13);
			this.lblPlayer1.TabIndex = 3;
			this.lblPlayer1.Text = "Speler 1:";
			// 
			// lvwPlayer2
			// 
			this.lvwPlayer2.AllowDrop = true;
			this.lvwPlayer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwPlayer2.DataSource = null;
			this.lvwPlayer2.FullRowSelect = true;
			this.lvwPlayer2.GridLines = true;
			this.lvwPlayer2.Location = new System.Drawing.Point(16, 41);
			this.lvwPlayer2.MultiSelect = false;
			this.lvwPlayer2.Name = "lvwPlayer2";
			this.lvwPlayer2.ScrollPosition = 0;
			this.lvwPlayer2.Size = new System.Drawing.Size(315, 368);
			this.lvwPlayer2.TabIndex = 3;
			this.lvwPlayer2.UseCompatibleStateImageBehavior = false;
			this.lvwPlayer2.View = System.Windows.Forms.View.Details;
			// 
			// lblPlayer2
			// 
			this.lblPlayer2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblPlayer2.AutoSize = true;
			this.lblPlayer2.Location = new System.Drawing.Point(155, 15);
			this.lblPlayer2.Name = "lblPlayer2";
			this.lblPlayer2.Size = new System.Drawing.Size(49, 13);
			this.lblPlayer2.TabIndex = 4;
			this.lblPlayer2.Text = "Speler 2:";
			// 
			// ttSelectPoule
			// 
			this.ttSelectPoule.ToolTipTitle = "Weergegeven Poules";
			// 
			// ArrangeTeamsStep
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlTeamPools);
			this.Controls.Add(this.pnlConfirmAll);
			this.Controls.Add(this.pnlSelectPool);
			this.Name = "ArrangeTeamsStep";
			this.Size = new System.Drawing.Size(700, 500);
			this.pnlSelectPool.ResumeLayout(false);
			this.pnlSelectPool.PerformLayout();
			this.pnlTeamPools.ResumeLayout(false);
			this.pnlArrange.ResumeLayout(false);
			this.spcPlayers.Panel1.ResumeLayout(false);
			this.spcPlayers.Panel1.PerformLayout();
			this.spcPlayers.Panel2.ResumeLayout(false);
			this.spcPlayers.Panel2.PerformLayout();
			this.spcPlayers.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		

        private System.Windows.Forms.Panel pnlSelectPool;
        private System.Windows.Forms.Panel pnlConfirmAll;
		private System.Windows.Forms.Panel pnlTeamPools;
		private UserControls.PouleControls.PouleComboBox cboPoules;
		private System.Windows.Forms.Label lblPoule;
		private System.Windows.Forms.Panel pnlArrange;
		private System.Windows.Forms.Label lblPlayer1;
		private System.Windows.Forms.Label lblPlayer2;
		private UserControls.TeamControls.TeamPlayerListView lvwPlayer2;
		private UserControls.TeamControls.TeamPlayerListView lvwPlayer1;
		private System.Windows.Forms.SplitContainer spcPlayers;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.ToolTip ttSelectPoule;
    }
}