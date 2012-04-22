namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
    partial class PlayerEditForm
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
			this.radFemale = new System.Windows.Forms.RadioButton();
			this.radMale = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnAccept = new System.Windows.Forms.Button();
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblGender = new System.Windows.Forms.Label();
			this.lblClub = new System.Windows.Forms.Label();
			this.lblPreferences = new System.Windows.Forms.Label();
			this.lblComment = new System.Windows.Forms.Label();
			this.txtClub = new System.Windows.Forms.TextBox();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.btnAddPref = new System.Windows.Forms.Button();
			this.ltbPrefs = new System.Windows.Forms.ListBox();
			this.btnDeletePref = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// radFemale
			// 
			this.radFemale.AutoSize = true;
			this.radFemale.Location = new System.Drawing.Point(153, 61);
			this.radFemale.Name = "radFemale";
			this.radFemale.Size = new System.Drawing.Size(55, 17);
			this.radFemale.TabIndex = 3;
			this.radFemale.Text = "Vrouw";
			this.radFemale.UseVisualStyleBackColor = true;
			this.radFemale.Click += new System.EventHandler(this.genderChanged);
			this.radFemale.CheckedChanged += new System.EventHandler(this.entryChanged);
			// 
			// radMale
			// 
			this.radMale.AutoSize = true;
			this.radMale.Checked = true;
			this.radMale.Location = new System.Drawing.Point(92, 61);
			this.radMale.Name = "radMale";
			this.radMale.Size = new System.Drawing.Size(46, 17);
			this.radMale.TabIndex = 2;
			this.radMale.TabStop = true;
			this.radMale.Text = "Man";
			this.radMale.UseVisualStyleBackColor = true;
			this.radMale.Click += new System.EventHandler(this.genderChanged);
			this.radMale.CheckedChanged += new System.EventHandler(this.entryChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(286, 331);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnAccept
			// 
			this.btnAccept.Location = new System.Drawing.Point(10, 331);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 9;
			this.btnAccept.Text = "Toevoegen";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(11, 25);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(38, 13);
			this.lblName.TabIndex = 11;
			this.lblName.Text = "Naam:";
			// 
			// txtName
			// 
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtName.BackColor = System.Drawing.Color.Pink;
			this.txtName.Location = new System.Drawing.Point(91, 22);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(165, 20);
			this.txtName.TabIndex = 1;
			this.txtName.TextChanged += new System.EventHandler(this.entryChanged);
			// 
			// lblGender
			// 
			this.lblGender.AutoSize = true;
			this.lblGender.Location = new System.Drawing.Point(11, 63);
			this.lblGender.Name = "lblGender";
			this.lblGender.Size = new System.Drawing.Size(52, 13);
			this.lblGender.TabIndex = 16;
			this.lblGender.Text = "Geslacht:";
			// 
			// lblClub
			// 
			this.lblClub.AutoSize = true;
			this.lblClub.Location = new System.Drawing.Point(11, 102);
			this.lblClub.Name = "lblClub";
			this.lblClub.Size = new System.Drawing.Size(31, 13);
			this.lblClub.TabIndex = 17;
			this.lblClub.Text = "Club:";
			// 
			// lblPreferences
			// 
			this.lblPreferences.AutoSize = true;
			this.lblPreferences.Location = new System.Drawing.Point(11, 140);
			this.lblPreferences.Name = "lblPreferences";
			this.lblPreferences.Size = new System.Drawing.Size(65, 13);
			this.lblPreferences.TabIndex = 18;
			this.lblPreferences.Text = "Voorkeuren:";
			// 
			// lblComment
			// 
			this.lblComment.AutoSize = true;
			this.lblComment.Location = new System.Drawing.Point(11, 235);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(73, 13);
			this.lblComment.TabIndex = 19;
			this.lblComment.Text = "Opmerkingen:";
			// 
			// txtClub
			// 
			this.txtClub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtClub.Location = new System.Drawing.Point(91, 98);
			this.txtClub.Name = "txtClub";
			this.txtClub.Size = new System.Drawing.Size(165, 20);
			this.txtClub.TabIndex = 4;
			this.txtClub.TextChanged += new System.EventHandler(this.entryChanged);
			// 
			// txtComment
			// 
			this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtComment.Location = new System.Drawing.Point(91, 235);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(165, 68);
			this.txtComment.TabIndex = 8;
			this.txtComment.TextChanged += new System.EventHandler(this.entryChanged);
			// 
			// btnAddPref
			// 
			this.btnAddPref.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btnAddPref.Location = new System.Drawing.Point(263, 140);
			this.btnAddPref.Name = "btnAddPref";
			this.btnAddPref.Size = new System.Drawing.Size(100, 25);
			this.btnAddPref.TabIndex = 6;
			this.btnAddPref.Text = "Toevoegen";
			this.btnAddPref.UseVisualStyleBackColor = true;
			this.btnAddPref.Click += new System.EventHandler(this.btnAddPref_Click);
			// 
			// ltbPrefs
			// 
			this.ltbPrefs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ltbPrefs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ltbPrefs.FormattingEnabled = true;
			this.ltbPrefs.Location = new System.Drawing.Point(92, 140);
			this.ltbPrefs.Name = "ltbPrefs";
			this.ltbPrefs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ltbPrefs.Size = new System.Drawing.Size(165, 67);
			this.ltbPrefs.TabIndex = 5;
			this.ltbPrefs.DataSourceChanged += new System.EventHandler(this.entryChanged);
			// 
			// btnDeletePref
			// 
			this.btnDeletePref.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btnDeletePref.Location = new System.Drawing.Point(263, 171);
			this.btnDeletePref.Name = "btnDeletePref";
			this.btnDeletePref.Size = new System.Drawing.Size(100, 25);
			this.btnDeletePref.TabIndex = 7;
			this.btnDeletePref.Text = "Verwijderen";
			this.btnDeletePref.UseVisualStyleBackColor = true;
			this.btnDeletePref.Click += new System.EventHandler(this.btnDeletePref_Click);
			// 
			// PlayerEditForm
			// 
			this.AcceptButton = this.btnAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(373, 365);
			this.Controls.Add(this.btnDeletePref);
			this.Controls.Add(this.ltbPrefs);
			this.Controls.Add(this.btnAddPref);
			this.Controls.Add(this.txtComment);
			this.Controls.Add(this.txtClub);
			this.Controls.Add(this.lblComment);
			this.Controls.Add(this.lblPreferences);
			this.Controls.Add(this.lblClub);
			this.Controls.Add(this.lblGender);
			this.Controls.Add(this.radFemale);
			this.Controls.Add(this.radMale);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PlayerEditForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Speler Toevoegen";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radFemale;
        private System.Windows.Forms.RadioButton radMale;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblGender;
		private System.Windows.Forms.Label lblClub;
		private System.Windows.Forms.Label lblPreferences;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.TextBox txtClub;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Button btnAddPref;
		private System.Windows.Forms.ListBox ltbPrefs;
		private System.Windows.Forms.Button btnDeletePref;
    }
}