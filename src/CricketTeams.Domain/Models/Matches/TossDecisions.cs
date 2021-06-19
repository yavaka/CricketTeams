namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;

    public class TossDecisions : Enumeration
    {
        public static readonly TossDecisions Batting = new TossDecisions(1,"Batting");
        public static readonly TossDecisions Bowling = new TossDecisions(2,"Bowling");

        private TossDecisions(int value) 
            : this(value, FromValue<TossDecisions>(value).Name)
        {
        }

        private TossDecisions(int value, string name)
            : base(value, name)
        {
        }
    }
}