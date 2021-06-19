namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;

    public class PlayerOutTypes : Enumeration
    {
        public static readonly PlayerOutTypes Wicket = new PlayerOutTypes(1, nameof(Wicket));
        public static readonly PlayerOutTypes Catch = new PlayerOutTypes(2, nameof(Catch));
        public static readonly PlayerOutTypes RunOut = new PlayerOutTypes(3, nameof(RunOut));
        public static readonly PlayerOutTypes LegBeforeWicket = new PlayerOutTypes(4, nameof(LegBeforeWicket));
        public static readonly PlayerOutTypes HitBallTwice = new PlayerOutTypes(5, nameof(HitBallTwice));

        private PlayerOutTypes(int value)
            : this(value, FromValue<PlayerOutTypes>(value).Name)
        {
        }

        private PlayerOutTypes(int value, string name)
            : base(value, name)
        {
        }
    }
}