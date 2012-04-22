using System;
using UserControls.ServerControls;

namespace Client.Forms.ClientWizard.LoginWizard
{
	partial class SelectServerStep
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ddlServers = new UserControls.ServerControls.ServerComboBox();
			this.lblServer = new System.Windows.Forms.Label();
			this.lblCurrentAction = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ddlServers
			// 
			this.ddlServers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ddlServers.DataSource = null;
			this.ddlServers.DisplayMember = "Address";
			this.ddlServers.FormattingEnabled = true;
			this.ddlServers.Location = new System.Drawing.Point(0, 16);
			this.ddlServers.Name = "ddlServers";
			this.ddlServers.Size = new System.Drawing.Size(211, 21);
			this.ddlServers.TabIndex = 0;
			this.ddlServers.ValueMember = "Address";
			this.ddlServers.TextChanged += new System.EventHandler(this.ddlServer_OnTextChanged);
			// 
			// lblServer
			// 
			this.lblServer.AutoSize = true;
			this.lblServer.Location = new System.Drawing.Point(-3, 0);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(38, 13);
			this.lblServer.TabIndex = 1;
			this.lblServer.Text = "Server";
			// 
			// lblCurrentAction
			// 
			this.lblCurrentAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCurrentAction.AutoSize = true;
			this.lblCurrentAction.Location = new System.Drawing.Point(29, 40);
			this.lblCurrentAction.Name = "lblCurrentAction";
			this.lblCurrentAction.Size = new System.Drawing.Size(71, 13);
			this.lblCurrentAction.TabIndex = 1;
			this.lblCurrentAction.Text = "CurrentAction";
			// 
			// SelectServerStep
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblServer);
			this.Controls.Add(this.ddlServers);
			this.Controls.Add(this.lblCurrentAction);
			this.Name = "SelectServerStep";
			this.Size = new System.Drawing.Size(211, 150);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ServerComboBox ddlServers;
		private System.Windows.Forms.Label lblServer;
		private System.Windows.Forms.Label lblCurrentAction;
	}
}
