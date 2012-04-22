using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Shared.Domain;
using Shared.Views;
using System;
using System.Runtime.InteropServices;

namespace UserControls.AbstractControls
{
	/// <summary>
	/// Abstracte ListView voor domeinobjecten
	/// </summary>
	/// <typeparam name="ViewTypes"> Het viewstype van het domeinobject </typeparam>
	/// <typeparam name="ViewType"> Het viewtype van het domeinobject </typeparam>
	/// <typeparam name="DomainType"> Het type domeinobject </typeparam>
	public abstract class DomainListView<ViewTypes, ViewType, DomainType> : ListView
		where ViewType : AbstractView<DomainType>
		where ViewTypes : AbstractViews<ViewType, DomainType>
		where DomainType : Abstract
	{
		private ViewTypes dataSource;
		
		/// <summary>
		/// De datasource van de ListView
		/// </summary>
		public virtual ViewTypes DataSource
		{
			get { return dataSource; }
			set {
				dataSource = value;
				DataBind();
			}
		}

		/// <summary>
		/// Reset elementen en ververst ze met nieuwe elementen, afhankelijk van de DataSource en kolommon
		/// Reflection exceptions worden opzettelijk niet afgevangen
		/// </summary>
		public virtual void DataBind()
		{
			Items.Clear();
			if (DataSource != null)
			{
				foreach (ViewType view in DataSource)
				{
					string[] values = new string[Columns.Count];

					int i = 0;
					foreach (ColumnHeader col in Columns)
					{
						PropertyInfo propInfo = typeof(ViewType).GetProperty(col.Name);

						if (propInfo != null)
						{
							//Rond af op 2 decimalen bij floats om rare waardes tegen te gaan.
							string value;
							if (propInfo.PropertyType == typeof(float))
							{
								float toFloat = (float)propInfo.GetValue(view, null);
								value = toFloat.ToString("0.##");
							}
							else
								value = propInfo.GetValue(view, null).ToString();

							values[i] = value;
							i++;
						}
					}

					ListViewItem item = new ListViewItem(values);
					
					//voegt en tag toe van de view. het blijft echter wel een object.
					item.Tag = view;
					
					Items.Add(item);
				}
			}
		}

		/// <summary>
		/// Een scrollposition die je bij elke DomainListView implementatie kan gebruiken. De Event onScroll kan worden gebruikt
		/// maar dan moet je in de constructor van je implementatie van DomainListView, de IsOnScroll op true zetten. En moet je 
		/// een methode aan je onscrollevent kopppelen
		/// </summary>
		#region ScrollPosition & ScrollPosition

		public bool IsOnScroll = false;

		private const int SB_VERT = 1;
		private const int LVM_FIRST = 0x1000;
		private const int LVM_SCROLL = (LVM_FIRST + 20);

		//Scroll mogelijkheden
		private const int WM_VSCROLL = 0x115;
		private const int WM_MOUSEWHEEL = 0x20A;
		private const int WM_KEYDOWN = 0x100;

		/// <summary>
		/// Event voor wanneer er gescrolld wordt
		/// </summary>
		public event ScrollEventHandler onScroll;

		/// <summary>
		/// Een variabele die de positie van de verticale scrollbar, kan getten en kan setten.
		/// </summary>
		public int ScrollPosition
		{
			get
			{
				try
				{
					return Kernel32.GetScrollPos(Handle, SB_VERT);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				try
				{
					int prevPos = Kernel32.GetScrollPos(Handle, SB_VERT);
					int scrollVal = -(prevPos - value);

					if (ShowGroups && Groups.Count > 0)
					{
						Kernel32.SendMessage(Handle, LVM_SCROLL, (IntPtr)0, (IntPtr)scrollVal);
					}

					else
					{
						Kernel32.SendMessage(Handle, LVM_SCROLL, (IntPtr)0, (IntPtr)(scrollVal * 17));
					}
				}
				catch { }
			}
		}

		/// <summary>
		/// Overriding van de WndProc om de events van scrollen en toetsen te triggeren
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (IsOnScroll)
			{
				switch (m.Msg)
				{
					case WM_MOUSEWHEEL:
					case WM_VSCROLL:
						onScroll(this, new ScrollEventArgs(ScrollEventType.EndScroll, Kernel32.GetScrollPos(Handle, SB_VERT)));
						break;

					case WM_KEYDOWN:
						switch (m.WParam.ToInt32())
						{
							case (int) Keys.Up:
							case (int) Keys.PageUp:
								onScroll(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, Kernel32.GetScrollPos(Handle, SB_VERT)));
								break;

							case (int) Keys.Down:
							case (int) Keys.PageDown:
								onScroll(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, Kernel32.GetScrollPos(Handle, SB_VERT)));
								break;

							case (int) Keys.Home:
								onScroll(this, new ScrollEventArgs(ScrollEventType.First, Kernel32.GetScrollPos(Handle, SB_VERT)));
								break;

							case (int) Keys.End:
								onScroll(this, new ScrollEventArgs(ScrollEventType.Last, Kernel32.GetScrollPos(Handle, SB_VERT)));
								break;
						}
						break;
				}
			}
		}

		#endregion

		/// <summary>
		/// Bron: http://stackoverflow.com/questions/660663/c-implementing-auto-scroll-in-a-listview-while-drag-dropping
		/// Originele code: http://www.knowdotnet.com/articles/listviewdragdropscroll.html
		/// </summary>
		#region Automatisch Scrollen (bij draggen)

		private Timer tmrLVScroll;
		private System.ComponentModel.IContainer components;
		private int mintScrollDirection;
		const int SB_LINEUP = 0; // Scrolls one line up
		const int SB_LINEDOWN = 1; // Scrolls one line down

		/// <summary>
		/// Initialiseert de autoscroll
		/// </summary>
		protected void InitializeAutoScroll()
		{
			components = new System.ComponentModel.Container();
			tmrLVScroll = new Timer(components);
			SuspendLayout();

			tmrLVScroll.Tick += tmrLVScroll_Tick;

			DragOver += AutoScrollListView_DragOver;
			ResumeLayout(false);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void AutoScrollListView_DragOver(object sender, DragEventArgs e)
		{
			Point position = PointToClient(new Point(e.X, e.Y));

			if (position.Y <= (Font.Height / 2))
			{
				// getting close to top, ensure previous item is visible
				mintScrollDirection = SB_LINEUP;
				tmrLVScroll.Enabled = true;
			}
			else if (position.Y >= ClientSize.Height - Font.Height / 2)
			{
				// getting close to bottom, ensure next item is visible
				mintScrollDirection = SB_LINEDOWN;
				tmrLVScroll.Enabled = true;
			}
			else
			{
				tmrLVScroll.Enabled = false;
			}
		}

		private void tmrLVScroll_Tick(object sender, EventArgs e)
		{
			Kernel32.SendMessage(Handle, WM_VSCROLL, (IntPtr)mintScrollDirection, IntPtr.Zero);
		}

		#endregion

	}

	#region Extra klasse om de SendMessage en de GetScrollPos te doen

	/// <summary>
	/// Extra klasse om de SendMessage en de GetScrollPos te doen
	/// </summary>
	public static class Kernel32
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="wMsg"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// Vraag de scrollpositie op
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="nBar"></param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern int GetScrollPos(IntPtr hWnd, int nBar);
	}

	#endregion

}
