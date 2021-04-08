namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Players;

    public class Ball : ValueObject
    {
        /// <summary>
        /// When one of the batsmen is out
        /// </summary>
        internal Ball(
            Player bowler,
            Player striker,
            Player nonStriker,
            Player bowlingTeamPlayer,
            Player dismissedBatsman,
            PlayerOutTypes playerOutType)
        {
            this.Bowler = bowler;
            this.Striker = striker;
            this.NonStriker = nonStriker;
            this.IsBatsmanOut = true;
            this.BowlingTeamPlayer = bowlingTeamPlayer;
            this.DismissedBatsman = dismissedBatsman;
            this.OutType = playerOutType;
        }

        /// <summary>
        /// Striker score six or four
        /// </summary>
        /// <param name="six">If striker score six, four must be false</param>
        /// <param name="four">If striker score four, six must be false</param>
        internal Ball(
            Player bowler,
            Player striker,
            Player nonStriker,
            bool six,
            bool four)
        {
            ValidateSixAndFour(six, four);

            this.Bowler = bowler;
            this.Striker = striker;
            this.NonStriker = nonStriker;
            this.Six = six;
            this.Four = four;
        }

        /// <summary>
        /// Striker and non striker do runs
        /// </summary>
        internal Ball(
            Player bowler,
            Player striker,
            Player nonStriker,
            int runs)
        {
            ValidateRuns(runs);

            this.Bowler = bowler;
            this.Striker = striker;
            this.NonStriker = nonStriker;
            this.Runs = runs;
        }

        /// <summary>
        /// There is a no ball or wide ball with runs
        /// </summary>
        /// <param name="noBall">If there is a no ball, wide ball must be false</param>
        /// <param name="wideBall">If there is a wide ball, no ball must be false</param>
        internal Ball(
            Player bowler,
            Player striker,
            Player nonStriker,
            int runs,
            bool noBall,
            bool wideBall)
        {
            ValidateRuns(runs);

            ValidateNoBallAndWideBall(noBall, wideBall);

            this.Bowler = bowler;
            this.Striker = striker;
            this.NonStriker = nonStriker;
            this.Runs = runs;
            this.NoBall = noBall;
            this.WideBall = wideBall;
        }

        public Player Bowler { get; set; }
        public Player Striker { get; private set; }
        public Player NonStriker { get; private set; }
        public int Runs { get; private set; }
        public bool WideBall { get; private set; }
        public bool NoBall { get; private set; }
        public bool Six { get; private set; }
        public bool Four { get; private set; }

        #region dismiss player

        public bool IsBatsmanOut { get; private set; }
        /// <summary>
        /// Player id from the bowling team that dismissed one of the batsmen 
        /// </summary>
        public Player? BowlingTeamPlayer { get; private set; }
        public Player? DismissedBatsman { get; private set; }
        public PlayerOutTypes? OutType { get; set; }

        #endregion

        private void ValidateSixAndFour(bool six, bool four)
        {
            if (six is true && four is true)
            {
                throw new InvalidBallException("Batsman cannot score six and four from one ball.");
            }
        }

        private void ValidateNoBallAndWideBall(bool noBall, bool wideBall)
        {
            if (noBall is true && wideBall is true)
            {
                throw new InvalidBallException("Having wide ball and no ball is not allowed.");
            }
        }

        private void ValidateRuns(int runs)
            => Guard.AgainstNegativeValue<InvalidBallException>(
                runs,
                nameof(this.Runs));
    }
}