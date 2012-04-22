using System;
using Shared.Views;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	partial class DefineConfigurationStep
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
			this.defineCourtsForm = new Client.Forms.DefineCourts.DefineCourtsForm();
			this.nudAmountSets = new System.Windows.Forms.NumericUpDown();
			this.lblAmountSets = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.nudAmountSets)).BeginInit();
			this.SuspendLayout();
			// 
			// defineCourtsForm
			// 
			this.defineCourtsForm.Location = new System.Drawing.Point(1, 2);
			this.defineCourtsForm.MaximumSize = new System.Drawing.Size(229, 21);
			this.defineCourtsForm.MinimumSize = new System.Drawing.Size(229, 21);
			this.defineCourtsForm.Name = "defineCourtsForm";
			this.defineCourtsForm.Size = new System.Drawing.Size(229, 21);
			this.defineCourtsForm.TabIndex = 0;
			// 
			// nudAmountSets
			// 
			this.nudAmountSets.BackColor = System.Drawing.SystemColors.Window;
			this.nudAmountSets.Location = new System.Drawing.Point(80, 28);
			this.nudAmountSets.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.nudAmountSets.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudAmountSets.Name = "nudAmountSets";
			this.nudAmountSets.ReadOnly = true;
			this.nudAmountSets.Size = new System.Drawing.Size(150, 20);
			this.nudAmountSets.TabIndex = 1;
			this.nudAmountSets.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudAmountSets.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// lblAmountSets
			// 
			this.lblAmountSets.AutoSize = true;
			this.lblAmountSets.Location = new System.Drawing.Point(-1, 30);
			this.lblAmountSets.Name = "lblAmountSets";
			this.lblAmountSets.Size = new System.Drawing.Size(62, 13);
			this.lblAmountSets.TabIndex = 2;
			this.lblAmountSets.Text = "Aantal sets:";
			// 
			// DefineConfigurationStep
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblAmountSets);
			this.Controls.Add(this.nudAmountSets);
			this.Controls.Add(this.defineCourtsForm);
			this.Name = "DefineFieldsStep";
			this.Size = new System.Drawing.Size(233, 51);
			((System.ComponentModel.ISupportInitialize)(this.nudAmountSets)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private Client.Forms.DefineCourts.DefineCourtsForm defineCourtsForm;
		private System.Windows.Forms.NumericUpDown nudAmountSets;
		private System.Windows.Forms.Label lblAmountSets;


    }
}