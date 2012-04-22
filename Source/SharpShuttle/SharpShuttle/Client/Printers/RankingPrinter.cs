using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Domain;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using Shared.Logging;

namespace Client.Printers
{
	/// <summary>
	/// Bied functionaliteit voor het uitprinten van ranglijsten en wedstrijden.
	/// </summary>
	static class RankingPrinter
	{

		#region external access

		/// <summary>
		/// Print een gegeven ranking.
		/// </summary>
		/// <param name="poule">De poule waarvan de ranking is.</param>
		/// <param name="teams">Een gesorteerde lijst van ladderteams.</param>
		/// <returns>Een bool die aangeeft of printen gelukt is.</returns>
		public static bool PrintRanking(Poule poule, IList<LadderTeam> teams)
		{
			PrintableRanking rankobject = new PrintableRanking(poule, new List<LadderTeam>(teams));
			return print(rankobject);
		}

		/// <summary>
		/// Print een gegeven ranking en de wedstrijden die tot nu toe gespeeld zijn.
		/// </summary>
		/// <param name="poule">De poule waarvan de ranking is.</param>
		/// <param name="teams">Een gesorteerde (op rang) lijst van ladderteams.</param>
		/// <param name="matches">Een gesorteerde (op ronde) lijst van gespeelde wedstrijden.</param>
		/// <returns>Een bool die aangeeft of printen gelukt is.</returns>
		public static bool PrintRankingAndMatches(Poule poule, IList<LadderTeam> teams, IList<Match> matches)
		{
			PrintableRanking rankobject = new PrintableRanking(poule, new List<LadderTeam>(teams), new List<Match>(matches));
			return print(rankobject);
		}

		#endregion
		#region internal access

		/// <summary>
		/// Handelt het printen zelf af.
		/// </summary>
		/// <param name="rankingobject"></param>
		/// <returns></returns>
		private static bool print(PrintableRanking rankingobject)
		{
			if (!rankingobject.IsValid()) return false;
			try
			{
				//Print voorbereiding
				PrintDocument printdoc = new PrintDocument();
				printdoc.PrintPage += rankingobject.printdoc_PrintPage;
				printdoc.PrinterSettings.PrinterName = Configurations.PrinterName;
				if (printdoc.PrinterSettings.IsValid && printdoc.DefaultPageSettings.PaperSize.Kind == PaperKind.A4)
				{
					//Printen op gedraaid A4.
					printdoc.DefaultPageSettings.Landscape = true;
					printdoc.Print();
					//Voor testdoeleinden een preview ipv een print.
					//PrintPreviewDialog preview = new PrintPreviewDialog();
					//preview.Document = printdoc;
					//preview.ShowDialog();
				}
				else
				{
					MessageBox.Show("Geen of ongeldige printer geselecteerd, of de printer kan geen A4 printen.", "Ongeldige Print", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				printdoc.PrintPage -= rankingobject.printdoc_PrintPage;
			}
			catch (Exception ex)
			{
				Logger.Write("Printererror", ex.ToString());
				return false;
			}
			return true;
		}

		#endregion
	}

	/// <summary>
	/// Deze klasse bevat een enkele ranglijst die geprint moet worden. Reageert zelf op printevents.
	/// </summary>
	class PrintableRanking
	{
		/// <summary>
		/// De poule waarvoor geprint moet worden.
		/// </summary>
		private Poule poule;
		/// <summary>
		/// De lijst van teams die geprint moeten worden.
		/// </summary>
		private IList<LadderTeam> teams;
		/// <summary>
		/// De mogelijke lijst van wedstrijden die geprint moeten worden.
		/// </summary>
		private IList<Match> matches;
		/// <summary>
		/// Bool die aangeeft of wedstrijden ook geprint moeten worden.
		/// </summary>
		private bool includeMatches = false;
		/// <summary>
		/// Het nummer van de huidige pagina.
		/// </summary>
		private int pageNumber = 0;
		/// <summary>
		/// De gebruikte brush. Standaard zwart.
		/// </summary>
		private Brush brush;
		/// <summary>
		/// De gebruikte pen. Standaard een zwarte pen van dikte 2.
		/// </summary>
		private Pen pen;
		/// <summary>
		/// Het font gebruikt voor de titel.
		/// </summary>
		private Font titleFont;
		/// <summary>
		/// Het font gebruikt voor headers.
		/// </summary>
		private Font headerFont;
		/// <summary>
		/// Het font gebruikt voor de rest van de tekst.
		/// </summary>
		private Font textFont;

		#region constructors
		/// <summary>
		/// Constructor voor een ranking-only print.
		/// </summary>
		/// <param name="poule">De poule die geprint word.</param>
		/// <param name="teams">De lijst van ladderteams.</param>

		internal PrintableRanking(Poule poule, IList<LadderTeam> teams)
		{
			this.poule = poule;
			this.teams = teams;
			setDefaults();
		}

		/// <summary>
		/// Constructor voor een totale overzichts print.
		/// </summary>
		/// <param name="poule">De poule die geprint word.</param>
		/// <param name="teams">De lijst van ladderteams.</param>
		/// <param name="matches">De lijst van wedstrijden.</param>
		internal PrintableRanking(Poule poule, IList<LadderTeam> teams, IList<Match> matches)
		{
			this.poule = poule;
			this.teams = teams;
			this.matches = matches;
			includeMatches = true;
			setDefaults();
		}

		/// <summary>
		/// Zet een aantal algemene variabelen die veelgebruikt worden.
		/// </summary>
		private void setDefaults()
		{
			brush = Brushes.Black;
			pen = new Pen(brush, 2);
			titleFont = new Font("Times New Roman", 18, FontStyle.Bold);
			headerFont = new Font("Times New Roman", 14, FontStyle.Bold);
			textFont = new Font("Times New Roman", 14, FontStyle.Regular);
		}
		#endregion
		#region events
		/// <summary>
		/// Wordt voor elke te printen pagina aangeroepen.
		/// </summary>
		/// <param name="sender">De oorsprong van het event.</param>
		/// <param name="e">De parameters van dit printerevent.</param>
		public void printdoc_PrintPage(object sender, PrintPageEventArgs e)
		{
			//init variabelen.
			int[] starts = createStarts(e.MarginBounds.Width, true);
			int left = e.MarginBounds.Left;
			int currentheight = e.MarginBounds.Top;
			Graphics graphics = e.Graphics;

			//Print de titel op de eerste pagina.
			if (pageNumber == 0)
			{
				currentheight += drawTitle(graphics, left, currentheight);
			}

			pageNumber++;

			//Teken de headers voor de wedstrijden. Komt bovenaan elke pagina met teams.
			if (teams.Count != 0)
				currentheight += drawRankingHeaders(e.Graphics, left, currentheight, starts);

			//Bepaal de hoogte per string en de hoogte per rij in de tabel.
			int heightperrow = (int)graphics.MeasureString("jtKG", textFont).Height;
			int textheight = heightperrow;

			if (poule.Discipline == Poule.Disciplines.FemaleDouble ||
				poule.Discipline == Poule.Disciplines.MaleDouble ||
				poule.Discipline == Poule.Disciplines.Mixed ||
				poule.Discipline == Poule.Disciplines.UnisexDouble)
				heightperrow *= 2;

			//Teken teams zolang ze op de pagina passen.
			while (teams.Count > 0 && e.MarginBounds.Bottom - currentheight >= heightperrow) 
			{
				drawTeam(e.Graphics, teams[0], left, currentheight, starts, heightperrow, textheight);
				currentheight += heightperrow;
				teams.RemoveAt(0);
			}

			//Als er nog teams over zijn, teken nog een pagina met teams.
			if (teams.Count > 0)
			{
				e.HasMorePages = true;
				return;
			}

			//Er zijn geen volgende pagina's meer als de wedstrijden op zijn,
			//of als wedstrijden niet geprint hoeven te worden.
			if ((includeMatches && matches.Count == 0) || !includeMatches) {
				e.HasMorePages = false;
				return;
			}

			//Nieuwe tabelindeling voor wedstrijden.
			starts = createStarts(e.MarginBounds.Width, false);

			//Als de headers en een rij nog op de pagina passen, begin dan hier met tekenen.
			//Ga anders verder naar volgende pagina.
			if (e.MarginBounds.Bottom - currentheight >= 2 * heightperrow + 20)
			{
				if (currentheight != e.MarginBounds.Top)
					currentheight += 20;

				currentheight += drawMatchHeaders(graphics, left, currentheight, starts);
			}
			else { e.HasMorePages = true; return; }

			//Teken wedstrijden zolang deze er zijn en passen.
			while (matches.Count != 0 && e.MarginBounds.Bottom - currentheight >= heightperrow)
			{
				drawMatch(graphics, matches[0], left, currentheight, starts, heightperrow, textheight);
				currentheight += heightperrow;
				matches.RemoveAt(0);
			}

			//Ga verder op een volgende pagina als het printen niet klaar is.
			e.HasMorePages = matches.Count != 0;
		}

		/// <summary>
		/// Bepaalt de verdelign van de tabel.
		/// </summary>
		/// <param name="width">De gegeven maximale breedte.</param>
		/// <param name="forRanking">Bool die aangeeft of de verdeling voor teams of wedstrijden zijn.</param>
		/// <returns>Een lijst van ints, elke int geeft een kolom start of einde aan.</returns>
		private static int[] createStarts(int width, bool forRanking)
		{
			int[] st;

			if (forRanking)
			{
				st = new int[13];
				st[0] = 0;
				st[1] = 50;
				//Variabel deel voor teamnamen.
				st[2] = width - 11 * 50 + st[1];
				for (int i = 3; i < st.Length-1; i++)
				{
					st[i] = st[i - 1] + 50;
				}
				st[12] = width;
			}
			else
			{
				st = new int[6];
				st[0] = 0;
				st[1] = 100;
				//Variabel deel voor teamnamen.
				st[2] = (width - 400) / 2 + st[1];
				st[3] = width - 300;
				st[4] = width - 200;
				st[5] = width;
			}

			return st;
		}
		#endregion

		#region drawing

		/// <summary>
		/// Tekent de titel voor deze print.
		/// </summary>
		/// <param name="graphics">Graphics om mee te tekenen.</param>
		/// <param name="x">X locatie.</param>
		/// <param name="y">Y locatie.</param>
		/// <returns>De hoogte die de titel inneemt.</returns>
		private int drawTitle(Graphics graphics, int x, int y)
		{
			string title;

			if (includeMatches)
				title = string.Format("Overzicht van {0} in ronde {1}", poule.Name, poule.Round);
			else title = string.Format("Tussenstand voor {0} in ronde {1}", poule.Name, poule.Round);
			
			graphics.DrawString(title, titleFont, Brushes.Black, x, y);
			return (int)graphics.MeasureString(title, titleFont).Height;
		}

		/// <summary>
		/// Tekent de headers voor de rankingtabel.
		/// </summary>
		/// <param name="graphics">Graphics om mee te tekenen.</param>
		/// <param name="x">X locatie.</param>
		/// <param name="y">Y locatie.</param>
		/// <param name="starts">De array met kolomverdelingen.</param>
		/// <returns>De hoogte die de headers innemen.</returns>
		private int drawRankingHeaders(Graphics graphics, int x, int y, int[] starts)
		{
			StringFormat vertical = new StringFormat(StringFormatFlags.DirectionVertical);
			string largest = "Gemiddeld";

			int headerheight = (int)graphics.MeasureString(largest, headerFont, int.MaxValue).Height;
			int verticalheight = (int)graphics.MeasureString(largest, textFont, int.MaxValue, vertical).Height;
			int totalheight = verticalheight + headerheight;

			//Teken lijnen structuur.
			graphics.DrawRectangle(pen, x, y, starts[starts.Length - 1], totalheight);

			for (int i = 1; i < starts.Length; i++)
			{
				if (i == 1 || i == 2 || i == 6 || i == 9)
					graphics.DrawLine(pen, x + starts[i], y, x + starts[i], y + totalheight);
			}

			for (int i = 3; i < starts.Length; i++)
			{
				graphics.DrawLine(pen, x + starts[i], y + headerheight, x + starts[i], y + totalheight);
			}

			graphics.DrawLine(pen, x + starts[2], y + headerheight, x + starts[starts.Length - 1], y + headerheight);

			//Horizontale headers
			drawCenteredHeader(graphics, "Rang", x + starts[0], y, starts[1] - starts[0]);
			drawCenteredHeader(graphics, "Team", x + starts[1], y, starts[2] - starts[1]);
			drawCenteredHeader(graphics, "Wedstrijden", x + starts[2], y, starts[6] - starts[2]);
			drawCenteredHeader(graphics, "Sets", x + starts[6], y, starts[9] - starts[6]);
			drawCenteredHeader(graphics, "Punten", x + starts[9], y, starts[starts.Length - 1] - starts[9]);

			//Verticale headers.
			graphics.DrawString("Winst", textFont, brush, x + starts[2], y + headerheight, vertical);
			graphics.DrawString("Verlies", textFont, brush, x + starts[3], y + headerheight, vertical);
			graphics.DrawString("Gelijk", textFont, brush, x + starts[4], y + headerheight, vertical);
			graphics.DrawString("Gespeeld", textFont, brush, x + starts[5], y + headerheight, vertical);
			graphics.DrawString("Winst", textFont, brush, x + starts[6], y + headerheight, vertical);
			graphics.DrawString("Verlies", textFont, brush, x + starts[7], y + headerheight, vertical);
			graphics.DrawString("gewonnen\nGemiddeld", textFont, brush, x + starts[8], y + headerheight, vertical);
			graphics.DrawString("Voor", textFont, brush, x + starts[9], y + headerheight, vertical);
			graphics.DrawString("Tegen", textFont, brush, x + starts[10], y + headerheight, vertical);
			graphics.DrawString("saldo\nGemiddeld", textFont, brush, x + starts[11], y + headerheight, vertical);
			return totalheight;
		}

		/// <summary>
		/// Tekent de headers wedstrijdentabel.
		/// </summary>
		/// <param name="graphics">Graphics om mee te tekenen.</param>
		/// <param name="x">X locatie.</param>
		/// <param name="y">Y locatie.</param>
		/// <param name="starts">De array met kolomverdelingen.</param>
		/// <returns>De hoogte die de headers innemen.</returns>
		private int drawMatchHeaders(Graphics graphics, int x, int y, int[] starts)
		{
			int height = (int)graphics.MeasureString("jtKG", textFont).Height;
			drawTableRow(graphics, x, y, starts, height);
			drawCenteredHeader(graphics, "Ronde", x + starts[0], y, starts[1] - starts[0]);
			drawCenteredHeader(graphics, "Team A", x + starts[1], y, starts[2] - starts[1]);
			drawCenteredHeader(graphics, "Team B", x + starts[2], y, starts[3] - starts[2]);
			drawCenteredHeader(graphics, "Eindstand", x + starts[3], y, starts[4] - starts[3]);
			drawCenteredHeader(graphics, "Punten", x + starts[4], y, starts[5] - starts[4]);
			return height;
		}

		/// <summary>
		/// Tekent een rij met daarin de informatie van een team.
		/// </summary>
		/// <param name="graphics">Graphics om mee te tekenen.</param>
		/// <param name="team">Het te tekenen team</param>
		/// <param name="x">X locatie.</param>
		/// <param name="y">Y locatie.</param>
		/// <param name="starts">De array met kolomverdelingen.</param>
		/// <param name="totalheight">De totale hoogte van de rij.</param>
		/// <param name="textheight">De hoogte van een regel text.</param>
		private void drawTeam(Graphics graphics, LadderTeam team, int x, int y, int[] starts, int totalheight, int textheight)
		{
			drawTableRow(graphics, x, y, starts, totalheight);
			drawNumber(graphics, team.Rank, x, y, 50, totalheight, textheight);

			if (team.Team.Player2 == null)
				graphics.DrawString(" - " + team.Team.Player1.Name, textFont, brush, x + starts[1], y);
			else graphics.DrawString(" - " + team.Team.Player1.Name + "\n - " + team.Team.Player2.Name, textFont, brush, x + starts[1], y);

			drawNumber(graphics, team.MatchesWon, x + starts[2], y, 50, totalheight, textheight);
			drawNumber(graphics, team.MatchesLost, x + starts[3], y, 50, totalheight, textheight);
			drawNumber(graphics, team.MatchesPlayed - team.MatchesWon - team.MatchesLost, x + starts[4], y, 50, totalheight, textheight);
			drawNumber(graphics, team.MatchesPlayed, x + starts[5], y, 50, totalheight, textheight);
			drawNumber(graphics, team.SetsWon, x + starts[6], y, 50, totalheight, textheight);
			drawNumber(graphics, team.SetsLost, x + starts[7], y, 50, totalheight, textheight);
			drawNumber(graphics, team.AverageSetsWon, x + starts[8], y, 50, totalheight, textheight);
			drawNumber(graphics, team.PointsWon, x + starts[9], y, 50, totalheight, textheight);
			drawNumber(graphics, team.PointsLost, x + starts[10], y, 50, totalheight, textheight);
			drawNumber(graphics, team.AverageScore, x + starts[11], y, 50, totalheight, textheight);
		}

		/// <summary>
		/// Tekent een rij met daarin de informatie van een wedstrijd.
		/// </summary>
		/// <param name="graphics">Graphics om mee te tekenen.</param>
		/// <param name="match">De wedstrijd om te tekenen.</param>
		/// <param name="x">X locatie.</param>
		/// <param name="y">Y locatie.</param>
		/// <param name="starts">De array met kolomverdelingen.</param>
		/// <param name="totalheight">De totale hoogte van de rij.</param>
		/// <param name="textheight">De hoogte van een regel text.</param>
		private void drawMatch(Graphics graphics, Match match, int x, int y, int[] starts, int totalheight, int textheight)
		{
			drawTableRow(graphics, x, y, starts, totalheight);
			drawNumber(graphics, match.Round, x, y, 100, totalheight, textheight);

			if (match.TeamA.Player2 == null)
				graphics.DrawString(" - " + match.TeamA.Player1.Name, textFont, brush, x + starts[1], y);
			else graphics.DrawString(" - " + match.TeamA.Player1.Name + "\n - " + match.TeamA.Player2.Name, textFont, brush, x + starts[1], y);

			if (match.TeamB.Player2 == null)
				graphics.DrawString(" - " + match.TeamB.Player1.Name, textFont, brush, x + starts[2], y);
			else graphics.DrawString(" - " + match.TeamB.Player1.Name + "\n - " + match.TeamB.Player2.Name, textFont, brush, x + starts[2], y);

			drawScore(graphics, x + starts[3], y, starts[4] - starts[3], totalheight, match.SetsTeamA, match.SetsTeamB, textheight);
			graphics.DrawString(match.SetData, textFont, brush, x + starts[4], y);
		}

		/// <summary>
		/// Tekent een score precies in het midden van een cel.
		/// </summary>
		/// <param name="graphics">Graphics om mee te tekenen.</param>
		/// <param name="x">X locatie.</param>
		/// <param name="y">Y locatie.</param>
		/// <param name="width">De breedte van de cel.</param>
		/// <param name="height">De hoogte van de cel.</param>
		/// <param name="score1">De score van de ene kant.</param>
		/// <param name="score2">De score van de andere kant.</param>
		/// <param name="textheight">De hoogte van een regel text.</param>
		private void drawScore(Graphics graphics, int x, int y, int width, int height, int score1, int score2, int textheight)
		{
			string result = string.Format("{0} - {1}", score1, score2);
			graphics.DrawString(result, textFont, brush, x + (width - (int)graphics.MeasureString(result,textFont).Width) / 2, y + (height - textheight) / 2);
		}

		/// <summary>
		/// Tekent een integer aan de rechterkant van een cel, verticaal gecentreerd.
		/// </summary>
		/// <param name="graphics">Graphics om mee te tekenen.</param>
		/// <param name="value">De waarde van het getal.</param>
		/// <param name="x">X locatie.</param>
		/// <param name="y">Y locatie.</param>
		/// <param name="width">De breedte van de cel.</param>
		/// <param name="height">De hoogte van de cel.</param>
		/// <param name="textheight">De hoogte van een regel text.</param>
		private void drawNumber(Graphics graphics, int value, int x, int y, int width, int height, int textheight)
		{
			int textwidth = (int)graphics.MeasureString(value + "", textFont).Width;
			graphics.DrawString(value + "", textFont, brush, x + width - textwidth, y + (height - textheight) / 2);
		}

		/// <summary>
		/// Tekent een float aan de rechterkant van een cel, verticaal gecentreerd.
		/// </summary>
		/// <param name="graphics">Graphics om mee te tekenen.</param>
		/// <param name="value">De waarde van het getal.</param>
		/// <param name="x">X locatie.</param>
		/// <param name="y">Y locatie.</param>
		/// <param name="width">De breedte van de cel.</param>
		/// <param name="height">De hoogte van de cel.</param>
		/// <param name="textheight">De hoogte van een regel text.</param>
		private void drawNumber(Graphics graphics, float value, int x, int y, int width, int height, int textheight)
		{
			string formatted;

			if (value < 10)
				formatted = value.ToString("0.##");
			else if (value < 100)
				formatted = value.ToString("00.#");
			else formatted = value.ToString("###0");
			
			int textwidth = (int)graphics.MeasureString(formatted, textFont).Width;
			graphics.DrawString(formatted, textFont, brush, x + width - textwidth, y + (height - textheight) / 2);
		}

		/// <summary>
		/// Tekent een enkele rij in de tabel, inclusief omgrenzing.
		/// </summary>
		/// <param name="graphics">Graphics om mee te tekenen.</param>
		/// <param name="x">X locatie.</param>
		/// <param name="y">Y locatie.</param>
		/// <param name="starts">De array met kolomverdelingen.</param>
		/// <param name="height">De hoogte van de cel.</param>
		private void drawTableRow(Graphics graphics, int x, int y, int[] starts, int height)
		{
			graphics.DrawRectangle(pen, x, y, starts[starts.Length - 1], height);

			for (int i = 1; i < starts.Length; i++)
			{
				graphics.DrawLine(pen, starts[i] + x, y, starts[i] + x, y + height);
			}
		}

		/// <summary>
		/// Tekent een horizontaal gecentreerd header.
		/// </summary>
		/// <param name="graphics">Graphics om mee te tekenen.</param>
		/// <param name="header">De header om te tekenen.</param>
		/// <param name="x">X locatie.</param>
		/// <param name="y">Y locatie.</param>
		/// <param name="width">De breedte van de cel.</param>
		private void drawCenteredHeader(Graphics graphics,string header, int x, int y, int width)
		{
			int textwidth = (int)graphics.MeasureString(header, headerFont).Width;
			graphics.DrawString(header, headerFont, brush, x + (width-textwidth)/2, y);
		}

		#endregion

		#region validation

		/// <summary>
		/// Controleert of alle informatie in dit object correct is.
		/// </summary>
		/// <returns>Een bool die geldigheid aangeeft.</returns>
		public bool IsValid()
		{
			if (poule == null || teams == null) return false;
			if (includeMatches && matches == null) return false;
			if (teams.Count == 0) return false;
			return true;
		}
		#endregion
	}
}
