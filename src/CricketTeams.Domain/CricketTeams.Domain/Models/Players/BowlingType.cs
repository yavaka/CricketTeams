namespace CricketTeams.Domain.Models
{
    using CricketTeams.Domain.Common;

    public class BowlingType : Enumeration
    {
        public static readonly BowlingType FastBowling = new BowlingType(1, "Fast bowling");
        public static readonly BowlingType SpinBowling = new BowlingType(2, "Spin bowling");
        public static readonly BowlingType LegSpinBowling = new BowlingType(3, "Leg spin bowling");

        private BowlingType(int value)
            : this(value, FromValue<BowlingType>(value).Name)
        {
        }

        private BowlingType(int value, string name)
            : base(value, name)
        {
        }
    }
}