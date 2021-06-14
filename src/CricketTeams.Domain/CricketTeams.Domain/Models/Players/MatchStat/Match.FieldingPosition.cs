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

        internal MatchFieldingPosition(FieldingPosition fieldingPosition)
        {
            ValidateFieldingPosition(fieldingPosition);

            this.FieldingPosition = fieldingPosition;

            this.PlayersOut = new List<PlayerOut>();
        }

        private MatchFieldingPosition() 
        {
            this.FieldingPosition = default!;
            this.PlayersOut = default!;
        }

        public FieldingPosition FieldingPosition { get; private set; }
        public ICollection<PlayerOut> PlayersOut { get; private set; }

        public MatchFieldingPosition PlayerOut(Player player, PlayerOutTypes outType)
        {
            this.PlayersOut.Add(new PlayerOut(player, outType));

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