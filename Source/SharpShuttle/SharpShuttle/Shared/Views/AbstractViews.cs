using System;
using System.Collections;
using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Een lijst van AbstractViews
	/// </summary>
	/// <typeparam name="ViewType"></typeparam>
	/// <typeparam name="DomainType"></typeparam>
	[Serializable]
	public abstract class AbstractViews<ViewType, DomainType> : IList <ViewType>, System.ComponentModel.IListSource
		where ViewType : AbstractView<DomainType>
		where DomainType : Abstract
	{
		//public static Type SingularType = typeof(ViewType);

		/// <summary>
		/// De lijst van AbstractViews
		/// </summary>
		internal List<ViewType> viewList;
		/// <summary>
		/// De lijst van domeinobjecten
		/// </summary>
		private ICollection<DomainType> domainList;

		#region Implementatie van IEnumerable

		/// <summary>
		/// Een Enumerator
		/// </summary>
		/// <returns></returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Implementatie van IEnumerable<ViewType>

		/// <summary>
		/// Een ViewType Enumerator
		/// </summary>
		/// <returns></returns>
		public IEnumerator<ViewType> GetEnumerator()
		{
			return viewList.GetEnumerator();
		}

		#endregion

		#region Implementatie van ICollection<ViewType>

		/// <summary>
		/// Voegt een view toe
		/// </summary>
		/// <param name="item"></param>
		public void Add(ViewType item)
		{
			viewList.Add(item);
			domainList.Add(item.Domain);
		}

		/// <summary>
		/// Maakt de AbstractViews leeg
		/// </summary>
		public void Clear()
		{
			viewList.Clear();
			domainList.Clear();
		}

		/// <summary>
		/// Bevat de AbstractViews een View
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(ViewType item)
		{
			return viewList.Contains(item);
		}

		/// <summary>
		/// Kopier een array van Views naar de AbstractViews
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(ViewType[] array, int arrayIndex)
		{
			viewList.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Verwijder een view
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(ViewType item)
		{
			return (viewList.Remove(item) && domainList.Remove(item.Domain));
		}

		/// <summary>
		/// Het aantal Views
		/// </summary>
		public int Count
		{
			get { return viewList.Count; }
		}

		/// <summary>
		/// AbstractViews zijn niet read-only
		/// </summary>
		public bool IsReadOnly
		{
			get { return false; }
		}

		#endregion

		#region Implementatie van IList<ViewType>

		/// <summary>
		/// De index van een view in de views
		/// </summary>
		/// <param name="item"> De view </param>
		/// <returns> De index van de view</returns>
		public int IndexOf(ViewType item)
		{
			return viewList.IndexOf(item);
		}

		/// <summary>
		/// Insert een view in de views op een index
		/// </summary>
		/// <param name="index"> De index waarin de view geinsert wordt </param>
		/// <param name="item"> De view die geinsert wordt </param>
		public void Insert(int index, ViewType item)
		{
			throw new NotImplementedException();
			//viewList.Insert(index, item);
			//domainList.Insert(index, item.Domain);
		}

		/// <summary>
		/// Verwijdert een view op een index
		/// </summary>
		/// <param name="index"> De index waar een view verwijderd wordt </param>
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
			//viewList.RemoveAt(index);
			//domainList.RemoveAt(index);
		}

		/// <summary>
		/// ViewType op een gegeven index
		/// </summary>
		/// <param name="index"> De index </param>
		/// <returns> Het ViewType op de index </returns>
		public ViewType this[int index]
		{
			get
			{ 
				return viewList[index];
			}
			
			set
			{
				throw new NotImplementedException("laat het even weten als je deze exception krijgt aub");
			}
		}

		#endregion

		#region Nieuwe Methodes

		/// <summary>
		/// Gewoon AddRange van een List.
		/// </summary>
		/// <param name="collection"></param>
		public void AddRange(IEnumerable<ViewType> collection)
		{
			viewList.AddRange(collection);

			foreach (ViewType v in collection)
				domainList.Add(v.Domain);
		}

		/// <summary>
		/// Maakt gegeven een implementatie van een IList, als deze een ChangeTrackingList is 
		/// wordt deze opgeslagen.
		/// Bij een andere implementie van IList wordt er geen ChangeTrackingList bijhgehouden
		/// En er wordt altijd een lege Views lijst aangemaakt. die gevuld kan worden.
		/// </summary>
		/// <param name="list">IList</param>
		public void SetDomainList(ICollection<DomainType> list)
		{
			domainList = list;
			viewList = new List<ViewType>();
		}

		/// <summary>
		/// Er wordt een IList teruggegeven die de zelfde implementatie is als 
		/// de implemenatatie is die is ingegeven
		/// </summary>
		/// <returns>ChangeTrackingList</returns>
		public ICollection<DomainType> GetDomainList()
		{
			return domainList;
		}

		/// <summary>
		/// Er wordt een ChangeTrackingList teruggegeven als deze ook is meegegeven aan de 
		/// SetDomainList() methode.
		/// </summary>
		/// <returns></returns>
		public ChangeTrackingList<DomainType> GetChangeTrackingList()
		{
			if (domainList is ChangeTrackingList <DomainType>)
				return domainList as ChangeTrackingList<DomainType>;

			throw new Exception(@"Er is geen ChangeTrackingList ingegeven dus deze kan ook
								  kan ook niet worden terug gegeven.");
		}

		/// <summary>
		/// Maakt een nieuwe lijst aan voor de views.
		/// </summary>
		public void SetViews()
		{
			viewList = new List<ViewType>();
			domainList = new List<DomainType>();
		}

		#endregion


		#region IListSource Members

		//voor databinding

		bool System.ComponentModel.IListSource.ContainsListCollection
		{
			get { return false; }
		}

		IList System.ComponentModel.IListSource.GetList()
		{
			return new System.ComponentModel.BindingList<ViewType>(this);
		}

		#endregion
	}

}
