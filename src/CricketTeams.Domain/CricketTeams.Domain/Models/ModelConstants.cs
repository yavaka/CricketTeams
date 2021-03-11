﻿namespace CricketTeams.Domain.Models
{
    public class ModelConstants
    {
        public class Common 
        {
            public const int MinDescriptionLength = 2;
            public const int MaxDescriptionLength = 20;

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
    }
}
