namespace CricketTeams.Domain.Models.Players
{
    using CricketTeams.Domain.Common;

    public class BowlingTypes : Enumeration
    {
        public static readonly BowlingTypes FastBowling = new BowlingTypes(1, "Fast bowling");
        public static readonly BowlingTypes SpinBowling = new BowlingTypes(2, "Spin bowling");
        public static readonly BowlingTypes LegSpinBowling = new BowlingTypes(3, "Leg spin bowling");

        private BowlingTypes(int value)
            : this(value, FromValue<BowlingTypes>(value).Name)
        {
        }

        private BowlingTypes(int value, string name)
            : base(value, name)
        {
        }
    }
}