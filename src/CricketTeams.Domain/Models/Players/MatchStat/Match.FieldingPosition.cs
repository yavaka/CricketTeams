namespace CricketTeams.Domain.Models.Players.MatchStat
{
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Matches;
    using System.Collections.Generic;
    using System.Linq;
    
    public class MatchFieldingPosition
    {
        private static readonly IEnumerable<FieldingPosition> AllowedFieldingPositions
             = new FieldingPositionData().GetData().Cast<FieldingPosition>();

        private List<PlayerOut> _playersOut;

        internal MatchFieldingPosition(FieldingPosition fieldingPosition)
        {
            ValidateFieldingPosition(fieldingPosition);

            this.FieldingPosition = fieldingPosition;

            this._playersOut = new List<PlayerOut>();
        }

        private MatchFieldingPosition() 
        {
            this.FieldingPosition = default!;
            this._playersOut = default!;
        }

        public FieldingPosition FieldingPosition { get; private set; }
        public IReadOnlyCollection<PlayerOut> PlayersOut
            => this._playersOut.ToList().AsReadOnly();

        public MatchFieldingPosition PlayerOut(Player player, PlayerOutTypes outType)
        {
            this._playersOut.Add(new PlayerOut(player, outType));

            return this;
        }

        private void ValidateFieldingPosition(FieldingPosition fieldingPosition)
        {
            var positionName = fieldingPosition?.PositionName;

            if (AllowedFieldingPositions.Any(p => p.PositionName == positionName))
            {
                return;
            }

            var allowedPositionNames = string.Join(
                " ,",
                AllowedFieldingPositions.Select(p => p.PositionName));

            throw new InvalidFieldingPositionException($"{positionName} is not valid fielding position. Allowed values are: {allowedPositionNames}");
        }
    }
}