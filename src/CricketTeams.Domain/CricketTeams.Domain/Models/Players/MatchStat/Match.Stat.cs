namespace CricketTeams.Domain.Models.Players.MatchStat
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Matches;
    using System;
    using System.Collections.Generic;
    
    using static Models.ModelConstants.MatchStat;

    public class MatchStat : ValueObject
    {
        internal MatchStat(
            int matchId, 
            MatchBatsman matchBatsman, 
            MatchBowler bowler, 
            MatchFieldingPosition fieldingPosition)
        {
            ValidateMatchId(matchId);

            this.MatchId = matchId;
            this.Batsman = matchBatsman;
            this.Bowler = bowler;
            this.FieldingPosition = fieldingPosition;
        }

        private MatchStat(int matchId)
        {
            this.MatchId = matchId;

            this.Batsman = default!;
            this.Bowler = default!;
            this.FieldingPosition = default!;
        }

        private void ValidateMatchId(int matchId)
        {
            // TODO: Should check is there any matches with this id
        }

        public int MatchId { get; private set; }
        public MatchBatsman? Batsman { get; private set; }
        public MatchBowler? Bowler { get; private set; }
        public MatchFieldingPosition? FieldingPosition { get; private set; }

        public MatchStat TakeOutBatsman(Player player, PlayerOutTypes outType)
        {
            this.FieldingPosition!.PlayerOut(player, outType);

            return this;
        }

        public MatchStat IncreaseWideBalls()
        {
            this.Bowler!.IncreaseWideBalls();

            return this;
        }

        public MatchStat IncreaseWickets()
        {
            this.Bowler!.IncreaseWickets();

            return this;
        }

        public MatchStat IncreaseSix()
        {
            this.Batsman!.IncreaseSix();

            return this;
        }

        public MatchStat IncreaseFour()
        {
            this.Batsman!.IncreaseFour();

            return this;
        }

        /// <param name="player">Player who took out this batsman</param>
        /// <param name="outType">Type of how the player took out the batsman</param>
        public MatchStat BatsmanOut(Player fielder, PlayerOutTypes outType)
        {
            this.Batsman!.DismissPlayer(
                new KeyValuePair<Player, PlayerOutTypes>(fielder, outType));

            return this;
        }
    }
}
