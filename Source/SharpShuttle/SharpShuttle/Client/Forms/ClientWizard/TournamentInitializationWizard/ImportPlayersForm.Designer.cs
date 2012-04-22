namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
    partial class ImportPlayersForm
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
			this.txtFile = new System.Windows.Forms.TextBox();
			this.btnOpenFile = new System.Windows.Forms.Button();
			this.lblFile = new System.Windows.Forms.Label();
			this.btnAccept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtFile
			// 
			this.txtFile.Location = new System.Drawing.Point(12, 33);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(209, 20);
			this.txtFile.TabIndex = 1;
			this.txtFile.TextChanged += new System.EventHandler(this.txtBFile_TextChanged);
			// 
			// btnOpenFile
			// 
			this.btnOpenFile.Location = new System.Drawing.Point(227, 31);
			this.btnOpenFile.Name = "btnOpenFile";
			this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
			this.btnOpenFile.TabIndex = 2;
			this.btnOpenFile.Text = "Bladeren...";
			this.btnOpenFile.UseVisualStyleBackColor = true;
			this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
			// 
			// lblFile
			// 
			this.lblFile.AutoSize = true;
			this.lblFile.Location = new System.Drawing.Point(9, 16);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(49, 13);
			this.lblFile.TabIndex = 2;
			this.lblFile.Text = "Bestand:";
			// 
			// btnAccept
			// 
			this.btnAccept.Enabled = false;
			this.btnAccept.Location = new System.Drawing.Point(12, 72);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 3;
			this.btnAccept.Text = "Importeren";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(227, 72);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// ImportPlayersForm
			// 
			this.AcceptButton = this.btnAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(314, 107);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.lblFile);
			this.Controls.Add(this.btnOpenFile);
			this.Controls.Add(this.txtFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportPlayersForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Inschrijvingen Importeren";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;

    }
}