namespace CricketTeams.Domain.Models.Players.MatchStat
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Matches;
    using System.Collections.Generic;

    public class MatchBatsman : ValueObject
    {
        internal MatchBatsman(
            int numberOfSix,
            int numberOfFour,
            bool isPlayerOut,
            KeyValuePair<Player,PlayerOutTypes> playerOut) 
        {
            this.NumberOfSix = numberOfSix;
            this.NumberOfFour = numberOfFour;
            this.IsPlayerOut = isPlayerOut;
            this.PlayerOut = playerOut;
        }

        private MatchBatsman() 
        {
            this.NumberOfSix = default!;
            this.NumberOfFour = default!;
            this.IsPlayerOut = false;
            this.PlayerOut = default!;
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
        /// Player who took out this player and the type of out 
        /// </summary>
        public KeyValuePair<Player,PlayerOutTypes> PlayerOut { get; set; }

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

        public MatchBatsman DismissPlayer(KeyValuePair<Player, PlayerOutTypes> playerOut) 
        {
            this.PlayerOut = playerOut;
            this.IsPlayerOut = true;

            return this;
        }
    }
}
