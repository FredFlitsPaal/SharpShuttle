namespace Client.Forms.PopUpWindows
{
	partial class HelpForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
			this.lbl1 = new System.Windows.Forms.Label();
			this.ImageHelp = new System.Windows.Forms.PictureBox();
			this.lbl2 = new System.Windows.Forms.Label();
			this.txtBHelp = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.ImageHelp)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl1
			// 
			this.lbl1.AutoSize = true;
			this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl1.Location = new System.Drawing.Point(18, 15);
			this.lbl1.Name = "lbl1";
			this.lbl1.Size = new System.Drawing.Size(155, 25);
			this.lbl1.TabIndex = 1;
			this.lbl1.Text = "Sharp Shuttle";
			// 
			// ImageHelp
			// 
			this.ImageHelp.Image = ((System.Drawing.Image)(resources.GetObject("ImageHelp.Image")));
			this.ImageHelp.Location = new System.Drawing.Point(179, 3);
			this.ImageHelp.Name = "ImageHelp";
			this.ImageHelp.Size = new System.Drawing.Size(110, 76);
			this.ImageHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.ImageHelp.TabIndex = 0;
			this.ImageHelp.TabStop = false;
			// 
			// lbl2
			// 
			this.lbl2.AutoSize = true;
			this.lbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl2.Location = new System.Drawing.Point(112, 40);
			this.lbl2.Name = "lbl2";
			this.lbl2.Size = new System.Drawing.Size(60, 25);
			this.lbl2.TabIndex = 2;
			this.lbl2.Text = "Help";
			// 
			// txtBHelp
			// 
			this.txtBHelp.BackColor = System.Drawing.SystemColors.Control;
			this.txtBHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtBHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBHelp.Location = new System.Drawing.Point(23, 100);
			this.txtBHelp.Multiline = true;
			this.txtBHelp.Name = "txtBHelp";
			this.txtBHelp.ReadOnly = true;
			this.txtBHelp.Size = new System.Drawing.Size(266, 168);
			this.txtBHelp.TabIndex = 3;
			this.txtBHelp.TabStop = false;
			this.txtBHelp.Text = resources.GetString("txtBHelp.Text");
			// 
			// HelpForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtBHelp);
			this.Controls.Add(this.lbl1);
			this.Controls.Add(this.ImageHelp);
			this.Controls.Add(this.lbl2);
			this.Name = "HelpForm";
			this.Size = new System.Drawing.Size(300, 275);
			((System.ComponentModel.ISupportInitialize)(this.ImageHelp)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox ImageHelp;
		private System.Windows.Forms.Label lbl1;
		private System.Windows.Forms.Label lbl2;
		private System.Windows.Forms.TextBox txtBHelp;
	}
}