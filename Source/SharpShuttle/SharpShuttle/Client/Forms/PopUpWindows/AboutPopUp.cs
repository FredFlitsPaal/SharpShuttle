using System.Drawing;
using Client.Forms.PopUpWindows;

namespace Client.Forms.PopUpWindows
{
	/// <summary>
	/// Popup schermpje waar "about" informatie op staat
	/// </summary>
	public class AboutPopUp : AbstractPopUp
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public AboutPopUp() : base("Over Sharp Shuttle", Closes){}

		/// <summary>
		/// Initialiseert het AboutForm
		/// </summary>
		public override void Init()
		{
			setPanel(new AboutForm());
			BackColor = Color.White;
		}

		/// <summary>
		/// Override die altijd true returnt
		/// </summary>
		/// <returns></returns>
		public override bool confirmed()
		{
			return true;
		}
	}
}
