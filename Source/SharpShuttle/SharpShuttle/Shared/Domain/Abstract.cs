using System;

namespace Shared.Domain
{
	/// <summary>
	/// Abstracte superklasse voor alle domein objecten
	/// </summary>
	[Serializable]
    public abstract class Abstract : ICloneable
    {
		/// <summary>
		/// Is het domeinobject veranderd
		/// </summary>
		internal bool changed = false;

		/// <summary>
		/// Is het domeinobject veranderd
		/// </summary>
		public virtual bool Changed
		{
			get
			{
				return changed;
			}
			set
			{
				changed = value;
			}
		}

		#region ICloneable Members

		/// <summary>
		/// Kopieer het domeinobject
		/// </summary>
		/// <returns></returns>
		public virtual object Clone()
		{
			return MemberwiseClone();
		}

		#endregion
	}
}