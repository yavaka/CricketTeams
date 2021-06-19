namespace CricketTeams.Domain.Models.Players.MatchStat
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Matches;

    public class PlayerOut : ValueObject
    {
        internal PlayerOut(
            Player dismissedPlayer,
            PlayerOutTypes playerOutType)
        {
            this.DismissedPlayer = dismissedPlayer;
            this.PlayerOutType = playerOutType;
        }

        private PlayerOut()
        {
            this.DismissedPlayer = default!;
            this.PlayerOutType = default!;
        }

        public Player DismissedPlayer { get; private set; }
        public PlayerOutTypes PlayerOutType { get; private set; }
    }
}
