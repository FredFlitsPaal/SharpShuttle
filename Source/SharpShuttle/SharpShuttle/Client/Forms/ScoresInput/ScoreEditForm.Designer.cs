namespace Client.Forms.ScoresInput
{
    partial class ScoreEditForm
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnAccept = new System.Windows.Forms.Button();
			this.grpSet3 = new System.Windows.Forms.GroupBox();
			this.lblStripe3 = new System.Windows.Forms.Label();
			this.txtSet3PointsB = new System.Windows.Forms.TextBox();
			this.txtSet3PointsA = new System.Windows.Forms.TextBox();
			this.grpResult = new System.Windows.Forms.GroupBox();
			this.lblStripeEnd = new System.Windows.Forms.Label();
			this.txtSetsWonTeamB = new System.Windows.Forms.TextBox();
			this.txtSetsWonTeamA = new System.Windows.Forms.TextBox();
			this.grpMatchInformation = new System.Windows.Forms.GroupBox();
			this.txtRound = new System.Windows.Forms.TextBox();
			this.txtMatchID = new System.Windows.Forms.TextBox();
			this.txtPoule = new System.Windows.Forms.TextBox();
			this.lblRound = new System.Windows.Forms.Label();
			this.lblID = new System.Windows.Forms.Label();
			this.lblPoule = new System.Windows.Forms.Label();
			this.lblTeamB = new System.Windows.Forms.Label();
			this.lblTeamA = new System.Windows.Forms.Label();
			this.grpSet2 = new System.Windows.Forms.GroupBox();
			this.lblStripe2 = new System.Windows.Forms.Label();
			this.txtSet2PointsB = new System.Windows.Forms.TextBox();
			this.txtSet2PointsA = new System.Windows.Forms.TextBox();
			this.grpSet1 = new System.Windows.Forms.GroupBox();
			this.lblStripe1 = new System.Windows.Forms.Label();
			this.txtSet1PointsB = new System.Windows.Forms.TextBox();
			this.txtSet1PointsA = new System.Windows.Forms.TextBox();
			this.grpSet3.SuspendLayout();
			this.grpResult.SuspendLayout();
			this.grpMatchInformation.SuspendLayout();
			this.grpSet2.SuspendLayout();
			this.grpSet1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(268, 246);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnAccept
			// 
			this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAccept.Location = new System.Drawing.Point(187, 246);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new System.Drawing.Size(75, 23);
			this.btnAccept.TabIndex = 3;
			this.btnAccept.Text = "Invoeren";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new System.EventHandler(this.acceptButton_Click);
			// 
			// grpSet3
			// 
			this.grpSet3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpSet3.Controls.Add(this.lblStripe3);
			this.grpSet3.Controls.Add(this.txtSet3PointsB);
			this.grpSet3.Controls.Add(this.txtSet3PointsA);
			this.grpSet3.Location = new System.Drawing.Point(201, 112);
			this.grpSet3.Name = "grpSet3";
			this.grpSet3.Size = new System.Drawing.Size(68, 120);
			this.grpSet3.TabIndex = 2;
			this.grpSet3.TabStop = false;
			this.grpSet3.Text = "Set 3";
			// 
			// lblStripe3
			// 
			this.lblStripe3.AutoSize = true;
			this.lblStripe3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStripe3.Location = new System.Drawing.Point(28, 47);
			this.lblStripe3.Name = "lblStripe3";
			this.lblStripe3.Size = new System.Drawing.Size(14, 20);
			this.lblStripe3.TabIndex = 2;
			this.lblStripe3.Text = "-";
			// 
			// txtSet3PointsB
			// 
			this.txtSet3PointsB.BackColor = System.Drawing.Color.Pink;
			this.txtSet3PointsB.Location = new System.Drawing.Point(14, 72);
			this.txtSet3PointsB.Name = "txtSet3PointsB";
			this.txtSet3PointsB.Size = new System.Drawing.Size(40, 20);
			this.txtSet3PointsB.TabIndex = 1;
			this.txtSet3PointsB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSet3PointsB.TextChanged += new System.EventHandler(this.scoreBox_TextChanged);
			// 
			// txtSet3PointsA
			// 
			this.txtSet3PointsA.BackColor = System.Drawing.Color.Pink;
			this.txtSet3PointsA.Location = new System.Drawing.Point(14, 24);
			this.txtSet3PointsA.Name = "txtSet3PointsA";
			this.txtSet3PointsA.Size = new System.Drawing.Size(40, 20);
			this.txtSet3PointsA.TabIndex = 0;
			this.txtSet3PointsA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSet3PointsA.TextChanged += new System.EventHandler(this.scoreBox_TextChanged);
			// 
			// grpResult
			// 
			this.grpResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpResult.Controls.Add(this.lblStripeEnd);
			this.grpResult.Controls.Add(this.txtSetsWonTeamB);
			this.grpResult.Controls.Add(this.txtSetsWonTeamA);
			this.grpResult.Location = new System.Drawing.Point(275, 112);
			this.grpResult.Name = "grpResult";
			this.grpResult.Size = new System.Drawing.Size(68, 120);
			this.grpResult.TabIndex = 0;
			this.grpResult.TabStop = false;
			this.grpResult.Text = "Eindstand";
			// 
			// lblStripeEnd
			// 
			this.lblStripeEnd.AutoSize = true;
			this.lblStripeEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStripeEnd.Location = new System.Drawing.Point(29, 47);
			this.lblStripeEnd.Name = "lblStripeEnd";
			this.lblStripeEnd.Size = new System.Drawing.Size(14, 20);
			this.lblStripeEnd.TabIndex = 3;
			this.lblStripeEnd.Text = "-";
			// 
			// txtSetsWonTeamB
			// 
			this.txtSetsWonTeamB.Location = new System.Drawing.Point(16, 72);
			this.txtSetsWonTeamB.Name = "txtSetsWonTeamB";
			this.txtSetsWonTeamB.ReadOnly = true;
			this.txtSetsWonTeamB.Size = new System.Drawing.Size(38, 20);
			this.txtSetsWonTeamB.TabIndex = 1;
			this.txtSetsWonTeamB.TabStop = false;
			this.txtSetsWonTeamB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSetsWonTeamB.TextChanged += new System.EventHandler(this.scoreBox_TextChanged);
			// 
			// txtSetsWonTeamA
			// 
			this.txtSetsWonTeamA.Location = new System.Drawing.Point(16, 24);
			this.txtSetsWonTeamA.Name = "txtSetsWonTeamA";
			this.txtSetsWonTeamA.ReadOnly = true;
			this.txtSetsWonTeamA.Size = new System.Drawing.Size(38, 20);
			this.txtSetsWonTeamA.TabIndex = 0;
			this.txtSetsWonTeamA.TabStop = false;
			this.txtSetsWonTeamA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSetsWonTeamA.TextChanged += new System.EventHandler(this.scoreBox_TextChanged);
			// 
			// grpMatchInformation
			// 
			this.grpMatchInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpMatchInformation.Controls.Add(this.txtRound);
			this.grpMatchInformation.Controls.Add(this.txtMatchID);
			this.grpMatchInformation.Controls.Add(this.txtPoule);
			this.grpMatchInformation.Controls.Add(this.lblRound);
			this.grpMatchInformation.Controls.Add(this.lblID);
			this.grpMatchInformation.Controls.Add(this.lblPoule);
			this.grpMatchInformation.Location = new System.Drawing.Point(8, 8);
			this.grpMatchInformation.Name = "grpMatchInformation";
			this.grpMatchInformation.Size = new System.Drawing.Size(335, 100);
			this.grpMatchInformation.TabIndex = 8;
			this.grpMatchInformation.TabStop = false;
			this.grpMatchInformation.Text = "Wedstrijdinformatie";
			// 
			// txtRound
			// 
			this.txtRound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtRound.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtRound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRound.Location = new System.Drawing.Point(120, 72);
			this.txtRound.Name = "txtRound";
			this.txtRound.ReadOnly = true;
			this.txtRound.Size = new System.Drawing.Size(208, 13);
			this.txtRound.TabIndex = 5;
			this.txtRound.TabStop = false;
			// 
			// txtMatchID
			// 
			this.txtMatchID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtMatchID.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtMatchID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMatchID.Location = new System.Drawing.Point(120, 48);
			this.txtMatchID.Name = "txtMatchID";
			this.txtMatchID.ReadOnly = true;
			this.txtMatchID.Size = new System.Drawing.Size(208, 13);
			this.txtMatchID.TabIndex = 4;
			this.txtMatchID.TabStop = false;
			// 
			// txtPoule
			// 
			this.txtPoule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPoule.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtPoule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPoule.Location = new System.Drawing.Point(120, 24);
			this.txtPoule.Name = "txtPoule";
			this.txtPoule.ReadOnly = true;
			this.txtPoule.Size = new System.Drawing.Size(208, 13);
			this.txtPoule.TabIndex = 3;
			this.txtPoule.TabStop = false;
			// 
			// lblRound
			// 
			this.lblRound.AutoSize = true;
			this.lblRound.Location = new System.Drawing.Point(16, 72);
			this.lblRound.Name = "lblRound";
			this.lblRound.Size = new System.Drawing.Size(42, 13);
			this.lblRound.TabIndex = 2;
			this.lblRound.Text = "Ronde:";
			// 
			// lblID
			// 
			this.lblID.AutoSize = true;
			this.lblID.Location = new System.Drawing.Point(16, 48);
			this.lblID.Name = "lblID";
			this.lblID.Size = new System.Drawing.Size(65, 13);
			this.lblID.TabIndex = 1;
			this.lblID.Text = "WedstrijdID:";
			// 
			// lblPoule
			// 
			this.lblPoule.AutoSize = true;
			this.lblPoule.Location = new System.Drawing.Point(16, 24);
			this.lblPoule.Name = "lblPoule";
			this.lblPoule.Size = new System.Drawing.Size(37, 13);
			this.lblPoule.TabIndex = 0;
			this.lblPoule.Text = "Poule:";
			// 
			// lblTeamB
			// 
			this.lblTeamB.AutoSize = true;
			this.lblTeamB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTeamB.Location = new System.Drawing.Point(8, 168);
			this.lblTeamB.MaximumSize = new System.Drawing.Size(208, 48);
			this.lblTeamB.MinimumSize = new System.Drawing.Size(10, 48);
			this.lblTeamB.Name = "lblTeamB";
			this.lblTeamB.Size = new System.Drawing.Size(10, 48);
			this.lblTeamB.TabIndex = 10;
			this.lblTeamB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTeamA
			// 
			this.lblTeamA.AutoSize = true;
			this.lblTeamA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTeamA.Location = new System.Drawing.Point(8, 120);
			this.lblTeamA.MaximumSize = new System.Drawing.Size(208, 48);
			this.lblTeamA.MinimumSize = new System.Drawing.Size(10, 48);
			this.lblTeamA.Name = "lblTeamA";
			this.lblTeamA.Size = new System.Drawing.Size(10, 48);
			this.lblTeamA.TabIndex = 9;
			this.lblTeamA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grpSet2
			// 
			this.grpSet2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpSet2.Controls.Add(this.lblStripe2);
			this.grpSet2.Controls.Add(this.txtSet2PointsB);
			this.grpSet2.Controls.Add(this.txtSet2PointsA);
			this.grpSet2.Location = new System.Drawing.Point(127, 112);
			this.grpSet2.Name = "grpSet2";
			this.grpSet2.Size = new System.Drawing.Size(68, 120);
			this.grpSet2.TabIndex = 1;
			this.grpSet2.TabStop = false;
			this.grpSet2.Text = "Set 2";
			// 
			// lblStripe2
			// 
			this.lblStripe2.AutoSize = true;
			this.lblStripe2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStripe2.Location = new System.Drawing.Point(28, 47);
			this.lblStripe2.Name = "lblStripe2";
			this.lblStripe2.Size = new System.Drawing.Size(14, 20);
			this.lblStripe2.TabIndex = 2;
			this.lblStripe2.Text = "-";
			// 
			// txtSet2PointsB
			// 
			this.txtSet2PointsB.BackColor = System.Drawing.Color.Pink;
			this.txtSet2PointsB.Location = new System.Drawing.Point(14, 72);
			this.txtSet2PointsB.Name = "txtSet2PointsB";
			this.txtSet2PointsB.Size = new System.Drawing.Size(40, 20);
			this.txtSet2PointsB.TabIndex = 1;
			this.txtSet2PointsB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSet2PointsB.TextChanged += new System.EventHandler(this.scoreBox_TextChanged);
			// 
			// txtSet2PointsA
			// 
			this.txtSet2PointsA.BackColor = System.Drawing.Color.Pink;
			this.txtSet2PointsA.Location = new System.Drawing.Point(14, 24);
			this.txtSet2PointsA.Name = "txtSet2PointsA";
			this.txtSet2PointsA.Size = new System.Drawing.Size(40, 20);
			this.txtSet2PointsA.TabIndex = 0;
			this.txtSet2PointsA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSet2PointsA.TextChanged += new System.EventHandler(this.scoreBox_TextChanged);
			// 
			// grpSet1
			// 
			this.grpSet1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpSet1.Controls.Add(this.lblStripe1);
			this.grpSet1.Controls.Add(this.txtSet1PointsB);
			this.grpSet1.Controls.Add(this.txtSet1PointsA);
			this.grpSet1.Location = new System.Drawing.Point(53, 112);
			this.grpSet1.Name = "grpSet1";
			this.grpSet1.Size = new System.Drawing.Size(68, 120);
			this.grpSet1.TabIndex = 0;
			this.grpSet1.TabStop = false;
			this.grpSet1.Text = "Set 1";
			// 
			// lblStripe1
			// 
			this.lblStripe1.AutoSize = true;
			this.lblStripe1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblStripe1.Location = new System.Drawing.Point(28, 47);
			this.lblStripe1.Name = "lblStripe1";
			this.lblStripe1.Size = new System.Drawing.Size(14, 20);
			this.lblStripe1.TabIndex = 2;
			this.lblStripe1.Text = "-";
			// 
			// txtSet1PointsB
			// 
			this.txtSet1PointsB.BackColor = System.Drawing.Color.Pink;
			this.txtSet1PointsB.Location = new System.Drawing.Point(14, 72);
			this.txtSet1PointsB.Name = "txtSet1PointsB";
			this.txtSet1PointsB.Size = new System.Drawing.Size(40, 20);
			this.txtSet1PointsB.TabIndex = 1;
			this.txtSet1PointsB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSet1PointsB.TextChanged += new System.EventHandler(this.scoreBox_TextChanged);
			// 
			// txtSet1PointsA
			// 
			this.txtSet1PointsA.BackColor = System.Drawing.Color.Pink;
			this.txtSet1PointsA.Location = new System.Drawing.Point(14, 24);
			this.txtSet1PointsA.Name = "txtSet1PointsA";
			this.txtSet1PointsA.Size = new System.Drawing.Size(40, 20);
			this.txtSet1PointsA.TabIndex = 0;
			this.txtSet1PointsA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSet1PointsA.TextChanged += new System.EventHandler(this.scoreBox_TextChanged);
			// 
			// ScoreEditForm
			// 
			this.AcceptButton = this.btnAccept;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(352, 279);
			this.Controls.Add(this.grpSet1);
			this.Controls.Add(this.grpSet2);
			this.Controls.Add(this.lblTeamB);
			this.Controls.Add(this.lblTeamA);
			this.Controls.Add(this.grpMatchInformation);
			this.Controls.Add(this.grpResult);
			this.Controls.Add(this.grpSet3);
			this.Controls.Add(this.btnAccept);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ScoreEditForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Score Invoeren";
			this.grpSet3.ResumeLayout(false);
			this.grpSet3.PerformLayout();
			this.grpResult.ResumeLayout(false);
			this.grpResult.PerformLayout();
			this.grpMatchInformation.ResumeLayout(false);
			this.grpMatchInformation.PerformLayout();
			this.grpSet2.ResumeLayout(false);
			this.grpSet2.PerformLayout();
			this.grpSet1.ResumeLayout(false);
			this.grpSet1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.GroupBox grpSet3;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.Label lblStripe3;
        private System.Windows.Forms.TextBox txtSet3PointsB;
        private System.Windows.Forms.TextBox txtSet3PointsA;
        private System.Windows.Forms.TextBox txtSetsWonTeamB;
		private System.Windows.Forms.TextBox txtSetsWonTeamA;
		private System.Windows.Forms.GroupBox grpMatchInformation;
		private System.Windows.Forms.Label lblPoule;
		private System.Windows.Forms.Label lblStripeEnd;
		private System.Windows.Forms.Label lblRound;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.TextBox txtMatchID;
		private System.Windows.Forms.TextBox txtPoule;
		private System.Windows.Forms.TextBox txtRound;
		private System.Windows.Forms.Label lblTeamB;
		private System.Windows.Forms.Label lblTeamA;
		private System.Windows.Forms.GroupBox grpSet2;
		private System.Windows.Forms.Label lblStripe2;
		private System.Windows.Forms.TextBox txtSet2PointsB;
		private System.Windows.Forms.TextBox txtSet2PointsA;
		private System.Windows.Forms.GroupBox grpSet1;
		private System.Windows.Forms.Label lblStripe1;
		private System.Windows.Forms.TextBox txtSet1PointsB;
		private System.Windows.Forms.TextBox txtSet1PointsA;
    }
}