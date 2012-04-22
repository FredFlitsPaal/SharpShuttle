namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	partial class FinalInitStep
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
			this.txtInfo = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// txtInfo
			// 
			this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInfo.BackColor = System.Drawing.SystemColors.Control;
			this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtInfo.Location = new System.Drawing.Point(15, 18);
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.ReadOnly = true;
			this.txtInfo.Size = new System.Drawing.Size(233, 174);
			this.txtInfo.TabIndex = 0;
			this.txtInfo.Text = "De toernooi initialisatie is nu voltooid. \n\nAls u de wizard voltooit, kunt U niet" +
				" meer de inschrijvingen of de poules veranderen. ";
			// 
			// FinalInitStep
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtInfo);
			this.Name = "FinalInitStep";
			this.Size = new System.Drawing.Size(262, 208);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox txtInfo;
	}
}