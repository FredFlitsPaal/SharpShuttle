using System;
using System.Windows.Forms;

namespace Client.Forms.PopUpWindows
{
	/// <summary>
	/// Een abstracte klasse voor Pop-up vensters
	/// </summary>
	public abstract partial class AbstractPopUp : Form
	{
		/// <summary>
		/// Kan de popup geresized worden
		/// </summary>
		protected bool resizable;
		/// <summary>
		/// Titel van de popup
		/// </summary>
		protected string title;
		/// <summary>
		/// Is de OK knop zichtbaar
		/// </summary>
		protected bool ok_visible = false;
		/// <summary>
		/// Is de Cancel knop zichtbaar
		/// </summary>
		protected bool cancel_visible = false;
		/// <summary>
		/// Is de close knop zichtbaar
		/// </summary>
		protected bool close_visible = true;
		/// <summary>
		/// De zichtbaarheid van de Close knop
		/// </summary>
		public static int Closes = 0;
		/// <summary>
		/// De zichtbaarheid van de Cancel knop
		/// </summary>
		public static int OkCancel = 1;
		/// <summary>
		/// Form die de popup maakt
		/// </summary>
		public UserControl form;

		/// <summary>
		/// Constructor die een titel 
		/// </summary>
		/// <param name="title"></param>
		/// <param name="buttonVisibility"></param>
		protected AbstractPopUp(string title, int buttonVisibility)
		{
			InitializeComponent();
			Resizable = false;
			Title = title;
			setButtons(buttonVisibility);
			setVisibleButtons();
			Init();
			setSize();
		}

		/// <summary>
		/// Zet de juiste grootte in
		/// </summary>
		private void setSize()
		{
			Size = Size - popUpPanel.Size + form.Size;
		}
		
		/// <summary>
		/// Bepaal welke knoppen zichtbaar zijn
		/// </summary>
		/// <param name="buttonVisibility"></param>
		private void setButtons(int buttonVisibility)
		{
			ok_visible = false;
			cancel_visible = false;
			close_visible = false;

			if (buttonVisibility == Closes)
			{
				close_visible = true;
			}
			else if (buttonVisibility == OkCancel)
			{
				ok_visible = true;
				cancel_visible = true;
			}
		}

		/// <summary>
		/// Maak knoppen zichtbaar
		/// </summary>
		private void setVisibleButtons()
		{
			btnOk.Visible = ok_visible;
			btnCancel.Visible = cancel_visible;
			btnClose.Visible = close_visible;
		}

		/// <summary>
		/// Initialiseer de popup
		/// </summary>
		public abstract void Init();
		/// <summary>
		/// Is de popup bevestigd
		/// </summary>
		/// <returns></returns>
		public abstract bool confirmed();

		/// <summary>
		/// Titel van de popup
		/// </summary>
		public string Title
		{
			get { return title; }
			set 
			{ 
				title = value;
				Text = value;
			}
		}

		/// <summary>
		/// Kan de popup geresized worden
		/// </summary>
		public bool Resizable
		{
			get { return resizable; }
			set 
			{
				resizable = value;
				FormBorderStyle = value ? FormBorderStyle.SizableToolWindow : FormBorderStyle.FixedToolWindow;
			}
		}

		/// <summary>
		/// Set het paneel 
		/// </summary>
		/// <param name="usercontrol"></param>
		public void setPanel(UserControl usercontrol)
		{
			form = usercontrol;
			popUpPanel.Controls.Add(usercontrol);
		}

		/// <summary>
		/// Als de gebruiker op ok klikt, sluit dan na bevestiging de popup
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOk_Click(object sender, EventArgs e)
		{
			if (confirmed())
				Close();
		}

		/// <summary>
		/// Als de gebruiker op cancel klikt, sluit de popup
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Als de gebruiker op close klikt, sluit de popup
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

	}
}
