namespace Client.Forms.TeamProperties
{
	partial class PlayersListForm
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
			this.btnSelect = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lvwPlayers = new UserControls.PlayerControls.RegisteredPlayerListView();
			this.SuspendLayout();
			// 
			// btnSelect
			// 
			this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSelect.Location = new System.Drawing.Point(12, 260);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(75, 23);
			this.btnSelect.TabIndex = 0;
			this.btnSelect.Text = "Selecteer";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(529, 260);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
			this.lvwPlayers.Size = new System.Drawing.Size(587, 242);
			this.lvwPlayers.TabIndex = 3;
			this.lvwPlayers.UseCompatibleStateImageBehavior = false;
			this.lvwPlayers.View = System.Windows.Forms.View.Details;
			this.lvwPlayers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwPlayers_ColumnClick);
			// 
			// PlayersListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(611, 291);
			this.Controls.Add(this.lvwPlayers);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSelect);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PlayersListForm";
			this.ShowInTaskbar = false;
			this.Text = "Spelers Lijst";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnCancel;
		private UserControls.PlayerControls.RegisteredPlayerListView lvwPlayers;
	}
}