using System;
using Shared.Domain;

namespace Shared.Views
{
	/// <summary>
	/// Abstracte view klasse die de basis vormt voor alle andere view klassen
	/// </summary>
	/// <typeparam name="DomainType"></typeparam>
	[Serializable]
    public abstract class AbstractView <DomainType>
		where DomainType : Abstract
    {
		/// <summary>
		/// De data die de view bevat
		/// </summary>
		internal DomainType data;

		/// <summary>
		/// Het domeinobject dat de view bevat
		/// </summary>
		public abstract DomainType Domain
		{
			get;
		}

    }

}
