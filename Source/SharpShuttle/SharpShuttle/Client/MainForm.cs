using System;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Windows.Forms;
using Client.Controls;
using Client.Forms.ClientWizard.LoginWizard;
using Client.Forms.ClientWizard.TournamentInitializationWizard;
using Client.Forms.CourtInformation;
using Client.Forms.PopUpWindows;
using Client.Forms.PouleInformation;
using Client.Forms.ScoresInput;
using Client.Printers;
using Shared.Communication;
using Shared.Data;
using UserControls.AbstractWizard;
using Client.Forms.AddPoule;
using Client.Forms.AddPlayer;

namespace Client
{
	/// <summary>
	/// Het hoofdscherm van de applicatie
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// Business logica
		/// </summary>
		private MainFormActionControl actionControl;
		/// <summary>
		/// Is de toernooi initialisatie afgerond
		/// </summary>
		public bool initDone;

		#region singleton window declarations

		/// <summary>
		/// Het wedstrijdoverzicht
		/// </summary>
		private ScoreForm matchOverviewForm;
		/// <summary>
		/// Het veld overzicht
		/// </summary>
		private CourtInformationForm courtInformationForm;

		#endregion

		/// <summary>
		/// Default constructor 
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			actionControl = new MainFormActionControl();
			Configurations.UpdateLocal();
			Communication.ServerDisconnected += new Shared.Delegates.Delegates.ServerDisconnectedDelegate(Communication_ServerDisconnected);
		}

		void Communication_ServerDisconnected()
		{
			Invoke(new MethodInvoker(() =>
			{
				Form[] childs = MdiChildren;
				foreach (var child in childs)
				{
					RemoveOwnedForm(child);
					child.Close();
				}
				FunctionalityControl.Instance.CurrentRole = null;
				FunctionalitiesChanged();
			}));
		}

		/// <summary>
		/// Initialiseert de functionaliteiten en start de login wizard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Maximized;
			actionControl.FunctionalitiesChanged += FunctionalitiesChanged;
			FunctionalitiesChanged();
			runLoginWizard();
		}

		#region Functionaliteiten

		#region voor FunctionalityControl

		/// <summary>
		/// Kijkt of de initialisatie is afgelopen, en zet daarna alle functionaliteiten goed.
		/// </summary>
		public void FunctionalitiesChanged()
		{
			actionControl.CheckInitDone(FunctionalitiesAfterInit, FunctionalitiesBeforeInit);
		}

		/// <summary>
		/// Zet alle functionaliteiten goed.
		/// </summary>
		public void setAllFunctionalities()
		{
			//MENU
			//-Bestand
			func_setLogin();
			func_setPrintMatchPaper();
			//-Bewerken
			func_setInitTournament();
			func_setAddPoule();
			func_setManagePlayers();
			//-Vensters
			func_setScoreInput();
			func_setPouleInformation();
			func_setCourtInformation();
			//-Instellingen
			func_setAmountFields();
		}

		/// <summary>
		/// Zet de initialisatie op gedaan en verandert functionaliteiten.
		/// </summary>
		public void FunctionalitiesAfterInit() 
		{
			initDone = true;
			setAllFunctionalities();
		}

		/// <summary>
		/// Zet de initialisatie op ongedaan en verandert functionaliteiten.
		/// </summary>
		public void FunctionalitiesBeforeInit()
		{
			initDone = false;
			setAllFunctionalities();
			if (actionControl.CheckTournamentInit())
				this.Invoke(new MethodInvoker(runTournamentInitializationWizard));
		}

		#endregion

		#region Login Logout

		/// <summary>
		/// Enablet of Disablet de Login/Logout functionliteit in dit form
		/// </summary>
		private void func_setLogin()
		{
			fileLoginMenuItem.Enabled = actionControl.CheckLogin();
			fileLogoutMenuItem.Enabled = actionControl.CheckLogout() 
				&& Communication.Instance.Connected;
		}
		
		/// <summary>
		/// Start de login wizard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fileLoginMenuItem_Click(object sender, EventArgs e)
		{
			runLoginWizard();
		}

		/// <summary>
		/// Uitloggen en disablen van menuitems.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fileLogoutMenuItem_Click(object sender, EventArgs e)
		{
			Communication.Instance.Disconnect();

			fileLoginMenuItem.Enabled = true;
			fileLogoutMenuItem.Enabled = false;
			filePrintMenuItem.Enabled = false;

			editInitMenuItem.Enabled = false;
			editAddPouleMenuItem.Enabled = false;
			editManagePlayersMenuItem.Enabled = false;

			windowsScoreInputMenuItem.Enabled = false;
			windowsPouleInformationMenuItem.Enabled = false;
			windowsCourtInformationMenuItem.Enabled = false;
			settingsAmountFieldsMenuItem.Enabled = false;
		}

		/// <summary>
		/// Opent de login wizard
		/// </summary>
		public void runLoginWizard()
		{
			Enabled = true;
			LoginWizardForm wizard = new LoginWizardForm();
			wizard.Finish = loginWizard_OnFinish;
			wizard.Closing += loginWizard_OnClosing;
			wizard.ShowDialog(dockPanel);
		}

		/// <summary>
		/// Start na afloop van de login wizard de toernooi initialisatie wizard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="complete_input"></param>
		protected void loginWizard_OnFinish(AbstractWizardForm<object> sender, object complete_input)
		{
			sender.Closing -= loginWizard_OnClosing;
		}

		/// <summary>
		/// Vraag om bevestiging voordat de login wizard wordt gesloten
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void loginWizard_OnClosing(object sender, CancelEventArgs e)
		{
			DialogResult result = MessageBox.Show("Weet u zeker dat u het inloggen wilt afbreken?", "Afsluiten inloggen bevestigen",
				MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			e.Cancel = result == DialogResult.Cancel;
			if (result == DialogResult.OK)
				Focus();
		}

		#endregion

		#region Print MatchPaper

		/// <summary>
		/// Bepaalt of de gebruiker wel of niet mag printen.
		/// </summary>
		private void func_setPrintMatchPaper()
		{
			if (initDone)
				filePrintMenuItem.Enabled = actionControl.CheckPrintMatchPapers();
		}

		/// <summary>
		/// Print alle wedstrijdbriefjes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void allMatchPapersMenuItem_Click(object sender, EventArgs e)
		{
			MatchNotePrinter.Printer.PrintAll();
		}

		/// <summary>
		/// Print wedstrijdbriefjes in veelvouden van 4
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fullPagesMenuItem_Click(object sender, EventArgs e)
		{
			MatchNotePrinter.Printer.PrintFull();
		}

		#endregion

		#region Tournament Initialization

		/// <summary>
		/// Bepaalt of de initialisatiewizard wel of niet gestart mag worden.
		/// </summary>
		private void func_setInitTournament()
		{
			editInitMenuItem.Enabled = (!initDone && actionControl.CheckTournamentInit());
		}

		/// <summary>
		/// Als het toernooi nog niet geinitialiseerd is wordt de initialisatie wizard geopend.
		/// Anders krijgt de gebruiker een waarschuwing dat het al geinitialiseerd is.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editInitMenuItem_Click(object sender, EventArgs e)
		{
			//check of al geinitialiseerd
			runTournamentInitializationWizard();
		}

		/// <summary>
		/// Start de toernooi initialisatie wizard
		/// </summary>
		public void runTournamentInitializationWizard()
		{
			initDone = false;
			TournamentInitializationWizardForm wizard = new TournamentInitializationWizardForm();
			wizard.Finish = tournamentWizard_OnFinish;
			wizard.Closing += tournamentWizard_OnClosing;
			wizard.ShowDialog(dockPanel);
		}

		/// <summary>
		/// Na afloop van de initialisatiewizard wordt ingesteld dat de initialisatie is afgerond
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="complete_input"></param>
		protected void tournamentWizard_OnFinish(AbstractWizardForm<object> sender, object complete_input)
		{
			//remove Closing listener
			sender.Closing -= tournamentWizard_OnClosing;
			Enabled = true;
			FunctionalitiesChanged();
			Focus();
		}

		/// <summary>
		/// Vraag eerst om bevestiging van de gebruiker voordat de initialisatie wizard gesloten wordt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void tournamentWizard_OnClosing(object sender, CancelEventArgs e)
		{
			DialogResult result = MessageBox.Show("Weet u zeker dat u de wizard wilt afsluiten? Niet opgeslagen gegevens gaan verloren.", "Wizard afsluiten bevestigen",
				MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			e.Cancel = result == DialogResult.Cancel;
			if (result == DialogResult.OK)
				Focus();
		}

		#endregion

		#region Poule Toevoegen
		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om een nieuwe poule toe te voegen
		/// </summary>
		private void func_setAddPoule()
		{
			if (initDone)
				editAddPouleMenuItem.Enabled = actionControl.CheckAddPoule();
		}

		/// <summary>
		/// Open het scherm waar tijdens het toernooi een nieuwe poule aangemaakt kan worden
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editAddPouleMenuItem_Click(object sender, EventArgs e)
		{
			//check of niet geinitialiseerd
			AddPouleForm pouleForm = new AddPouleForm();
			pouleForm.ShowDialog(this);
		}

		#endregion

		#region Spelers Beheren
		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om spelers te beheren
		/// </summary>
		private void func_setManagePlayers()
		{
			if (initDone)
				editManagePlayersMenuItem.Enabled = actionControl.CheckManagePlayers();
		}

		/// <summary>
		/// Open het scherm waar tijdens het toernooi spelers beheerd kunnen worden
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void editManagePlayersMenuItem_Click(object sender, EventArgs e)
		{
			AddPlayerForm playerForm = new AddPlayerForm();
			playerForm.ShowDialog(this);
		}
		#endregion

		#region Score Input

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om scores in te voeren
		/// </summary>
		private void func_setScoreInput()
		{
			if (initDone)
				windowsScoreInputMenuItem.Enabled = actionControl.CheckScoreInput();
		}
		
		/// <summary>
		/// Opent het wedstrijdoverzicht
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void windowsScoreInputMenuItem_Click(object sender, EventArgs e)
		{
			if (matchOverviewForm == null || matchOverviewForm.IsDisposed)
				matchOverviewForm = new ScoreForm();

			matchOverviewForm.Show(dockPanel);
			matchOverviewForm.Focus();
		}

		#endregion

		#region Poule Information

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om poule informatie te bekijken
		/// </summary>
		private void func_setPouleInformation()
		{
			if (initDone)
				windowsPouleInformationMenuItem.Enabled = actionControl.CheckPouleInformation();
		}

		/// <summary>
		/// Opent het poule informatie scherm
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void windowsPouleInformationMenuItem_Click(object sender, EventArgs e)
		{
			PouleInformationForm form = new PouleInformationForm();
			form.Show(dockPanel);
			form.Focus();
		}

		#endregion

		#region Court Information

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om velden te beheren
		/// </summary>
		private void func_setCourtInformation()
		{
			if (initDone)
				windowsCourtInformationMenuItem.Enabled = actionControl.CheckCourtInformation();
		}

		/// <summary>
		/// Opent het veldoverzicht
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void windowsCourtInformationMenuItem_Click(object sender, EventArgs e)
		{
			if (courtInformationForm == null || courtInformationForm.IsDisposed)
				courtInformationForm = new CourtInformationForm();

			courtInformationForm.Show(dockPanel);
			courtInformationForm.Focus();
		}

		#endregion

		#region Aantal Velden

		/// <summary>
		/// Kijk of de gebruiker toestemming heeft om het aantal velden te wijzigen
		/// </summary>
		private void func_setAmountFields()
		{
			if (initDone)
				settingsAmountFieldsMenuItem.Enabled = actionControl.CheckAmountFields();
		}

		/// <summary>
		/// Open een popup waar het aantal velden gewijzigd kan worden
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void settingsAmountFieldsMenuItem_Click(object sender, EventArgs e)
		{
			showPopUp(new DefineCourtsPopUp());
		}

		#endregion

		#endregion

		/// <summary>
		/// Vraag de gebruiker om bevestiging, sluit de verbinding met de server 
		/// en sluit het programma af
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Weet u zeker dat u Sharp Shuttle Client volledig wilt afsluiten?", "Afsluiten bevestigen",
			MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				if (Communication.Instance.Connected)
					Communication.Instance.Disconnect();
			}

			else
			{
				e.Cancel = true;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="complete_input"></param>
		protected void default_OnFinish(AbstractWizardForm<object> sender, object complete_input)
		{
			sender.Closing -= default_OnClosing;
			Enabled = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void default_OnClosing(object sender, CancelEventArgs e)
		{
			Enabled = true;
		}

		/// <summary>
		/// Laat een popup zien
		/// </summary>
		/// <param name="popup"> de popup </param>
		protected void showPopUp(AbstractPopUp popup)
		{
			Enabled = false;
			popup.Closing += default_OnClosing;
			popup.ShowDialog(dockPanel);
		}

		/// <summary>
		/// Sluit het programma af
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fileExitMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Wisselt tussen het laten zien en het verbergen van de statusbalk
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void viewStatusBarMenuItem_Click(object sender, EventArgs e)
		{
			if (viewStatusBarMenuItem.Checked)
			{
				Controls.Add(statusStrip);
				dockPanel.Height -= statusStrip.Height;
			}
			else
			{
				Controls.Remove(statusStrip);
				dockPanel.Height += statusStrip.Height;
			}
		}

		/// <summary>
		/// Laat de "about" popup zien
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void aboutSharpShuttleMenuItem_Click(object sender, EventArgs e)
		{
			showPopUp(new AboutPopUp());
		}

		/// <summary>
		/// Geeft de gebruiker de mogelijkheid om de printer te wijzigen.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void printerMenuItem_Click(object sender, EventArgs e)
		{
			PrinterSettings printersettings = new PrinterSettings();
			string currentName = Configurations.PrinterName;
			if (currentName != "none")
				printersettings.PrinterName = currentName;
			PrintDialog printdialog = new PrintDialog();
			printdialog.PrinterSettings = printersettings;
			printdialog.AllowSomePages = false;
			printdialog.AllowSelection = false;
			printdialog.AllowPrintToFile = false;
			printdialog.AllowCurrentPage = false;

			DialogResult result = printdialog.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				Configurations.PrinterName = printersettings.PrinterName;
			}
		}

		/// <summary>
		/// Laat de "help" popup zien
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sharpShuttleHelpMenuItem_Click(object sender, EventArgs e)
		{
			showPopUp(new HelpPopUp());
		}
	}
}