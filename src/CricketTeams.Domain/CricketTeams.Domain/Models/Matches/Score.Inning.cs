namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Models.Teams;
    using System.Collections.Generic;

    public class ScoreInning
    {
        public Team BattingTeam { get; private set; }
        public Team BowlingTeam { get; private set; }
        public Over CurrentOver { get; private set; }
        public int OversPerInning { get; private set; }
        public int TotalPoints{ get; private set; }
        public bool IsCompleted { get; private set; }
        public ICollection<Over> Overs { get; set; }
    }
}
