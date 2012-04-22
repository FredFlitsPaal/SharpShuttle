using System;
using System.ComponentModel;
using Shared.Communication.Serials;
using Shared.Views;
using UserControls.NotificationControls;

namespace Client.Forms.ClientWizard.LoginWizard
{
	partial class SelectUserStep
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
			this.lblUserTitle = new System.Windows.Forms.Label();
			this.lblDescriptionTitle = new System.Windows.Forms.Label();
			this.ddlUsers = new UserControls.UserControls.UserComboBox();
			this.lblInfo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblUserTitle
			// 
			this.lblUserTitle.AutoSize = true;
			this.lblUserTitle.Location = new System.Drawing.Point(-3, 55);
			this.lblUserTitle.Name = "lblUserTitle";
			this.lblUserTitle.Size = new System.Drawing.Size(64, 13);
			this.lblUserTitle.TabIndex = 9;
			this.lblUserTitle.Text = "Beschrijving";
			// 
			// lblDescriptionTitle
			// 
			this.lblDescriptionTitle.AutoSize = true;
			this.lblDescriptionTitle.Location = new System.Drawing.Point(-3, 0);
			this.lblDescriptionTitle.Name = "lblDescriptionTitle";
			this.lblDescriptionTitle.Size = new System.Drawing.Size(53, 13);
			this.lblDescriptionTitle.TabIndex = 8;
			this.lblDescriptionTitle.Text = "Gebruiker";
			// 
			// ddlUsers
			// 
			this.ddlUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ddlUsers.DataSource = null;
			this.ddlUsers.DisplayMember = "Name";
			this.ddlUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlUsers.FormattingEnabled = true;
			this.ddlUsers.Location = new System.Drawing.Point(0, 16);
			this.ddlUsers.Name = "ddlUsers";
			this.ddlUsers.SelectedValue = 0;
			this.ddlUsers.Size = new System.Drawing.Size(299, 21);
			this.ddlUsers.TabIndex = 7;
			this.ddlUsers.ValueMember = "Id";
			this.ddlUsers.SelectedValueChanged += new System.EventHandler(this.ddlUsers_SelectedIndexChanged);
			// 
			// lblInfo
			// 
			this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblInfo.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblInfo.Location = new System.Drawing.Point(0, 68);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(299, 196);
			this.lblInfo.TabIndex = 6;
			// 
			// SelectUserStep
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblUserTitle);
			this.Controls.Add(this.lblDescriptionTitle);
			this.Controls.Add(this.ddlUsers);
			this.Controls.Add(this.lblInfo);
			this.Name = "SelectUserStep";
			this.Size = new System.Drawing.Size(299, 264);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.Label lblUserTitle;
		private System.Windows.Forms.Label lblDescriptionTitle;
		private UserControls.UserControls.UserComboBox ddlUsers;
		private System.Windows.Forms.Label lblInfo;
	}
}
