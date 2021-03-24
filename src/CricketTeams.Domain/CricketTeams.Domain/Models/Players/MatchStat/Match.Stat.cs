namespace CricketTeams.Domain.Models.Players.MatchStat
{
    using CricketTeams.Domain.Common;

    using static Models.ModelConstants.MatchStat;
    
    public class MatchStat : ValueObject
    {
        internal MatchStat()
        {
            this.Batsman = new MatchBatsman(DefaultSix, DefaultFour, DefaultIsPlayerOut, default!);
            this.Bowler = new MatchBowler(DefaultWideBalls, DefaultWickets);
            this.FieldingPosition = new MatchFieldingPosition();
        }

        public MatchBatsman Batsman { get; private set; }
        public MatchBowler Bowler { get; private set; }
        public MatchFieldingPosition FieldingPosition { get; private set; }
    }
}
