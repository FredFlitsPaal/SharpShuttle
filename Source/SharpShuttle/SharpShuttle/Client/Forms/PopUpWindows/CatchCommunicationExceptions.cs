using System.Windows.Forms;
using Shared.Communication;
using Shared.Communication.Exceptions;

namespace Client.Forms.PopUpWindows
{
	/// <summary>
	/// Klasse die alle Communication Exceptions afhandelt
	/// </summary>
	class CatchCommunicationExceptions
	{
		/// <summary>
		/// 
		/// </summary>
		public enum ActionResult
		{
			/// <summary>
			/// Niks doen
			/// </summary>
			Nothing = 0,
			/// <summary>
			/// Reload de data
			/// </summary>
			Reload = 1
		}

		/// <summary>
		/// Afvangen van alle soorten CommunicationExceptions, als laatst wordt
		/// de standaard CommunicationException afgevangen.
		/// </summary>
		/// <param name="exc"></param>
		public static ActionResult Show(CommunicationException exc)
		{
			ActionResult result;
			
			try
			{
				throw exc;
			}
			catch (DataOutOfDateException)
			{
				MessageBox.Show("Uw data is niet up to date. Data wordt opnieuw ingeladen.", "Data Out Of Date",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = ActionResult.Reload;
			}
			catch (DataReferenceException)
			{
				MessageBox.Show("De bewerking die u probeert uitvoeren heeft nog referentie naar andere data.", "Referentie Fout",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = ActionResult.Nothing;
			}
			catch (DataDoesNotExistsException)
			{
				if (MessageBox.Show("De data die u probeert te updaten bestaat niet. Wilt u de data opnieuw laden?", "Data bestaat niet",
								MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
					result = ActionResult.Reload;
				else
					result = ActionResult.Nothing;
			}
			catch (PouleAlreadyRunningException)
			{
				MessageBox.Show("De poule is al bezig.", "Poule Bezig",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = ActionResult.Nothing;
			}
			catch (PouleMatchScoreNotCompleteException)
			{
				MessageBox.Show("Nog niet alle scores van de huidige ronde zijn igevuld.", "Score",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = ActionResult.Nothing;
			}
			catch (CommunicationTimeOutException)
			{
				if (Communication.Instance.Connected)
				{
					MessageBox.Show("Er is een Time-out opgetreden.\nProbeer opnieuw of log opnieuw in.", "Time-out",
									MessageBoxButtons.OK, MessageBoxIcon.Error);
					result = ActionResult.Nothing;
				}
				else
				{
					MessageBox.Show("De verbinding met de Sharp Shuttle Server is verloren gegaan.\nLog opnieuw in.", "Verbinding Verloren",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
					result = ActionResult.Nothing;
				}
			}
			catch (NotConnectedException)
			{
				MessageBox.Show("De verbinding met de Sharp Shuttle Server is verloren gegaan.\nLog opnieuw in.", "Verbinding Verloren",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = ActionResult.Nothing;
			}
			catch (CommunicationException)
			{
				MessageBox.Show("Er is een onbekende fout opgetreden.\nLog opnieuw in.", "Onbekende Fout",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = ActionResult.Nothing;
			}

			return result;
		}
	}
}
