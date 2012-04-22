using PlayerUserControl=UserControls.PlayerControls.PlayerUserControl;

namespace Client.Forms.TeamProperties
{
	partial class TeamPropertiesForm
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
			this.txtTeamName = new System.Windows.Forms.TextBox();
			this.lblTeamName = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnNonActive = new System.Windows.Forms.Button();
			this.grpPlayers = new System.Windows.Forms.GroupBox();
			this.playerPanel2 = new PlayerUserControl();
			this.playerPanel1 = new PlayerUserControl();
			this.lblId = new System.Windows.Forms.Label();
			this.grpPlayers.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtTeamName
			// 
			this.txtTeamName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtTeamName.Location = new System.Drawing.Point(81, 35);
			this.txtTeamName.Name = "txtTeamName";
			this.txtTeamName.Size = new System.Drawing.Size(201, 20);
			this.txtTeamName.TabIndex = 0;
			// 
			// lblTeamName
			// 
			this.lblTeamName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblTeamName.AutoSize = true;
			this.lblTeamName.Location = new System.Drawing.Point(9, 38);
			this.lblTeamName.Name = "lblTeamName";
			this.lblTeamName.Size = new System.Drawing.Size(66, 13);
			this.lblTeamName.TabIndex = 1;
			this.lblTeamName.Text = "Team naam:";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(140, 172);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(221, 172);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnNonActive
			// 
			this.btnNonActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnNonActive.Location = new System.Drawing.Point(9, 172);
			this.btnNonActive.Name = "btnNonActive";
			this.btnNonActive.Size = new System.Drawing.Size(110, 23);
			this.btnNonActive.TabIndex = 3;
			this.btnNonActive.Text = "Inactief maken";
			this.btnNonActive.UseVisualStyleBackColor = true;
			this.btnNonActive.Click += new System.EventHandler(this.btnNonActive_Click);
			// 
			// grpPlayers
			// 
			this.grpPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.grpPlayers.Controls.Add(this.playerPanel2);
			this.grpPlayers.Controls.Add(this.playerPanel1);
			this.grpPlayers.Location = new System.Drawing.Point(12, 74);
			this.grpPlayers.Name = "grpPlayers";
			this.grpPlayers.Size = new System.Drawing.Size(270, 82);
			this.grpPlayers.TabIndex = 9;
			this.grpPlayers.TabStop = false;
			this.grpPlayers.Text = "Spelers";
			// 
			// playerPanel2
			// 
			this.playerPanel2.DataSource = null;
			this.playerPanel2.Location = new System.Drawing.Point(7, 45);
			this.playerPanel2.Name = "playerPanel2";
			this.playerPanel2.Player = null;
			this.playerPanel2.Size = new System.Drawing.Size(256, 29);
			this.playerPanel2.TabIndex = 2;
			// 
			// playerPanel1
			// 
			this.playerPanel1.DataSource = null;
			this.playerPanel1.Location = new System.Drawing.Point(7, 20);
			this.playerPanel1.Name = "playerPanel1";
			this.playerPanel1.Player = null;
			this.playerPanel1.Size = new System.Drawing.Size(256, 29);
			this.playerPanel1.TabIndex = 1;
			// 
			// lblId
			// 
			this.lblId.AutoSize = true;
			this.lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblId.Location = new System.Drawing.Point(7, 9);
			this.lblId.Name = "lblId";
			this.lblId.Size = new System.Drawing.Size(46, 13);
			this.lblId.TabIndex = 10;
			this.lblId.Text = "Team: ";
			// 
			// TeamPropertiesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(308, 200);
			this.Controls.Add(this.lblId);
			this.Controls.Add(this.grpPlayers);
			this.Controls.Add(this.btnNonActive);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lblTeamName);
			this.Controls.Add(this.txtTeamName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "TeamPropertiesForm";
			this.ShowInTaskbar = false;
			this.Text = "Team Gegevens";
			this.grpPlayers.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtTeamName;
		private System.Windows.Forms.Label lblTeamName;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnNonActive;
		private System.Windows.Forms.GroupBox grpPlayers;
		private System.Windows.Forms.Label lblId;
		private PlayerUserControl playerPanel1;
		private PlayerUserControl playerPanel2;
	}
}