using Client.Forms.DefineCourts;

namespace Client.Forms.PopUpWindows
{
	/// <summary>
	/// Popup waarop de gebruiker het aantal velden kan instellen
	/// </summary>
	public class DefineCourtsPopUp : AbstractPopUp
	{
		/// <summary>
		/// Het scherm waarop de aanpassingen gemaakt kunnen worden
		/// </summary>
		private DefineCourtsForm subform = new DefineCourtsForm();

		/// <summary>
		/// Default constructor
		/// </summary>
		public DefineCourtsPopUp() : base("Aantal Velden", OkCancel) { }

		/// <summary>
		/// Initialiseert het DefineCourtsForm
		/// </summary>
		public override void Init()
		{
			subform.Value = Configurations.NumberOfCourts;
			setPanel(subform);
		}

		/// <summary>
		/// Controleer of er meer dan 0 velden zijn
		/// </summary>
		/// <returns></returns>
		public override bool confirmed()
		{
			int value = subform.Value;
			if (value > 0)
			{
				Configurations.NumberOfCourts = value;
				return true;
			}
			return false;
		}
	}
}
