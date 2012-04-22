namespace Client.Forms.DefineCourts
{
    partial class DefineCourtsForm
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
			this.tbCount = new System.Windows.Forms.NumericUpDown();
			this.lblFields = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.tbCount)).BeginInit();
			this.SuspendLayout();
			// 
			// tbCount
			// 
			this.tbCount.Location = new System.Drawing.Point(79, 0);
			this.tbCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.tbCount.Name = "tbCount";
			this.tbCount.Size = new System.Drawing.Size(150, 20);
			this.tbCount.TabIndex = 0;
			this.tbCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tbCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lblFields
			// 
			this.lblFields.AutoSize = true;
			this.lblFields.Location = new System.Drawing.Point(-2, 3);
			this.lblFields.Name = "lblFields";
			this.lblFields.Size = new System.Drawing.Size(75, 13);
			this.lblFields.TabIndex = 2;
			this.lblFields.Text = "Aantal velden:";
			// 
			// DefineCourtsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblFields);
			this.Controls.Add(this.tbCount);
			this.MaximumSize = new System.Drawing.Size(229, 26);
			this.MinimumSize = new System.Drawing.Size(229, 26);
			this.Name = "DefineCourtsForm";
			this.Size = new System.Drawing.Size(229, 26);
			((System.ComponentModel.ISupportInitialize)(this.tbCount)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.NumericUpDown tbCount;
		private System.Windows.Forms.Label lblFields;

    }
}