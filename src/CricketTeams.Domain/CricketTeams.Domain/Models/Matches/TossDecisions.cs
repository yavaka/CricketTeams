namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;

    public class TossDecisions : Enumeration
    {
        public readonly TossDecisions Batting = new TossDecisions(1,"Batting");
        public readonly TossDecisions Bowling = new TossDecisions(2,"Bowling");

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