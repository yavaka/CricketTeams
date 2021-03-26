namespace CricketTeams.Domain.Models
{
    public class ModelConstants
    {
        public class Common
        {
            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 200;

            public const int MinNameLength = 2;
            public const int MaxNameLength = 20;

            public const int MinAge = 1;
            public const int MaxAge = 100;

            public const int MaxUrlLength = 2048;
        }

        public class BowlingStyle
        {
            public const int MinStyleNameLength = 3;
            public const int MaxStyleNameLength = 30;
        }

        public class FieldingPosition
        {
            public const int MinPositionName = 3;
            public const int MaxPositionName = 10;
        }

        public class Achievement
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 30;
        }

        public class Team
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 30;
        }

        public class Stadium
        {
            public const int MinOwnerLength = 3;
            public const int MaxOwnerLength = 20;

            public const int MinCapacityLength = 100;
            public const int MaxCapacityLength = 200000;
        }

        public class Match
        {
            public const int StandardInnings = 2;
            public const int MinInnings = 2;
            public const int MaxInnings = 4;

            public const int DefaultOvers = 20;
            public const int MinOvers = 2;
            public const int MaxOvers = 20;
        }

        public class Over
        {
            public const int MaxBallsPerOver = 6;
        }

        public class Ball
        {
            public const int MinRuns = 0;
            public const int MaxRuns = 20;

            public const int MaxBallsPerOver = 6;
        }

        public class MatchStat
        {
            public const int DefaultSix = 0;
            public const int DefaultFour = 0;
            public const bool DefaultIsPlayerOut = false;

            public const int DefaultWideBalls = 0;
            public const int DefaultWickets = 0;
        }
    }
}
