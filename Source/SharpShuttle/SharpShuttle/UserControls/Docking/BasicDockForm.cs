using WeifenLuo.WinFormsUI.Docking;

namespace UserControls.Docking
{
	/// <summary>
	/// Superklasse voor alle forms die gedockt kunnen worden
	/// </summary>
	public class BasicDockForm : DockContent
	{
		/// <summary>
		/// Als het form gesloten wordt, zet dan de focus weer op mainform
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosed(System.EventArgs e)
		{
			DockPanel.Focus();
			base.OnClosed(e);
		}
	}
}
