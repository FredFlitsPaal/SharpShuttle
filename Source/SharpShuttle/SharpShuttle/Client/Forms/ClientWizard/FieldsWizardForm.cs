using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Client.Forms.DefineFields;

namespace Client.Forms.ClientWizard
{
    public partial class FieldsWizardForm : Form
    {
        public MainForm parent;
        public FieldsWizardForm(MainForm p)
        {
            InitializeComponent();

            DefineFieldsForm df = new DefineFieldsForm(this);
            df.Location = new System.Drawing.Point(12, 12);
            this.Controls.Add(df);

            Width = df.Width + 30;
            Height = df.Height + 55;
            this.setUnResizeable();
            parent = p;


        }
        protected override void OnClosed(EventArgs e)
        {
            parent.Enabled = true;
            base.OnClosed(e);
        }

        public void switchTo(UserControl remove, UserControl add)
        {
            add.Location = new System.Drawing.Point(12, 12);
            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
            Width = add.Width + 30;
            Height = add.Height + 55;
            this.Top = (rect.Height / 2) - (this.Height / 2);
            this.Left = (rect.Width / 2) - (this.Width / 2);
            this.setUnResizeable();
            Controls.Remove(remove);
            Controls.Add(add);

        }

        public void setUnResizeable()
        {
            this.Size = new Size(Width, Height);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
    }
}

