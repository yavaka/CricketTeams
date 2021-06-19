namespace CricketTeams.Domain.Models.Players
{
    using CricketTeams.Domain.Common;

    public class BattingStyle : Enumeration
    {
        public static readonly BattingStyle LeftHand = new BattingStyle(1, "Left hand");
        public static readonly BattingStyle RightHand = new BattingStyle(2, "Right hand");

        private BattingStyle(int value)
            : this(value, FromValue<BattingStyle>(value).Name)
        {
        }

        private BattingStyle(int value, string name)
            : base(value, name)
        {
        }
    }
}