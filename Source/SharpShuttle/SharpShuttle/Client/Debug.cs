using Shared.Communication;
using Shared.Communication.Exceptions;
using Shared.Data;

namespace Client
{
	/// <summary>
	/// Debug
	/// </summary>
	public class Debug
	{
		/// <summary>
		/// Debug
		/// </summary>
		public Debug() { }

		/// <summary>
		/// Debug
		/// </summary>
		public void StartDebug()
		{

			System.Windows.Forms.MessageBox.Show("Starting Debug");

			if (!Communication.Instance.Connect("127.0.0.1"))
				System.Windows.Forms.MessageBox.Show("Geen verbinding");

			CommunicationException e;

			//Server Get/Set settings proberen
			var obj = DataCache.Instance.GetSettings();

			obj.Sets = 999;

			if (DataCache.Instance.SetSettings(obj, out e))
			{
				System.Windows.Forms.MessageBox.Show("Gelukt!");
			}
			else
			{
				System.Windows.Forms.MessageBox.Show(e.ToString());
			}

		}
	}
}
