namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using System.Collections.Generic;

    public class History : ValueObject
    {
        internal History(
            int totalWins,
            int totalLoses) 
        {
            Validate(totalWins,nameof(this.TotalWins));
            Validate(totalLoses,nameof(this.TotalLoses));

            this.TotalWins = totalWins;
            this.TotalLoses = totalLoses;
            this.MatchesIds = new List<int>();
        }

        public ICollection<int> MatchesIds { get; private set; } 
        public int TotalWins{ get; private set; }
        public int TotalLoses{ get; private set; }

        public History IncreaseWins() 
        {
            this.TotalWins++;

            return this;
        }

        public History IncreaseLoses() 
        {
            this.TotalLoses++;

            return this;
        }

        public History AddMatchId(int id) 
        {
            Validate(id, nameof(this.MatchesIds));

            this.MatchesIds.Add(id);

            return this;
        }

        private void Validate(int value, string propName) 
            => Guard.AgainstNegativeValue<InvalidTeamException>(
                value, propName);
    }
}
