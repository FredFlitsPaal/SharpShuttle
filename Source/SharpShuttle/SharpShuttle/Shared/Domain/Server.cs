
using System;

namespace Shared.Domain
{
	/// <summary>
	/// Een server
	/// </summary>
	[Serializable]
    public class Server : Abstract
    {
		/// <summary>
		/// Default constructor
		/// </summary>
        public Server()
        {
            Address = null;
        }

		/// <summary>
		/// Constructor die een adres string meekrijgt
		/// </summary>
		/// <param name="address"> Het adres van de server </param>
        public Server(string address)
        {
            Address = address;
        }

		/// <summary>
		/// Het adres van de server
		/// </summary>
        public string Address
        {
            get; set;
        }
    }
}
