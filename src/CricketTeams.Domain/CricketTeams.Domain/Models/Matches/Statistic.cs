namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;
    using System;

    public class Statistic : ValueObject
    {
        internal Statistic(
            int tossWinnerTeamId,
            TossDecisions tossDecision)
        {
            this.Date = DateTime.Now.Date;
            this.TossWinnerTeamId = tossWinnerTeamId;
            this.TossDecision = tossDecision;
        }

        public DateTime Date { get; private set; }
        public int ManOfTheMatchId { get; private set; }
        public int WinningTeamId { get; private set; }
        public int WinningTeamTotalRuns { get; set; }
        public int LosingTeamId { get; private set; }
        public int LosingTeamTotalRuns { get; set; }
        public int TossWinnerTeamId { get; private set; }
        public TossDecisions TossDecision { get; private set; }
    }
}