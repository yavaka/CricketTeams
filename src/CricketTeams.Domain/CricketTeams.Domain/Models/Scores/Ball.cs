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
            KeyValuePair<KeyValuePair<Player, PlayerOutTypes>, Player> batsmanOut)
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
            this.IsPlayerOut = true;
            this.BatsmanOut = batsmanOut;
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
        public bool IsPlayerOut { get; private set; } = false;
        /// <summary>
        /// The key is key value pair where the key is the bowler, 
        /// wicket keeper or other player from fielding team and the value is the 
        /// type of out (catch, wicket, bye or other).
        /// The value is the player who is out.
        /// </summary>
        public KeyValuePair<KeyValuePair<Player, PlayerOutTypes>, Player>? BatsmanOut { get; private set; }

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
            if (runs > 6 && six is true || runs > 6 && six is true)
            {
                throw new InvalidBallException("When the batsman score six, it must be added 6 runs.");
            }
            else if (runs > 4 && four is true || runs > 4 && four is true)
            {
                throw new InvalidBallException("When the batsman score four, it must be added 4 runs.");
            }
        }

        private void ValidateIsPlayerOut(int runs, bool six, bool four, bool wideBall, bool noBall)
        {
            if (this.IsPlayerOut is true && IsAnyTrue(six, four, wideBall, noBall) ||
                this.IsPlayerOut is true && runs > 0)
            {
                throw new InvalidBallException("When batsman is out he cannot do runs and the bowler recieve wide or no ball.");
            }
        }

        private void ValidateRuns(int runs)
            => Guard.AgainstOutOfRange<InvalidBallException>(
                runs,
                MinRuns,
                MaxRuns,
                nameof(this.Runs));

        private bool IsAnyTrue(bool six, bool four, bool wideBall, bool noBall)
            => six is true || four is true || wideBall is true || noBall is true ?
                true :
                false;
    }
}