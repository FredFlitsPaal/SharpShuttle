using System;
using System.Windows.Forms;
using Shared.Sorters;
using Shared.Views;
using Client.Controls;
using Shared.Domain;
using System.Text.RegularExpressions;

namespace Client.Forms.AddPoule
{

	/// <summary>
	/// Scherm om tijdens het toernooi een nieuwe poule toe te voegen
	/// </summary>
	public partial class AddPouleForm : Form
	{

		/// <summary>
		/// De notification control
		/// </summary>
		private AddPouleControl control = new AddPouleControl();
		/// <summary>
		/// Bool of de naam door gebruiker verandert is 
		/// </summary>
		protected bool nameChanged;
		/// <summary>
		/// Op welke kolom de lijst het laatst is gesorteerd
		/// </summary>
		private int playersSort = -1;

		/// <summary>
		/// Default constructor
		/// </summary>
		public AddPouleForm()
		{
			InitializeComponent();
			initializeDisciplines();
			control.PlayerListUpdated += UpdatePoulePlayersList;
			control.UpdateData();
			control.Poule.Discipline = (String)cboDisciplines.SelectedItem;
			UpdatePoulePlayersList();
			fillInNameBox();
		}

		#region Check namen
		/// <summary>
		/// Poulenaam gelijk maken aan "discipline niveau".
		/// </summary>
		protected void fillInNameBox()
		{
			txtName.Text = cboDisciplines.Text.Trim() + " " + txtNiveau.Text.Trim();
			NameCorrectness();
			nameChanged = false;
		}

		/// <summary>
		/// Controleer op illegale characters
		/// </summary>
		/// <param name="toCheck">De string die gecontroleerd moet worden</param>
		/// <returns>Of de string correct is</returns>
		protected static bool containsIllegalCharacters(string toCheck)
		{
			//TODO Commentaar voor deze harige regex aub
			Regex pattern = new Regex("^[\\w\\n]+(([-_' ][\\w\\n])?[\\w\\n]*)*$");
			return !pattern.IsMatch(toCheck);
		}

		/// <summary>
		/// Geef aan wat er fout is 
		/// </summary>
		private void NameCorrectness()
		{
			lblNameCorrect.Text = "";
			if (txtName.BackColor == System.Drawing.Color.Pink)
				lblNameCorrect.Text = "Poule naam bevat illegale leestekens of de naam bestaat al";
			if (txtNiveau.BackColor == System.Drawing.Color.Pink)
			{
				if (lblNameCorrect.Text != "")
					lblNameCorrect.Text += ", ";
				lblNameCorrect.Text += "Niveau bevat illegale leestekens";
			}
			if (txtNiveau.Text.Trim() == "")
			{
				if (lblNameCorrect.Text != "")
					lblNameCorrect.Text += ", ";
				lblNameCorrect.Text += "Niveau is leeg";
			}
			else if (txtNiveau.BackColor == System.Drawing.Color.White && txtName.BackColor == System.Drawing.Color.White)
				lblNameCorrect.Text = "Correcte naam";

			if (lblNameCorrect.Text != "")
				lblNameCorrect.Text += ".";
		}

		#endregion

		#region Poule Players

		/// <summary>
		/// Voegt een lijst van spelers toe aan de poule
		/// </summary>
		/// <param name="players"> De lijst van spelers </param>
		public void AddPlayersToPoule(PlayerViews players)
		{
			PlayerViews newPlayers = new PlayerViews();
			foreach (PlayerView player in players)
				if ((player.Gender == "M" && (control.Poule.getGender() == PouleView.Gender.M || control.Poule.getGender() == PouleView.Gender.U))
					|| (player.Gender == "V" && (control.Poule.getGender() == PouleView.Gender.V || control.Poule.getGender() == PouleView.Gender.U)))
					newPlayers.Add(player);
			if (newPlayers.Count > 0)
				control.AddPlayersToPoule(newPlayers);
		}

		/// <summary>
		/// Valideert of een poule spelers van het juiste geslacht bevat
		/// </summary>
		/// <param name="oldPoule"></param>
		public void CheckPoulePlayers(string oldPoule)
		{
			bool correctList = false;
			PlayerViews removeList = new PlayerViews();

			foreach (PlayerView player in control.Poule.Players)
			{
				// Speler hoort niet in deze nieuwe poule thuis
				if (!((player.Gender == "M" && (control.Poule.getGender() == PouleView.Gender.M || control.Poule.getGender() == PouleView.Gender.U))
					|| (player.Gender == "V" && (control.Poule.getGender() == PouleView.Gender.V || control.Poule.getGender() == PouleView.Gender.U))))
				{
					if (!correctList && MessageBox.Show("Er mogen geen vrouwen in mannenpoules of mannen in vrouwenpoules. Als U doorgaat worden deze spelers uit de poule verwijderd.\n Wilt U doorgaan?", "Indelingsfout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					{
						control.Poule.Discipline = oldPoule;
						cboDisciplines.SelectedItem = oldPoule;
					}
					else
						correctList = true;
					removeList.Add(player);
				}
			}

			if (correctList)
				foreach (PlayerView player in removeList)
				{
					control.Poule.Players.Remove(player);
				}

			UpdatePoulePlayersList();
		}


		/// <summary>
		/// Update de spelerlijst en valideert de correctheid van de poule
		/// </summary>
		public void UpdatePoulePlayersList()
		{
			lvwPoulePlayers.DataSource = control.GetPoulePlayers();
			pouleCorrectness();
			lblAmount.Text = control.GetPoulePlayers().Count.ToString();
		}

		/// <summary>
		/// Initializeert de disciplines in de combobox
		/// </summary>
		protected void initializeDisciplines()
		{
			cboDisciplines.Items.AddRange(control.Poule.getDisciplines());
			cboDisciplines.SelectedIndex = 0;
		}

		/// <summary>
		/// Valideert of een poule correct is
		/// </summary>
		private void pouleCorrectness()
		{
			PouleView poule = control.Poule;
			btnOk.Enabled = false;

			//Geen pselers
			if (control.Poule.AmountPlayers == 0)
				lblPouleCorrectness.Text = ("Poule bevat geen spelers.");
			else if (poule.DisciplineEnum == Poule.Disciplines.Mixed)
			{
				//Te weinig spelers
				if (poule.AmountPlayers < 8)
					lblPouleCorrectness.Text = ("Te weinig spelers. Minimaal 8 nodig.");
				//Aantal mannen/vrouwen niet gelijk
				else if (poule.AmountMen != poule.AmountWomen)
					lblPouleCorrectness.Text = ("Aantal mannen komt niet overeen met het aantal vrouwen");
				//Niet genoeg spelers om een even aantal teams te vormen 
				else if (poule.AmountPlayers % 2 == 0 && poule.AmountPlayers % 4 != 0)
				{
					lblPouleCorrectness.Text = ("Niet genoeg spelers om even aantal teams te vormen");
					btnOk.Enabled = true;
				}
				//Aantal spelers niet deelbaar door 4
				else if (poule.AmountPlayers % 4 != 0)
					lblPouleCorrectness.Text = ("Spelersaantal niet deelbaar door 4");
				//Poule is correct
				else
					pouleCorrect();
			}
			else if (poule.KindofTeam == PouleView.Kind.Dubbel)
			{
				//Te weinig spelers
				if (poule.AmountPlayers < 8)
					lblPouleCorrectness.Text = ("Te weinig spelers. Minimaal 8 nodig.");
				//Niet genoeg spelers om een even aantal teams te vormen
				else if (poule.AmountPlayers % 2 == 0 && poule.AmountPlayers % 4 != 0)
				{
					lblPouleCorrectness.Text = ("Niet genoeg spelers om even aantal teams te vormen");
					btnOk.Enabled = true;
				}
				//Aantal spelers niet deelbaar door 4
				else if (poule.AmountPlayers % 4 != 0)
					lblPouleCorrectness.Text = ("Spelersaantal niet deelbaar door 4");
				//Poule is correct
				else
					pouleCorrect();
			}
			else
			{
				//Te weinig spelers
				if (poule.AmountPlayers < 4)
					lblPouleCorrectness.Text = ("Te weinig spelers. Minimaal 4 nodig.");
				//Oneven aantal spelers
				else if (poule.AmountPlayers % 2 != 0)
				{
					lblPouleCorrectness.Text = ("Oneven aantal spelers");
					btnOk.Enabled = true;
				}
				//Poule is correct
				else 
					pouleCorrect();
			}
		}

		/// <summary>
		/// Laat zien dat de poule correct is
		/// </summary>
		private void pouleCorrect()
		{
			lblPouleCorrectness.Text = ("Correcte pouleindeling.");
			btnOk.Enabled = true;
		}
		#endregion

		#region Poule Toevoegen

		/// <summary>
		/// Voegt een poule toe, gegeven een lijst van teams
		/// </summary>
		/// <param name="teams"> De teams in de poule </param>
		public void AddPoule(TeamViews teams)
		{
			control.AddPoule(txtName.Text.Trim(), cboDisciplines.Text, txtNiveau.Text.Trim(), txtComments.Text.Trim(), teams);
			Close();
		}
		#endregion

		#region Buttons
		/// <summary>
		/// Opent een PlayersForm waar de gebruiker spelers kan selecteren om
		/// aan de poule toe te voegen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddPlayers_Click(object sender, EventArgs e)
		{
			PlayersForm playersForm;

			if (control.Poule.getGender() == PouleView.Gender.M)
				playersForm = new PlayersForm(control.GetMalePlayers());
			else if (control.Poule.getGender() == PouleView.Gender.V)
				playersForm = new PlayersForm(control.GetFemalePlayers());
			else
				playersForm = new PlayersForm(control.GetAllPlayers());

			playersForm.SelectDone += AddPlayersToPoule;
			playersForm.ShowDialog(this);
		}

		/// <summary>
		/// Controleert of alles geldig is ingevuld en maakt vervolgens de poule aan
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOk_Click(object sender, EventArgs e)
		{
			string name = txtName.Text.Trim();

			if (name.Length == 0)
				MessageBox.Show("Voer een naam in voor de poule.", "Ongeldige poulenaam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if (containsIllegalCharacters(name))
				MessageBox.Show("De opgegeven poulenaam is niet geldig.\n" +
					"(De naam moet beginnen en eindigen met een letter of cijfer \n" +
					"en alleen de leestekens -_' en spatie zijn toegestaan)", "Ongeldige Poulenaam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if (!control.CheckForValidPouleName(name))
				MessageBox.Show("Er bestaat al een poule met deze naam.", "Ongeldige Poulenaam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if (txtNiveau.Text.Trim() == "")
				MessageBox.Show("Voer een niveau in voor de poule.", "Ongeldige niveau", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else if (containsIllegalCharacters(name))
				MessageBox.Show("Het opgegeven niveau is niet geldig.\n" +
					"(De naam moet beginnen en eindigen met een letter of cijfer \n" +
					"en alleen de leestekens -_' en spatie zijn toegestaan)", "Ongeldig niveau", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else
			{
				// Kijk of je de teams moet arrangen 
				if (control.Poule.KindofTeam == PouleView.Kind.Enkel)
				{
					PlayerViews players = control.GetPoulePlayers();
					TeamViews teams = new TeamViews();
					foreach (PlayerView player in players)
						teams.Add(new TeamView(new Team(player.Name, player.Domain)));
					AddPoule(teams);
				}
				else
				{
					TeamsForm teamsForm = new TeamsForm(control.Poule, control.GetPoulePlayers());
					teamsForm.DoneTeaming += AddPoule;
					teamsForm.ShowDialog(this);
				}
			}
		}
		
		/// <summary>
		/// Sluit het scherm
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		#region ContextMenu

		/// <summary>
		/// Verwijdert een speler uit de poule
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void verwijderSpelerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			control.RemovePlayerFromPoule((PlayerView)lvwPoulePlayers.SelectedItems[0].Tag);
		}
		/// <summary>
		/// Verwijdert meerdere spelers uit de poule
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmsItemRemovePlayers_Click(object sender, EventArgs e)
		{
			PlayerViews removePlayers = new PlayerViews();
			for (int i = 0; i < lvwPoulePlayers.SelectedItems.Count; i++)
				removePlayers.Add((PlayerView)lvwPoulePlayers.SelectedItems[i].Tag);	
				
			control.RemovePlayersFromPoule(removePlayers);
		}

		#endregion

		#region Listeners

		/// <summary>
		/// Open rechtsklik menu.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvwPoulePlayers_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (lvwPoulePlayers.SelectedIndices.Count == 1)
				{
					cmsItemRemovePlayer.Visible = true;
					cmsItemRemovePlayers.Visible = false;
					cmsPlayerList.Show(lvwPoulePlayers, e.Location);
				}
				else
				{
					cmsItemRemovePlayer.Visible = false;
					cmsItemRemovePlayers.Visible = true;
					cmsPlayerList.Show(lvwPoulePlayers, e.Location);
				}
			}
		}

		/// <summary>
		/// Als een andere discipline is geselecteerd in de combobox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbxDisciplines_SelectedIndexChanged(object sender, EventArgs e)
		{
			string oldPoule = control.Poule.Discipline;
			control.Poule.Discipline = (String)cboDisciplines.SelectedItem;
			CheckPoulePlayers(oldPoule);
			if (!nameChanged)
				fillInNameBox();
			pouleCorrectness();
		}

		/// <summary>
		/// Als de poulenaam wordt aangepast.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtName_TextChanged(object sender, EventArgs e)
		{
			nameChanged = true;

			string input = txtName.Text.Trim();

			// Controleer de input en geef de gebruiker feedback
			if (containsIllegalCharacters(input) || !control.CheckForValidPouleName(input))
				txtName.BackColor = System.Drawing.Color.Pink;
			else
				txtName.BackColor = System.Drawing.Color.White;
			NameCorrectness();
		}

		/// <summary>
		/// Als het pouleniveau wordt aangepast.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtNiveau_TextChanged(object sender, EventArgs e)
		{
			if (!nameChanged)
			{
				if (containsIllegalCharacters(txtNiveau.Text.Trim()))
					txtNiveau.BackColor = System.Drawing.Color.Pink;
				else
					txtNiveau.BackColor = System.Drawing.Color.White;
				fillInNameBox();
			}
		}

		/// <summary>
		/// Het sorteren van de spelerslijst per kolom.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvwPoulePlayers_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			// Als de kolom nu een andere kolom is
			if (e.Column != playersSort)
			{
				// Update welke kolom je hebt gesorteerd
				playersSort = e.Column;

				// Zet het sorteren nu op oplopend
				lvwPoulePlayers.Sorting = SortOrder.Ascending;
			}
			else
			{
				// Als het dezelfde kolom was, kijk dan of hij
				// vorige keer oplopend was, en doe nu het tegenovergestelde
				if (lvwPoulePlayers.Sorting == SortOrder.Ascending)
					lvwPoulePlayers.Sorting = SortOrder.Descending;
				else
					lvwPoulePlayers.Sorting = SortOrder.Ascending;
			}

			// Laat de lijst sorteren
			lvwPoulePlayers.Sort();

			// Stel nu een nieuwe sorteerder in
			lvwPoulePlayers.ListViewItemSorter = new AllColumnSorter(e.Column, lvwPoulePlayers.Sorting);
		}

		#endregion

	}
}
