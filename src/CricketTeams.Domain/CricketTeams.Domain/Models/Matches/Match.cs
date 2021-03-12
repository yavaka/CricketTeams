namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Teams;
    
    using static ModelConstants.Match;

    public class Match : Entity<int>, IAggregateRoot
    {
        internal Match(
            Team teamA,
            Team teamB,
            int innings,
            int overs,
            Score score,
            Statistic statistic,
            BallTypes ballType,
            Award winnerAward)
        {
            Validate(innings, overs);

            this.TeamA = teamA;
            this.TeamB = teamB;
            this.Innings = innings;
            this.Overs = overs;
            this.Score = score;
            this.Statistic = statistic;
            this.BallType = ballType;
            this.WinnerAward = winnerAward;
        }

        private Match(
            Team teamA,
            Team teamB,
            int innings,
            int overs) 
        {
            this.TeamA = teamA;
            this.TeamB = teamB;
            this.Innings = innings;
            this.Overs = overs;

            this.Score = default!;
            this.Statistic = default!;
            this.BallType = default!;
            this.WinnerAward = default!;
        }

        #region Properties

        public Team TeamA { get; private set; }
        public Team TeamB { get; private set; }

        private int _innings = StandardInnings;
        public int Innings
        {
            get => this._innings;
            private set
            {
                if (value == 3)
                {
                    throw new InvalidMatchException("Invalid innings value! Possible values are 2 and 4.");
                }
                this._innings = value;
            }
        }

        public int Overs { get; private set; } = DefaultOvers;
        public bool InProgress { get; private set; } = false;
        public bool Ended { get; private set; } = false;
        public Score? Score { get; private set; }
        public Statistic? Statistic { get; private set; }
        public BallTypes? BallType { get; private set; }
        public Award? WinnerAward { get; private set; }

        #endregion

        private void Validate(int innings, int overs)
        {
            ValidateInnings(innings);
            ValidateOvers(overs);
        }

        private void ValidateInnings(int innings)
            => Guard.AgainstOutOfRange<InvalidMatchException>(
                innings,
                MinInnings,
                MaxInnings,
                nameof(this.Innings));

        private void ValidateOvers(int overs)
            => Guard.AgainstOutOfRange<InvalidMatchException>(
                overs,
                MinOvers,
                MaxOvers,
                nameof(this.Overs));
    }
}

/*
 * Statistic
 * - match date
 * - man of the match if the match ended
 * - totalMatchTime
 * - each over to be tracked and listed
 * - depending on how many innings are there record the time for each inning
 * - tossWinner
 */

//public Match StartMatch()
//{
//    this.InProgress = true;

//    return this;
//}