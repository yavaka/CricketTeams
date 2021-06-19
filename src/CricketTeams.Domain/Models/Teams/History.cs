namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Matches;
    using System.Collections.Generic;
    using System.Linq;

    public class History : Entity<int>
    {
        internal History(
            int totalWins,
            int totalLoses) 
        {
            Validate(totalWins,nameof(this.TotalWins));
            Validate(totalLoses,nameof(this.TotalLoses));

            this.TotalWins = totalWins;
            this.TotalLoses = totalLoses;
        }

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

        private void Validate(int value, string propName) 
            => Guard.AgainstNegativeValue<InvalidTeamException>(
                value, propName);
    }
}
