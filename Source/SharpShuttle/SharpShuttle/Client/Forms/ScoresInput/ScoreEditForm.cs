using System;
using System.Windows.Forms;
using Shared.Views;
using System.Drawing;
using Client.Controls;

namespace Client.Forms.ScoresInput
{
	/// <summary>
	/// Scherm waar de scores van een wedstrijd ingevoerd worden
	/// </summary>
	public partial class ScoreEditForm : Form
	{
		/// <summary>
		/// Event die aangeeft dat het score invoeren klaar is
		/// </summary>
		public event ManageScoreControl.ScoreEditDoneEvent EditDone;
        
		/// <summary>
		/// De wedstrijd waarvan de score ingevoerd wordt
		/// </summary>
		protected MatchView editMatch;
		
		/// <summary>
		/// Is dit een nieuwe score of een wijziging van een oude score
		/// </summary>
		protected bool newScore;
		
		/// <summary>
		/// Het aantal sets
		/// </summary>
		protected byte sets;
		
		/// <summary>
		/// De scores die de teams in de sets gehaald hebben
		/// [teams, sets]
		/// </summary>
		protected TextBox[,] setscores;
		
		/// <summary>
		/// De oorspronkelijke score die de teams in de sets gehaald hebebn
		/// [teams, sets]
		/// </summary>
		protected int[,] originalscores;
		
		/// <summary>
		/// Het aantal gewonnen sets van de teams
		/// </summary>
		protected TextBox[] result; 

        #region Constructor en initialisatie

		/// <summary>
		/// Constructor die de wedstrijd en een bool die aangeeft of het een nieuwe
		/// score is meekrijgt
		/// </summary>
		/// <param name="match"></param>
		/// <param name="newScore"></param>
        public ScoreEditForm(MatchView match, bool newScore)
        {
            editMatch = match;
			this.newScore = newScore;
			sets = (byte)Configurations.NumberOfSets;
			
			InitializeComponent();
			buildSetGroups();
			buildControlArrays();
			fillInForm();
			resizeForm();
        }

		/// <summary>
		/// Maakt aan de hand van het aantal sets de invoervelden
		/// </summary>
		private void buildControlArrays() 
		{
			result = new TextBox[] { txtSetsWonTeamA, txtSetsWonTeamB };
			
			switch (sets)
			{
				case 3:
					setscores = new TextBox[,] { { txtSet1PointsA, txtSet2PointsA, txtSet3PointsA }, { txtSet1PointsB, txtSet2PointsB, txtSet3PointsB } };
					break;
				case 2:
					setscores = new TextBox[,] { { txtSet1PointsA, txtSet2PointsA }, { txtSet1PointsB, txtSet2PointsB } };
					break;
				case 1:
					setscores = new TextBox[,] { { txtSet1PointsA }, { txtSet1PointsB } };
					break;
			}
		}

		/// <summary>
		/// Verplaatst/verbergt een aantal setgroups zodat het form past bij het gewenste aantal sets.
		/// </summary>
		protected void buildSetGroups()
		{
			switch (sets) 
			{
				case 3:
					return;
				case 2:
					grpSet3.Visible = false;
					int gap = grpSet3.Width + grpSet3.Left - grpSet2.Right;
					grpSet2.Left += gap;
					grpSet1.Left += gap;
					return;
				case 1:
					grpSet3.Visible = false;
					grpSet2.Visible = false;
					grpSet1.Left += grpSet2.Width + grpSet3.Width + (grpSet3.Left - grpSet2.Right) + (grpSet2.Left - grpSet1.Right);
					return;
			}
		}

		/// <summary>
		/// Creeert alle GUI elementen
		/// </summary>
        protected void fillInForm()
        {
			if (newScore)
				Text = String.Format("Score Wedstrijd {0} Invoeren", editMatch.MatchID);
			else
			{
				Text = String.Format("Score Wedstrijd {0} Wijzigen", editMatch.MatchID);
				btnAccept.Text = "Wijzigen";
			}

			txtPoule.Text = editMatch.Domain.PouleName;
			txtMatchID.Text = editMatch.MatchID;
			txtRound.Text = editMatch.Round.ToString();

			if (editMatch.Domain.TeamA.Player2 == null)
				lblTeamA.Text = editMatch.Domain.TeamA.Player1.Name;
			else 
				lblTeamA.Text = editMatch.Domain.TeamA.Player1.Name + "\n" + editMatch.Domain.TeamA.Player2.Name;

			if (editMatch.Domain.TeamB.Player2 == null)
				lblTeamB.Text = editMatch.Domain.TeamB.Player1.Name;
			else 
				lblTeamB.Text = editMatch.Domain.TeamB.Player1.Name + "\n" + editMatch.Domain.TeamB.Player2.Name;

			if (!newScore)
			{
				btnAccept.Enabled = false;

				string[] splitsets = editMatch.Domain.SetData.Split(';');
				string[,] splitscores = new string[2, sets];

				for (int i = 0; i < sets; i++)
				{
					string[] splitsetscore = splitsets[i].Split('-');
					
					if (splitsetscore.Length > 1)
					{
						splitscores[0, i] = splitsetscore[0];
						splitscores[1, i] = splitsetscore[1];
					}
				}

				for (int x = 0; x < 2; x++)
					for (int y = 0; y < sets; y++)
						setscores[x, y].Text = splitscores[x, y];

				originalscores = getValues();
			}
		}

		#endregion

		#region Resizing

		/// <summary>
		/// Met resizeForm kijken we hoe breed het scorevenster moet zijn zodat de teamnaam past
		/// </summary>
		protected void resizeForm()
		{
			//Kijk hoe groot de grootste label is
			int minimum = Math.Max(lblTeamA.Right, lblTeamB.Right) + 8;
			//Bereken het verschil met het SET invulgebied
			int diff = grpSet1.Left - minimum;
			//Maak het formulier groter
			Width -= diff;
		}

		/// <summary>
		/// Bepaalt de breedte van een string, gegeven een font
		/// </summary>
		/// <param name="graphics"> Graphics object dat wordt gebruikt om de breedte te "meten" </param>
		/// <param name="text"> De string </param>
		/// <param name="font"> Het font van de string </param>
		/// <returns> De breedte van de string </returns>
		public static int MeasureDisplayStringWidth(Graphics graphics, string text,	Font font)
		{
			StringFormat format = new StringFormat();
			RectangleF rect = new RectangleF(0, 0, 1000, 1000);
			CharacterRange[] ranges = { new CharacterRange(0, text.Length) };

			format.SetMeasurableCharacterRanges(ranges);

			Region[] regions = graphics.MeasureCharacterRanges(text, font, rect, format);
			rect = regions[0].GetBounds(graphics);

			return (int)(rect.Right + 1.0f);
		}

        #endregion

        #region Opslaan en validatie

		/// <summary>
		/// Slaat de score op
		/// </summary>
		/// <param name="values"></param>
		protected void saveScore(int[,] values)
		{
			editMatch.SetsWonTeam1 = setsTeam(0, values);
			editMatch.SetsWonTeam2 = setsTeam(1, values);
			int sumA = 0;
			int sumB = 0;

			for (int i = 0; i < sets; i++)
			{
				sumA += values[0, i];
				sumB += values[1, i];
			}

			editMatch.PointsTeam1 = sumA;
			editMatch.PointsTeam2 = sumB;
			string data = "";

			for (int i = 0; i < sets; i++)
				data += values[0, i] + "-" + values[1, i] + ";"; 

			editMatch.Domain.SetData = data;
		}

		/// <summary>
		/// Verandert het aantal gewonnen sets
		/// </summary>
		/// <param name="values"></param>
		protected void changeSets(int[,] values)
		{
			for (int i = 0; i < 2; i++)
				result[i].Text = setsTeam(i, values).ToString();
		}

		/// <summary>
		/// Kijkt of de invoerwaarden verschillen van de originele waarden
		/// </summary>
		protected void checkForChanges(int[,] values)
		{
			bool changed = false;

			if (values == null)
				changed = true;
			else
			{
				for (int x = 0; x < 2; x++)
					for (int y = 0; y < sets; y++)
						if (values[x, y] != originalscores[x, y])
							changed = true;
			}

			btnAccept.Enabled = changed;
		}

		/// <summary>
		/// Returnt het aantal gewonnen sets van een team
		/// </summary>
		/// <param name="teamindex"> De index van het team </param>
		/// <param name="values"> De behaalde punten </param>
		/// <returns> Het aantal gewonnen sets </returns>
		protected int setsTeam(int teamindex, int[,] values)
		{
			if (values == null)
				return 0;

			int[] wins = new int[2];

			for (int i = 0; i < sets; i++)
			{
				if (values[0, i] > values[1, i])
					wins[0]++;
				else if (values[0, i] < values[1, i])
					wins[1]++;
			}

			return wins[teamindex];
		}

		/// <summary>
		/// Kijkt of een reeks van scores geldig is
		/// </summary>
		/// <param name="input"> Een reeks van scores </param>
		/// <returns> De geldigheid van de scorereeks </returns>
        protected static bool validInput(int[,] input)
        {
			if (input == null) 
				return false;

			foreach (int checkvalue in input)
				if (checkvalue < 0)
					return false;

			return true;
        }

		/// <summary>
		/// Kijkt of een score geldig is
		/// </summary>
		/// <param name="score"> Een score </param>
		/// <returns> De geldigheid van de score </returns>
		protected static bool validScore(string score)
		{
			int num;

			if (score.Length == 0 || !int.TryParse(score, out num) || num < 0)
				return false;

			return true;
		}

		/// <summary>
		/// Zet de ingevoerde score-strings om in een 2D array van scores
		/// </summary>
		/// <returns> Een 2D array van scores </returns>
		protected int[,] getValues()
		{
			int[,] result = new int[2, sets];

			for (int x = 0; x < 2; x++)
			{
				for (int y = 0; y < sets; y++)
				{
					int num;

					if (int.TryParse(setscores[x, y].Text, out num))
						result[x, y] = num;
					else
						return null;
				}
			}

			return result;
		}

        #endregion

        #region Buttons

		/// <summary>
		/// Controleer of de ingevoerde scores geldig zijn, en sluit het scherm
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void acceptButton_Click(object sender, EventArgs e)
		{
			int[,] values = getValues();

			if (values == null)
				MessageBox.Show("Niet alle velden zijn met numerieke waarden ingevuld.", "Ongeldige Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if (!validInput(values))
				MessageBox.Show("De opgegeven score is niet geldig.", "Ongeldige Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else
			{
				saveScore(values);

				if (EditDone != null)
					EditDone(editMatch);

				Close();
			}
		}

        #endregion

		#region Events

		/// <summary>
		/// Kijk voor veranderingen als de score wordt aangepast.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void scoreBox_TextChanged(object sender, EventArgs e)
		{
			TextBox scoreBox = (TextBox)sender;
			int[,] values = getValues();

			// Controleer de input en geef de gebruiker feedback
			if (!validScore(scoreBox.Text.Trim()))
				scoreBox.BackColor = System.Drawing.Color.Pink;
			else
				scoreBox.BackColor = System.Drawing.Color.White;

			// Update de uitslagevelden
			if (validInput(values))
				changeSets(values);

			// Bij het wijzigen van een score: kijk of er iets veranderd is
			if (!newScore && originalscores != null)
				checkForChanges(values);
		}

		#endregion
	}
}
