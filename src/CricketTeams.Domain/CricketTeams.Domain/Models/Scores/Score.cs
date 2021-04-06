namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Players;
    using System.Collections.Generic;

    public class Score : Entity<int>
    {
        public Score(
            int oversPerInning,
            int numberOfInnings,
            Inning currentInning)
        {
            this.OversPerInning = oversPerInning;
            this.NumberOfInnings = numberOfInnings;
            this.CurrentInning = currentInning;
            this.Innings = new Inning[this.NumberOfInnings];
        }

        public int OversPerInning { get; private set; }
        public int NumberOfInnings { get; set; }
        public Inning CurrentInning { get; private set; }
        public ICollection<Inning> Innings { get; private set; }
        public bool IsMatchEnd { get; private set; } = false;

        public Score UpdateBall(Ball ball)
        {
            this.CurrentInning.UpdateBall(ball);

            return this;
        }

        /// <summary>
        /// Update ball when there is a batsman out
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="newBatsman"></param>
        /// <returns></returns>
        public Score UpdateBall(Ball ball, Player newBatsman)
        {
            this.CurrentInning.UpdateBall(ball, newBatsman);

            return this;
        }

        public Score UpdateOver(Over over)
        {
            this.CurrentInning.UpdateCurrentOver(over);

            return this;
        }

        public Score UpdateCurrentInning(Inning inning)
        {
            if (this.Innings.Count > this.NumberOfInnings)
            {
                EndMatch();
                throw new InvalidScoreException($"Max innings for this match was reached, Match was ended.");
            }
            EndInning();

            this.CurrentInning = inning;

            return this;
        }

        public Score EndMatch()
        {
            if (this.Innings.Count < this.NumberOfInnings)
            {
                throw new InvalidScoreException($"Match still in progress.");
            }

            this.IsMatchEnd = true;

            return this;
        }

        private void EndInning()
        {
            if (this.CurrentInning == default!)
            {
                throw new InvalidInningException($"Set value of {nameof(this.CurrentInning)}");
            }

            this.CurrentInning.EndInning();

            AddInning();
        }

        private void AddInning()
            => this.Innings.Add(this.CurrentInning);
    }
}