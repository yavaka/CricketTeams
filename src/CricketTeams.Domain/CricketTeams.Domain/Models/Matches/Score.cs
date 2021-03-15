namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    public class Score : ValueObject
    {
        internal Score(
            int oversPerInning,
            int numberOfInnings,
            ScoreInning currentInning)
        {
            this.OversPerInning = oversPerInning;
            this.NumberOfInnings = numberOfInnings;
            this.CurrentInning = currentInning;
            this.Innings = new ScoreInning[this.NumberOfInnings];
        }

        public int OversPerInning { get; private set; }
        public int NumberOfInnings { get; set; }
        public bool IsMatchEnd { get; private set; } = false;
        public ScoreInning CurrentInning { get; private set; }
        public ICollection<ScoreInning> Innings { get; private set; }

        public Score EndMatch()
        {
            ValidateIsMatchEnd();

            this.IsMatchEnd = true;

            return this;
        }

        public Score EndCurrentInning()
        {
            if (this.CurrentInning.IsCompleted)
            {
                ValidateInning();

                this.Innings.Add(
                    this.CurrentInning != null ?
                    this.CurrentInning :
                    throw new InvalidScoreException("Invalid inning!"));
            }
            return this;
        }

        private void ValidateInning()
        {
            if (this.Innings.Count > this.NumberOfInnings)
            {
                throw new InvalidScoreException($"Maximum number of innings can be {this.NumberOfInnings}");
            }
        }

        private void ValidateIsMatchEnd()
        {
            if (this.Innings.All(i => i == default))
            {
                throw new InvalidScoreException($"Match is still in progress.");
            }
        }
    }
}