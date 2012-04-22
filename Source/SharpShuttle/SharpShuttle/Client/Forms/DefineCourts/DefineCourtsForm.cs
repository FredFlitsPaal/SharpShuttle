using System.Windows.Forms;

namespace Client.Forms.DefineCourts
{
	/// <summary>
	/// Scherm waarop het aantal velden gedefinieerd kan worden
	/// </summary>
    public partial class DefineCourtsForm : UserControl
    {

		/// <summary>
		/// Default constructor
		/// </summary>
        public DefineCourtsForm()
        {
            InitializeComponent();
        }

		/// <summary>
		/// Het aantal velden
		/// </summary>
		public int Value
		{
			get { return (int)tbCount.Value; }
			set { tbCount.Value = value; }
		}
    }
}
