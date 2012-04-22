using System.Windows.Forms;
using System;
namespace Client.Forms.CourtInformation
{
    partial class CourtInformationForm
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
			this.components = new System.ComponentModel.Container();
			this.spcMatchFields = new System.Windows.Forms.SplitContainer();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.lvwCourts = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.lblFields = new System.Windows.Forms.Label();
			this.lvwMatches = new UserControls.MatchControls.CourtMatchListView();
			this.lblMatches = new System.Windows.Forms.Label();
			this.cmsCourts = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmsStart = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsStop = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsBack = new System.Windows.Forms.ToolStripMenuItem();
			this.courtTimer = new System.Windows.Forms.Timer(this.components);
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.spcMatchFields.Panel1.SuspendLayout();
			this.spcMatchFields.Panel2.SuspendLayout();
			this.spcMatchFields.SuspendLayout();
			this.cmsCourts.SuspendLayout();
			this.SuspendLayout();
			// 
			// spcMatchFields
			// 
			this.spcMatchFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spcMatchFields.Location = new System.Drawing.Point(0, 0);
			this.spcMatchFields.Name = "spcMatchFields";
			this.spcMatchFields.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spcMatchFields.Panel1
			// 
			this.spcMatchFields.Panel1.Controls.Add(this.btnStart);
			this.spcMatchFields.Panel1.Controls.Add(this.btnStop);
			this.spcMatchFields.Panel1.Controls.Add(this.lvwCourts);
			this.spcMatchFields.Panel1.Controls.Add(this.lblFields);
			// 
			// spcMatchFields.Panel2
			// 
			this.spcMatchFields.Panel2.Controls.Add(this.lvwMatches);
			this.spcMatchFields.Panel2.Controls.Add(this.lblMatches);
			this.spcMatchFields.Size = new System.Drawing.Size(855, 633);
			this.spcMatchFields.SplitterDistance = 342;
			this.spcMatchFields.TabIndex = 3;
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.Location = new System.Drawing.Point(630, 308);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(95, 23);
			this.btnStart.TabIndex = 2;
			this.btnStart.Text = "Start wedstrijd";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStop.Location = new System.Drawing.Point(731, 308);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(112, 23);
			this.btnStop.TabIndex = 3;
			this.btnStop.Text = "Beëindig wedstrijd";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// lvwCourts
			// 
			this.lvwCourts.AllowDrop = true;
			this.lvwCourts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwCourts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader7,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			this.lvwCourts.FullRowSelect = true;
			this.lvwCourts.GridLines = true;
			this.lvwCourts.HideSelection = false;
			this.lvwCourts.Location = new System.Drawing.Point(7, 27);
			this.lvwCourts.Name = "lvwCourts";
			this.lvwCourts.Size = new System.Drawing.Size(836, 266);
			this.lvwCourts.TabIndex = 1;
			this.lvwCourts.UseCompatibleStateImageBehavior = false;
			this.lvwCourts.View = System.Windows.Forms.View.Details;
			this.lvwCourts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwCourts_MouseClick);
			this.lvwCourts.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvCourts_DragDrop);
			this.lvwCourts.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvCourts_DragEnter);
			this.lvwCourts.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvCourts_ItemDrag);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Veld";
			this.columnHeader1.Width = 95;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Team 1";
			this.columnHeader2.Width = 185;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Team 2";
			this.columnHeader3.Width = 172;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Poule";
			this.columnHeader4.Width = 124;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Ronde";
			this.columnHeader5.Width = 48;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Tijd bezig";
			// 
			// lblFields
			// 
			this.lblFields.AutoSize = true;
			this.lblFields.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFields.Location = new System.Drawing.Point(8, 7);
			this.lblFields.Name = "lblFields";
			this.lblFields.Size = new System.Drawing.Size(63, 17);
			this.lblFields.TabIndex = 8;
			this.lblFields.Text = "Velden:";
			// 
			// lvwMatches
			// 
			this.lvwMatches.AllowDrop = true;
			this.lvwMatches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwMatches.DataSource = null;
			this.lvwMatches.FullRowSelect = true;
			this.lvwMatches.GridLines = true;
			this.lvwMatches.HideSelection = false;
			this.lvwMatches.Location = new System.Drawing.Point(7, 24);
			this.lvwMatches.MultiSelect = false;
			this.lvwMatches.Name = "lvwMatches";
			this.lvwMatches.ScrollPosition = 0;
			this.lvwMatches.Size = new System.Drawing.Size(836, 251);
			this.lvwMatches.TabIndex = 4;
			this.lvwMatches.UseCompatibleStateImageBehavior = false;
			this.lvwMatches.View = System.Windows.Forms.View.Details;
			this.lvwMatches.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvMatches_DragDrop);
			this.lvwMatches.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvMatches_DragEnter);
			this.lvwMatches.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvMatches_ItemDrag);
			// 
			// lblMatches
			// 
			this.lblMatches.AutoSize = true;
			this.lblMatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMatches.Location = new System.Drawing.Point(8, 3);
			this.lblMatches.Name = "lblMatches";
			this.lblMatches.Size = new System.Drawing.Size(99, 17);
			this.lblMatches.TabIndex = 9;
			this.lblMatches.Text = "Wedstrijden:";
			// 
			// cmsCourts
			// 
			this.cmsCourts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsStart,
            this.cmsStop,
            this.cmsBack});
			this.cmsCourts.Name = "cmsCourts";
			this.cmsCourts.Size = new System.Drawing.Size(198, 70);
			// 
			// cmsStart
			// 
			this.cmsStart.Name = "cmsStart";
			this.cmsStart.Size = new System.Drawing.Size(197, 22);
			this.cmsStart.Text = "Start wedstrijd";
			this.cmsStart.Click += new System.EventHandler(this.cmsStart_Click);
			// 
			// cmsStop
			// 
			this.cmsStop.Name = "cmsStop";
			this.cmsStop.Size = new System.Drawing.Size(197, 22);
			this.cmsStop.Text = "Beëindig wedstrijd";
			this.cmsStop.Click += new System.EventHandler(this.cmsStop_Click);
			// 
			// cmsBack
			// 
			this.cmsBack.Name = "cmsBack";
			this.cmsBack.Size = new System.Drawing.Size(197, 22);
			this.cmsBack.Text = "Terug naar Wedstrijden";
			this.cmsBack.Click += new System.EventHandler(this.cmsBack_Click);
			// 
			// courtTimer
			// 
			this.courtTimer.Interval = 1000;
			this.courtTimer.Tick += new System.EventHandler(this.courtTimer_Tick);
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Wedstrijd ID";
			this.columnHeader7.Width = 70;
			// 
			// CourtInformationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(855, 633);
			this.Controls.Add(this.spcMatchFields);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "CourtInformationForm";
			this.Text = "Veld Informatie";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CourtInformationForm_FormClosed);
			this.spcMatchFields.Panel1.ResumeLayout(false);
			this.spcMatchFields.Panel1.PerformLayout();
			this.spcMatchFields.Panel2.ResumeLayout(false);
			this.spcMatchFields.Panel2.PerformLayout();
			this.spcMatchFields.ResumeLayout(false);
			this.cmsCourts.ResumeLayout(false);
			this.ResumeLayout(false);

        }

       

        #endregion

		private System.Windows.Forms.SplitContainer spcMatchFields;
		private System.Windows.Forms.Label lblFields;
        private System.Windows.Forms.Label lblMatches;
        private System.Windows.Forms.ListView lvwCourts;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private UserControls.MatchControls.CourtMatchListView lvwMatches;
		private Button btnStop;
		private Button btnStart;
		private ColumnHeader columnHeader5;
		private ColumnHeader columnHeader6;
		private Timer courtTimer;
		private ContextMenuStrip cmsCourts;
		private ToolStripMenuItem cmsStart;
		private ToolStripMenuItem cmsStop;
		private ToolStripMenuItem cmsBack;
		private ColumnHeader columnHeader7;


    }
}