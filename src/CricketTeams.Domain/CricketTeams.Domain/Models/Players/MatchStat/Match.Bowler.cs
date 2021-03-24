namespace CricketTeams.Domain.Models.Players.MatchStat
{
    using CricketTeams.Domain.Common;

    public class MatchBowler : ValueObject
    {
        internal MatchBowler(int wideBalls, int wickets)
        {
            this.WideBalls = wideBalls;
            this.Wickets = wickets;
        }

        public int WideBalls { get; private set; }
        public int Wickets { get; private set; }

        public MatchBowler IncreaseWideBalls()
        {
            this.WideBalls++;

            return this;
        }

        public MatchBowler IncreaseWickets()
        {
            this.Wickets++;

            return this;
        }
    }
}
