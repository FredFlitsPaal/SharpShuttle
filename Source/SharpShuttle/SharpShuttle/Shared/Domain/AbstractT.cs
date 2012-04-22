using System;
namespace Shared.Domain
{
	/// <summary>
	/// Abstracte superklasse voor alle domeinobjecten met data met een type 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public abstract class AbstractT<T> : Abstract
	{
		/// <summary>
		/// Update de data
		/// </summary>
		/// <param name="newdata"> De nieuwe data </param>
		internal virtual void UpdateData(T newdata) { }
	}
}