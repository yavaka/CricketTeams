namespace CricketTeams.Domain.Models
{
    using CricketTeams.Domain.Common;
    
    public class AchievementType : Enumeration
    {
        public static readonly AchievementType Trophy = new AchievementType(1, nameof(Trophy));
        public static readonly AchievementType Medal = new AchievementType(2, nameof(Medal));
        public static readonly AchievementType PlayerOfTheMatch = new AchievementType(3, nameof(PlayerOfTheMatch));

        private AchievementType(int value)
            : this(value, FromValue<AchievementType>(value).Name)
        {
        }

        private AchievementType(int value, string name)
            : base(value, name)
        {
        }
    }
}