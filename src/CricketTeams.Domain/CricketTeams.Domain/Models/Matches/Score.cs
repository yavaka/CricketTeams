namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using System.Collections.Generic;

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
        public ScoreInning CurrentInning { get; private set; }
        public ICollection<ScoreInning> Innings { get; private set; }
        public bool IsMatchEnd { get; private set; } = false;

        public Score UpdateBall(Ball ball)
        {
            this.CurrentInning.UpdateBall(ball);

            return this;
        }

        public Score UpdateBall(Ball ball, Player batsman)
        {
            this.CurrentInning.UpdateBall(ball, batsman);

            return this;
        }

        public Score UpdateCurrentOver(Over over)
        {
            this.CurrentInning.UpdateCurrentOver(over);

            return this;
        }

        public Score UpdateCurrentInning(ScoreInning inning)
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