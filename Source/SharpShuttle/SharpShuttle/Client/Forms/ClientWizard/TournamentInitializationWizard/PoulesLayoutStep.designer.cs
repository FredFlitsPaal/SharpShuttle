namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	partial class PoulesLayoutStep
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoulesLayoutStep));
			this.spcPoulesPlayers = new System.Windows.Forms.SplitContainer();
			this.tvwPoules = new UserControls.PouleControls.PouleTreeView();
			this.lblPoules = new System.Windows.Forms.Label();
			this.spcPlayersDescription = new System.Windows.Forms.SplitContainer();
			this.tvwPlayers = new UserControls.PlayerControls.PlayerTreeView();
			this.lblPlayers = new System.Windows.Forms.Label();
			this.txtInfo = new System.Windows.Forms.RichTextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.cmsPoules = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmsItemRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsItemRemoveAllPlayers = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsItemRemovePlayers = new System.Windows.Forms.ToolStripMenuItem();
			this.btnGenLayout = new System.Windows.Forms.Button();
			this.spcPoulesPlayers.Panel1.SuspendLayout();
			this.spcPoulesPlayers.Panel2.SuspendLayout();
			this.spcPoulesPlayers.SuspendLayout();
			this.spcPlayersDescription.Panel1.SuspendLayout();
			this.spcPlayersDescription.Panel2.SuspendLayout();
			this.spcPlayersDescription.SuspendLayout();
			this.cmsPoules.SuspendLayout();
			this.SuspendLayout();
			// 
			// spcPoulesPlayers
			// 
			this.spcPoulesPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.spcPoulesPlayers.IsSplitterFixed = true;
			this.spcPoulesPlayers.Location = new System.Drawing.Point(3, 9);
			this.spcPoulesPlayers.Name = "spcPoulesPlayers";
			// 
			// spcPoulesPlayers.Panel1
			// 
			this.spcPoulesPlayers.Panel1.Controls.Add(this.tvwPoules);
			this.spcPoulesPlayers.Panel1.Controls.Add(this.lblPoules);
			// 
			// spcPoulesPlayers.Panel2
			// 
			this.spcPoulesPlayers.Panel2.Controls.Add(this.spcPlayersDescription);
			this.spcPoulesPlayers.Size = new System.Drawing.Size(740, 370);
			this.spcPoulesPlayers.SplitterDistance = 229;
			this.spcPoulesPlayers.TabIndex = 24;
			// 
			// tvwPoules
			// 
			this.tvwPoules.AllowDrop = true;
			this.tvwPoules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tvwPoules.DataSource = null;
			this.tvwPoules.Location = new System.Drawing.Point(3, 16);
			this.tvwPoules.Name = "tvwPoules";
			this.tvwPoules.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("tvwPoules.SelectedNodes")));
			this.tvwPoules.Size = new System.Drawing.Size(227, 354);
			this.tvwPoules.TabIndex = 1;
			this.tvwPoules.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwPoules_AfterSelect);
			this.tvwPoules.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwPoules_AfterSelect);
			// 
			// lblPoules
			// 
			this.lblPoules.AutoSize = true;
			this.lblPoules.Location = new System.Drawing.Point(3, 0);
			this.lblPoules.Name = "lblPoules";
			this.lblPoules.Size = new System.Drawing.Size(39, 13);
			this.lblPoules.TabIndex = 12;
			this.lblPoules.Text = "Poules";
			// 
			// spcPlayersDescription
			// 
			this.spcPlayersDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcPlayersDescription.IsSplitterFixed = true;
			this.spcPlayersDescription.Location = new System.Drawing.Point(0, 0);
			this.spcPlayersDescription.Name = "spcPlayersDescription";
			// 
			// spcPlayersDescription.Panel1
			// 
			this.spcPlayersDescription.Panel1.Controls.Add(this.tvwPlayers);
			this.spcPlayersDescription.Panel1.Controls.Add(this.lblPlayers);
			// 
			// spcPlayersDescription.Panel2
			// 
			this.spcPlayersDescription.Panel2.Controls.Add(this.txtInfo);
			this.spcPlayersDescription.Panel2.Controls.Add(this.lblDescription);
			this.spcPlayersDescription.Size = new System.Drawing.Size(507, 370);
			this.spcPlayersDescription.SplitterDistance = 219;
			this.spcPlayersDescription.TabIndex = 0;
			// 
			// tvwPlayers
			// 
			this.tvwPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tvwPlayers.DataSource = null;
			this.tvwPlayers.Location = new System.Drawing.Point(0, 16);
			this.tvwPlayers.Name = "tvwPlayers";
			this.tvwPlayers.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("tvwPlayers.SelectedNodes")));
			this.tvwPlayers.Size = new System.Drawing.Size(218, 354);
			this.tvwPlayers.TabIndex = 2;
			this.tvwPlayers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwPlayers_AfterSelect);
			this.tvwPlayers.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvwPlayers_AfterSelect);
			// 
			// lblPlayers
			// 
			this.lblPlayers.AutoSize = true;
			this.lblPlayers.Location = new System.Drawing.Point(3, 0);
			this.lblPlayers.Name = "lblPlayers";
			this.lblPlayers.Size = new System.Drawing.Size(63, 13);
			this.lblPlayers.TabIndex = 11;
			this.lblPlayers.Text = "Deelnemers";
			// 
			// txtInfo
			// 
			this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInfo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtInfo.Location = new System.Drawing.Point(1, 16);
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.ReadOnly = true;
			this.txtInfo.Size = new System.Drawing.Size(277, 354);
			this.txtInfo.TabIndex = 3;
			this.txtInfo.Text = "";
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Location = new System.Drawing.Point(3, 0);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(64, 13);
			this.lblDescription.TabIndex = 10;
			this.lblDescription.Text = "Beschrijving";
			// 
			// cmsPoules
			// 
			this.cmsPoules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemRemove,
            this.cmsItemRemoveAllPlayers,
            this.cmsItemRemovePlayers});
			this.cmsPoules.Name = "CMenu";
			this.cmsPoules.Size = new System.Drawing.Size(238, 70);
			// 
			// cmsItemRemove
			// 
			this.cmsItemRemove.Name = "cmsItemRemove";
			this.cmsItemRemove.Size = new System.Drawing.Size(237, 22);
			this.cmsItemRemove.Text = "Verwijder";
			// 
			// cmsItemRemoveAllPlayers
			// 
			this.cmsItemRemoveAllPlayers.Name = "cmsItemRemoveAllPlayers";
			this.cmsItemRemoveAllPlayers.Size = new System.Drawing.Size(237, 22);
			this.cmsItemRemoveAllPlayers.Text = "Verwijder alle spelers";
			// 
			// cmsItemRemovePlayers
			// 
			this.cmsItemRemovePlayers.Name = "cmsItemRemovePlayers";
			this.cmsItemRemovePlayers.Size = new System.Drawing.Size(237, 22);
			this.cmsItemRemovePlayers.Text = "Verwijder geselecteerde spelers";
			// 
			// btnGenLayout
			// 
			this.btnGenLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnGenLayout.Location = new System.Drawing.Point(3, 385);
			this.btnGenLayout.Name = "btnGenLayout";
			this.btnGenLayout.Size = new System.Drawing.Size(173, 23);
			this.btnGenLayout.TabIndex = 4;
			this.btnGenLayout.Text = "Deel spelers in naar voorkeur";
			this.btnGenLayout.UseVisualStyleBackColor = true;
			this.btnGenLayout.Click += new System.EventHandler(this.btnGenLayout_Click);
			// 
			// PoulesLayoutStep
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.spcPoulesPlayers);
			this.Controls.Add(this.btnGenLayout);
			this.Name = "PoulesLayoutStep";
			this.Size = new System.Drawing.Size(747, 417);
			this.spcPoulesPlayers.Panel1.ResumeLayout(false);
			this.spcPoulesPlayers.Panel1.PerformLayout();
			this.spcPoulesPlayers.Panel2.ResumeLayout(false);
			this.spcPoulesPlayers.ResumeLayout(false);
			this.spcPlayersDescription.Panel1.ResumeLayout(false);
			this.spcPlayersDescription.Panel1.PerformLayout();
			this.spcPlayersDescription.Panel2.ResumeLayout(false);
			this.spcPlayersDescription.Panel2.PerformLayout();
			this.spcPlayersDescription.ResumeLayout(false);
			this.cmsPoules.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer spcPoulesPlayers;
		private UserControls.PouleControls.PouleTreeView tvwPoules;
		private System.Windows.Forms.Label lblPoules;
		private System.Windows.Forms.SplitContainer spcPlayersDescription;
		private UserControls.PlayerControls.PlayerTreeView tvwPlayers;
		private System.Windows.Forms.Label lblPlayers;
		private System.Windows.Forms.RichTextBox txtInfo;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.ContextMenuStrip cmsPoules;
		private System.Windows.Forms.ToolStripMenuItem cmsItemRemove;
		private System.Windows.Forms.ToolStripMenuItem cmsItemRemoveAllPlayers;
		private System.Windows.Forms.ToolStripMenuItem cmsItemRemovePlayers;
		private System.Windows.Forms.Button btnGenLayout;
	}
}

