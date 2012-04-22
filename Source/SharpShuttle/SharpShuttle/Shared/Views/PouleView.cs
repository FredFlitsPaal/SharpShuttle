using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// View die een poule beschrijft
	/// </summary>
	public class PouleView : AbstractView <Poule>
	{
		#region Constructors

		/// <summary>
		/// Default constructor, maakt een "leeg" poule domeinobject
		/// </summary>
		public PouleView()
		{
			data = new Poule("", Poule.Disciplines.Mixed, "", "");

			AmountPlayers = 0;
			AmountWomen = 0;
			AmountMen = 0;
			Correctness = "Incorrect";
		}

		/// <summary>
		/// Constructor met een naam, discipline, niveau en opmerkingen
		/// </summary>
		/// <param name="name"></param>
		/// <param name="discipline"></param>
		/// <param name="niveau"></param>
		/// <param name="comment"></param>
		public PouleView(string name, string discipline, string niveau, string comment)
		{
			data = new Poule(name, DisciplineToEnum(discipline), niveau, comment);

			AmountPlayers = 0;
			AmountWomen = 0;
			AmountMen = 0;
			Correctness = "Incorrect";
		}

		/// <summary>
		/// Maakt een PouleView van een Poule domeinobject
		/// </summary>
		/// <param name="poule"></param>
		public PouleView(Poule poule)
		{
			data = poule;
			
			AmountPlayers = 0;
			AmountWomen = 0;
			AmountMen = 0;
			Correctness = "Incorrect";
		}

		#endregion

		/// <summary>
		/// Het Poule domeinobject
		/// </summary>
		public override Poule Domain 
		{
			get { return data; }
		}

		#region Eigenschappen van een PouleView

		/// <summary>
		/// Het ID van de poule
		/// </summary>
		public int Id
		{
			get { return data.PouleID; }
		}

		/// <summary>
		/// De naam van de poule
		/// </summary>
		public string Name
		{
			get { return data.Name; }
			set { data.Name = value; }
		}

		/// <summary>
		/// De discipline van de poule
		/// </summary>
		public string Discipline
		{
			get { return DisciplineToString(data.Discipline); }
			set { DisciplineEnum = DisciplineToEnum(value); }
		}

		/// <summary>
		/// Het niveau van de poule
		/// </summary>
		public string Niveau
		{
			get { return data.Niveau; }
			set { data.Niveau = value; }
		}

		/// <summary>
		/// Opmerkingen bij de poule
		/// </summary>
		public string Comment
		{
			get { return data.Comment; }
			set { data.Comment = value; }
		}

		/// <summary>
		/// Correctheid van de poule
		/// </summary>
		public string Correctness
		{
			get;
			set;
		}

		/// <summary>
		/// Het aantal vrouwen in de poule
		/// </summary>
		public int AmountWomen
		{
			get;
			set;
		}

		/// <summary>
		/// Het aantal mannen in de poule
		/// </summary>
		public int AmountMen
		{
			get;
			set;
		}

		/// <summary>
		/// Het aantal spelers in de poule
		/// </summary>
		public int AmountPlayers
		{
			get;
			set;
		}

		/// <summary>
		/// De Discipline van depoule
		/// </summary>
		public Poule.Disciplines DisciplineEnum
		{
			get { return data.Discipline; }
			set { data.Discipline = value; }
		}

		/// <summary>
		/// De spelers in de poule
		/// </summary>
		private PlayerViews playersList;

		/// <summary>
		/// De spelers in de poule
		/// </summary>
		public PlayerViews Players
		{
			get
			{
				if (playersList == null)
					playersList = new PlayerViews();
				return playersList;
			}
			set { playersList = value; }
		}

		#endregion

		#region Hulp Enums

		/// <summary>
		/// Het soort poule. 1 of 2 speler teams
		/// </summary>
		public enum Kind
		{
			/// <summary>
			/// 1-speler teams
			/// </summary>
			Enkel = 0,
			/// <summary>
			/// 2-speler teams
			/// </summary>
			Dubbel = 1
		}

		/// <summary>
		/// Het geslacht van de poule
		/// </summary>
		public enum Gender
		{
			/// <summary>
			/// Mannen
			/// </summary>
			M = 0,
			/// <summary>
			/// Vrouwen
			/// </summary>
			V = 1,
			/// <summary>
			/// Gemengd
			/// </summary>
			U = 2
		}

		#endregion

		#region HulpMethodes

		/// <summary>
		/// Het soort team. 1 of 2 spelers
		/// </summary>
		public Kind KindofTeam
		{
			get
			{
				if (DisciplineEnum == Poule.Disciplines.MaleSingle || DisciplineEnum == Poule.Disciplines.FemaleSingle
						|| DisciplineEnum == Poule.Disciplines.UnisexSingle)
					return Kind.Enkel;
				return Kind.Dubbel;
			}
		}

		/// <summary>
		/// Het geslacht van de poule
		/// </summary>
		/// <returns></returns>
		public Gender getGender()
		{
			if (DisciplineEnum == Poule.Disciplines.FemaleDouble || DisciplineEnum == Poule.Disciplines.FemaleSingle)
				return Gender.V;
			if (DisciplineEnum == Poule.Disciplines.MaleDouble || DisciplineEnum == Poule.Disciplines.MaleSingle)
				return Gender.M;
			return Gender.U;
		}

		/// <summary>
		/// Een string representatie van de discipline van de poule
		/// </summary>
		/// <param name="discipline"></param>
		/// <returns></returns>
        public static string DisciplineToString(Poule.Disciplines discipline)
        {
			switch (discipline)
			{
				case Poule.Disciplines.MaleSingle:
					return "Heren Enkel";
				case Poule.Disciplines.MaleDouble:
					return "Heren Dubbel";
				case Poule.Disciplines.FemaleSingle:
					return "Dames Enkel";
				case Poule.Disciplines.FemaleDouble:
					return "Dames Dubbel";
				case Poule.Disciplines.Mixed:
					return "Mixed Dubbel";
				case Poule.Disciplines.UnisexSingle:
					return "Unisex Enkel";
				case Poule.Disciplines.UnisexDouble:
					return "Unisex Dubbel";
				default:
					throw new System.Exception("Unknown Discipline");
			}
        }

		/// <summary>
		/// Maakt een discipline van een string representatie ervan
		/// </summary>
		/// <param name="discipline"></param>
		/// <returns></returns>
        public Poule.Disciplines DisciplineToEnum(string discipline)
        {
            if (discipline == "Heren Enkel")
                return Poule.Disciplines.MaleSingle;
            if (discipline == "Heren Dubbel")
                return Poule.Disciplines.MaleDouble;
            if (discipline == "Dames Enkel")
                return Poule.Disciplines.FemaleSingle;
            if (discipline == "Dames Dubbel")
                return Poule.Disciplines.FemaleDouble;
			if (discipline == "Mixed Dubbel")
				return Poule.Disciplines.Mixed;
			if (discipline == "Unisex Enkel")
				return Poule.Disciplines.UnisexSingle;
			if (discipline == "Unisex Dubbel")
				return Poule.Disciplines.UnisexDouble;
			throw new System.Exception("Unknown Discipline");
        }

		/// <summary>
		/// Een lijst van alle mogelijke disciplines
		/// </summary>
		/// <returns></returns>
        public string[] getDisciplines()
        {
            return new string[] { "Heren Enkel", "Heren Dubbel", 
                "Dames Enkel", "Dames Dubbel", "Mixed Dubbel" , 
				"Unisex Enkel", "Unisex Dubbel" };
		}

		/// <summary>
		/// Hoog het aantal spelers van een geslacht op met 1
		/// </summary>
		/// <param name="Gender"> Het geslacht</param>
		public void incAmountPlayers(string Gender)
		{
			if (Gender == "M")
				AmountMen++;
			else
				AmountWomen++;
			AmountPlayers++;
		}

		/// <summary>
		/// Verlaag het aantal spelers van een geslacht met 1
		/// </summary>
		/// <param name="Gender"> Het geslacht</param>
		public void decAmountPlayers(string Gender)
		{
			if (Gender == "M")
				AmountMen--;
			else
				AmountWomen--;
			AmountPlayers--;
		}

		#endregion

	}
}
