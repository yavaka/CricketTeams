namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Players;
    using CricketTeams.Domain.Models.Teams;
    using System.Collections.Generic;
    using System.Linq;

    public class Inning : ValueObject
    {
        internal Inning(
            Team battingTeam,
            Team bowlingTeam,
            int oversPerInning)
        {
            ValidateOversPerInning(oversPerInning);

            this.BattingTeam = battingTeam;
            this.BowlingTeam = bowlingTeam;
            this.OversPerInning = oversPerInning;
            this.TotalBatsmenOut = new List<Player>();
            this.Overs = new List<Over>();

            this.CurrentOver = default!;
        }

        public Team BattingTeam { get; private set; }
        public Team BowlingTeam { get; private set; }
        public int OversPerInning { get; private set; }
        public int TotalRuns { get; private set; }
        public Over? CurrentOver { get; private set; }
        public ICollection<Over> Overs { get; private set; }
        public ICollection<Player> TotalBatsmenOut { get; private set; }
        public bool IsInningEnd { get; private set; } = false;

        #region Over methods

        public Inning UpdateCurrentBallWithRuns(int runs)
        {
            ValidateIsOverSet();

            this.CurrentOver!.UpdateCurrentBallWithRuns(runs);

            return this;
        }

        public Inning UpdateCurrentBallWithSix()
        {
            ValidateIsOverSet();

            this.CurrentOver!.UpdateCurrentBallWithSix();

            return this;
        }

        public Inning UpdateCurrentBallWithFour()
        {
            ValidateIsOverSet();

            this.CurrentOver!.UpdateCurrentBallWithFour();

            return this;
        }

        public Inning UpdateCurrentBallWithNoBall(int runs)
        {
            ValidateIsOverSet();

            this.CurrentOver!.UpdateCurrentBallWithNoBall(runs);

            return this;
        }

        public Inning UpdateCurrentBallWithWideBall(int runs)
        {
            ValidateIsOverSet();

            this.CurrentOver!.UpdateCurrentBallWithWideBall(runs);

            return this;
        }

        public Inning UpdateCurrentBallWithDismissedBatsman(
            bool isStrikerOut,
            Player newBatsman,
            Player bowlingTeamPlayer,
            Player dismissedBatsman,
            PlayerOutTypes batsmanOutType)
        {
            ValidateIsOverSet();
            if (AreAllBatsmenDismissed())
            {
                EndInning();
                UpdateInningStat();
            }
            else
            {
                ValidateBatsman(newBatsman);
                ValidateBowlingTeamPlayer(bowlingTeamPlayer);

                this.CurrentOver!.UpdateCurrentBallWithDismissedBatsman(
                    isStrikerOut,
                    newBatsman,
                    bowlingTeamPlayer,
                    dismissedBatsman,
                    batsmanOutType);

                if (AreAllBatsmenDismissed())
                {
                    EndInning();
                    UpdateInningStat();
                }
            }
            return this;
        }

        #endregion

        #region Inning methods

        public Inning UpdateCurrentOver(Player bowler, Player striker, Player nonStriker)
        {
            Validate(bowler, striker, nonStriker);

            if (this.CurrentOver is not null)
            {
                // Create new over with batsmen from the last ball 
                // of the previous over if one was not out.
                var lastBall = this.CurrentOver.Balls.Last();
                if (lastBall.IsBatsmanOut is false)
                {
                    UpdateInningStat();
                    this.CurrentOver = new Over(bowler, lastBall.Striker, lastBall.NonStriker);
                }
                else
                {
                    throw new InvalidInningException($"Batsman needed. Use {nameof(UpdateCurrentOverWithBatsman)} method.");
                }
            }
            else
            {
                this.CurrentOver = new Over(bowler, striker, nonStriker);
            }
            return this;
        }

        public Inning UpdateCurrentOverWithBatsman(Player bowler, Player newBatsman)
        {
            ValidateIsOverSet();
            Validate(bowler: bowler);

            var lastBall = this.CurrentOver!.Balls.Last();
            if (lastBall.IsBatsmanOut)
            {
                UpdateInningStat();
                ValidateBatsman(newBatsman);

                //Set new batsman on a striker position
                if (lastBall.DismissedBatsman!.Equals(lastBall.Striker))
                {
                    this.CurrentOver = new Over(bowler, newBatsman, lastBall.NonStriker);
                }
                else // Set new batsman on a non striker position
                {
                    this.CurrentOver = new Over(bowler, lastBall.Striker, newBatsman);
                }
            }
            else
            {
                throw new InvalidInningException("Batsmen are not out.");
            }
            return this;
        }

        public Inning EndInning()
        {
            if (this.Overs.Count == this.OversPerInning ||
                AreAllBatsmenDismissed())
            {
                this.IsInningEnd = true;
            }
            else
            {
                throw new InvalidInningException($"Inning still in progress, {this.OversPerInning - this.Overs.Count}");
            }
            return this;
        }

        private void UpdateInningStat()
        {
            this.TotalRuns = this.TotalRuns + this.CurrentOver!.TotalRuns;

            if (this.CurrentOver.BatsmenOut.Count > 0)
            {
                this.CurrentOver.BatsmenOut
                    .ToList()
                    .ForEach(b => this.TotalBatsmenOut.Add(b));
            }
            AddLastOver();
        }

        private void AddLastOver()
        {
            if (this.CurrentOver is not null)
            {
                this.Overs.Add(this.CurrentOver);
            }
        }

        #endregion

        #region Validations

        private void Validate(Player? bowler = null, Player? striker = null, Player? nonStriker = null)
        {
            if (this.CurrentOver is not null)
            {
                ValidateIsInningEnd();
                if (AreAllBatsmenDismissed())
                {
                    EndInning();
                    UpdateInningStat();
                }
                else
                {
                    ValidateIsCurrentOverEnd();
                }
            }
            if (bowler is not null)
            {
                ValidateBowlingTeamPlayer(bowler);
            }
            if (striker is not null)
            {
                ValidateBatsman(striker);
            }
            if (nonStriker is not null)
            {
                ValidateBatsman(nonStriker);
            }
        }

        private void ValidateIsInningEnd()
        {
            if (this.IsInningEnd)
            {
                throw new InvalidOverException("Inning ended.");
            }

            if (this.OversPerInning == this.Overs.Count)
            {
                AddLastOver();
                UpdateInningStat();
                EndInning();
            }
        }

        private bool AreAllBatsmenDismissed()
        {
            var dismissedBatsmen = this.TotalBatsmenOut.Count + this.CurrentOver!.BatsmenOut.Count;
            if (dismissedBatsmen == this.BattingTeam.Players.Batsmen.Count - 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Is the batsman from the batting team
        /// </summary>
        /// <param name="batsman"></param>
        private void ValidateBatsman(Player batsman)
        {
            if (!this.BattingTeam.Players.AllPlayers.Any(b => b.Equals(batsman)))
            {
                throw new InvalidInningException($"{batsman.FullName} is not part of the batting team. The batting team is: {string.Join(", ", this.BattingTeam.Players.Batsmen)}");
            }
            if (this.CurrentOver is not null && this.CurrentOver.BatsmenOut.Any(b => b.FullName == batsman.FullName))
            {
                throw new InvalidInningException($"{batsman.FullName} is out.");
            }
        }

        /// <summary>
        /// Validate is the bowler from the bowling team and 
        /// is he same as this from last over
        /// </summary>
        private void ValidateBowlingTeamPlayer(Player fielder)
        {
            if (!this.BowlingTeam.Players.AllPlayers.Any(p => p.Equals(fielder)))
            {
                throw new InvalidInningException($"{fielder.FullName} is not part of bowling team.");
            }
            if (this.CurrentOver is not null && this.CurrentOver.IsOverEnd)
            {
                if (this.CurrentOver.Bowler.Equals(fielder))
                {
                    throw new InvalidInningException($"One bowler cannot play two overs in row.");
                }
            }
        }

        private void ValidateIsCurrentOverEnd()
        {
            if (!this.CurrentOver!.IsOverEnd)
            {
                throw new InvalidInningException("Over in progress.");
            }
        }

        private void ValidateOversPerInning(int oversPerInning)
        {
            IReadOnlyCollection<int> allowedOversPerInning = new int[] { 10, 15, 20 };

            if (!allowedOversPerInning.Any(o => o == oversPerInning))
            {
                throw new InvalidInningException($"Invalid allowed overs per inning. Expected value can be: {string.Join(", ", allowedOversPerInning)}.");
            }
        }

        private void ValidateIsOverSet()
        {
            if (this.CurrentOver is null)
            {
                throw new InvalidInningException($"Current over not initialised. Use {nameof(UpdateCurrentOver)} method.");
            }
        }

        #endregion
    }
}
