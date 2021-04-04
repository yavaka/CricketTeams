namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Players;
    using CricketTeams.Domain.Models.Teams;
    using System.Collections.Generic;
    using System.Linq;

    public class ScoreInning : ValueObject
    {
        internal ScoreInning(
            Team battingTeam,
            Team bowlingTeam,
            Over currentOver)
        {
            this.BattingTeam = battingTeam;
            this.BowlingTeam = bowlingTeam;
            this.CurrentOver = currentOver;

            this.TotalBatsmenOut = new List<Player>();
            this.Overs = new List<Over>();
        }

        public Team BattingTeam { get; private set; }
        public Team BowlingTeam { get; private set; }
        public int OversPerInning { get; private set; }
        public int TotalRuns { get; private set; }
        public Over CurrentOver { get; private set; }
        public ICollection<Over> Overs { get; private set; }
        public ICollection<Player> TotalBatsmenOut { get; private set; }
        public bool IsInningEnd { get; private set; } = false;

        /// <summary>
        /// Update the current ball
        /// </summary>
        public ScoreInning UpdateBall(Ball newBall)
        {
            ValidateAreAllBatsmenDismissed();

            this.CurrentOver.UpdateCurrentBall(newBall);

            return this;
        }

        /// <summary>
        /// Update the current ball when there is a batsman out from the previous ball
        /// </summary>
        public ScoreInning UpdateBall(Ball newBall, Player batsman)
        {
            ValidateAreAllBatsmenDismissed();

            ValidateBatsman(batsman);

            if (this.CurrentOver.IsStrikerOut)
            {
                this.CurrentOver.UpdateStriker(batsman);
            }
            else if (this.CurrentOver.IsNonStrikerOut)
            {
                this.CurrentOver.UpdateNonStriker(batsman);
            }

            this.CurrentOver.UpdateCurrentBall(newBall);

            return this;
        }

        public ScoreInning UpdateCurrentOver(Over over)
        {
            ValidateAreAllBatsmenDismissed();

            if (this.OversPerInning == this.Overs.Count)
            {
                EndInning();
                throw new InvalidInningException($"Max over for this inning was reached, Inning was ended.");
            }
            EndOver();

            this.CurrentOver = over;

            return this;
        }

        public ScoreInning EndInning()
        {
            if (this.Overs.Count == this.OversPerInning)
            {
                this.IsInningEnd = true;
            }
            else
            {
                throw new InvalidInningException($"Inning still in progress, {this.OversPerInning - this.Overs.Count}");
            }
            return this;
        }

        private void EndOver()
        {
            if (this.CurrentOver == default!)
            {
                throw new InvalidInningException($"Set value of {nameof(this.CurrentOver)}");
            }

            this.CurrentOver.EndOver();

            this.TotalRuns += this.CurrentOver.TotalRuns;

            AddOver();
        }

        private void AddOver()
            => this.Overs.Add(this.CurrentOver);

        private void ValidateAreAllBatsmenDismissed()
        {
            if (this.TotalBatsmenOut.Count == this.BattingTeam.Players.Batsmen.Count)
            {
                EndInning();
                throw new InvalidInningException($"All batsmen are out, Inning was ended.");
            }
        }

        /// <summary>
        /// Is the batsman from the batting team
        /// </summary>
        /// <param name="batsman"></param>
        private void ValidateBatsman(Player batsman)
        {
            if (this.BattingTeam.Players.Batsmen.Any(b => b.FullName != batsman.FullName))
            {
                throw new InvalidInningException($"{batsman.FullName} is not from the batting team. The batting team is: {string.Join(", ", this.BattingTeam.Players.Batsmen)}");
            }
            if (this.CurrentOver.BatsmenOut.Any(b => b.FullName == batsman.FullName))
            {
                throw new InvalidInningException($"Batsman {batsman.FullName} is out.");
            }
        }
    }
}
