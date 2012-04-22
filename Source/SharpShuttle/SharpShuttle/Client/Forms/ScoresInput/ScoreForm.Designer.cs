namespace Client.Forms.ScoresInput
{
    partial class ScoreForm
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
			this.lvwMatches = new UserControls.MatchControls.MatchListView();
			this.btnSetScore = new System.Windows.Forms.Button();
			this.lblMatches = new System.Windows.Forms.Label();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lvwMatches
			// 
			this.lvwMatches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwMatches.DataSource = null;
			this.lvwMatches.FullRowSelect = true;
			this.lvwMatches.GridLines = true;
			this.lvwMatches.HideSelection = false;
			this.lvwMatches.Location = new System.Drawing.Point(5, 28);
			this.lvwMatches.MultiSelect = false;
			this.lvwMatches.Name = "lvwMatches";
			this.lvwMatches.ScrollPosition = 0;
			this.lvwMatches.Size = new System.Drawing.Size(755, 439);
			this.lvwMatches.TabIndex = 1;
			this.lvwMatches.UseCompatibleStateImageBehavior = false;
			this.lvwMatches.View = System.Windows.Forms.View.Details;
			this.lvwMatches.SelectedIndexChanged += new System.EventHandler(this.listVMatches_SelectedIndexChanged);
			this.lvwMatches.DoubleClick += new System.EventHandler(this.listVMatches_DoubleClick);
			this.lvwMatches.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listVMatches_ColumnClick);
			// 
			// btnSetScore
			// 
			this.btnSetScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSetScore.Location = new System.Drawing.Point(660, 473);
			this.btnSetScore.Name = "btnSetScore";
			this.btnSetScore.Size = new System.Drawing.Size(100, 23);
			this.btnSetScore.TabIndex = 2;
			this.btnSetScore.Text = "Score Invoeren";
			this.btnSetScore.UseVisualStyleBackColor = true;
			this.btnSetScore.Click += new System.EventHandler(this.btnSetScore_Click);
			// 
			// lblMatches
			// 
			this.lblMatches.AutoSize = true;
			this.lblMatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMatches.Location = new System.Drawing.Point(3, 7);
			this.lblMatches.Name = "lblMatches";
			this.lblMatches.Size = new System.Drawing.Size(99, 17);
			this.lblMatches.TabIndex = 10;
			this.lblMatches.Text = "Wedstrijden:";
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.Location = new System.Drawing.Point(610, 4);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(150, 20);
			this.txtSearch.TabIndex = 3;
			this.txtSearch.Text = "Zoeken...";
			this.txtSearch.TextChanged += new System.EventHandler(this.txtBSearch_TextChanged);
			this.txtSearch.GotFocus += new System.EventHandler(this.txtBSearch_GotFocus);
			this.txtSearch.LostFocus += new System.EventHandler(this.txtBSearch_LostFocus);
			// 
			// ScoreForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(765, 502);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.lblMatches);
			this.Controls.Add(this.btnSetScore);
			this.Controls.Add(this.lvwMatches);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ScoreForm";
			this.Text = "Wedstrijdscores Invoeren";
			this.Click += new System.EventHandler(this.scoreForm_Click);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.scoreForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button btnSetScore;
		private UserControls.MatchControls.MatchListView lvwMatches;
		private System.Windows.Forms.Label lblMatches;
		private System.Windows.Forms.TextBox txtSearch;
    }
}