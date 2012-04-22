using System.Windows.Forms;

namespace UserControls.AbstractWizard
{
    abstract partial class AbstractWizardForm<CompleteInputType>
		where CompleteInputType : class
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
			this.navigationPanel = new System.Windows.Forms.Panel();
			this.btnFinish = new System.Windows.Forms.Button();
			this.btnPrevious = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.stepPanel = new System.Windows.Forms.Panel();
			this.navigationPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// navigationPanel
			// 
			this.navigationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.navigationPanel.Controls.Add(this.btnFinish);
			this.navigationPanel.Controls.Add(this.btnPrevious);
			this.navigationPanel.Controls.Add(this.btnNext);
			this.navigationPanel.Location = new System.Drawing.Point(12, 256);
			this.navigationPanel.Name = "navigationPanel";
			this.navigationPanel.Size = new System.Drawing.Size(309, 22);
			this.navigationPanel.TabIndex = 2;
			// 
			// btnFinish
			// 
			this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFinish.Location = new System.Drawing.Point(235, 0);
			this.btnFinish.Name = "btnFinish";
			this.btnFinish.Size = new System.Drawing.Size(75, 23);
			this.btnFinish.TabIndex = 102;
			this.btnFinish.Text = "Voltooien";
			this.btnFinish.UseVisualStyleBackColor = true;
			this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
			// 
			// btnPrevious
			// 
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrevious.Location = new System.Drawing.Point(0, 0);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(75, 23);
			this.btnPrevious.TabIndex = 100;
			this.btnPrevious.Text = "Vorige";
			this.btnPrevious.UseVisualStyleBackColor = true;
			this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
			// 
			// btnNext
			// 
			this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNext.Location = new System.Drawing.Point(235, 0);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(75, 23);
			this.btnNext.TabIndex = 101;
			this.btnNext.Text = "Volgende";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// stepPanel
			// 
			this.stepPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.stepPanel.Location = new System.Drawing.Point(12, 13);
			this.stepPanel.Name = "stepPanel";
			this.stepPanel.Size = new System.Drawing.Size(309, 237);
			this.stepPanel.TabIndex = 1;
			// 
			// AbstractWizardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(333, 290);
			this.Controls.Add(this.stepPanel);
			this.Controls.Add(this.navigationPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AbstractWizardForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AbstractWizardForm";
			this.navigationPanel.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private Panel navigationPanel;
		protected Button btnNext;
		private Button btnFinish;
		private Button btnPrevious;
		private Panel stepPanel;
    }
}