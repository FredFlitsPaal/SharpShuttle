using System.Windows.Forms;
using Client.Controls;
using Client.Forms.ScoresInput;
using Shared.Communication.Serials;
using Shared.Domain;
using Shared.Views;
using System;
using System.Drawing;
using System.Collections.Generic;
using UserControls.Docking;
using UserControls.NotificationControls;

namespace Client.Forms.CourtInformation
{
	/// <summary>
	/// Het veldplanner scherm dat een overzicht biedt van de wedstrijden en de velden.
	/// </summary>
	public partial class CourtInformationForm : BasicDockForm
	{
		/// <summary>
		/// Businesslogica
		/// </summary>
		private CourtMatchControl control;

		/// <summary>
		/// Invoke om een Matchupdate te invoken
		/// </summary>
		/// <param name="matchID"></param>
		public delegate void MatchInvoker(int matchID);

		/// <summary>
		/// Aantal velden
		/// </summary>
		private int numberCourts = 10;

		/// <summary>
		/// Nog niet afgeronde wedstrijden
		/// </summary>
		private Dictionary<int, MatchView> currentMatches;

		/// <summary>
		/// Velden
		/// </summary>
		private Dictionary<int, Court> courts;

		/// <summary>
		/// ActionControl voor dit scherm.
		/// </summary>
		private CourtInformationActionControl actionControl = new CourtInformationActionControl();

		#region Constructor & init methode

		/// <summary>
		/// Default constructor
		/// </summary>
		public CourtInformationForm()
		{
			control = new CourtMatchControl();

			InitializeComponent();

			init();

			SerialTracker.Instance.MatchUpdated += matchUpdated;
			Configurations.TournamentSettingsChangedEvent += CourtInformationForm_ConfigsChanged;

			func_SetFunctionalities();
		}

		/// <summary>
		/// Voegt een nieuwe geupdate wedstrijd toe aan de lijst of verwijdert hem juist
		/// </summary>
		/// <param name="matchID"> De wedstrijd ID </param>
		public void processUpdatedMatch(int matchID) 
		{
			MatchView mv = control.GetMatch(matchID);

			if (mv != null && !mv.Domain.Disabled)
			{
				int courtID = -1;

				//Kijk of de wedstrijd al op een veld staat
				for (int i = 1; i <= courts.Count; i++)
					if (courts[i].Match != null)
						if (courts[i].Match.Domain.MatchID == matchID)
						{
							courtID = i;
							break;
						}

				//Toevoegen van nieuwe wedstrijden aan de wedstrijdenlijst
				if (mv.StartTime != null && mv.EndTime != null && mv.Court == 0 && !mv.Domain.Played)
				{
					bool inList = false;

					//Wedstrijd is verplaatst van een veld naar de wedstrijdenlijst
					if (courtID > 0)
					{
						clearCourt(courtID);
						courts[courtID].Reset();
						checkLocks();
					}

					for (int i = 0; i < lvwMatches.Items.Count; i++)
					{
						MatchView aMatch = (MatchView)lvwMatches.Items[i].Tag;

						if (aMatch != null && aMatch.MatchID == mv.MatchID)
						{
							inList = true;
							break;
						}
					}

					if (!inList)
					{
						addMatchtoMatchList(mv, -1);
						checkLocks();
					}
				}

				// Wedstrijd is gepland op een veld maar nog niet gespeeld
				if (mv.Court > 0 && !mv.Domain.Played)
				{
					// Wedstrijd is verplaatst van een veld naar een ander veld
					if (courtID > 0 && mv.Court != courtID)
					{
						clearCourt(courtID);
						courts[courtID].Reset();
					}
					
					// Wedstrijd opslaan op veld
					courts[mv.Court].Match = mv;
					removeMatchFromMatchList(matchID);
					addMatchToCourt(mv.Court, mv);
					checkLocks();
				}

				// Verwijderen van een wedstrijd waarvan de score is ingevuld van de velden.
				if (mv.Domain.Played || mv.Domain.EndTime != "")
				{
					for (int i = 0; i < lvwCourts.Items.Count; i++)
					{
						MatchView aMatch = (MatchView)lvwCourts.Items[i].Tag;

						if (aMatch != null && aMatch.MatchID == mv.MatchID)
						{
							clearCourt(i + 1);
							courts[i+1].Reset();
						}
					}
				}

				// Verwijderen van een wedstrijd waarvan de score is ingevuld uit de wedstrijdenlijst
				if (mv.Domain.Played)
				{
					removeMatchFromMatchList(matchID);
				}
			}
			else
			{
				//Als een wedstrijd verwijderd is, dan moet deze hier ook verwijderd worden.
				removeMatchFromMatchList(matchID);

				//of van een veld gehaald worden.
				for (int i = 1; i <= courts.Count; i++)
					if (courts[i].Match != null)
						if (courts[i].Match.Domain.MatchID == matchID)
						{
							courts[i].Reset();
							clearCourt(i);
							break;
						}
			}
		}

		private void matchUpdated(int matchID, SerialEventTypes e)
		{
			Invoke(new MatchInvoker(processUpdatedMatch), new object[] {matchID});
		}

		/// <summary>
		/// Initialiseert de GUI elementen en de timer
		/// </summary>
		private void init()
		{
			numberCourts = control.NumberOfCourts;

			courts = new Dictionary<int, Court>();

			// Voeg velden toe
			for (int x = 1; x <= numberCourts; x++)
				addNewCourt(x);

			//Vul de velden met wedstrijden die bezig zijn.
			fillCourts();

			//Haal alle nog niet gestarte en niet geëindigde wedstrijden op
			lvwMatches.DataSource = control.getMatchesToPlay();
			currentMatches = new Dictionary<int, MatchView>();

			foreach (MatchView mv in control.getMatchesToPlay())
				currentMatches.Add(mv.Domain.MatchID, mv);

			checkLocks();

			// Timer instellen
			courtTimer.Start();
		}

		/// <summary>
		/// Zet de functionaliteiten van het scherm goed.
		/// </summary>
		private void func_SetFunctionalities() 
		{
			cmsBack.Enabled = actionControl.CheckUnplaceMatch();
			cmsStart.Enabled = actionControl.CheckStartMatch();
			cmsStop.Enabled = actionControl.CheckEndMatch();
			btnStart.Enabled = actionControl.CheckStartMatch();
			btnStop.Enabled = actionControl.CheckEndMatch();
			lvwCourts.AllowDrop = actionControl.CheckPlaceMatch();
			//Niet perfect, maar moeilijk om onderscheid te maken tussen 2 soorten drags.
			lvwMatches.AllowDrop = (actionControl.CheckSortMatches() && actionControl.CheckUnplaceMatch());
		}

		/// <summary>
		/// Het sluiten van de timer die actief is bij het sluiten van het venster.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CourtInformationForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			courtTimer.Stop();
			Configurations.TournamentSettingsChangedEvent -= CourtInformationForm_ConfigsChanged;
			SerialTracker.Instance.MatchUpdated -= matchUpdated;
		}

		#endregion

		#region Drag-drop methodes

		/// <summary>
		/// Het draggen van een item van de veldenlijst.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvCourts_ItemDrag(object sender, ItemDragEventArgs e)
		{
			// Geselecteerde items
			ListViewItem selected = lvwCourts.SelectedItems[0];

			// De drag starten
			lvwCourts.DoDragDrop(selected, DragDropEffects.Move);
		}

		/// <summary>
		/// Het draggen van een item uit de wedstrijdenlijst
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvMatches_ItemDrag(object sender, ItemDragEventArgs e)
		{
			// Geselecteerds items
			ListViewItem selected = lvwMatches.SelectedItems[0];

			// Drag starten
			lvwMatches.DoDragDrop(selected, DragDropEffects.Move);
		}

		/// <summary>
		/// Het drageffect bij het draggen in de veldenlijst.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvCourts_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		/// <summary>
		/// Het drageffect bij het draggen in de wedstrijdenlijst.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvMatches_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		/// <summary>
		/// Het droppen van items op de lvCourts wordt afgehandeld.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvCourts_DragDrop(object sender, DragEventArgs e)
		{
			// Het ontvangen listview item
			ListViewItem fromItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
			ListView fromList = fromItem.ListView;

			// Punt waar het event is
			Point p = lvwCourts.PointToClient(new Point(e.X, e.Y));

			// Het geraakte listviewitem
			ListViewItem toItem = lvwCourts.GetItemAt(p.X, p.Y);

			if (toItem == null)
				return;

			while (lvwCourts.SelectedItems.Count > 0)
				lvwCourts.SelectedItems[0].Selected = false;

			toItem.Selected = true;
			lvwCourts.Focus();

			// Matchview ophalen
			MatchView mv = (MatchView)fromItem.Tag;

			//Een wedstrijd slepen vanuit de wedstrijdenlijst naar een veld. Alleen mogelijk
			//als het veld leeg is
			if (fromList == lvwMatches && courts[toItem.Index + 1].Match == null)
			{
				DialogResult result = DialogResult.Yes;
				
				//Asl er een speler is die al speelt wordt een messagebox getoond.
				if (fromItem.SubItems[0].BackColor == Color.Tomato)
				{
					result = MessageBox.Show("De gesleepte wedstrijd bevat spelers die nog ingepland staan op een veld."
					                + "\nWeet u zeker dat u de wedstrijd wilt inplannen op het veld?",
					                "Wedstrijd met spelende spelers"
					                , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				}

				if (result == DialogResult.Yes)
				{
					// Wedstrijd opslaan op veld
					courts[toItem.Index + 1].Match = mv;

					//wedstrijd uit de lijst van aankomende wedstrijden halen.
					lvwMatches.Items.Remove(fromItem);

					//gui updaten dat de wedstrijd te zien is op de court
					addMatchToCourt(toItem.Index + 1, mv);

					//zet het veld in de match op het desbetreffende veld.
					mv.Court = toItem.Index + 1;

					checkLocks();
				}
			}
			else if (fromList == lvwCourts && toItem != fromItem)
			{
				if (toItem.Tag == null)
				{
					DateTime time = courts[fromItem.Index + 1].StartTime;
					MatchView matchView = courts[fromItem.Index + 1].Match;

					courts[fromItem.Index + 1].Reset();
					courts[toItem.Index + 1].Match = matchView;
					courts[toItem.Index + 1].StartTime = time;

					addMatchToCourt(toItem.Index + 1, matchView);
					clearCourt(fromItem.Index + 1);
				}
				else
				{
					DateTime fromTime = courts[fromItem.Index + 1].StartTime;
					MatchView fromMatchView = courts[fromItem.Index + 1].Match;

					DateTime toTime = courts[toItem.Index + 1].StartTime;
					MatchView toMatchView = courts[toItem.Index + 1].Match;

					courts[toItem.Index + 1].Match = fromMatchView;
					courts[toItem.Index + 1].StartTime = fromTime;
					courts[fromItem.Index + 1].Match = toMatchView;
					courts[fromItem.Index + 1].StartTime = toTime;

					addMatchToCourt(toItem.Index + 1, fromMatchView);
					addMatchToCourt(fromItem.Index + 1, toMatchView);
				}
			}
		}

		/// <summary>
		/// Het droppen van items op de wedstrijdenlijst.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvMatches_DragDrop(object sender, DragEventArgs e)
		{
			// Het ontvangen listview item
			ListViewItem fromItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
			ListView fromList = fromItem.ListView;

			// Punt waar het event is
			Point p = lvwMatches.PointToClient(new Point(e.X, e.Y));

			// Het geraakte listviewitem
			ListViewItem toItem = lvwMatches.GetItemAt(p.X, p.Y);

			if (fromList == lvwMatches)
			{
				if (toItem == null)
					return;

				//Sleep het item boven het item waar je opsleept
				if (toItem.Index != fromItem.Index)
				{
					lvwMatches.Items.Remove(fromItem);
					lvwMatches.Items.Insert(toItem.Index, fromItem);
					lvwMatches.Refresh();
				}
			}
			//terugslepen van een wedstrijd op een veld naar de wedstrijdenlijst.
			else if (fromList == lvwCourts)
			{
				MatchView tv = (MatchView)fromItem.Tag;

				clearCourt(tv.Court);
				courts[tv.Court].Reset();

				control.resetMatch(tv);

				checkLocks();
			}

		}

		#endregion

		#region Events afhandeling

		/// <summary>
		/// Opvangen van een een verandering in de configurations file.
		/// </summary>
		private void CourtInformationForm_ConfigsChanged()
		{
			int newFields = Configurations.NumberOfCourts;

			if (newFields == courts.Count)
				return;
			
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(CourtInformationForm_ConfigsChanged));
				return;
			}
			
			//velden verwijderen inculsief de wedstrijden terug zetten
			if (newFields < courts.Count)
			{
				int[] keys = new int[courts.Count];
				courts.Keys.CopyTo(keys,0);
				
				for (int i = keys[keys.Length-1]; i > newFields; i--)
				{
					MatchView mv = courts[i].Match;

					if (mv != null)
					{
						addMatchtoMatchList(mv, -1);
						control.resetMatch(mv);
					}

					lvwCourts.Items.RemoveAt(i - 1);

					courts.Remove(i);
				}
			}

			//extra velden toevoegen
			else if (newFields > courts.Count)
			{
				for (int x = courts.Count + 1; x <= newFields; x++)
					addNewCourt(x);
			}
		}

		#endregion

		#region Buttons

		/// <summary>
		/// Start de geselecteerde wedstrijd
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStart_Click(object sender, EventArgs e)
		{
			bool reStart = false;
			DialogResult result = DialogResult.None;

			foreach (ListViewItem lvi in lvwCourts.SelectedItems)
			{
				if (lvi.Tag != null)
				{
					MatchView mv = (MatchView) lvi.Tag;
					if (mv.StartTime != "")
						reStart = true;
				}
			}

			if (reStart)
			{
				if (lvwCourts.SelectedItems.Count == 1)
					result = MessageBox.Show("Wilt u de wedstrijd herstarten?", "Wedstrijd Herstarten",
											 MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				else if (lvwCourts.SelectedItems.Count > 1)
					result = MessageBox.Show("Wilt u de wedstrijden herstarten?", "Wedstrijden Herstarten",
											 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			}

			else result = DialogResult.Yes;

			if (result == DialogResult.Yes)
			{
				foreach (ListViewItem lvi in lvwCourts.SelectedItems)
				{
					if (lvi.Tag != null)
					{
						MatchView mv = (MatchView) lvi.Tag;

						DateTime now = DateTime.Now;
						control.startMatch(mv, now);
						courts[lvi.Index + 1].StartTime = now;
					}
				}
			}
		}

		/// <summary>
		/// Beeindigt de geselecteerde wedstrijd
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStop_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in lvwCourts.SelectedItems)
			{
				if (lvi.Tag != null && ((MatchView)lvi.Tag).StartTime != "")
				{
					MatchView mv = (MatchView)lvi.Tag;
					control.endMatch(mv, DateTime.Now);

					courts[lvi.Index + 1].Reset();

					clearCourt(lvi.Index + 1);

					checkLocks();
				}
			}
		}

		#endregion

		#region Rechtermuisknop op de velden lijst

		/// <summary>
		/// rechter muisklik op de lvwCourts
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvwCourts_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (lvwCourts.SelectedItems.Count == 1)
					if(lvwCourts.SelectedItems[0].Tag != null)
						cmsCourts.Show(lvwCourts, e.Location);
			}
		}

		/// <summary>
		/// Rechtermuisknop start wedstrijd wordt de wedstrijd gestart.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmsStart_Click(object sender, EventArgs e)
		{
			ListViewItem lvi = lvwCourts.SelectedItems[0];

			if (lvi.Tag != null)
			{
				MatchView mv = (MatchView)lvi.Tag;

				DateTime now = DateTime.Now;
				control.startMatch(mv, now);
				courts[lvi.Index + 1].StartTime = now;
			}
		}

		/// <summary>
		/// Stop een wedstrijd die bezig is op een veld met de rechtermuisknop.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmsStop_Click(object sender, EventArgs e)
		{
			ListViewItem lvi = lvwCourts.SelectedItems[0];

			if (lvi.Tag != null && ((MatchView)lvi.Tag).StartTime != "")
			{
				MatchView mv = (MatchView)lvi.Tag;
				control.endMatch(mv, DateTime.Now);

				courts[lvi.Index + 1].Reset();

				clearCourt(lvi.Index + 1);

				checkLocks();
			}
		}

		/// <summary>
		/// Zet een wedstrijd die is ingepland op een veld terug naar aankomende wedstrijden.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmsBack_Click(object sender, EventArgs e)
		{
			MatchView tv = (MatchView)lvwCourts.SelectedItems[0].Tag;

			clearCourt(tv.Court);
			courts[tv.Court].Reset();

			addMatchtoMatchList(tv, -1);

			control.resetMatch(tv);

			checkLocks();
		}

		#endregion

		#region Bewerking op de veldenlijst

		/// <summary>
		/// Voeg een nieuwe court toe aan de veldenlijst
		/// </summary>
		/// <param name="courtID">Het nummer van de court</param>
		private void addNewCourt(int courtID)
		{
			Court court = new Court(courtID);
			courts.Add(courtID, court);
			lvwCourts.Items.Add(new ListViewItem(new[] { "Veld " + court.court, "", "", "", "", "", "" }));
			lvwCourts.Items[courtID - 1].Tag = null;
		}

		/// <summary>
		/// Het vullen van de veldenlijst met wedstrijden die al op velden staan.
		/// </summary>
		private void fillCourts()
		{
			MatchViews matches = control.getCourtMatches();

			foreach (MatchView mv in matches)
			{
				//bij veld verkleining  wordt een wedstrijd die bezig was op het een veld wat 
				//niet meer bestaat beëindigd.
				if (mv.Court > courts.Count)
				{
					control.resetMatch(mv);
				}

				else
				{
					courts[mv.Court].Match = mv;

					if (mv.StartTime != "")
					{
						DateTime date = Convert.ToDateTime(mv.StartTime);

						courts[mv.Court].StartTime = date;
					}

					addMatchToCourt(mv.Court, mv);
				}
			}
		}

		/// <summary>
		/// Voegt een wedstrijd toe aan de velden listview.
		/// </summary>
		/// <param name="court"></param>
		/// <param name="mv"></param>
		private void addMatchToCourt(int court, MatchView mv)
		{
			control.assignToCourt(mv, court);

			lvwCourts.Items[court - 1].Tag = mv;
			lvwCourts.Items[court - 1].SubItems[1].Text = mv.MatchID;
			lvwCourts.Items[court - 1].SubItems[2].Text = mv.Team1;
			lvwCourts.Items[court - 1].SubItems[3].Text = mv.Team2;
			lvwCourts.Items[court - 1].SubItems[4].Text = mv.PouleName;
			lvwCourts.Items[court - 1].SubItems[5].Text = mv.Round.ToString();
		}

		/// <summary>
		/// Het leeg maken van het velditem uit de veldenlijst.
		/// </summary>
		/// <param name="court"></param>
		private void clearCourt(int court)
		{
			lvwCourts.Items[court - 1].BackColor = Color.White;
			lvwCourts.Items[court - 1].Tag = null;
			lvwCourts.Items[court - 1].SubItems[1].Text = "";
			lvwCourts.Items[court - 1].SubItems[2].Text = "";
			lvwCourts.Items[court - 1].SubItems[3].Text = "";
			lvwCourts.Items[court - 1].SubItems[4].Text = "";
			lvwCourts.Items[court - 1].SubItems[5].Text = "";
			lvwCourts.Items[court - 1].SubItems[6].Text = "";
		}

		/// <summary>
		/// De timers van de wedstrijden die zijn gestart.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void courtTimer_Tick(object sender, EventArgs e)
		{
			foreach (Court c in courts.Values)
			{
				if (c.StartTime != DateTime.MinValue)
				{
					// Tijd formatteren
					TimeSpan timel = DateTime.Now.Subtract(c.StartTime);
					string time = formatTime(timel);

					if (c.Match != null)
					{
						lvwCourts.Items[c.court - 1].SubItems[6].Text = time;

						if (timel.TotalMinutes > 15)
							lvwCourts.Items[c.court - 1].BackColor = Color.Orange;

						else lvwCourts.Items[c.court - 1].BackColor = Color.White;
					}
				}
			}
		}

		#endregion

		#region Bewerking op wedstrijdenlijst

		/// <summary>
		/// Het toevoegen van een wedstrijd aan de wedstrijdelijst.
		/// -1 als index als je onderaan wilt toevoegen
		/// </summary>
		/// <param name="mv"></param>
		/// <param name="index">index waar het itme moet komen, -1 voor onderaan</param>
		private void addMatchtoMatchList(MatchView mv, int index)
		{
			ListViewItem newlvi = new ListViewItem(mv.MatchID);

			newlvi.SubItems.Add(mv.Team1Player1);
			newlvi.SubItems.Add(mv.Team1Player2);
			newlvi.SubItems.Add(mv.Team2Player1);
			newlvi.SubItems.Add(mv.Team2Player2);

			newlvi.SubItems.Add(mv.PouleName);
			newlvi.SubItems.Add(mv.Round.ToString());
			newlvi.Tag = mv;

			if (index != -1)
				lvwMatches.Items.Insert(index, newlvi);
			else lvwMatches.Items.Add(newlvi);

			lvwMatches.Refresh();
		}

		/// <summary>
		/// Verwijder een match uit de matchlijst
		/// </summary>
		/// <param name="matchID">Het match id</param>
		private void removeMatchFromMatchList(int matchID)
		{
			for (int i = 0; i < lvwMatches.Items.Count; i++)
				if (((MatchView)lvwMatches.Items[i].Tag).Domain.MatchID == matchID)
				{
					lvwMatches.Items[i].Remove();
					break;
				}
		}

		#endregion

		#region Kleuringen van de spelers in de wedstrijdenlijst

		/// <summary>
		/// Bekijkt of er spelers zijn die al op een veld zijn ingedeeld en kleurt de
		/// wedstrijd in de wedstrijden listview rood.
		/// </summary>
		private void checkLocks()
		{
			//Een set met alle spelende spelers
			HashSet<int> playing = new HashSet<int>();

			//Nu alle spelende players ophalen en in de hashset stoppen
			foreach (Court c in courts.Values)
			{
				if (c.Match != null)
				{
					playing.Add(c.Match.Domain.TeamA.Player1.PlayerID);
					if (c.Match.Domain.TeamA.Player2 != null)
						playing.Add(c.Match.Domain.TeamA.Player2.PlayerID);

					playing.Add(c.Match.Domain.TeamB.Player1.PlayerID);
					if (c.Match.Domain.TeamB.Player2 != null)
						playing.Add(c.Match.Domain.TeamB.Player2.PlayerID);
				}
			}

			foreach (ListViewItem lvi in lvwMatches.Items)
			{
				lvi.UseItemStyleForSubItems = false;

				Shared.Domain.Match mv = ((MatchView)lvi.Tag).Domain;

				//Controleren of geen van de spelers van een westrijd speelt
				if (!playing.Contains(mv.TeamA.Player1.PlayerID) &&
					!playing.Contains(mv.TeamB.Player1.PlayerID) &&
					(mv.TeamA.Player2 == null || !playing.Contains(mv.TeamA.Player2.PlayerID)) &&
					(mv.TeamB.Player2 == null || !playing.Contains(mv.TeamB.Player2.PlayerID)))
				{
					lvi.SubItems[0].BackColor = Color.LightGreen;
					lvi.SubItems[1].BackColor = Color.LightGreen;
					lvi.SubItems[3].BackColor = Color.LightGreen;

					//afhankelijk van een 1speler team of 2 speler team wordt
					//de 2de spler van het team gekleurd.
					if (mv.TeamA.Player2 != null && mv.TeamB.Player2 != null)
					{
						lvi.SubItems[2].BackColor = Color.LightGreen;
						lvi.SubItems[4].BackColor = Color.LightGreen;
					}

					else
					{
						lvi.SubItems[2].BackColor = Color.White;
						lvi.SubItems[4].BackColor = Color.White;
					}
				}

				//Als er een of meerdere spelers van de wedstrijd aan het spelen zijn
				else
				{
					lvi.SubItems[0].BackColor = Color.Tomato;

					if (playing.Contains(mv.TeamA.Player1.PlayerID))
						lvi.SubItems[1].BackColor = Color.Tomato;
					else lvi.SubItems[1].BackColor = Color.LightGreen;

					if (playing.Contains(mv.TeamB.Player1.PlayerID))
						lvi.SubItems[3].BackColor = Color.Tomato;
					else lvi.SubItems[3].BackColor = Color.LightGreen;

					if (mv.TeamA.Player2 == null)
						lvi.SubItems[2].BackColor = Color.White;
					else if (playing.Contains(mv.TeamA.Player2.PlayerID))
						lvi.SubItems[2].BackColor = Color.Tomato;
					else lvi.SubItems[2].BackColor = Color.LightGreen;

					if (mv.TeamB.Player2 == null)
						lvi.SubItems[4].BackColor = Color.White;
					else if (playing.Contains(mv.TeamB.Player2.PlayerID))
						lvi.SubItems[4].BackColor = Color.Tomato;
					else lvi.SubItems[4].BackColor = Color.LightGreen;
				}
			}
		}

		#endregion

		#region Hulpmethodes

		/// <summary>
		/// Formatteren van de tijd naar een string.
		/// </summary>
		/// <param name="time">de tijd die moet worden omgezet</param>
		/// <returns>string met de tijd</returns>
		public string formatTime(TimeSpan time)
		{
			string h, m, s;

			int hours = time.Days * 24;
			hours += time.Hours;

			int minutes = time.Minutes;

			int seconds = time.Seconds;


			if (hours < 10)
				h = "0" + hours;
			else
				h = "" + hours;

			if (minutes < 10)
				m = "0" + minutes;
			else
				m = "" + minutes;

			if (seconds < 10)
				s = "0" + seconds;
			else
				s = "" + seconds;

			return String.Format("{0}:{1}:{2}", h, m, s);
		}

		#endregion

	}

	#region Klasse Court voor een veld

	/// <summary>
	/// Veld met wedstrijd en begintijd en het veldnummer.
	/// </summary>
	public class Court
    {
        /// Veldnummer
        public int court;

        /// Wedstrijd op het veld
        private MatchView match;
        
        // Begintijd
    	private DateTime startTime;

		/// <summary>
		/// Nieuw veld
		/// </summary>
		/// <param name="c"></param>
        public Court(int c)
        {
            court = c;
        }

		/// <summary>
		/// De wedstrijd op het veld.
		/// </summary>
    	public MatchView Match
    	{
			get { return match; }
			set 
			{ 
				match = value;
				if (value.StartTime == "")
					startTime = DateTime.MinValue;
				else startTime = Convert.ToDateTime(value.StartTime);

				value.Court = court;
			}
    	}

		/// <summary>
		/// Starttijd van de wedstrijd op het veld.
		/// </summary>
    	public DateTime StartTime
    	{
			get { return startTime; }
			set { startTime = value; }
    	}

		/// <summary>
		/// Resetten van een veld.
		/// </summary>
    	public void Reset()
        {
            // Match resetten
            if (match != null)
            {
                match = null;
            }

            // Startijd en resetten
            startTime = DateTime.MinValue;
        }
	}

	#endregion

}
