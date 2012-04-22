using System;
using System.Runtime.InteropServices;

namespace Shared.Communication.Serials
{

	/// <summary>
	/// Type van serial
	/// </summary>
	public enum SerialTypes : short
	{
		/// <summary>
		/// Alle Users
		/// </summary>
		AllUsers = 0,
		/// <summary>
		/// Alle Poules
		/// </summary>
		AllPoules = 1,
		/// <summary>
		/// Alle Players
		/// </summary>
		AllPlayers = 2,
		/// <summary>
		/// Een specifieke poule
		/// </summary>
		Poule = 3,
		/// <summary>
		/// Een poule planning
		/// </summary>
		PoulePlanning = 4,
		/// <summary>
		/// Teams in een poule
		/// </summary>
		PouleTeams = 5,
		/// <summary>
		/// De ladder van een poule
		/// </summary>
		PouleLadder = 6,
		/// <summary>
		/// De matches van een poule
		/// </summary>
		PouleMatches = 7,
		/// <summary>
		/// History matches van een poule
		/// </summary>
		PouleHistoryMatches = 8,
		/// <summary>
		/// Een match
		/// </summary>
		Match = 9,
		/// <summary>
		/// Alle matches
		/// </summary>
		AllMatches = 10,
		/// <summary>
		/// Alle history matches
		/// </summary>
		AllHistoryMatches = 11,
		/// <summary>
		/// Settings
		/// </summary>
		Settings = 12
	}

	/// <summary>
	/// Specificeert een versienummer van een bepaald stukje data
	/// </summary>
	[StructLayout(LayoutKind.Explicit, Size = 12), Serializable]
	public struct SerialDefinition
	{
		/// <summary>
		/// Serienummer
		/// </summary>
		[FieldOffset(0)]
		public long SerialNumber;

		/// <summary>
		/// Type
		/// </summary>
		[FieldOffset(0)]
		public SerialTypes Type;

		/// <summary>
		/// Category
		/// </summary>
		[FieldOffset(2)]
		public int Category;

		/// <summary>
		/// Subcategory
		/// </summary>
		[FieldOffset(6)]
		public short SubCategory;

		/// <summary>
		/// Versienummer
		/// </summary>
		[FieldOffset(8)]
		public int VersionNumber;

		/// <summary>
		/// Maakt een SerialDefinition met een bepaald type en verder 0
		/// </summary>
		public SerialDefinition(SerialTypes type)
		{
			SerialNumber = 0;
			Type = type;
			Category = 0;
			SubCategory = 0;
			VersionNumber = 0;
		}

		/// <summary>
		/// Maakt een SerialDefinition met een bepaald type en category, verder 0
		/// </summary>
		public SerialDefinition(SerialTypes type, int category)
		{
			SerialNumber = 0;
			Type = type;
			Category = category;
			SubCategory = 0;
			VersionNumber = 0;
		}

		/// <summary>
		/// Maakt een SerialDefinition met een bepaald type en category en versienummer, verder 0
		/// </summary>
		public SerialDefinition(SerialTypes type, int category, int versionnumber)
		{
			SerialNumber = 0;
			Type = type;
			Category = category;
			SubCategory = 0;
			VersionNumber = versionnumber;
		}

		/// <summary>
		/// Maakt een SerialDefinition met een bepaald serienummer, verder 0
		/// </summary>
		public SerialDefinition(long serialnumber)
		{
			Type = 0;
			Category = 0;
			SubCategory = 0;
			SerialNumber = serialnumber;
			VersionNumber = 0;
		}

		/// <summary>
		/// Maakt een SerialDefinition met een bepaald serienummer en versienummer, verder 0
		/// </summary>
		public SerialDefinition(long serialnumber, int versionnumber)
		{
			Type = 0;
			Category = 0;
			SubCategory = 0;
			SerialNumber = serialnumber;
			VersionNumber = versionnumber;
		}
	}

	internal class Serial
	{
		public SerialDefinition SerialID;
		public bool Valid = true;

		public Serial(SerialDefinition serialid, bool valid)
		{
			SerialID = serialid;
			Valid = valid;
		}

		public bool CheckVersion(SerialDefinition sd)
		{
			if (SerialID.SerialNumber != sd.SerialNumber)
				throw new Exception("Wrong Serial ID");

			return (SerialID.VersionNumber == sd.VersionNumber);

		}

		public bool SynchronizeVersion(SerialDefinition sd)
		{
			if (CheckVersion(sd))
			{
				Valid = true;
				return true;
			}

            Valid = false;
			return false;
		}

		public void UpdateVersion(SerialDefinition sd)
		{
			if (SerialID.SerialNumber != sd.SerialNumber)
				throw new Exception("Wrong Serial ID");

			SerialID.VersionNumber = sd.VersionNumber;
			Valid = true;
		}
	}
}
