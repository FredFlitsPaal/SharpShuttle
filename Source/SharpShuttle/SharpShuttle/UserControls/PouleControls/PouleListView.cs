using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;
using UserControls.AbstractControls;

namespace UserControls.PouleControls
{
	/// <summary>
	/// ListView van poules
	/// </summary>
    public class PouleListView : DomainListView<PouleViews, PouleView, Poule>
    {
		/// <summary>
		/// Default constructor
		/// </summary>
        public PouleListView()
		{
			//kolommen bepalen
			//PouleView moet dus ook de volgende eigenschappen kennen (case sensitive):
			//Name, Discipline, Niveau, Comment
			ColumnHeader col;

			//Name
			col = new ColumnHeader {Name = "Name", Text = "Naam", DisplayIndex = 0, Width = 140};
			Columns.Add(col);
			//Discipline
			col = new ColumnHeader {Name = "Discipline", Text = "Discipline", DisplayIndex = 1, Width = 100};
			Columns.Add(col);
			//Niveau
			col = new ColumnHeader {Name = "Niveau", Text = "Niv", DisplayIndex = 2, Width = 35};
			Columns.Add(col);
            //Comment
            col = new ColumnHeader {Name = "Comment", Text = "Opmerkingen", DisplayIndex = 3, Width = 122};
			Columns.Add(col);
		}

		/// <summary>
		/// De datasource
		/// </summary>
		public new PouleViews DataSource
		{
			get { return base.DataSource; }
			set { base.DataSource = value; }
		}
    }
}
