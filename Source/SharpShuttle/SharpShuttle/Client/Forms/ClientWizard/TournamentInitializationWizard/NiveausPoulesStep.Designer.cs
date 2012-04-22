using UserControls.NotificationControls;
namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	public partial class NiveausPoulesStep
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
			this.tabBox = new System.Windows.Forms.TabControl();
			this.tbpNiveau = new System.Windows.Forms.TabPage();
			this.ltbNiveau = new System.Windows.Forms.ListBox();
			this.btnChangeNiveau = new System.Windows.Forms.Button();
			this.btnDeleteNiveau = new System.Windows.Forms.Button();
			this.btnAddNiveau = new System.Windows.Forms.Button();
			this.tbpPoules = new System.Windows.Forms.TabPage();
			this.lvwPoules = new UserControls.PouleControls.PouleListView();
			this.btnDeletePoule = new System.Windows.Forms.Button();
			this.btnChangePoule = new System.Windows.Forms.Button();
			this.btnAddPoule = new System.Windows.Forms.Button();
			this.tabBox.SuspendLayout();
			this.tbpNiveau.SuspendLayout();
			this.tbpPoules.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabBox
			// 
			this.tabBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabBox.Controls.Add(this.tbpNiveau);
			this.tabBox.Controls.Add(this.tbpPoules);
			this.tabBox.Location = new System.Drawing.Point(1, 1);
			this.tabBox.Name = "tabBox";
			this.tabBox.SelectedIndex = 0;
			this.tabBox.Size = new System.Drawing.Size(445, 328);
			this.tabBox.TabIndex = 5;
			// 
			// tbpNiveau
			// 
			this.tbpNiveau.Controls.Add(this.ltbNiveau);
			this.tbpNiveau.Controls.Add(this.btnChangeNiveau);
			this.tbpNiveau.Controls.Add(this.btnDeleteNiveau);
			this.tbpNiveau.Controls.Add(this.btnAddNiveau);
			this.tbpNiveau.Location = new System.Drawing.Point(4, 22);
			this.tbpNiveau.Name = "tbpNiveau";
			this.tbpNiveau.Padding = new System.Windows.Forms.Padding(3);
			this.tbpNiveau.Size = new System.Drawing.Size(437, 302);
			this.tbpNiveau.TabIndex = 0;
			this.tbpNiveau.Text = "Niveaus";
			this.tbpNiveau.UseVisualStyleBackColor = true;
			// 
			// ltbNiveau
			// 
			this.ltbNiveau.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ltbNiveau.FormattingEnabled = true;
			this.ltbNiveau.Location = new System.Drawing.Point(10, 14);
			this.ltbNiveau.Name = "ltbNiveau";
			this.ltbNiveau.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ltbNiveau.Size = new System.Drawing.Size(318, 277);
			this.ltbNiveau.Sorted = true;
			this.ltbNiveau.TabIndex = 4;
			this.ltbNiveau.DoubleClick += new System.EventHandler(this.changeNiveauButton_Click);
			// 
			// btnChangeNiveau
			// 
			this.btnChangeNiveau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnChangeNiveau.Enabled = false;
			this.btnChangeNiveau.Location = new System.Drawing.Point(334, 43);
			this.btnChangeNiveau.Name = "btnChangeNiveau";
			this.btnChangeNiveau.Size = new System.Drawing.Size(100, 25);
			this.btnChangeNiveau.TabIndex = 2;
			this.btnChangeNiveau.Text = "Wijzigen...";
			this.btnChangeNiveau.UseVisualStyleBackColor = true;
			this.btnChangeNiveau.Click += new System.EventHandler(this.changeNiveauButton_Click);
			// 
			// btnDeleteNiveau
			// 
			this.btnDeleteNiveau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteNiveau.Enabled = false;
			this.btnDeleteNiveau.Location = new System.Drawing.Point(334, 74);
			this.btnDeleteNiveau.Name = "btnDeleteNiveau";
			this.btnDeleteNiveau.Size = new System.Drawing.Size(100, 25);
			this.btnDeleteNiveau.TabIndex = 3;
			this.btnDeleteNiveau.Text = "Verwijderen";
			this.btnDeleteNiveau.UseVisualStyleBackColor = true;
			this.btnDeleteNiveau.Click += new System.EventHandler(this.removeNiveauButton_Click);
			// 
			// btnAddNiveau
			// 
			this.btnAddNiveau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddNiveau.Location = new System.Drawing.Point(334, 14);
			this.btnAddNiveau.Name = "btnAddNiveau";
			this.btnAddNiveau.Size = new System.Drawing.Size(100, 25);
			this.btnAddNiveau.TabIndex = 1;
			this.btnAddNiveau.Text = "Toevoegen...";
			this.btnAddNiveau.UseVisualStyleBackColor = true;
			this.btnAddNiveau.Click += new System.EventHandler(this.addNiveauButton_Click);
			// 
			// tbpPoules
			// 
			this.tbpPoules.Controls.Add(this.lvwPoules);
			this.tbpPoules.Controls.Add(this.btnDeletePoule);
			this.tbpPoules.Controls.Add(this.btnChangePoule);
			this.tbpPoules.Controls.Add(this.btnAddPoule);
			this.tbpPoules.Location = new System.Drawing.Point(4, 22);
			this.tbpPoules.Name = "tbpPoules";
			this.tbpPoules.Padding = new System.Windows.Forms.Padding(3);
			this.tbpPoules.Size = new System.Drawing.Size(437, 302);
			this.tbpPoules.TabIndex = 1;
			this.tbpPoules.Text = "Poules";
			this.tbpPoules.UseVisualStyleBackColor = true;
			// 
			// lvwPoules
			// 
			this.lvwPoules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwPoules.DataSource = null;
			this.lvwPoules.FullRowSelect = true;
			this.lvwPoules.GridLines = true;
			this.lvwPoules.HideSelection = false;
			this.lvwPoules.Location = new System.Drawing.Point(12, 14);
			this.lvwPoules.Name = "lvwPoules";
			this.lvwPoules.ScrollPosition = 0;
			this.lvwPoules.Size = new System.Drawing.Size(417, 240);
			this.lvwPoules.TabIndex = 4;
			this.lvwPoules.UseCompatibleStateImageBehavior = false;
			this.lvwPoules.View = System.Windows.Forms.View.Details;
			this.lvwPoules.DoubleClick += new System.EventHandler(this.changePouleButton_Click);
			this.lvwPoules.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.pouleList_ColumnClick);
			// 
			// btnDeletePoule
			// 
			this.btnDeletePoule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDeletePoule.Enabled = false;
			this.btnDeletePoule.Location = new System.Drawing.Point(224, 266);
			this.btnDeletePoule.Name = "btnDeletePoule";
			this.btnDeletePoule.Size = new System.Drawing.Size(100, 25);
			this.btnDeletePoule.TabIndex = 3;
			this.btnDeletePoule.Text = "Verwijderen";
			this.btnDeletePoule.UseVisualStyleBackColor = true;
			this.btnDeletePoule.Click += new System.EventHandler(this.removePouleButton_Click);
			// 
			// btnChangePoule
			// 
			this.btnChangePoule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnChangePoule.Enabled = false;
			this.btnChangePoule.Location = new System.Drawing.Point(118, 266);
			this.btnChangePoule.Name = "btnChangePoule";
			this.btnChangePoule.Size = new System.Drawing.Size(100, 25);
			this.btnChangePoule.TabIndex = 2;
			this.btnChangePoule.Text = "Wijzigen...";
			this.btnChangePoule.UseVisualStyleBackColor = true;
			this.btnChangePoule.Click += new System.EventHandler(this.changePouleButton_Click);
			// 
			// btnAddPoule
			// 
			this.btnAddPoule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAddPoule.Enabled = false;
			this.btnAddPoule.Location = new System.Drawing.Point(12, 266);
			this.btnAddPoule.Name = "btnAddPoule";
			this.btnAddPoule.Size = new System.Drawing.Size(100, 25);
			this.btnAddPoule.TabIndex = 1;
			this.btnAddPoule.Text = "Toevoegen...";
			this.btnAddPoule.UseVisualStyleBackColor = true;
			this.btnAddPoule.Click += new System.EventHandler(this.addPouleButton_Click);
			// 
			// NiveausPoulesStep
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabBox);
			this.Name = "NiveausPoulesStep";
			this.Size = new System.Drawing.Size(446, 330);
			this.tabBox.ResumeLayout(false);
			this.tbpNiveau.ResumeLayout(false);
			this.tbpPoules.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabBox;
        private System.Windows.Forms.TabPage tbpNiveau;
        private System.Windows.Forms.TabPage tbpPoules;
        private System.Windows.Forms.Button btnDeleteNiveau;
        private System.Windows.Forms.Button btnAddNiveau;
        private System.Windows.Forms.Button btnDeletePoule;
        private System.Windows.Forms.Button btnChangePoule;
        private System.Windows.Forms.Button btnAddPoule;
        private UserControls.PouleControls.PouleListView lvwPoules;
        private System.Windows.Forms.Button btnChangeNiveau;
		private System.Windows.Forms.ListBox ltbNiveau;
    }
}