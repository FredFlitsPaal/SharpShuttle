namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	/// <summary>
	/// Form on een niveau aan te passen
	/// </summary>
    partial class NiveauEditForm
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
			this.lblNiveau = new System.Windows.Forms.Label();
			this.txtNiveau = new System.Windows.Forms.TextBox();
			this.btnAccept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblNiveau
			// 
			this.lblNiveau.AutoSize = true;
			this.lblNiveau.Location = new System.Drawing.Point(9, 22);
			this.lblNiveau.Name = "lblNiveau";
			this.lblNiveau.Size = new System.Drawing.Size(44, 13);
			this.lblNiveau.TabIndex = 0;
			this.lblNiveau.Text = "Niveau:";
			// 
			// txtNiveau
			// 
			this.txtNiveau.BackColor = System.Drawing.Color.Pink;
			this.txtNiveau.Location = new System.Drawing.Point(59, 19);
			this.txtNiveau.Name = "txtNiveau";
			this.txtNiveau.Size = new System.Drawing.Size(120, 20);
			this.txtNiveau.TabIndex = 1;
			this.txtNiveau.TextChanged += new System.EventHandler(this.txtBNiveau_TextChanged);
			// 
			// btnAccept
			// 
			this.btnAccept.Location = new System.Drawing.Point(12, 58);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 2;
			this.btnAccept.Text = "Toevoegen";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(104, 58);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// NiveauEditForm
			// 
			this.AcceptButton = this.btnAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(191, 93);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.txtNiveau);
			this.Controls.Add(this.lblNiveau);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NiveauEditForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Niveau Toevoegen";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNiveau;
        private System.Windows.Forms.TextBox txtNiveau;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
    }
}