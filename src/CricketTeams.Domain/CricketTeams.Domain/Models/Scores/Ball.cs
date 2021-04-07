namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Players;
    using System.Collections.Generic;

    using static ModelConstants.Ball;

    public class Ball : ValueObject
    {
        /// <summary>
        /// When one batsman is out
        /// </summary>
        internal Ball(
            Player bowler,
            Player striker,
            Player nonStriker,
            int runs,
            bool six,
            bool four,
            bool wideBall,
            bool noBall,
            Player bowlingTeamPlayer,
            Player dismissedBatsman,
            PlayerOutTypes playerOutType)
        {
            Validate(runs, six, four, wideBall, noBall);

            this.Bowler = bowler;
            this.Striker = striker;
            this.NonStriker = nonStriker;
            this.Runs = runs;
            this.Six = six;
            this.Four = four;
            this.WideBall = wideBall;
            this.NoBall = noBall;
            this.IsBatsmanOut = true;
            this.BowlingTeamPlayer = bowlingTeamPlayer;
            this.DismissedBatsman = dismissedBatsman;
            this.OutType = playerOutType;
        }

        /// <summary>
        /// When the batsmen are not out
        /// </summary>
        internal Ball(
            Player bowler,
            Player striker,
            Player nonStriker,
            int runs,
            bool six,
            bool four,
            bool wideBall,
            bool noBall)
        {
            Validate(runs, six, four, wideBall, noBall);

            this.Bowler = bowler;
            this.Striker = striker;
            this.NonStriker = nonStriker;
            this.Runs = runs;
            this.Six = six;
            this.Four = four;
            this.WideBall = wideBall;
            this.NoBall = noBall;
        }

        public Player Bowler { get; set; }
        public Player Striker { get; private set; }
        public Player NonStriker { get; private set; }
        public int Runs { get; private set; }
        public bool WideBall { get; private set; } = false;
        public bool NoBall { get; private set; } = false;
        /// <summary>
        /// How many times a given player hit 6 in this over
        /// </summary>
        public bool Six { get; private set; } = false;
        /// <summary>
        /// How many times a given player hit 4 in this over
        /// </summary>
        public bool Four { get; private set; } = false;
        
        #region dismiss player
        
        public bool IsBatsmanOut { get; private set; } = false;
        /// <summary>
        /// Player id from the bowling team that dismissed one of the batsmen 
        /// </summary>
        public Player? BowlingTeamPlayer { get; private set; }
        public Player? DismissedBatsman { get; private set; }
        public PlayerOutTypes? OutType{ get; set; }

        #endregion
        
        private void Validate(
            int runs,
            bool six,
            bool four,
            bool wideBall,
            bool noBall)
        {
            ValidateRuns(runs);

            ValidateIsPlayerOut(runs, six, four, wideBall, noBall);

            if (six is true && four is true)
            {
                throw new InvalidBallException("Batsman cannot score six and four from one ball.");
            }
            else if (noBall is true && six is true && four is true)
            {
                throw new InvalidBallException("Batsman cannot score six and four at same time when there is no ball.");
            }
            else if (wideBall is true && noBall is true)
            {
                throw new InvalidBallException("Not allowed to have wide ball and no ball.");
            }

            ValidateAreRunsCorrect(runs, six, four);
        }

        private void ValidateAreRunsCorrect(int runs, bool six, bool four)
        {
            if (runs > 0 && six is true)
            {
                throw new InvalidBallException("When the batsman score six, it must be added 6 runs only.");
            }
            else if (runs > 0 && four is true)
            {
                throw new InvalidBallException("When the batsman score four, it must be added 4 runs only.");
            }
        }

        private void ValidateIsPlayerOut(int runs, bool six, bool four, bool wideBall, bool noBall)
        {
            if (this.IsBatsmanOut is true && IsAnyTrue(six, four, wideBall, noBall) ||
                this.IsBatsmanOut is true && runs > 0)
            {
                throw new InvalidBallException("When batsman is out he cannot do runs and the bowler recieve wide or no ball.");
            }
        }

        private void ValidateRuns(int runs)
            => Guard.AgainstNegativeValue<InvalidBallException>(
                runs,
                nameof(this.Runs));

        private bool IsAnyTrue(bool six, bool four, bool wideBall, bool noBall)
            => six is true || four is true || wideBall is true || noBall is true ?
                true :
                false;
    }
}