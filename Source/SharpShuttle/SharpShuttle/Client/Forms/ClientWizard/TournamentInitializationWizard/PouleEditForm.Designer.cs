namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
    partial class PouleEditForm
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
			this.cboDiscipline = new System.Windows.Forms.ComboBox();
			this.cboNiveau = new System.Windows.Forms.ComboBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.lblDiscipline = new System.Windows.Forms.Label();
			this.lblNiveau = new System.Windows.Forms.Label();
			this.txtComments = new System.Windows.Forms.TextBox();
			this.lblComments = new System.Windows.Forms.Label();
			this.btnAccept = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cboDiscipline
			// 
			this.cboDiscipline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDiscipline.FormattingEnabled = true;
			this.cboDiscipline.Location = new System.Drawing.Point(13, 78);
			this.cboDiscipline.Name = "cboDiscipline";
			this.cboDiscipline.Size = new System.Drawing.Size(121, 21);
			this.cboDiscipline.TabIndex = 1;
			this.cboDiscipline.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			// 
			// cboNiveau
			// 
			this.cboNiveau.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboNiveau.FormattingEnabled = true;
			this.cboNiveau.Location = new System.Drawing.Point(139, 78);
			this.cboNiveau.Name = "cboNiveau";
			this.cboNiveau.Size = new System.Drawing.Size(89, 21);
			this.cboNiveau.Sorted = true;
			this.cboNiveau.TabIndex = 2;
			this.cboNiveau.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(12, 30);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(216, 20);
			this.txtName.TabIndex = 0;
			this.txtName.TextChanged += new System.EventHandler(this.txtBName_TextChanged);
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(10, 14);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 3;
			this.lblName.Text = "Naam";
			// 
			// lblDiscipline
			// 
			this.lblDiscipline.AutoSize = true;
			this.lblDiscipline.Location = new System.Drawing.Point(10, 62);
			this.lblDiscipline.Name = "lblDiscipline";
			this.lblDiscipline.Size = new System.Drawing.Size(52, 13);
			this.lblDiscipline.TabIndex = 4;
			this.lblDiscipline.Text = "Discipline";
			// 
			// lblNiveau
			// 
			this.lblNiveau.AutoSize = true;
			this.lblNiveau.Location = new System.Drawing.Point(137, 62);
			this.lblNiveau.Name = "lblNiveau";
			this.lblNiveau.Size = new System.Drawing.Size(41, 13);
			this.lblNiveau.TabIndex = 5;
			this.lblNiveau.Text = "Niveau";
			// 
			// txtComments
			// 
			this.txtComments.Location = new System.Drawing.Point(13, 126);
			this.txtComments.Multiline = true;
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(215, 85);
			this.txtComments.TabIndex = 3;
			this.txtComments.TextChanged += new System.EventHandler(this.txtBComment_TextChanged);
			// 
			// lblComments
			// 
			this.lblComments.AutoSize = true;
			this.lblComments.Location = new System.Drawing.Point(11, 110);
			this.lblComments.Name = "lblComments";
			this.lblComments.Size = new System.Drawing.Size(124, 13);
			this.lblComments.TabIndex = 7;
			this.lblComments.Text = "Opmerkingen (Optioneel)";
			// 
			// btnAccept
			// 
			this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAccept.Location = new System.Drawing.Point(72, 221);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 4;
			this.btnAccept.Text = "Toevoegen";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(154, 221);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// PouleEditForm
			// 
			this.AcceptButton = this.btnAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(241, 256);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.lblComments);
			this.Controls.Add(this.txtComments);
			this.Controls.Add(this.lblNiveau);
			this.Controls.Add(this.lblDiscipline);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.cboNiveau);
			this.Controls.Add(this.cboDiscipline);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PouleEditForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Poule Toevoegen";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDiscipline;
        private System.Windows.Forms.ComboBox cboNiveau;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDiscipline;
        private System.Windows.Forms.Label lblNiveau;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
    }
}