namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Players;
    using System.Collections.Generic;
    using System.Linq;

    using static ModelConstants.Over;

    public class Over : Entity<int>
    {
        internal Over(
            Player bowler,
            Player striker,
            Player nonStriker)
        {
            ValidateBatsmen(striker, nonStriker);

            this.Bowler = bowler;
            this.Striker = striker;
            this.NonStriker = nonStriker;
            this.Balls = new List<Ball>();
            this.BatsmenOut = new List<Player>();

            this.CurrentBall = default!;
        }

        private Over() 
        {
            this.Bowler = default!;
            this.Striker = default!;
            this.NonStriker = default!;
            this.Balls = default!;
            this.BatsmenOut = default!;
        }

        public Player Bowler { get; set; }
        public Player Striker { get; private set; }
        public Player NonStriker { get; private set; }
        public int TotalRuns { get; private set; }
        public int ExtraBalls { get; private set; }
        public Ball? CurrentBall { get; private set; }
        public ICollection<Ball> Balls { get; private set; }
        public ICollection<Player> BatsmenOut { get; private set; }
        public bool IsOverEnd { get; private set; }

        public Over UpdateCurrentBallWithRuns(int runs)
        {
            ValidateIsOverEnd();

            this.CurrentBall = new Ball(this.Bowler, this.Striker, this.NonStriker, runs);

            UpdateOverStats();
            EndOver();

            return this;
        }

        public Over UpdateCurrentBallWithSix()
        {
            ValidateIsOverEnd();

            this.CurrentBall = new Ball(
                this.Bowler,
                this.Striker,
                this.NonStriker,
                six: true,
                four: false);

            UpdateOverStats();
            EndOver();

            return this;
        }

        public Over UpdateCurrentBallWithFour()
        {
            ValidateIsOverEnd();

            this.CurrentBall = new Ball(
                this.Bowler,
                this.Striker,
                this.NonStriker,
                six: false,
                four: true);

            UpdateOverStats();
            EndOver();

            return this;
        }

        public Over UpdateCurrentBallWithNoBall(int runs)
        {
            ValidateIsOverEnd();

            this.CurrentBall = new Ball(
                this.Bowler,
                this.Striker,
                this.NonStriker,
                runs,
                noBall: true,
                wideBall: false);

            UpdateOverStats();
            EndOver();

            return this;
        }

        public Over UpdateCurrentBallWithWideBall(int runs)
        {
            ValidateIsOverEnd();

            this.CurrentBall = new Ball(
                 this.Bowler,
                this.Striker,
                this.NonStriker,
                runs,
                noBall: false,
                wideBall: true);

            UpdateOverStats();
            EndOver();

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
            Player bowlingTeamPlayer,
            Player dismissedBatsman,
            PlayerOutTypes batsmanOutType)
        {
            ValidateIsNewBatsmanOut(newBatsman);
            ValidateIsOverEnd();
            ValidateIsNoBall();

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
                    bowlingTeamPlayer,
                    dismissedBatsman,
                    batsmanOutType);

                this.NonStriker = newBatsman;

            }
            UpdateOverStats();
            EndOver();

            return this;
        }

        private void EndOver()
        {
            var totalBallsAllowed = MaxBallsPerOver + this.ExtraBalls;
            if (totalBallsAllowed == this.Balls.Count)
            {
                this.IsOverEnd = true;
            }
        }

        private void UpdateOverStats()
        {
            AddLastBall();

            if (this.Balls.Last().IsBatsmanOut)
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
            var dismissedBatsman = this.Balls.Last().DismissedBatsman!;

            this.BatsmenOut.Add(dismissedBatsman);
        }

        private void CalculateRuns()
        {
            if (this.CurrentBall!.WideBall || this.CurrentBall!.NoBall)
            {
                if (this.CurrentBall.Runs > 0)
                {
                    this.Striker = SetStriker();
                    this.NonStriker = SetNonStriker();

                    this.ExtraBalls++;

                    this.TotalRuns += this.CurrentBall.Runs;
                }
                this.TotalRuns++;
            }
            else if (this.CurrentBall.Runs > 0)
            {
                this.Striker = SetStriker();
                this.NonStriker = SetNonStriker();

                this.TotalRuns += this.CurrentBall.Runs;
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
            => this.Striker == this.CurrentBall!.Striker ?
               this.CurrentBall.NonStriker :
               this.CurrentBall.Striker;

        /// <summary>
        /// Set the striker for the next ball to be non striker
        /// </summary>
        private Player SetStriker()
            => this.CurrentBall!.Runs % 2 is 0 ?
               this.CurrentBall.Striker :
               this.CurrentBall.NonStriker;

        private void AddLastBall()
        {
            if (this.CurrentBall is not null)
            {
                this.Balls.Add(CurrentBall);
            }
        }

        private void ValidateIsOverEnd()
        {
            if (this.IsOverEnd)
            {
                throw new InvalidOverException("Over ended.");
            }
            else
            {
                EndOver();
            }
        }

        private void ValidateIsNoBall()
        {
            if (this.Balls.Count > 0 && this.Balls.Last().NoBall is true)
            {
                throw new InvalidOverException("Batsmen cannot be out when there was a no ball in the last ball.");
            }
        }

        private void ValidateBatsmen(Player striker, Player nonStriker)
        {
            if (striker == nonStriker)
            {
                throw new InvalidOverException("Cannot add same striker and non striker.");
            }
        }

        private void ValidateIsNewBatsmanOut(Player newBatsman)
        {
            if (this.BatsmenOut.Any(b => b == newBatsman))
            {
                throw new InvalidOverException($"{newBatsman.FullName} is already out.");
            }
        }
    }
}