namespace CricketTeams.Domain.Models.Players.MatchStat
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Matches;

    public class MatchBatsman : ValueObject
    {
        /// <summary>
        /// Dismissed batsman
        /// </summary>
        /// <param name="numberOfSix"></param>
        /// <param name="numberOfFour"></param>
        /// <param name="isPlayerOut"></param>
        /// <param name="playerOut"></param>
        internal MatchBatsman(
            int numberOfSix,
            int numberOfFour,
            bool isPlayerOut,
            Player fielder,
            PlayerOutTypes playerOutType)
        {
            this.NumberOfSix = numberOfSix;
            this.NumberOfFour = numberOfFour;
            this.IsPlayerOut = isPlayerOut;
            this.Fielder = fielder;
            this.PlayerOutType = playerOutType;
        }

        /// <summary>
        /// Not dismissed batsman
        /// </summary>
        /// <param name="numberOfSix"></param>
        /// <param name="numberOfFour"></param>
        internal MatchBatsman(
            int numberOfSix,
            int numberOfFour)
        {
            this.NumberOfSix = numberOfSix;
            this.NumberOfFour = numberOfFour;
        }

        private MatchBatsman()
        {
            this.NumberOfSix = default!;
            this.NumberOfFour = default!;
            this.IsPlayerOut = false;
            this.Fielder = default!;
            this.PlayerOutType = default!;
        }

        public int TotalRuns { get; private set; }
        /// <summary>
        /// The number of sixes scored throught the match
        /// </summary>
        public int NumberOfSix { get; private set; }
        /// <summary>
        /// The number of fours scored throught the match
        /// </summary>
        public int NumberOfFour { get; private set; }
        public bool IsPlayerOut { get; private set; }
        /// <summary>
        /// Player who dismissed this batsman
        /// </summary>
        public Player? Fielder { get; private set; }
        public PlayerOutTypes? PlayerOutType { get; private set; }

        public MatchBatsman IncreaseSix()
        {
            this.NumberOfSix++;
            this.TotalRuns += 6;

            return this;
        }

        public MatchBatsman IncreaseFour()
        {
            this.NumberOfFour++;
            this.TotalRuns += 4;

            return this;
        }

        public MatchBatsman DismissPlayer(Player fielder, PlayerOutTypes playerOutType)
        {
            this.Fielder = fielder;
            this.PlayerOutType = playerOutType;
            this.IsPlayerOut = true;

            return this;
        }
    }
}
