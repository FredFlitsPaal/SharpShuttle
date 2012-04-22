namespace Client.Forms.PrintDialog
{
	partial class PrintDialogForm
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
			this.grpPrintSelection = new System.Windows.Forms.GroupBox();
			this.radBoth = new System.Windows.Forms.RadioButton();
			this.radRankings = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.grpPrintSelection.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpPrintSelection
			// 
			this.grpPrintSelection.Controls.Add(this.radBoth);
			this.grpPrintSelection.Controls.Add(this.radRankings);
			this.grpPrintSelection.Location = new System.Drawing.Point(13, 13);
			this.grpPrintSelection.Name = "grpPrintSelection";
			this.grpPrintSelection.Size = new System.Drawing.Size(226, 72);
			this.grpPrintSelection.TabIndex = 0;
			this.grpPrintSelection.TabStop = false;
			this.grpPrintSelection.Text = "Print Selectie";
			// 
			// radBoth
			// 
			this.radBoth.AutoSize = true;
			this.radBoth.Location = new System.Drawing.Point(7, 43);
			this.radBoth.Name = "radBoth";
			this.radBoth.Size = new System.Drawing.Size(172, 17);
			this.radBoth.TabIndex = 2;
			this.radBoth.Text = "Tussenstanden en Wedstrijden";
			this.radBoth.UseVisualStyleBackColor = true;
			this.radBoth.CheckedChanged += new System.EventHandler(this.radioButtonBoth_CheckedChanged);
			// 
			// radRankings
			// 
			this.radRankings.AutoSize = true;
			this.radRankings.Checked = true;
			this.radRankings.Location = new System.Drawing.Point(7, 20);
			this.radRankings.Name = "radRankings";
			this.radRankings.Size = new System.Drawing.Size(98, 17);
			this.radRankings.TabIndex = 0;
			this.radRankings.TabStop = true;
			this.radRankings.Text = "Tussenstanden";
			this.radRankings.UseVisualStyleBackColor = true;
			this.radRankings.CheckedChanged += new System.EventHandler(this.radioButtonScores_CheckedChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(158, 91);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(81, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Annuleren";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnPrint.Location = new System.Drawing.Point(20, 91);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(75, 23);
			this.btnPrint.TabIndex = 2;
			this.btnPrint.Text = "Printen";
			this.btnPrint.UseVisualStyleBackColor = true;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// PrintDialogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(249, 122);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.grpPrintSelection);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PrintDialogForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ladder Printen";
			this.TopMost = true;
			this.grpPrintSelection.ResumeLayout(false);
			this.grpPrintSelection.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpPrintSelection;
		private System.Windows.Forms.RadioButton radBoth;
		private System.Windows.Forms.RadioButton radRankings;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnPrint;

	}
}