namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
    partial class RegisteredPlayersStep
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
			this.ListView_Contestants = new UserControls.PlayerControls.RegisteredPlayerListView();
			this.Button_Add = new System.Windows.Forms.Button();
			this.Button_Remove = new System.Windows.Forms.Button();
			this.Button_Import = new System.Windows.Forms.Button();
			this.Button_Edit = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ListView_Contestants
			// 
			this.ListView_Contestants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ListView_Contestants.DataSource = null;
			this.ListView_Contestants.FullRowSelect = true;
			this.ListView_Contestants.GridLines = true;
			this.ListView_Contestants.Location = new System.Drawing.Point(0, 0);
			this.ListView_Contestants.Name = "ListView_Contestants";
			this.ListView_Contestants.Size = new System.Drawing.Size(619, 475);
			this.ListView_Contestants.TabIndex = 0;
			this.ListView_Contestants.UseCompatibleStateImageBehavior = false;
			this.ListView_Contestants.View = System.Windows.Forms.View.Details;
			// 
			// Button_Add
			// 
			this.Button_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Button_Add.Location = new System.Drawing.Point(625, 0);
			this.Button_Add.Name = "Button_Add";
			this.Button_Add.Size = new System.Drawing.Size(101, 26);
			this.Button_Add.TabIndex = 1;
			this.Button_Add.Text = "Toevoegen...";
			this.Button_Add.UseVisualStyleBackColor = true;
			this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
			// 
			// Button_Remove
			// 
			this.Button_Remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Button_Remove.Location = new System.Drawing.Point(625, 64);
			this.Button_Remove.Name = "Button_Remove";
			this.Button_Remove.Size = new System.Drawing.Size(101, 26);
			this.Button_Remove.TabIndex = 2;
			this.Button_Remove.Text = "Verwijderen";
			this.Button_Remove.UseVisualStyleBackColor = true;
			this.Button_Remove.Click += new System.EventHandler(this.Button_Remove_Click);
			// 
			// Button_Import
			// 
			this.Button_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Button_Import.Location = new System.Drawing.Point(625, 96);
			this.Button_Import.Name = "Button_Import";
			this.Button_Import.Size = new System.Drawing.Size(101, 26);
			this.Button_Import.TabIndex = 5;
			this.Button_Import.Text = "Importeren...";
			this.Button_Import.UseVisualStyleBackColor = true;
			this.Button_Import.Click += new System.EventHandler(this.Button_Import_Click);
			// 
			// Button_Edit
			// 
			this.Button_Edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Button_Edit.Location = new System.Drawing.Point(625, 32);
			this.Button_Edit.Name = "Button_Edit";
			this.Button_Edit.Size = new System.Drawing.Size(101, 26);
			this.Button_Edit.TabIndex = 6;
			this.Button_Edit.Text = "Wijzigen...";
			this.Button_Edit.UseVisualStyleBackColor = true;
			this.Button_Edit.Click += new System.EventHandler(this.Button_Edit_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(637, 452);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 8;
			this.btnSave.Text = "Opslaan";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// RegisteredContestantsStep
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.Button_Edit);
			this.Controls.Add(this.Button_Import);
			this.Controls.Add(this.Button_Remove);
			this.Controls.Add(this.Button_Add);
			this.Controls.Add(this.ListView_Contestants);
			this.Name = "RegisteredContestantsStep";
			this.Size = new System.Drawing.Size(725, 478);
			this.ResumeLayout(false);

        }

        #endregion

        private UserControls.PlayerControls.RegisteredPlayerListView ListView_Contestants;
        private System.Windows.Forms.Button Button_Add;
		private System.Windows.Forms.Button Button_Remove;
        private System.Windows.Forms.Button Button_Import;
        private System.Windows.Forms.Button Button_Edit;
		private System.Windows.Forms.Button btnSave;
    }
}