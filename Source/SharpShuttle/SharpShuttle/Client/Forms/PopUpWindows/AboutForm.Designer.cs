namespace Client.Forms.PopUpWindows
{
	partial class AboutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.lbl1 = new System.Windows.Forms.Label();
			this.lbl2 = new System.Windows.Forms.Label();
			this.txtBInfo = new System.Windows.Forms.TextBox();
			this.lbl3 = new System.Windows.Forms.Label();
			this.txtBProgrammers = new System.Windows.Forms.TextBox();
			this.pnlWho = new System.Windows.Forms.Panel();
			this.txtBFrans = new System.Windows.Forms.TextBox();
			this.lbl4 = new System.Windows.Forms.Label();
			this.logo = new System.Windows.Forms.PictureBox();
			this.pnlWho.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl1
			// 
			this.lbl1.AutoSize = true;
			this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl1.Location = new System.Drawing.Point(-1, 100);
			this.lbl1.Name = "lbl1";
			this.lbl1.Size = new System.Drawing.Size(155, 25);
			this.lbl1.TabIndex = 0;
			this.lbl1.Text = "Sharp Shuttle";
			// 
			// lbl2
			// 
			this.lbl2.AutoSize = true;
			this.lbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl2.Location = new System.Drawing.Point(73, 129);
			this.lbl2.Name = "lbl2";
			this.lbl2.Size = new System.Drawing.Size(77, 16);
			this.lbl2.TabIndex = 1;
			this.lbl2.Text = "Versie 1.0";
			// 
			// txtBInfo
			// 
			this.txtBInfo.BackColor = System.Drawing.Color.White;
			this.txtBInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtBInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBInfo.Location = new System.Drawing.Point(19, 170);
			this.txtBInfo.Multiline = true;
			this.txtBInfo.Name = "txtBInfo";
			this.txtBInfo.ReadOnly = true;
			this.txtBInfo.Size = new System.Drawing.Size(271, 99);
			this.txtBInfo.TabIndex = 3;
			this.txtBInfo.TabStop = false;
			this.txtBInfo.Text = resources.GetString("txtBInfo.Text");
			// 
			// lbl3
			// 
			this.lbl3.AutoSize = true;
			this.lbl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl3.Location = new System.Drawing.Point(18, 5);
			this.lbl3.Name = "lbl3";
			this.lbl3.Size = new System.Drawing.Size(109, 16);
			this.lbl3.TabIndex = 4;
			this.lbl3.Text = "Ontwikkelaars:";
			// 
			// txtBProgrammers
			// 
			this.txtBProgrammers.BackColor = System.Drawing.SystemColors.Control;
			this.txtBProgrammers.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtBProgrammers.Location = new System.Drawing.Point(21, 24);
			this.txtBProgrammers.Multiline = true;
			this.txtBProgrammers.Name = "txtBProgrammers";
			this.txtBProgrammers.ReadOnly = true;
			this.txtBProgrammers.Size = new System.Drawing.Size(106, 136);
			this.txtBProgrammers.TabIndex = 5;
			this.txtBProgrammers.TabStop = false;
			this.txtBProgrammers.Text = "Maarten Abbink\r\nHarold Aptroot\r\nJalmer van de Berg\r\nKevin van Blokland\r\nRaynier v" +
				"an Dam\r\nRoy de Groot\r\nVincent Kreuzen\r\nJacob van Mourik\r\nJeffrey Resodikromo\r\nKi" +
				"m van Winden";
			// 
			// pnlWho
			// 
			this.pnlWho.BackColor = System.Drawing.SystemColors.Control;
			this.pnlWho.Controls.Add(this.txtBFrans);
			this.pnlWho.Controls.Add(this.lbl4);
			this.pnlWho.Controls.Add(this.txtBProgrammers);
			this.pnlWho.Controls.Add(this.lbl3);
			this.pnlWho.Location = new System.Drawing.Point(0, 275);
			this.pnlWho.Name = "pnlWho";
			this.pnlWho.Size = new System.Drawing.Size(300, 171);
			this.pnlWho.TabIndex = 6;
			// 
			// txtBFrans
			// 
			this.txtBFrans.BackColor = System.Drawing.SystemColors.Control;
			this.txtBFrans.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtBFrans.Location = new System.Drawing.Point(221, 140);
			this.txtBFrans.Name = "txtBFrans";
			this.txtBFrans.ReadOnly = true;
			this.txtBFrans.Size = new System.Drawing.Size(76, 13);
			this.txtBFrans.TabIndex = 7;
			this.txtBFrans.TabStop = false;
			this.txtBFrans.Text = "Frans Wiering";
			// 
			// lbl4
			// 
			this.lbl4.AutoSize = true;
			this.lbl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl4.Location = new System.Drawing.Point(186, 122);
			this.lbl4.Name = "lbl4";
			this.lbl4.Size = new System.Drawing.Size(102, 15);
			this.lbl4.TabIndex = 6;
			this.lbl4.Text = "Speciale dank:";
			// 
			// logo
			// 
			this.logo.Image = global::Client.Properties.Resources.SharpShuttle_Icon;
			this.logo.InitialImage = ((System.Drawing.Image)(resources.GetObject("logo.InitialImage")));
			this.logo.Location = new System.Drawing.Point(139, 2);
			this.logo.Name = "logo";
			this.logo.Size = new System.Drawing.Size(168, 150);
			this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.logo.TabIndex = 2;
			this.logo.TabStop = false;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnlWho);
			this.Controls.Add(this.txtBInfo);
			this.Controls.Add(this.lbl2);
			this.Controls.Add(this.lbl1);
			this.Controls.Add(this.logo);
			this.Name = "AboutForm";
			this.Size = new System.Drawing.Size(300, 446);
			this.pnlWho.ResumeLayout(false);
			this.pnlWho.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl1;
		private System.Windows.Forms.Label lbl2;
		private System.Windows.Forms.PictureBox logo;
		private System.Windows.Forms.TextBox txtBInfo;
		private System.Windows.Forms.Label lbl3;
		private System.Windows.Forms.TextBox txtBProgrammers;
		private System.Windows.Forms.Panel pnlWho;
		private System.Windows.Forms.TextBox txtBFrans;
		private System.Windows.Forms.Label lbl4;

	}
}