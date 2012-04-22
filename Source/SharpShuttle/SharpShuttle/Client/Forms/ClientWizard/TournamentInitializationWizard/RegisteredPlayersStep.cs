using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Shared.Views;
using UserControls.AbstractWizard;
using Shared.Data;
using Shared.Domain;
using Shared.Communication.Exceptions;

namespace Client.Forms.ClientWizard.TournamentInitializationWizard
{
	public partial class RegisteredPlayersStep : GenericAbstractWizardStep<object>
    {
		readonly IDataCache cache = IntermediateDataCache.Instance;
		public LinkedList<Participant> participants = new LinkedList<Participant>();
		public ChangeTrackingList<Player> allPlayers;
		public int lastID;

        // Op welke kolom de lijst het laatst is gesorteerd
        //private int participantsSort = -1;

        public RegisteredPlayersStep()
        {
			init();
		}

		private void init()
		{
			InitializeComponent();
			Text = "Inschrijvingen";
			previous_visible = true;
			next_visible = true;
			finish_visible = false;
		}

		/// <summary>
		/// wordt elke keer als erop volgende in het scherm wat voor dit scherm komt op nieuw aangeroepen
		/// hier zou je data van de server moeten worden geladen.
		/// </summary>
		public void SetForm()
		{
			GetPlayersFromDatabase();
		}

		/// <summary>
		/// Wordt aangeroepen als er op OK gedrukt wordt in de savedialog
		/// </summary>
		public override void Save()
		{
			saveContestants();
		}

		private void GetPlayersFromDatabase()
		{
			allPlayers = cache.GetAllPlayers();
			//lastID = allPlayers.Last().PlayerID;

			UpdatePlayers();
		}

        #region Eventhandlers

        private void Button_Import_Click(object sender, EventArgs e)
        {
            ImportPlayersForm import = new ImportPlayersForm(this);
            import.ShowDialog(this);
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            PlayerEditForm newform = new PlayerEditForm(this);
            newform.ShowDialog(this);
        }

        /// <summary>
        /// Het triggeren van op de button aanpassen klikken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Edit_Click(object sender, EventArgs e)
        {
            // Deelnemer aanpassen, geef een aanpasscherm
            if (ListView_Contestants.SelectedItems.Count == 1)
            {
				int index = ListView_Contestants.SelectedItems[0].Index;
                PlayerEditForm nd = new PlayerEditForm(this, index);
                nd.ShowDialog(this);
            }
        }

        /// <summary>
        /// Het triggeren van  het op de button verwijderen klikken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Remove_Click(object sender, EventArgs e)
        {
			int selection = ListView_Contestants.SelectedItems.Count;

			if (selection > 1)
			{
				DialogResult result = MessageBox.Show(this, "Weet u zeker dat u de geselecteerde spelers" +
					" wilt verwijderen?", "Deelnemers verwijderen", MessageBoxButtons.YesNo);

				if (result == DialogResult.Yes)
				{
					for (int i = selection-1; i >= 0; i--)
					{
						allPlayers.Remove(allPlayers.ElementAt(ListView_Contestants.SelectedIndices[i]));
					}
					UpdatePlayers();
				}
			}
			// Deelnemer verwijderen
            if (ListView_Contestants.SelectedItems.Count == 1)
            {
				int index = ListView_Contestants.SelectedItems[0].Index;
				Player selectedPlayer = allPlayers.ElementAt(index);
				
                // Vraag of de gebruiker het zeker weet
				DialogResult result = MessageBox.Show(this, "Weet u zeker dat u "+ selectedPlayer.Name +
					" wilt verwijderen?", "Deelnemers verwijderen", MessageBoxButtons.YesNo);
                // Zoja, verwijder hem dan
                if (result == DialogResult.Yes)
                {
                    allPlayers.Remove(selectedPlayer);
					UpdatePlayers();
                }
            }
        }

        //Sorteren gedeactiveerd omdat anders de lijst niet meer klopt.
		//oplossing zou zijn als elke listviewitem een id zie krijgen een selectedValue waarde
		#region Column click gedactiveerd
		/* private void ListView_Contestants_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            // Als de kolom nu een andere kolom is
            if (e.Column != participantsSort)
            {
                // Update welke kolom je hebt gesorteerd
                participantsSort = e.Column;

                // Zet het sorteren nu op oplopend
                ListView_Contestants.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Als het dezelfde kolom was, kijk dan of hij
                // vorige keer oplopend was, en doe nu het tegenovergestelde
                if (ListView_Contestants.Sorting == SortOrder.Ascending)
                    ListView_Contestants.Sorting = SortOrder.Descending;
                else
                    ListView_Contestants.Sorting = SortOrder.Ascending;
            }

            // Laat de lijst sorteren
            ListView_Contestants.Sort();

            // Stel nu een nieuwe sorteerder in
            ListView_Contestants.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              ListView_Contestants.Sorting);
        }*/
        #endregion
		
		#endregion

		public void UpdatePlayers()
		{
			PlayerViews pv = new PlayerViews();

			foreach (Player p in allPlayers)
				pv.Add(new PlayerView(p));

			ListView_Contestants.DataSource = pv;
		}

        #region Oude Rotzooi

        public void addContestant(String name)
        {
            // Toevoegen aan lijst
            ListView_Contestants.Items.Add(name);
        }

        public void editContestant(int id, String name)
        {
            // Toevoegen aan lijst
            ListView_Contestants.Items.Add(name);
        }

        public void importFile(String filename)
        {
            // Voeg deelnemers toe uit file
            participants.AddFirst(new Participant(new String[]{"1111", "Aad de Boer","M","Heren Single A","1.15","21","2","1"}));
            participants.AddFirst(new Participant(new String[]{ "1112", "Bert Grootjes", "M", "Heren Single A", "0.95", "15", "3", "2" }));
            participants.AddFirst(new Participant(new String[]{ "1113", "Jennifer Lange", "V", "Heren Single A", "1.55", "11", "1", "1" }));
            participants.AddFirst(new Participant(new String[]{ "1114", "Christina Reinders", "V", "Heren Single A", "1.34", "4", "4", "4" }));
            participants.AddFirst(new Participant(new String[]{ "1115", "Erika van der Brugge", "V", "Heren Single A", "0.47", "8", "5", "3" }));
            participants.AddFirst(new Participant(new String[]{ "1116", "Floor Stronk", "V", "Heren Single A", "1.32", "31", "1", "2" }));
            participants.AddFirst(new Participant(new String[]{ "1117", "Girza Benston", "V", "Heren Single A", "0.52", "-10", "3", "3" }));
            participants.AddFirst(new Participant(new String[]{ "1118", "Harm de Koning", "M", "Heren Single A", "1.12", "-40", "4", "4" }));
            participants.AddFirst(new Participant(new String[]{ "1119", "Dirk Lenzen", "M", "Heren Single A", "1.95", "13", "6", "2" }));
            participants.AddFirst(new Participant(new String[]{ "1120", "Ismael Sungs", "M", "Heren Single A", "0.25", "6", "3", "0" }));
            participants.AddFirst(new Participant(new String[]{ "1121", "Jan Kort", "M", "Heren Single A", "0.56", "3", "6", "1" }));

            UpdateList();
        }

        private void VoegBijDeelnemers(Participant dn)
        {
            // Voeg deelnemer toe bij deelnemerslijst
            ListView_Contestants.Items.Add(new ListViewItem(new String[] { dn.info[0], dn.info[1], dn.info[2], dn.info[3],""}));
        }

        public void UpdateList()
        {
            // Maak de lijst schoon
            ListView_Contestants.Items.Clear();

            // Voeg iedereen toe die voldoet aan de zoekstring (lege string laat iedereen zien)
            foreach (Participant deelnemer in participants)
            {
                VoegBijDeelnemers(deelnemer);
            }
        }
        #endregion

		#region Overrides of AbstractWizardStep<ServerView>

		protected override object readInput()
		{
			return null;
		}

		protected override bool validateNextInt()
		{
			saveContestants();
			return true;
		}

		protected override bool validatePreviousInt()
		{
			bool valid = true;
			if (allPlayers.Changed)
				valid = saveDialog("Opslaan Spelers", "Wilt U de veranderingen in de spelers opslaan?");
			return valid;
		}

		#endregion

		private void saveContestants()
		{
			CommunicationException ex;

			if (cache.SetAllPlayers(allPlayers, out ex))// het lukt
			{
				allPlayers = cache.GetAllPlayers();
				UpdatePlayers();
			}
			else 
			{ 
				Shared.Logging.Logger.Write("RegisteredContestantStep opslagfout", ex.ToString()); 
			}//hetlukt niet
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			saveContestants();
			IntermediateDataCache.Save(Configurations.CurrentFileName);
		}

    }

    //Implementeert het handmatige sorteren van items aan de hand van kolommen
	#region sorteren uitgezet
	/*class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;

        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }

        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;
            double result;
            if (Double.TryParse(((ListViewItem)x).SubItems[col].Text, out result))
            {
                double a = Double.Parse(((ListViewItem)x).SubItems[col].Text);
                double b = Double.Parse(((ListViewItem)y).SubItems[col].Text);

                if (a > b)
                    returnVal = -1;
                else
                    returnVal = 1;

                if (order == SortOrder.Descending)
                    // Invert the value returned by String.Compare.
                    returnVal *= -1;

                return returnVal;
            }

            returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                    ((ListViewItem)y).SubItems[col].Text);
            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }
    }*/

	#endregion

    //Wordt niet meer gebruikt
	public class Participant
    {
        public String[] info;

        public Participant(String[] i)
        {
            info = i;
        }
    }
}
