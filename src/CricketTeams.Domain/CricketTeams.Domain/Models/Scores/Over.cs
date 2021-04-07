namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Players;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        public Player NonStriker { get; private set; }
        public int TotalRuns { get; private set; }
        public int ExtraBalls { get; private set; }
        public Ball CurrentBall { get; private set; }
        public ICollection<Ball> Balls { get; private set; }
        public ICollection<Player> BatsmenOut { get; private set; }
        public bool IsOverEnd { get; private set; } = false;

        public Over UpdateCurrentBall(int runs, bool six, bool four, bool wideBall, bool noBall)
        {
            ValidateIsOverEnd();

            AddLastBall();

            this.CurrentBall = new Ball(this.Bowler, this.Striker, this.NonStriker, runs, six, four, wideBall, noBall);

            UpdateOverStats();

            return this;
        }

        /// <param name="isStrikerOut">If true striker is out else non striker is out.</param>
        /// <param name="newBatsman">The batsman to be replaced with the one who is out.</param>
        /// <param name="bowlingTeamPlayer">The player from the bowling team who dismissed the batsman</param>
        /// <param name="dismissedBatsman">The batsman who is out. It must be the striker or the non striker.</param>
        /// <param name="batsmanOutType">The type of how the batsman has been dismissed.</param>
        /// <returns></returns>
        public Over UpdateCurrentBallWithDismissedBatsman(
            bool isStrikerOut,
            Player newBatsman,
            int runs, bool six, bool four, bool wideBall, bool noBall,
            Player bowlingTeamPlayer,
            Player dismissedBatsman,
            PlayerOutTypes batsmanOutType)
        {
            ValidateIsOverEnd();

            AddLastBall();

            if (isStrikerOut)
            {
                if (this.Striker.Age != dismissedBatsman.Age && 
                    this.Striker.FullName != dismissedBatsman.FullName)
                {
                    throw new InvalidOverException($"Dismissed batsman is {this.Striker.FullName}.");
                }

                this.CurrentBall = new Ball(
                    this.Bowler,
                    newBatsman,
                    this.NonStriker,
                    runs, six, four, wideBall, noBall,
                    bowlingTeamPlayer,
                    dismissedBatsman,
                    batsmanOutType);

                this.Striker = newBatsman;
            }
            else
            {
                if (this.NonStriker.Age != dismissedBatsman.Age && 
                    this.NonStriker.FullName != dismissedBatsman.FullName)
                {
                    throw new InvalidOverException($"Dismissed batsman is {this.NonStriker.FullName}.");
                }

                this.CurrentBall = new Ball(
                    this.Bowler,
                    this.Striker,
                    newBatsman,
                    runs, six, four, wideBall, noBall,
                    bowlingTeamPlayer,
                    dismissedBatsman,
                    batsmanOutType);

                this.NonStriker = newBatsman;
            }

            UpdateOverStats();

            return this;
        }

        public Over EndOver()
        {
            this.IsOverEnd = true;

            return this;
        }

        private void UpdateOverStats()
        {
            if (this.CurrentBall.IsBatsmanOut)
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
            var dismissedBatsman = this.CurrentBall.DismissedBatsman!;

            this.BatsmenOut.Add(dismissedBatsman);
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
        /// Set the non striker for the next ball to be striker
        /// </summary>
        private Player SetNonStriker()
            => this.Striker == this.CurrentBall.Striker ?
               this.CurrentBall.NonStriker :
               this.CurrentBall.Striker;

        /// <summary>
        /// Set the striker for the next ball to be non striker
        /// </summary>
        private Player SetStriker()
            => this.CurrentBall.Runs % 2 is 0 ?
               this.CurrentBall.Striker :
               this.CurrentBall.NonStriker;

        private void AddLastBall()
            => this.Balls.Add(CurrentBall);


        private void ValidateIsOverEnd()
        {
            var totalBallsAllowed = MaxBallsPerOver + this.ExtraBalls;
            if (totalBallsAllowed == this.Balls.Count)
            {
                EndOver();
                throw new InvalidOverException($"Max balls for this over was reached, Over was ended.");
            }
        }
    }
}