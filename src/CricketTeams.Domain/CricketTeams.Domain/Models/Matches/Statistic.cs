namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;
    using System;

    public class Statistic : ValueObject
    {
        internal Statistic(
            DateTime date,
            int manOfTheMatchId,
            int winningTeamId,
            int winningTeamTotalRuns,
            int losingTeamId,
            int losingTeamTotalRuns,
            int tossWinnerId,
            TossDecisions tossDecision) 
        {
            this.Date = date;
            this.ManOfTheMatchId = manOfTheMatchId;
            this.WinningTeamId = winningTeamId;
            this.WinningTeamTotalRuns = winningTeamTotalRuns;
            this.LosingTeamId = losingTeamId;
            this.LosingTeamTotalRuns = losingTeamTotalRuns;
            this.TossWinnerId = tossWinnerId;
            this.TossDecision = tossDecision;
        }

        public DateTime Date { get; private set; }
        public int ManOfTheMatchId { get; private set; }
        public int WinningTeamId { get; private set; }
        public int WinningTeamTotalRuns { get; set; }
        public int LosingTeamId { get; private set; }
        public int LosingTeamTotalRuns { get; set; }
        public int TossWinnerId { get; private set; }
        public TossDecisions TossDecision { get; private set; }
    }
}