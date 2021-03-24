namespace CricketTeams.Domain.Models
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Players.MatchStat;
    using System.Collections.Generic;

    public class History : ValueObject
    {
        internal History()
        {
            this.Matches = new List<MatchStat>();
        }

        public ICollection<MatchStat> Matches { get; private set; }

        public History AddMatch(MatchStat matchStat)
        {
            this.Matches.Add(matchStat);

            return this;
        }
    }
}