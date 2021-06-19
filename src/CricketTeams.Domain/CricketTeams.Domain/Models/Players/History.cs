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
        private List<MatchStat> _matches;
        internal History()
        {
            this._matches = new List<MatchStat>();
        }

        public IReadOnlyCollection<MatchStat> Matches
            => this._matches.ToList().AsReadOnly();

        public History AddMatch(MatchStat matchStat)
        {
            this._matches.Add(matchStat);

            return this;
        }

        public History TakeOutBatsman(Player player, PlayerOutTypes outType)
        {
            this._matches.Last().TakeOutBatsman(player, outType);

            return this;
        }

        public History IncreaseWideBalls()
        {
            this._matches.Last().IncreaseWideBalls();

            return this;
        }

        public History IncreaseWickets()
        {
            this._matches.Last().IncreaseWickets();

            return this;
        }

        public History IncreaseSix()
        {
            this._matches.Last().IncreaseSix();

            return this;
        }

        public History IncreaseFour()
        {
            this._matches.Last().IncreaseFour();

            return this;
        }

        /// <param name="player">Player who took out this batsman</param>
        /// <param name="outType">Type of how the player took out the batsman</param>
        public History BatsmanOut(Player fielder, PlayerOutTypes outType)
        {
            this._matches.Last().BatsmanOut(fielder, outType);

            return this;
        }
    }
}