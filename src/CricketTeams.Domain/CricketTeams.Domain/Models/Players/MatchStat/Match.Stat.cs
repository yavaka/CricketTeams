namespace CricketTeams.Domain.Models.Players.MatchStat
{
    public class MatchStat
    {
        public MatchBatsman Batsman { get; private set; }
        public MatchBowler Bowler { get; private set; }
        public MatchFieldingPosition FieldingPosition { get; private set; }
        public int TotalRuns { get; private set; }
    }
}
