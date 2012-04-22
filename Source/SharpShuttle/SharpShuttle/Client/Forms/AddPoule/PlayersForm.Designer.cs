namespace Client.Forms.AddPoule
{
	partial class PlayersForm
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
			this.lblAmount = new System.Windows.Forms.Label();
			this.lblNumberPlayers = new System.Windows.Forms.Label();
			this.lvwPlayers = new UserControls.PlayerControls.RegisteredPlayerListView();
			this.btnSelect = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblAmount
			// 
			this.lblAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblAmount.Location = new System.Drawing.Point(96, 491);
			this.lblAmount.Name = "lblAmount";
			this.lblAmount.Size = new System.Drawing.Size(78, 19);
			this.lblAmount.TabIndex = 15;
			this.lblAmount.Text = "0";
			this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblNumberPlayers
			// 
			this.lblNumberPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblNumberPlayers.AutoSize = true;
			this.lblNumberPlayers.Location = new System.Drawing.Point(12, 494);
			this.lblNumberPlayers.Name = "lblNumberPlayers";
			this.lblNumberPlayers.Size = new System.Drawing.Size(78, 13);
			this.lblNumberPlayers.TabIndex = 14;
			this.lblNumberPlayers.Text = "Aantal Spelers:";
			// 
			// lvwPlayers
			// 
			this.lvwPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwPlayers.DataSource = null;
			this.lvwPlayers.FullRowSelect = true;
			this.lvwPlayers.GridLines = true;
			this.lvwPlayers.Location = new System.Drawing.Point(12, 12);
			this.lvwPlayers.Name = "lvwPlayers";
			this.lvwPlayers.ScrollPosition = 0;
			this.lvwPlayers.ShowGroups = false;
			this.lvwPlayers.Size = new System.Drawing.Size(619, 470);
			this.lvwPlayers.TabIndex = 1;
			this.lvwPlayers.UseCompatibleStateImageBehavior = false;
			this.lvwPlayers.View = System.Windows.Forms.View.Details;
			this.lvwPlayers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwPoulePlayers_ColumnClick);
			// 
			// btnSelect
			// 
			this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelect.Location = new System.Drawing.Point(471, 491);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(75, 23);
			this.btnSelect.TabIndex = 2;
			this.btnSelect.Text = "Selecteer";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(556, 491);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// PlayersForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(643, 524);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.lblAmount);
			this.Controls.Add(this.lblNumberPlayers);
			this.Controls.Add(this.lvwPlayers);
			this.MinimizeBox = false;
			this.Name = "PlayersForm";
			this.Text = "Spelers Lijst";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblAmount;
		private System.Windows.Forms.Label lblNumberPlayers;
		private UserControls.PlayerControls.RegisteredPlayerListView lvwPlayers;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnCancel;

	}
}