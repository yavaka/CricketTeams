namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;

    public class PlayerOutTypes : Enumeration
    {
        public static readonly PlayerOutTypes Wicket = new PlayerOutTypes(1, "Wicket");
        public static readonly PlayerOutTypes Catch = new PlayerOutTypes(2, "Catch");
        public static readonly PlayerOutTypes RunOut = new PlayerOutTypes(3, "Run out");
        public static readonly PlayerOutTypes LegBeforeWicket = new PlayerOutTypes(4, "Leg before wicket");
        public static readonly PlayerOutTypes HitBallTwice = new PlayerOutTypes(5, "Hit ball twice");

        private PlayerOutTypes(int value)
            : this(value, FromValue<BowlingType>(value).Name)
        {
        }

        private PlayerOutTypes(int value, string name)
            : base(value, name)
        {
        }
    }
}