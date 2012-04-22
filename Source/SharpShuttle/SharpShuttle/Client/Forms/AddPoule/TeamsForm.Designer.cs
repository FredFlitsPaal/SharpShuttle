namespace Client.Forms.AddPoule
{
	partial class TeamsForm
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
			this.lvwPlayer2 = new UserControls.TeamControls.TeamPlayerListView();
			this.pnlArrange = new System.Windows.Forms.Panel();
			this.spcPlayers = new System.Windows.Forms.SplitContainer();
			this.lvwPlayer1 = new UserControls.TeamControls.TeamPlayerListView();
			this.lblPlayer1 = new System.Windows.Forms.Label();
			this.lblPlayer2 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.ttSelectPoule = new System.Windows.Forms.ToolTip(this.components);
			this.pnlTeamPools = new System.Windows.Forms.Panel();
			this.pnlArrange.SuspendLayout();
			this.spcPlayers.Panel1.SuspendLayout();
			this.spcPlayers.Panel2.SuspendLayout();
			this.spcPlayers.SuspendLayout();
			this.pnlTeamPools.SuspendLayout();
			this.SuspendLayout();
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
			this.lvwPlayer2.Size = new System.Drawing.Size(325, 371);
			this.lvwPlayer2.TabIndex = 2;
			this.lvwPlayer2.UseCompatibleStateImageBehavior = false;
			this.lvwPlayer2.View = System.Windows.Forms.View.Details;
			// 
			// pnlArrange
			// 
			this.pnlArrange.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlArrange.Controls.Add(this.spcPlayers);
			this.pnlArrange.Location = new System.Drawing.Point(0, 0);
			this.pnlArrange.Name = "pnlArrange";
			this.pnlArrange.Size = new System.Drawing.Size(726, 447);
			this.pnlArrange.TabIndex = 20;
			// 
			// spcPlayers
			// 
			this.spcPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
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
			this.spcPlayers.Size = new System.Drawing.Size(723, 444);
			this.spcPlayers.SplitterDistance = 360;
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
			this.lvwPlayer1.Size = new System.Drawing.Size(325, 371);
			this.lvwPlayer1.TabIndex = 1;
			this.lvwPlayer1.UseCompatibleStateImageBehavior = false;
			this.lvwPlayer1.View = System.Windows.Forms.View.Details;
			// 
			// lblPlayer1
			// 
			this.lblPlayer1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblPlayer1.AutoSize = true;
			this.lblPlayer1.Location = new System.Drawing.Point(146, 15);
			this.lblPlayer1.Name = "lblPlayer1";
			this.lblPlayer1.Size = new System.Drawing.Size(49, 13);
			this.lblPlayer1.TabIndex = 3;
			this.lblPlayer1.Text = "Speler 1:";
			// 
			// lblPlayer2
			// 
			this.lblPlayer2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblPlayer2.AutoSize = true;
			this.lblPlayer2.Location = new System.Drawing.Point(161, 15);
			this.lblPlayer2.Name = "lblPlayer2";
			this.lblPlayer2.Size = new System.Drawing.Size(49, 13);
			this.lblPlayer2.TabIndex = 4;
			this.lblPlayer2.Text = "Speler 2:";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(639, 453);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(558, 453);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// ttSelectPoule
			// 
			this.ttSelectPoule.ToolTipTitle = "Weergegeven Poules";
			// 
			// pnlTeamPools
			// 
			this.pnlTeamPools.AllowDrop = true;
			this.pnlTeamPools.Controls.Add(this.btnOk);
			this.pnlTeamPools.Controls.Add(this.btnCancel);
			this.pnlTeamPools.Controls.Add(this.pnlArrange);
			this.pnlTeamPools.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTeamPools.Location = new System.Drawing.Point(0, 0);
			this.pnlTeamPools.Name = "pnlTeamPools";
			this.pnlTeamPools.Size = new System.Drawing.Size(726, 488);
			this.pnlTeamPools.TabIndex = 5;
			// 
			// TeamsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(726, 488);
			this.Controls.Add(this.pnlTeamPools);
			this.MinimizeBox = false;
			this.Name = "TeamsForm";
			this.Text = "Team Indeling";
			this.pnlArrange.ResumeLayout(false);
			this.spcPlayers.Panel1.ResumeLayout(false);
			this.spcPlayers.Panel1.PerformLayout();
			this.spcPlayers.Panel2.ResumeLayout(false);
			this.spcPlayers.Panel2.PerformLayout();
			this.spcPlayers.ResumeLayout(false);
			this.pnlTeamPools.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private UserControls.TeamControls.TeamPlayerListView lvwPlayer2;
		private System.Windows.Forms.Panel pnlArrange;
		private System.Windows.Forms.SplitContainer spcPlayers;
		private UserControls.TeamControls.TeamPlayerListView lvwPlayer1;
		private System.Windows.Forms.Label lblPlayer1;
		private System.Windows.Forms.Label lblPlayer2;
		private System.Windows.Forms.ToolTip ttSelectPoule;
		private System.Windows.Forms.Panel pnlTeamPools;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;

	}
}