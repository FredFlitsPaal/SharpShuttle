namespace Client.Forms.AddPoule
{
	partial class AddPouleForm
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
			this.components = new System.ComponentModel.Container();
			this.txtNiveau = new System.Windows.Forms.TextBox();
			this.lvwPoulePlayers = new UserControls.PlayerControls.RegisteredPlayerListView();
			this.btnAddPlayers = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtComments = new System.Windows.Forms.RichTextBox();
			this.lblPouleCorrectness = new System.Windows.Forms.Label();
			this.cboDisciplines = new UserControls.PouleControls.PouleComboBox();
			this.lblName = new System.Windows.Forms.Label();
			this.lblNiveau = new System.Windows.Forms.Label();
			this.lblDiscipline = new System.Windows.Forms.Label();
			this.lblComments = new System.Windows.Forms.Label();
			this.grpPoule = new System.Windows.Forms.GroupBox();
			this.lblNameCorrect = new System.Windows.Forms.Label();
			this.grpPoulePlayers = new System.Windows.Forms.GroupBox();
			this.cmsPlayerList = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmsItemRemovePlayer = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsItemRemovePlayers = new System.Windows.Forms.ToolStripMenuItem();
			this.lblAmount = new System.Windows.Forms.Label();
			this.lblAmountPlayers = new System.Windows.Forms.Label();
			this.grpPoule.SuspendLayout();
			this.grpPoulePlayers.SuspendLayout();
			this.cmsPlayerList.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtNiveau
			// 
			this.txtNiveau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtNiveau.Location = new System.Drawing.Point(198, 77);
			this.txtNiveau.Name = "txtNiveau";
			this.txtNiveau.Size = new System.Drawing.Size(100, 20);
			this.txtNiveau.TabIndex = 3;
			this.txtNiveau.TextChanged += new System.EventHandler(this.txtNiveau_TextChanged);
			// 
			// lvwPoulePlayers
			// 
			this.lvwPoulePlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwPoulePlayers.DataSource = null;
			this.lvwPoulePlayers.FullRowSelect = true;
			this.lvwPoulePlayers.GridLines = true;
			this.lvwPoulePlayers.Location = new System.Drawing.Point(9, 25);
			this.lvwPoulePlayers.Name = "lvwPoulePlayers";
			this.lvwPoulePlayers.ScrollPosition = 0;
			this.lvwPoulePlayers.Size = new System.Drawing.Size(620, 213);
			this.lvwPoulePlayers.TabIndex = 1;
			this.lvwPoulePlayers.UseCompatibleStateImageBehavior = false;
			this.lvwPoulePlayers.View = System.Windows.Forms.View.Details;
			this.lvwPoulePlayers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwPoulePlayers_MouseClick);
			this.lvwPoulePlayers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwPoulePlayers_ColumnClick);
			// 
			// btnAddPlayers
			// 
			this.btnAddPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddPlayers.Location = new System.Drawing.Point(509, 244);
			this.btnAddPlayers.Name = "btnAddPlayers";
			this.btnAddPlayers.Size = new System.Drawing.Size(120, 23);
			this.btnAddPlayers.TabIndex = 2;
			this.btnAddPlayers.Text = "Voeg spelers toe";
			this.btnAddPlayers.UseVisualStyleBackColor = true;
			this.btnAddPlayers.Click += new System.EventHandler(this.btnAddPlayers_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(470, 444);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(571, 444);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(9, 37);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(201, 20);
			this.txtName.TabIndex = 1;
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// txtComments
			// 
			this.txtComments.Location = new System.Drawing.Point(313, 40);
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(253, 57);
			this.txtComments.TabIndex = 4;
			this.txtComments.Text = "";
			// 
			// lblPouleCorrectness
			// 
			this.lblPouleCorrectness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPouleCorrectness.AutoSize = true;
			this.lblPouleCorrectness.Location = new System.Drawing.Point(6, 261);
			this.lblPouleCorrectness.Name = "lblPouleCorrectness";
			this.lblPouleCorrectness.Size = new System.Drawing.Size(91, 13);
			this.lblPouleCorrectness.TabIndex = 10;
			this.lblPouleCorrectness.Text = "poule correctness";
			// 
			// cboDisciplines
			// 
			this.cboDisciplines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cboDisciplines.DataSource = null;
			this.cboDisciplines.DisplayMember = "Name";
			this.cboDisciplines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDisciplines.FormattingEnabled = true;
			this.cboDisciplines.Location = new System.Drawing.Point(9, 77);
			this.cboDisciplines.Name = "cboDisciplines";
			this.cboDisciplines.SelectedValue = 0;
			this.cboDisciplines.Size = new System.Drawing.Size(180, 21);
			this.cboDisciplines.TabIndex = 2;
			this.cboDisciplines.ValueMember = "Id";
			this.cboDisciplines.SelectedIndexChanged += new System.EventHandler(this.cbxDisciplines_SelectedIndexChanged);
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(6, 21);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(35, 13);
			this.lblName.TabIndex = 12;
			this.lblName.Text = "Naam";
			// 
			// lblNiveau
			// 
			this.lblNiveau.AutoSize = true;
			this.lblNiveau.Location = new System.Drawing.Point(195, 61);
			this.lblNiveau.Name = "lblNiveau";
			this.lblNiveau.Size = new System.Drawing.Size(41, 13);
			this.lblNiveau.TabIndex = 13;
			this.lblNiveau.Text = "Niveau";
			// 
			// lblDiscipline
			// 
			this.lblDiscipline.AutoSize = true;
			this.lblDiscipline.Location = new System.Drawing.Point(6, 60);
			this.lblDiscipline.Name = "lblDiscipline";
			this.lblDiscipline.Size = new System.Drawing.Size(52, 13);
			this.lblDiscipline.TabIndex = 14;
			this.lblDiscipline.Text = "Discipline";
			// 
			// lblComments
			// 
			this.lblComments.AutoSize = true;
			this.lblComments.Location = new System.Drawing.Point(313, 21);
			this.lblComments.Name = "lblComments";
			this.lblComments.Size = new System.Drawing.Size(124, 13);
			this.lblComments.TabIndex = 15;
			this.lblComments.Text = "Opmerkingen (Optioneel)";
			// 
			// grpPoule
			// 
			this.grpPoule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpPoule.Controls.Add(this.lblNameCorrect);
			this.grpPoule.Controls.Add(this.lblName);
			this.grpPoule.Controls.Add(this.lblComments);
			this.grpPoule.Controls.Add(this.txtNiveau);
			this.grpPoule.Controls.Add(this.lblDiscipline);
			this.grpPoule.Controls.Add(this.txtName);
			this.grpPoule.Controls.Add(this.lblNiveau);
			this.grpPoule.Controls.Add(this.txtComments);
			this.grpPoule.Controls.Add(this.cboDisciplines);
			this.grpPoule.Location = new System.Drawing.Point(12, 12);
			this.grpPoule.Name = "grpPoule";
			this.grpPoule.Size = new System.Drawing.Size(635, 135);
			this.grpPoule.TabIndex = 1;
			this.grpPoule.TabStop = false;
			this.grpPoule.Text = "Poule";
			// 
			// lblNameCorrect
			// 
			this.lblNameCorrect.AutoSize = true;
			this.lblNameCorrect.Location = new System.Drawing.Point(9, 109);
			this.lblNameCorrect.Name = "lblNameCorrect";
			this.lblNameCorrect.Size = new System.Drawing.Size(91, 13);
			this.lblNameCorrect.TabIndex = 16;
			this.lblNameCorrect.Text = "name correctness";
			// 
			// grpPoulePlayers
			// 
			this.grpPoulePlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpPoulePlayers.Controls.Add(this.lblAmount);
			this.grpPoulePlayers.Controls.Add(this.lblAmountPlayers);
			this.grpPoulePlayers.Controls.Add(this.lvwPoulePlayers);
			this.grpPoulePlayers.Controls.Add(this.btnAddPlayers);
			this.grpPoulePlayers.Controls.Add(this.lblPouleCorrectness);
			this.grpPoulePlayers.Location = new System.Drawing.Point(12, 153);
			this.grpPoulePlayers.Name = "grpPoulePlayers";
			this.grpPoulePlayers.Size = new System.Drawing.Size(635, 285);
			this.grpPoulePlayers.TabIndex = 2;
			this.grpPoulePlayers.TabStop = false;
			this.grpPoulePlayers.Text = "Poule Spelers";
			// 
			// cmsPlayerList
			// 
			this.cmsPlayerList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsItemRemovePlayer,
            this.cmsItemRemovePlayers});
			this.cmsPlayerList.Name = "contextMenuStrip1";
			this.cmsPlayerList.Size = new System.Drawing.Size(163, 48);
			// 
			// cmsItemRemovePlayer
			// 
			this.cmsItemRemovePlayer.Name = "cmsItemRemovePlayer";
			this.cmsItemRemovePlayer.Size = new System.Drawing.Size(162, 22);
			this.cmsItemRemovePlayer.Text = "Verwijder speler";
			this.cmsItemRemovePlayer.Click += new System.EventHandler(this.verwijderSpelerToolStripMenuItem_Click);
			// 
			// cmsItemRemovePlayers
			// 
			this.cmsItemRemovePlayers.Name = "cmsItemRemovePlayers";
			this.cmsItemRemovePlayers.Size = new System.Drawing.Size(162, 22);
			this.cmsItemRemovePlayers.Text = "Verwijder spelers";
			this.cmsItemRemovePlayers.Click += new System.EventHandler(this.cmsItemRemovePlayers_Click);
			// 
			// lblAmount
			// 
			this.lblAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblAmount.AutoSize = true;
			this.lblAmount.Location = new System.Drawing.Point(127, 241);
			this.lblAmount.Name = "lblAmount";
			this.lblAmount.Size = new System.Drawing.Size(13, 13);
			this.lblAmount.TabIndex = 11;
			this.lblAmount.Text = "0";
			// 
			// lblAmountPlayers
			// 
			this.lblAmountPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblAmountPlayers.AutoSize = true;
			this.lblAmountPlayers.Location = new System.Drawing.Point(6, 241);
			this.lblAmountPlayers.Name = "lblAmountPlayers";
			this.lblAmountPlayers.Size = new System.Drawing.Size(125, 13);
			this.lblAmountPlayers.TabIndex = 12;
			this.lblAmountPlayers.Text = "Aantal spelers ingedeeld:";
			// 
			// AddPouleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 474);
			this.Controls.Add(this.grpPoulePlayers);
			this.Controls.Add(this.grpPoule);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.MinimizeBox = false;
			this.Name = "AddPouleForm";
			this.Text = "Poule Toevoegen";
			this.grpPoule.ResumeLayout(false);
			this.grpPoule.PerformLayout();
			this.grpPoulePlayers.ResumeLayout(false);
			this.grpPoulePlayers.PerformLayout();
			this.cmsPlayerList.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtNiveau;
		private UserControls.PlayerControls.RegisteredPlayerListView lvwPoulePlayers;
		private System.Windows.Forms.Button btnAddPlayers;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.RichTextBox txtComments;
		private System.Windows.Forms.Label lblPouleCorrectness;
		private UserControls.PouleControls.PouleComboBox cboDisciplines;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblNiveau;
		private System.Windows.Forms.Label lblDiscipline;
		private System.Windows.Forms.Label lblComments;
		private System.Windows.Forms.GroupBox grpPoule;
		private System.Windows.Forms.GroupBox grpPoulePlayers;
		private System.Windows.Forms.ContextMenuStrip cmsPlayerList;
		private System.Windows.Forms.ToolStripMenuItem cmsItemRemovePlayer;
		private System.Windows.Forms.Label lblNameCorrect;
		private System.Windows.Forms.ToolStripMenuItem cmsItemRemovePlayers;
		private System.Windows.Forms.Label lblAmountPlayers;
		private System.Windows.Forms.Label lblAmount;

	}
}