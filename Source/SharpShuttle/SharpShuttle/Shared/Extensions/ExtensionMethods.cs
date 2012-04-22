using System.Text;

namespace Shared.Extensions
{
	/// <summary>
	/// 
	/// </summary>
	public static class ExtensionMethods
	{
		/// <summary>
		/// Zet een bitarray om in een Hex String
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static string ToHexString(this System.Collections.BitArray array)
		{
			StringBuilder result = new StringBuilder();
			result.AppendFormat("{0:X}:", array.Count);
			int numbits = 0;
			int bits = 0;
			for (int i = 0; i < array.Count; i++)
			{
				bits <<= 1;
				numbits++;
				if (array[i])
					bits++;
				if (numbits == 8)
				{
					numbits = 0;					
					result.AppendFormat("{0:X2}", bits);
					bits = 0;
				}
			}
			if (numbits != 0)
				result.AppendFormat("{0:X2}", bits);
			return result.ToString();
		}


		/// <summary>
		/// Test een attribute van een xml element
		/// </summary>
		/// <param name="element">Het xml element</param>
		/// <param name="attribute">De attribute waarop gestest moet worden</param>
		/// <param name="value">De waarde van de attribute waarop getest moet worden</param>
		/// <returns>
		/// true als de attribute dezelfde waarde heeft, anders false
		/// </returns>
		public static bool TestAttribute(this System.Xml.Linq.XElement element, string attribute, string value)
		{
			if (element.Attribute(attribute) == null)
				return false;

			if (element.Attribute(attribute).Value != value)
				return false;

			return true;
		}
	}
}
