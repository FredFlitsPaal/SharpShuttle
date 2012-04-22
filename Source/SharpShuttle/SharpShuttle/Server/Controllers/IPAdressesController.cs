using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace Server.Controllers
{
	class IPAdressesController
	{
		/// <summary>
		/// Haalt een lijst met alle lokale IP adressen van deze computer op
		/// </summary>
		/// <returns>De lijst met alle lokale IP adressen van deze computer</returns>
		public List<IPAddress> GetLocalIPAddresses()
		{
			NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
			List<IPAddress> result = new List<IPAddress>();

			for (int i = 0; i < interfaces.Length; i++)
			{
				var addresses = interfaces[i].GetIPProperties().UnicastAddresses;

				foreach (var item in addresses)
				{
					result.Add(item.Address);
				}
			}

			return result;
		}

		/// <summary>
		/// Haalt een lijst met alle lokale IP adressen van deze computer op, met de namen erbij
		/// </summary>
		/// <returns>De lijst met alle lokale IP adressen en hun namen van deze computer</returns>
		public List<InterfaceNameWithAddress> GetNamedLocalIPAddresses()
		{
			NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
			List<InterfaceNameWithAddress> result = new List<InterfaceNameWithAddress>();

			for (int i = 0; i < interfaces.Length; i++)
			{
				var addresses = interfaces[i].GetIPProperties().UnicastAddresses;

				foreach (var item in addresses)
				{
					result.Add(new InterfaceNameWithAddress(item.Address, interfaces[i].Name));
				}
			}

			return result;
		}

		/// <summary>
		/// Een (IPAddress,Naam) paar
		/// </summary>
		public class InterfaceNameWithAddress
		{
			/// <summary>
			/// Het adres
			/// </summary>
			public IPAddress Address
			{
				get;
				private set;
			}

			/// <summary>
			/// De naam
			/// </summary>
			public string InterfaceName
			{
				get;
				private set;
			}

			public override string ToString()
			{
				return string.Format("{0} {1}", Address, InterfaceName);
			}

			/// <summary>
			/// Maakt een nieuwe InterfaceNameWithAddress
			/// </summary>
			/// <param name="address">Het adres</param>
			/// <param name="name">De naam</param>
			public InterfaceNameWithAddress(IPAddress address, string name)
			{
				Address = address;
				InterfaceName = name;
			}
		}
	}
}