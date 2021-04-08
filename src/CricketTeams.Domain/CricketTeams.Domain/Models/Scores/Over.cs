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
            Player nonStriker)
        {
            this.Bowler = bowler;
            this.Striker = striker;
            this.NonStriker = nonStriker;
            this.Balls = new List<Ball>();
            this.BatsmenOut = new List<Player>();
            
            this.CurrentBall = default!;
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
            AddLastBall();
            
            ValidateIsOverEnd();

            this.CurrentBall = new Ball(this.Bowler, this.Striker, this.NonStriker, runs);

            UpdateOverStats();

            return this;
        }

        public Over UpdateCurrentBallWithSix()
        {
            AddLastBall();
            
            ValidateIsOverEnd();

            this.CurrentBall = new Ball(
                this.Bowler,
                this.Striker,
                this.NonStriker,
                six: true,
                four: false);

            UpdateOverStats();

            return this;
        }

        public Over UpdateCurrentBallWithFour()
        {
            AddLastBall();
            
            ValidateIsOverEnd();

            this.CurrentBall = new Ball(
                this.Bowler,
                this.Striker,
                this.NonStriker,
                six: false,
                four: true);

            UpdateOverStats();

            return this;
        }

        public Over UpdateCurrentBallWithNoBall(int runs)
        {
            AddLastBall();
            
            ValidateIsOverEnd();

            this.CurrentBall = new Ball(
                this.Bowler,
                this.Striker,
                this.NonStriker,
                runs,
                noBall: true,
                wideBall: false);

            UpdateOverStats();

            return this;
        }

        public Over UpdateCurrentBallWithWideBall(int runs)
        {
            AddLastBall();
            
            ValidateIsOverEnd();

            this.CurrentBall = new Ball(
                 this.Bowler,
                this.Striker,
                this.NonStriker,
                runs,
                noBall: false,
                wideBall: true);

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
            Player bowlingTeamPlayer,
            Player dismissedBatsman,
            PlayerOutTypes batsmanOutType)
        {
            AddLastBall();
            
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

            return this;
        }

        public Over EndOver()
        {
            if (this.Balls.Count < (MaxBallsPerOver + this.ExtraBalls))
            {
                throw new InvalidOverException($"Over in progress. {(MaxBallsPerOver + this.ExtraBalls) - this.Balls.Count} balls left.");
            }
            this.IsOverEnd = true;

            return this;
        }

        private void UpdateOverStats()
        {
            if (this.CurrentBall!.IsBatsmanOut)
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
            var dismissedBatsman = this.CurrentBall!.DismissedBatsman!;

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
            if (!(this.CurrentBall is null))
            {
                this.Balls.Add(CurrentBall!);
            }
        }

        private void ValidateIsOverEnd()
        {
            if (this.IsOverEnd)
            {
                throw new InvalidOverException("Over ended.");
            }

            var totalBallsAllowed = (MaxBallsPerOver + this.ExtraBalls) - 1;
            if (totalBallsAllowed == this.Balls.Count)
            {
                UpdateOverStats();
                AddLastBall();
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
    }
}