using System.Collections.Generic;
using System;
using Shared.Communication.Serials;
using System.Collections.ObjectModel;

namespace Shared.Domain
{
    /// <summary>
    /// Houdt de veranderingen van een lijst bij. Houdt geen veranderingen
    /// in de elementen zelf bij, die worden door de elementen zelf bijgehouden
    /// </summary>
    /// <typeparam name="T"> Het type van de elementen in de lijst </typeparam>
	[Serializable]
	public class ChangeTrackingList<T> : Abstract, ICollection<T>
		where T : Abstract
	{
		/// <summary>
		/// De elementen in de ChangeTrackingList
		/// </summary>
		Collection<T> data;

		/// <summary>
		/// Toegevoegde elementen
		/// </summary>
		Collection<T> adds;
		/// <summary>
		/// Verwijderde elementen
		/// </summary>
		Collection<T> deletes;

		private SerialDefinition serialnumber;

		/// <summary>
		/// Default constructor die een lege ChangeTrackingList maakt
		/// </summary>
		public ChangeTrackingList()
		{
//#if DEBUG
//            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(false);
//            string type = trace.GetFrame(1).GetMethod().DeclaringType.Name;
//            if (type != "IntermediateDataCache")
//                throw new Exception("ChangeTrackingLists kun je niet aanmaken, alleen gebruiken");
//#endif
			data = new Collection<T>();
			adds = new Collection<T>();
			deletes = new Collection<T>();
		}

		/// <summary>
		/// Maakt een ChangeTrackingList aan de hand van een data collection en een serienummer
		/// </summary>
		/// <param name="data"></param>
		/// <param name="serial"></param>
		public ChangeTrackingList(IEnumerable<T> data, SerialDefinition serial) : this()
		{
			serialnumber = serial;

			foreach (T item in data)
				this.data.Add((T)item.Clone());
	
		}

		/// <summary>
		/// Is de ChangeTrackingList veranderd
		/// </summary>
		public override bool Changed
		{
			get
			{
				//uit optimalisatieoogpunt kijken we eerst of er een delete/add lijstje is
				if (adds.Count > 0 || deletes.Count > 0)
					return true;

				//kijk of er elementen uit de lijst zijn veranderd
				for (int i = 0; i < data.Count; i++)
				{
					if (data[i].Changed)
						return true;
				}

				//anders is er niets veranderd
				return false;
			}
		}

		/// <summary>
		/// Alle elementen die toegevoegd zijn
		/// </summary>
		public Collection<T> Adds
		{
			get { return adds; }
		}

		/// <summary>
		/// Alle elementen die verwijderd zijn
		/// </summary>
		public Collection<T> Deletes
		{
			get { return deletes; }
		}

		#region ICollection<T> Members

		/// <summary>
		/// Voeg een element toe
		/// </summary>
		/// <param name="item"></param>
		public void Add(T item)
		{
			if (deletes.Contains(item))
			{
				data.Add(item);
				deletes.Remove(item);
				return;
			}

			if (adds.Contains(item))
			{
				return;
			}

			data.Add(item);
			adds.Add(item);
		}

		/// <summary>
		/// Voeg een collection van elementen toe aan de ChangeTrackingList
		/// </summary>
		/// <param name="collection"></param>
		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T item in collection)
				Add(item);
		}

		/// <summary>
		/// Maak de ChangeTrackingList leeg
		/// </summary>
		public void Clear()
		{
			while (data.Count > 0)
				Remove(data[data.Count - 1]);
		}

		/// <summary>
		/// Bevat een ChangeTrackingList een element
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(T item)
		{
			return data.Contains(item);
		}

		/// <summary>
		/// Kopieer een array naar de ChangeTrackingList vanaf een index
		/// </summary>
		/// <param name="array"></param>
		/// <param name="arrayIndex"></param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			data.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Het aantal elementen
		/// </summary>
		public int Count
		{
			get { return data.Count; }
		}

		/// <summary>
		/// Een ChangeTrackingList is niet readonly
		/// </summary>
		public bool IsReadOnly
		{
			get { return false; }
		}

		/// <summary>
		/// Verwijder een element uit de ChangeTrackingList
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(T item)
		{
			if (deletes.Contains(item))
			{
				return false;
			}

			if (adds.Contains(item))
			{
				data.Remove(item);
				adds.Remove(item);
				return true;
			}

			if (data.Remove(item))
			{
				deletes.Add(item);
				return true;
			}

			return false;
		}
		
		#endregion

		#region IEnumerable<T> Members

		/// <summary>
		/// De enumerator
		/// </summary>
		/// <returns></returns>
		public IEnumerator<T> GetEnumerator()
		{
			return data.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		/// <summary>
		/// Het serienummer van de ChangeTrackingList
		/// </summary>
		public SerialDefinition SerialNumber
		{
			get
			{
				return serialnumber;
			}
			set
			{
				serialnumber = value;
			}
		}

		/// <summary>
		/// Verandert het serienummer en returnt zichzelf
		/// </summary>
		/// <param name="serial"> Het nieuwe serienummer </param>
		/// <returns> Zichzelf, met het nieuwe serienummer </returns>
		public ChangeTrackingList<T> SetSerialNumber(SerialDefinition serial)
		{
			serialnumber = serial;
			return this;
		}

	}
}
