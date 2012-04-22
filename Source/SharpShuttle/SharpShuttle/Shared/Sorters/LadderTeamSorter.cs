using System.Collections.Generic;
using Shared.Domain;

namespace Shared.Sorters
{
	/// <summary>
	/// Sorteert ladderteams
	/// </summary>
	public class LadderTeamSorter : IComparer<LadderTeam>
	{

		#region IComparer<LadderTeam> Members

		/// <summary>
		/// Vergelijkt twee ladderteams. Kijkt eerst naar de activiteit, vervolgens naar
		/// het gemiddeld aantal gewonnen sets en als laatst naar het gemiddeld puntensaldo.
		/// </summary>
		/// <param name="x">Het ene LadderTeam</param>
		/// <param name="y">Het andere LadderTeam</param>
		/// <returns>Een int die aangeeft welk team groter is.</returns>
		public int Compare(LadderTeam x, LadderTeam y)
		{
			if (x.Team.IsInOperative & !y.Team.IsInOperative) return 1;
			if (!x.Team.IsInOperative & y.Team.IsInOperative) return -1;

			float sets = x.AverageSetsWon - y.AverageSetsWon;

			if (sets < 0) return 1;
			if (sets > 0) return -1;

			float score = x.AverageScore - y.AverageScore;

			if (score < 0) return 1;
			if (score > 0) return -1;

			return 0;
		}

		#endregion
	}
}
