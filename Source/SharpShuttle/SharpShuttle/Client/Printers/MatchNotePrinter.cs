using System;
using System.Collections.Generic;
using Shared.Domain;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Shared.Logging;

namespace Client.Printers
{
	/// <summary>
	/// Print wedstrijdbriefjes, gebaseerd op een queue.
	/// Werkt alleen op A4 papier.
	/// </summary>
	public class MatchNotePrinter
	{
		/// <summary>
		/// De instance van deze printer.
		/// </summary>
		private static MatchNotePrinter printerInstance = new MatchNotePrinter();
		/// <summary>
		/// De breedte van een enkel wedstrijdbriefje.
		/// </summary>
		private int elementWidth = 520;
		/// <summary>
		/// De hoogte van een enkel wedstrijdbriefje.
		/// </summary>
		private int elementHeight = 360;
		/// <summary>
		/// Het font gebruikt voor headers.
		/// </summary>
		private Font headerFont = new Font("Times New Roman", 16, FontStyle.Bold);
		/// <summary>
		/// Het font gebruikt voor de rest van de tekst.
		/// </summary>
		private Font textFont = new Font("Times New Roman", 14, FontStyle.Regular);
		/// <summary>
		/// De gebruikte brush. Standaard zwart.
		/// </summary>
		private Brush brush = Brushes.Black;
		/// <summary>
		/// De gebruikte pen. Standaard een zwarte pen van dikte 2.
		/// </summary>
		private Pen pen;
		/// <summary>
		/// Een queue die de wedstrijden bijhoudt die geprint moeten worden.
		/// </summary>
		private Queue<MatchNote> matchesToPrint = new Queue<MatchNote>();
		/// <summary>
		/// Een boolean die aangeeft of er alleen volle pagina's geprint worden.
		/// </summary>
		private bool printFull = false;

		/// <summary>
		/// Constructor.
		/// </summary>
		private MatchNotePrinter()
		{
			pen = new Pen(brush, 2);
		}

		#region external access

		/// <summary>
		/// Geeft toegang tot de instantie van deze printer.
		/// </summary>
		public static MatchNotePrinter Printer
		{
			get { return printerInstance; }
		}

		/// <summary>
		/// Print alle wedstrijdbriefjes die in de queue staan.
		/// </summary>
		/// <returns>Geeft aan of het printen gelukt is.</returns>
		public bool PrintAll()
		{
			printFull = false;
			return print();
		}

		/// <summary>
		/// Print een "veelvoud van 4" wedstrijdbriefjes die in de queue staan.
		/// Overgebleven wedstrijdbriefjes blijven in de queue.
		/// </summary>
		/// <returns>Geeft aan of het printen gelukt is.</returns>
		public bool PrintFull()
		{
			printFull = true;
			return print();
		}

		/// <summary>
		/// Voegt nieuwe wedstrijdbriefjes aan de queue toe.
		/// </summary>
		/// <param name="newmatches">De lijst van wedstrijdbriefjes.</param>
		public void AddMatches(ICollection<Match> newmatches)
		{
			lock (matchesToPrint)
			{
				if (matchesToPrint.Count > 100)
					matchesToPrint.Clear();
				foreach (Match match in newmatches)
					matchesToPrint.Enqueue(new MatchNote(match));
			}
		}

		/// <summary>
		/// Voegt een enkel wedstrijdbriefje aan de queue toe.
		/// </summary>
		/// <param name="match">De wedstrijd die uitgeprint moet worden.</param>
		public void AddMatch(Match match)
		{
			lock (matchesToPrint)
			{
				if (matchesToPrint.Count > 100)
					matchesToPrint.Clear();
				matchesToPrint.Enqueue(new MatchNote(match));
			}
		}

		#endregion

		#region internal access

		/// <summary>
		/// Geeft de eerstvolgende matchnote die geprint moet worden.
		/// </summary>
		/// <returns></returns>
		private MatchNote getMatch()
		{
			return matchesToPrint.Dequeue();
		}

		/// <summary>
		/// Print wedstrijdbriefjes in de queue
		/// </summary>
		/// <returns></returns>
		private bool print()
		{
			try
			{
				//Print voorbereiding
				PrintDocument printdoc = new PrintDocument();
				printdoc.PrintPage += printdoc_PrintPage;
				printdoc.PrinterSettings.PrinterName = Configurations.PrinterName;
				printdoc.DefaultPageSettings.Landscape = true;

				//Printen
				lock (matchesToPrint)
				{
					//Controleer of er genoeg briefjes zijn voor een pagina.
					if ((printFull && matchesToPrint.Count > 3) || (!printFull && matchesToPrint.Count > 0))
					{
						if (printdoc.PrinterSettings.IsValid && printdoc.DefaultPageSettings.PaperSize.Kind == PaperKind.A4)
						{
							printdoc.Print();
							//Voor testdoeleinden een preview ipv een print.
							//PrintPreviewDialog preview = new PrintPreviewDialog();
							//preview.Document = printdoc;
							//preview.ShowDialog();
						}
						else
						{
							MessageBox.Show("Geen of ongeldige printer geselecteerd, kind = " + printdoc.DefaultPageSettings.PaperSize.Kind, "Ongeldige Printer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Write("Printererror", ex.ToString());
				return false;
			}
			return true;
		}
		#endregion

		#region events

        /// <summary>
        /// Het PrintPage event treedt op voor elke pagina die geprint moet worden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="printevent"></param>
		private void printdoc_PrintPage(object sender, PrintPageEventArgs printevent)
		{
			//Variabele inits.
			int left = (printevent.PageBounds.Width - 2 * elementWidth) / 4;
			int horizontalgap = left * 2;
			int top = (printevent.PageBounds.Height - 2 * elementHeight) / 4;
			int verticalgap = top * 2;
			Graphics graphics = printevent.Graphics;
        	int sets = Configurations.NumberOfSets;

			//Teken de maximaal vier elementen.
			for (int y = 0; y < 2; y++)
			{
				int posx = left;

				for (int x = 0; x < 2; x++)
				{
					if (matchesToPrint.Count != 0)
						DrawElement(graphics, posx, top, getMatch(),sets);
					posx += elementWidth + horizontalgap;
				}

				top += elementHeight + verticalgap;
			}

			//Controleer of er meer pagina's geprint moeten worden.
			if ((printFull && matchesToPrint.Count > 3) || (!printFull && matchesToPrint.Count > 0))
				printevent.HasMorePages = true;
			else printevent.HasMorePages = false;
		}

		#endregion

		#region drawing

		/// <summary>
		/// Tekent een enkel wedstrijdbriefje.
		/// </summary>
		/// <param name="graphics">Het graphics object om mee te tekenen.</param>
		/// <param name="x">De X waarde van de linkerbovenhoek.</param>
		/// <param name="y">De Y waarde van de linkerbovenhoek.</param>
		/// <param name="match">De wedstrijd die geprint moet worden.</param>
		/// <param name="sets"> Het aantal sets</param>
		private void DrawElement(Graphics graphics, int x, int y, MatchNote match, int sets)
		{
			int scorewidth = 60;
			graphics.DrawRectangle(pen, x, y, elementWidth, elementHeight);
			int rowheight = elementHeight / 8;
			int divider = x + elementWidth - (sets + 1) * scorewidth;
			drawLines(graphics, x, y, scorewidth, rowheight, divider);
			drawHeaders(graphics, x, y + 2 * rowheight, sets, scorewidth, rowheight, divider);
			drawInbetween(graphics, x, y + 5 * rowheight, sets, scorewidth, rowheight, divider);
			drawTeam(graphics, x, y + 3 * rowheight, match.TeamA, rowheight, divider);
			drawTeam(graphics, x, y + 6 * rowheight, match.TeamB, rowheight, divider);
			drawScoreBoxes(graphics, x, y + 3 * rowheight, rowheight, scorewidth,divider, sets);
			drawScoreBoxes(graphics, x, y + 6 * rowheight, rowheight, scorewidth,divider, sets);
			drawInformation(graphics, x, y, rowheight, scorewidth, match);
		}

		/// <summary>
		/// Tekent de informatiebalk bovenaan het wedstrijdbriefje.
		/// </summary>
		/// <param name="graphics">Het graphics object om mee te tekenen.</param>
		/// <param name="x">De X waarde van de linkerbovenhoek.</param>
		/// <param name="y">De Y waarde van de linkerbovenhoek.</param>
		/// <param name="rowheight">De hoogte van 1 rij.</param>
		/// <param name="scorewidth">De breedte van een score kolom.</param>
		/// <param name="match">De wedstrijd die geprint moet worden.</param>
		private void drawInformation(Graphics graphics, int x, int y, int rowheight, int scorewidth, MatchNote match)
		{
			int third = elementWidth / 3;
			graphics.DrawLine(pen, x, y + rowheight, x + third * 2, y + rowheight);
			graphics.DrawLine(pen, x + third * 2, y, x + third * 2, y + 2 * rowheight);
			graphics.DrawLine(pen, x + third, y, x + third, y + rowheight);
			DrawVerticalCenteredText(graphics, x, y, rowheight, " " + match.MatchId, headerFont);
			DrawVerticalCenteredText(graphics, x + third, y, rowheight, " Ronde " + match.Round, textFont);
			string poule = " " + match.PouleName;

			while (graphics.MeasureString(poule, textFont).Width > 2 * third)
				poule = poule.Remove(poule.Length - 1);

			DrawVerticalCenteredText(graphics, x, y + rowheight, rowheight, " " + match.PouleName, textFont);
			DrawVerticalCenteredText(graphics, x + 2 * third, y, 2 * rowheight, " Baan:", textFont);
			drawBox(graphics, x + elementWidth - scorewidth, y, rowheight * 2, scorewidth);
		}

		/// <summary>
		/// Tekent de belangrijkste lijnen van het briefje.
		/// </summary>
		/// <param name="graphics">Het graphics object om mee te tekenen.</param>
		/// <param name="x">De X waarde van de linkerbovenhoek.</param>
		/// <param name="y">De Y waarde van de linkerbovenhoek.</param>
		/// <param name="scorewidth">De breedte van een score kolom.</param>
		/// <param name="rowheight">De hoogte van 1 rij.</param>
		/// <param name="divider">De splitsing tussen teams en scores.</param>
		private void drawLines(Graphics graphics, int x, int y, int scorewidth, int rowheight, int divider)
		{
			graphics.DrawLine(pen, x, y + 2 * rowheight, x + elementWidth, y + 2 * rowheight);
			graphics.DrawLine(pen, x, y + 3 * rowheight, x + elementWidth, y + 3 * rowheight);
			graphics.DrawLine(pen, x, y + 5 * rowheight, divider, y + 5 * rowheight);
			graphics.DrawLine(pen, x, y + 6 * rowheight, divider, y + 6 * rowheight);
			graphics.DrawLine(pen, divider, y + 2 * rowheight, divider, y + elementHeight);
			graphics.DrawLine(pen, x + elementWidth - scorewidth, y + 2 * rowheight, x + elementWidth - scorewidth, y + elementHeight);
		}

		/// <summary>
		/// Tekent de kolomheaders voor de scores en de header voor Team A.
		/// </summary>
		/// <param name="graphics">Het graphics object om mee te tekenen.</param>
		/// <param name="x">De X waarde van de linkerbovenhoek.</param>
		/// <param name="y">De Y waarde van de linkerbovenhoek.</param>
		/// <param name="sets">Het aantal sets</param>
		/// <param name="scorewidth">De breedte van een score kolom.</param>
		/// <param name="rowheight">De hoogte van 1 rij.</param>
		/// <param name="divider">De splitsing tussen teams en scores.</param>
		private void drawHeaders(Graphics graphics, int x, int y, int sets, int scorewidth, int rowheight, int divider)
		{
			DrawVerticalCenteredText(graphics, x, y, rowheight, " Team A", headerFont);
			DrawVerticalCenteredText(graphics, x+elementWidth-scorewidth, y, rowheight, "Score", headerFont);

			for (int i = 0; i < sets; i++)
			{
				DrawVerticalCenteredText(graphics, divider + i*scorewidth, y, rowheight, "Set " + (i+1), headerFont);
			}
		}

		/// <summary>
		/// Tekent de header van Team B en de streepjes tussen de scoreboxen.
		/// </summary>
		/// <param name="graphics">Het graphics object om mee te tekenen.</param>
		/// <param name="x">De X waarde van de linkerbovenhoek.</param>
		/// <param name="y">De Y waarde van de linkerbovenhoek.</param>
		/// <param name="sets">Het aantal sets</param>
		/// <param name="scorewidth">De breedte van een score kolom.</param>
		/// <param name="rowheight">De hoogte van 1 rij.</param>
		/// <param name="divider">De splitsing tussen teams en scores.</param>
		private void drawInbetween(Graphics graphics, int x, int y, int sets, int scorewidth, int rowheight, int divider)
		{
			DrawVerticalCenteredText(graphics, x, y, rowheight, " Team B", headerFont);
			drawDash(graphics, x + elementWidth - scorewidth, y, scorewidth, rowheight);

			for (int i = 0; i < sets; i++)
			{
				drawDash(graphics, divider + i * scorewidth, y, scorewidth,rowheight);
			}
		}

		/// <summary>
		/// Tekent de namen van een team. Teams met twee spelers worden onder elkaar getekend. Te lange namen worden geclipt.
		/// </summary>
		/// <param name="graphics">Het graphics object om mee te tekenen.</param>
		/// <param name="x">De X waarde van de linkerbovenhoek.</param>
		/// <param name="y">De Y waarde van de linkerbovenhoek.</param>
		/// <param name="team">Het te tekenen team.</param>
		/// <param name="rowheight">De hoogte van 1 rij.</param>
		/// <param name="divider">De splitsing tussen teams en scores.</param>
		private void drawTeam(Graphics graphics, int x, int y, Team team, int rowheight, int divider)
		{
			string player1, player2;
			player1 = " " + team.Player1.Name;

			if (team.Player2 == null)
				player2 = "";
			else player2 = " " + team.Player2.Name;

			while (graphics.MeasureString(player1, textFont).Width > divider - x)
				player1 = player1.Remove(player1.Length - 1);

			while (graphics.MeasureString(player2, textFont).Width > divider - x)
				player1 = player1.Remove(player2.Length - 1);

			if (player2 == "")
				DrawVerticalCenteredText(graphics, x, y, 2 * rowheight, player1, textFont);
			else DrawVerticalCenteredText(graphics, x, y, 2 * rowheight, player1 + "\n" + player2, textFont);
		}

		/// <summary>
		/// Tekent de scoreboxen voor sets en de eindstandbox op 1 rij.
		/// </summary>
		/// <param name="graphics">Het graphics object om mee te tekenen.</param>
		/// <param name="x">De X waarde van de linkerbovenhoek.</param>
		/// <param name="y">De Y waarde van de linkerbovenhoek.</param>
		/// <param name="rowheight">De hoogte van 1 rij.</param>
		/// <param name="scorewidth">De breedte van een score kolom.</param>
		/// <param name="divider">De splitsing tussen teams en scores.</param>
		/// <param name="sets">Het aantal sets</param>
		private void drawScoreBoxes(Graphics graphics, int x, int y, int rowheight, int scorewidth, int divider, int sets)
		{
			for (int i = 0; i < sets; i++)
			{
				drawBox(graphics, divider + i * scorewidth, y, rowheight * 2, scorewidth);
			}

			Pen oldpen = pen;
			pen = new Pen(brush, 3);
			drawBox(graphics, x + elementWidth - scorewidth, y, rowheight * 2, scorewidth);
			pen = oldpen;
		}

		/// <summary>
		/// Tekent een enkele scorebox.
		/// </summary>
		/// <param name="graphics">Het graphics object om mee te tekenen.</param>
		/// <param name="x">De X waarde van de linkerbovenhoek.</param>
		/// <param name="y">De Y waarde van de linkerbovenhoek.</param>
		/// <param name="rowheight">De hoogte van 1 rij.</param>
		/// <param name="scorewidth">De breedte van een score kolom.</param>
		private void drawBox(Graphics graphics, int x, int y, int rowheight, int scorewidth)
		{
			graphics.DrawRectangle(pen, x + 4, y + rowheight / 4, scorewidth - 8, rowheight - rowheight / 2);
		}

		/// <summary>
		/// Tekent een enkel dik streepje precies tussen de scoreboxen.
		/// </summary>
		/// <param name="graphics">Het graphics object om mee te tekenen.</param>
		/// <param name="x">De X waarde van de linkerbovenhoek.</param>
		/// <param name="y">De Y waarde van de linkerbovenhoek.</param>
		/// <param name="scorewidth">De breedte van een score kolom.</param>
		/// <param name="rowheight">De hoogte van 1 rij.</param>
		private void drawDash(Graphics graphics, int x, int y, int scorewidth, int rowheight)
		{
			Pen widepen = new Pen(brush, 3);
			graphics.DrawLine(widepen,x + (scorewidth / 2) - 7, y + rowheight / 2, x + (scorewidth / 2) + 7, y + rowheight / 2);
		}

		/// <summary>
		/// Tekent een verticaal gecentreerde text.
		/// </summary>
		/// <param name="graphics">Het graphics object om mee te tekenen.</param>
		/// <param name="x">De X waarde van de linkerbovenhoek.</param>
		/// <param name="y">De Y waarde van de linkerbovenhoek.</param>
		/// <param name="height">De maximale tekenruimte.</param>
		/// <param name="text">De string om te tekenen.</param>
		/// <param name="font">Het font waarin getekend moet worden.</param>
		private void DrawVerticalCenteredText(Graphics graphics, int x, int y, int height, string text, Font font)
		{
			int textheight = (int)graphics.MeasureString(text,font).Height;
			graphics.DrawString(text,font,brush, x, y + (height-textheight)/2);
		}

		#endregion
	}
}