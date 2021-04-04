namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Players;
    using System.Collections.Generic;

    using static ModelConstants.Over;

    public class Over : ValueObject
    {
        internal Over(
            Player bowler,
            Player striker,
            Player nonStriker,
            Ball currentBall)
        {
            this.Bowler = bowler;
            this.Striker = striker;
            this.NonStriker = nonStriker;
            this.CurrentBall = currentBall;
            this.Balls = new List<Ball>();
            this.BatsmenOut = new List<Player>();
        }

        public Player Bowler { get; set; }
        public Player Striker { get; private set; }
        public bool IsStrikerOut { get; private set; } = false;
        public Player NonStriker { get; private set; }
        public bool IsNonStrikerOut { get; private set; } = false;
        public int TotalRuns { get; private set; }
        public int ExtraBalls { get; private set; }
        public Ball CurrentBall { get; private set; }
        public ICollection<Ball> Balls { get; private set; }
        public ICollection<Player> BatsmenOut { get; private set; }
        public bool IsOverEnd { get; private set; } = false;

        public Over UpdateStriker(Player striker)
        {
            if (this.IsStrikerOut)
            {
                this.Striker = striker;

                this.IsStrikerOut = false;
            }
            return this;
        }

        public Over UpdateNonStriker(Player nonStriker)
        {
            if (this.IsNonStrikerOut)
            {
                this.NonStriker = nonStriker;

                this.IsNonStrikerOut = false;
            }
            return this;
        }

        public Over UpdateCurrentBall(Ball ball)
        {
            var totalBallsAllowed = MaxBallsPerOver + this.ExtraBalls;
            if (totalBallsAllowed < this.Balls.Count)
            {
                EndOver();
                throw new InvalidOverException($"Max balls for this over was reached, Over was ended.");
            }
            EndBall();

            this.CurrentBall = ball;

            return this;
        }

        public Over EndOver()
        {
            this.IsOverEnd = true;

            return this;
        }

        private void EndBall()
        {
            if (this.CurrentBall == default!)
            {
                throw new InvalidOverException($"Set value of {nameof(this.CurrentBall)}");
            }
            AddBall();

            if (this.CurrentBall.IsPlayerOut)
            {
                DismissBatsman();
            }
            else
            {
                CalculateRuns();
            }
        }

        private void DismissBatsman()
        {
            var dismissedBatsman = this.CurrentBall.BatsmanOut?.Value!;

            this.BatsmenOut.Add(dismissedBatsman);

            if (this.Striker == dismissedBatsman)
            {
                this.IsStrikerOut = true;
            }
            else if (this.NonStriker == dismissedBatsman)
            {
                this.IsNonStrikerOut = true;
            }
        }

        private void CalculateRuns()
        {
            if (this.CurrentBall.WideBall || this.CurrentBall.NoBall)
            {
                //When there are runs made by the batsmen these runs are added to the team total score
                if (this.CurrentBall.Runs > 0)
                {
                    this.Striker = SetStriker();
                    this.NonStriker = SetNonStriker();

                    this.ExtraBalls++;

                    this.TotalRuns += this.CurrentBall.Runs;
                }
                this.TotalRuns++;
            }

            if (this.CurrentBall.Six)
            {
                this.TotalRuns += 6;
            }

            if (this.CurrentBall.Four)
            {
                this.TotalRuns += 4;
            }
        }

        /// <summary>
        /// Set the non striker for the next ball
        /// </summary>
        private Player SetNonStriker()
            => this.Striker == this.CurrentBall.Striker ?
               this.CurrentBall.NonStriker :
               this.CurrentBall.Striker;

        /// <summary>
        /// Set the striker for the next ball
        /// </summary>
        private Player SetStriker()
            => this.CurrentBall.Runs % 2 is 0 ?
               this.CurrentBall.Striker :
               this.CurrentBall.NonStriker;

        private void AddBall()
            => this.Balls.Add(CurrentBall);
    }
}