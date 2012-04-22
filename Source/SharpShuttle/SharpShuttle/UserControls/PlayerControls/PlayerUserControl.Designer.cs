namespace UserControls.PlayerControls
{
	partial class PlayerUserControl
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
			this.btnPlayer = new System.Windows.Forms.Button();
			this.lbPlayer = new System.Windows.Forms.Label();
			this.tbPlayer = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnPlayer
			// 
			this.btnPlayer.Location = new System.Drawing.Point(218, 3);
			this.btnPlayer.Name = "btnPlayer";
			this.btnPlayer.Size = new System.Drawing.Size(33, 23);
			this.btnPlayer.TabIndex = 2;
			this.btnPlayer.Text = "...";
			this.btnPlayer.UseVisualStyleBackColor = true;
			this.btnPlayer.Click += new System.EventHandler(this.btnPlayer_Click);
			// 
			// lbPlayer
			// 
			this.lbPlayer.AutoSize = true;
			this.lbPlayer.Location = new System.Drawing.Point(3, 8);
			this.lbPlayer.Name = "lbPlayer";
			this.lbPlayer.Size = new System.Drawing.Size(37, 13);
			this.lbPlayer.TabIndex = 1;
			this.lbPlayer.Text = "Speler";
			// 
			// tbPlayer
			// 
			this.tbPlayer.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tbPlayer.Location = new System.Drawing.Point(63, 5);
			this.tbPlayer.Name = "tbPlayer";
			this.tbPlayer.ReadOnly = true;
			this.tbPlayer.Size = new System.Drawing.Size(149, 20);
			this.tbPlayer.TabIndex = 1;
			// 
			// PlayerUserControl
			// 
			this.Controls.Add(this.tbPlayer);
			this.Controls.Add(this.lbPlayer);
			this.Controls.Add(this.btnPlayer);
			this.Name = "PlayerUserControl";
			this.Size = new System.Drawing.Size(256, 29);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnPlayer;
		private System.Windows.Forms.Label lbPlayer;
		private System.Windows.Forms.TextBox tbPlayer;

	}
}