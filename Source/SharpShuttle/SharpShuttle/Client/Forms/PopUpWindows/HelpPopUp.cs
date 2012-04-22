using System.Drawing;
using Client.Forms.PopUpWindows;

namespace Client.Forms.PopUpWindows
{
	/// <summary>
	/// Popup schermpje met help
	/// </summary>
	public class HelpPopUp : AbstractPopUp
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public HelpPopUp() : base("Sharp Shuttle Help", Closes) { }

		/// <summary>
		/// Initializeert het HelpForm
		/// </summary>
		public override void Init()
		{
			setPanel(new HelpForm());
			//BackColor = Color.White;
		}

		/// <summary>
		/// override die altijd true returnt
		/// </summary>
		/// <returns></returns>
		public override bool confirmed()
		{
			return true;
		}
	}
}
