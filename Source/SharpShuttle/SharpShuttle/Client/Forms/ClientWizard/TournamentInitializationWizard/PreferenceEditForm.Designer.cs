namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	partial class PreferenceEditForm
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
			this.btnAccept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.ltbPrefs = new System.Windows.Forms.ListBox();
			this.lblPreferences = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnAccept
			// 
			this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAccept.Enabled = false;
			this.btnAccept.Location = new System.Drawing.Point(12, 170);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 2;
			this.btnAccept.Text = "Toevoegen";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(141, 170);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// ltbPrefs
			// 
			this.ltbPrefs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ltbPrefs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ltbPrefs.FormattingEnabled = true;
			this.ltbPrefs.Location = new System.Drawing.Point(12, 30);
			this.ltbPrefs.Name = "ltbPrefs";
			this.ltbPrefs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ltbPrefs.Size = new System.Drawing.Size(204, 132);
			this.ltbPrefs.TabIndex = 1;
			this.ltbPrefs.SelectedIndexChanged += new System.EventHandler(this.listBPrefs_SelectedIndexChanged);
			// 
			// lblPreferences
			// 
			this.lblPreferences.AutoSize = true;
			this.lblPreferences.Location = new System.Drawing.Point(9, 9);
			this.lblPreferences.Name = "lblPreferences";
			this.lblPreferences.Size = new System.Drawing.Size(127, 13);
			this.lblPreferences.TabIndex = 4;
			this.lblPreferences.Text = "Selecteer de voorkeuren:";
			// 
			// PreferenceEditForm
			// 
			this.AcceptButton = this.btnAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(228, 205);
			this.Controls.Add(this.lblPreferences);
			this.Controls.Add(this.ltbPrefs);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PreferenceEditForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Voorkeuren Toevoegen";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnAccept;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ListBox ltbPrefs;
		private System.Windows.Forms.Label lblPreferences;
	}
}