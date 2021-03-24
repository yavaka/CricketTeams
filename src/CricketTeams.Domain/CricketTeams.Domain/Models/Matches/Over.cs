namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Over : ValueObject
    {
        internal Over(
            Player bowler,
            Player striker,
            Player nonStriker,
            Ball currentBall,
            List<Player> battingTeam)
        {
            this.Bowler = bowler;
            this.Striker = striker;
            this.NonStriker = nonStriker;
            this.CurrentBall = currentBall;
            this.BattingTeam = battingTeam;
            this.Balls = new List<Ball>();
            this.WideBalls = new List<Player>();
        }

        public Player Bowler { get; set; }
        public Player Striker { get; private set; }
        public Player NonStriker { get; private set; }
        public int ExtraBalls { get; private set; }
        public int TotalRuns { get; private set; }
        public Ball CurrentBall { get; private set; }
        public ICollection<Ball> Balls { get; private set; }
        public ICollection<Player> WideBalls { get; private set; }
        public ICollection<Player> BattingTeam { get; private set; }

        public Over EndBall()
        {
            AddBall();

            CalculateRuns();



            return this;
        }

        private void CalculateRuns()
        {
            if (this.CurrentBall.WideBall)
            {
                //When there are runs made by the batsman these runs are added to the team total score
                if (this.CurrentBall.Runs > 0)
                {
                    this.Striker = SetStriker();
                    this.NonStriker = SetNonStriker();

                    this.Bowler.History
                        .Matches
                        .Last()
                        .Bowler
                        .IncreaseWideBalls();

                    this.TotalRuns += this.CurrentBall.Runs;
                }
                this.TotalRuns++;
            }

            if (this.CurrentBall.Six)
            {
                this.TotalRuns += 6;
                this.Striker.History
                    .Matches
                    .Last()
                    .Batsman
                    .IncreaseSix();
            }

            if (this.CurrentBall.Four)
            {
                this.TotalRuns += 4;
                this.Striker.History
                    .Matches
                    .Last()
                    .Batsman
                    .IncreaseFour();
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