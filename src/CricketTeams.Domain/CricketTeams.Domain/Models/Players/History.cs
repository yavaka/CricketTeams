namespace CricketTeams.Domain.Models
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Players;
    using CricketTeams.Domain.Models.Players.MatchStat;
    using System.Collections.Generic;
    using System.Linq;

    public class History : ValueObject
    {
        internal History()
        {
            this.Matches = new List<MatchStat>();
        }

        public ICollection<MatchStat> Matches { get; private set; }

        public History AddMatch(MatchStat matchStat)
        {
            this.Matches.Add(matchStat);

            return this;
        }

        public History TakeOutBatsman(Player player, PlayerOutTypes outType)
        {
            this.Matches.Last().TakeOutBatsman(player, outType);

            return this;
        }

        public History IncreaseWideBalls()
        {
            this.Matches.Last().IncreaseWideBalls();

            return this;
        }

        public History IncreaseWickets()
        {
            this.Matches.Last().IncreaseWickets();

            return this;
        }

        public History IncreaseSix()
        {
            this.Matches.Last().IncreaseSix();

            return this;
        }

        public History IncreaseFour()
        {
            this.Matches.Last().IncreaseFour();

            return this;
        }

        /// <param name="player">Player who took out this batsman</param>
        /// <param name="outType">Type of how the player took out the batsman</param>
        public History BatsmanOut(Player fielder, PlayerOutTypes outType)
        {
            this.Matches.Last().BatsmanOut(fielder, outType);

            return this;
        }
    }
}