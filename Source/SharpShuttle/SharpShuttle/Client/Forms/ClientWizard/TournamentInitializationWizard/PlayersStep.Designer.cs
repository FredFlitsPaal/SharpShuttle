using UserControls.NotificationControls;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
    partial class PlayersStep
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
			this.lvwPlayers = new UserControls.PlayerControls.RegisteredPlayerListView();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.lblNumberPlayers = new System.Windows.Forms.Label();
			this.lblAmount = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lvwPlayers
			// 
			this.lvwPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwPlayers.DataSource = null;
			this.lvwPlayers.FullRowSelect = true;
			this.lvwPlayers.GridLines = true;
			this.lvwPlayers.Location = new System.Drawing.Point(0, 0);
			this.lvwPlayers.Name = "lvwPlayers";
			this.lvwPlayers.ScrollPosition = 0;
			this.lvwPlayers.ShowGroups = false;
			this.lvwPlayers.Size = new System.Drawing.Size(619, 470);
			this.lvwPlayers.TabIndex = 1;
			this.lvwPlayers.UseCompatibleStateImageBehavior = false;
			this.lvwPlayers.View = System.Windows.Forms.View.Details;
			this.lvwPlayers.DoubleClick += new System.EventHandler(this.lvwPlayers_DoubleClick);
			this.lvwPlayers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwPlayers_ColumnClick);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(625, 0);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(100, 25);
			this.btnAdd.TabIndex = 2;
			this.btnAdd.Text = "Toevoegen...";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.Location = new System.Drawing.Point(625, 64);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(100, 25);
			this.btnRemove.TabIndex = 4;
			this.btnRemove.Text = "Verwijderen";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnImport
			// 
			this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnImport.Location = new System.Drawing.Point(625, 96);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(100, 25);
			this.btnImport.TabIndex = 5;
			this.btnImport.Text = "Importeren...";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.Location = new System.Drawing.Point(625, 32);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(100, 25);
			this.btnEdit.TabIndex = 3;
			this.btnEdit.Text = "Wijzigen...";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// lblNumberPlayers
			// 
			this.lblNumberPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblNumberPlayers.AutoSize = true;
			this.lblNumberPlayers.Location = new System.Drawing.Point(634, 134);
			this.lblNumberPlayers.Name = "lblNumberPlayers";
			this.lblNumberPlayers.Size = new System.Drawing.Size(78, 13);
			this.lblNumberPlayers.TabIndex = 7;
			this.lblNumberPlayers.Text = "Aantal Spelers:";
			// 
			// lblAmount
			// 
			this.lblAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblAmount.Location = new System.Drawing.Point(634, 151);
			this.lblAmount.Name = "lblAmount";
			this.lblAmount.Size = new System.Drawing.Size(78, 19);
			this.lblAmount.TabIndex = 8;
			this.lblAmount.Text = "0";
			this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PlayersStep
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblAmount);
			this.Controls.Add(this.lblNumberPlayers);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lvwPlayers);
			this.Name = "PlayersStep";
			this.Size = new System.Drawing.Size(725, 473);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private UserControls.PlayerControls.RegisteredPlayerListView lvwPlayers;
        private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Label lblNumberPlayers;
		private System.Windows.Forms.Label lblAmount;
    }
}