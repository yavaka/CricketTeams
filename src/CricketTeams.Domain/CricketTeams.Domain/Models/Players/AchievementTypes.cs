namespace CricketTeams.Domain.Models
{
    using CricketTeams.Domain.Common;

    public class AchievementTypes : Enumeration
    {
        public static readonly AchievementTypes Trophy = new AchievementTypes(1, nameof(Trophy));
        public static readonly AchievementTypes Medal = new AchievementTypes(2, nameof(Medal));
        public static readonly AchievementTypes PlayerOfTheMatch = new AchievementTypes(3, nameof(PlayerOfTheMatch));

        private AchievementTypes(int value)
            : this(value, FromValue<AchievementTypes>(value).Name)
        {
        }

        private AchievementTypes(int value, string name)
            : base(value, name)
        {
        }
    }
}