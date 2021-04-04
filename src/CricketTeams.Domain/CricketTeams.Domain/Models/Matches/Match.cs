namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Teams;
    using CricketTeams.Domain.Models.Stadiums;
    using CricketTeams.Domain.Models.Players;

    using static ModelConstants.Match;
    using CricketTeams.Domain.Models.Scores;

    public class Match : Entity<int>, IAggregateRoot
    {
        public Match(
            Team teamA,
            Team teamB,
            int numberOfInnings,
            int overs,
            Umpire firstUmpire,
            Umpire secondUmpire,
            Score score,
            Statistic statistic,
            Stadium stadium)
        {
            Validate(numberOfInnings, overs);

            this.TeamA = teamA;
            this.TeamB = teamB;
            this.NumberOfInnings = numberOfInnings;
            this.Overs = overs;
            this.FirstUmpire = firstUmpire;
            this.SecondUmpire = secondUmpire;
            this.Score = score;
            this.Statistic = statistic;
            this.Stadium = stadium;
        }

        private Match(
            Team teamA,
            Team teamB)
        {
            this.TeamA = teamA;
            this.TeamB = teamB;

            this.Overs = default!;
            this.NumberOfInnings = default!;
            this.FirstUmpire = default!;
            this.SecondUmpire = default!;
            this.Score = default!;
            this.Statistic = default!;
            this.Stadium = default!;
        }

        #region Properties

        public Team TeamA { get; private set; }
        public Team TeamB { get; private set; }
        public int NumberOfInnings { get; private set; } = StandardInnings;
        public int Overs { get; private set; } = DefaultOvers;
        public bool InProgress { get; private set; } = false;
        public bool Ended { get; private set; } = false;
        public Umpire? FirstUmpire { get; private set; }
        public Umpire? SecondUmpire { get; private set; }
        public Score? Score { get; private set; }
        public Statistic? Statistic { get; private set; }
        public Stadium? Stadium { get; private set; }

        #endregion

        #region Add & Update methods
        
        public Match StartMatch()
        {
            this.InProgress = true;

            return this;
        }

        public Match UpdateFirstUmpire(Umpire umpire)
        {
            this.FirstUmpire = umpire;

            return this;
        }

        public Match UpdateSecondUmpire(Umpire umpire)
        {
            this.SecondUmpire = umpire;

            return this;
        }


        public Match UpdateStatistic(Statistic stat)
        {
            this.Statistic = stat;

            return this;
        }

        public Match UpdateStadium(Stadium stadium)
        {
            this.Stadium = stadium;

            return this;
        }

        public Match EndMatch()
        {
            ValidateIsScoreDefault();

            this.Score!.EndMatch();

            this.Ended = true;
            this.InProgress = false;

            return this;
        }

        #region Score methods

        public Match UpdateScore(Score score)
        {
            this.Score = score;

            return this;
        }

        public Match UpdateBall(Ball ball)
        {
            ValidateIsScoreDefault();

            this.Score!.UpdateBall(ball);

            return this;
        }

        public Match UpdateBall(Ball ball, Player batsmen)
        {
            ValidateIsScoreDefault();

            this.Score!.UpdateBall(ball, batsmen);

            return this;
        }

        public Match UpdateOver(Over over)
        {
            ValidateIsScoreDefault();

            this.Score!.UpdateOver(over);

            return this;
        }

        public Match UpdateInning(ScoreInning inning)
        {
            ValidateIsScoreDefault();

            this.Score!.UpdateCurrentInning(inning);

            return this;
        }

        #endregion
        
        #endregion

        #region Validations

        private void Validate(int innings, int overs)
        {
            ValidateInnings(innings);
            ValidateOvers(overs);
        }

        private void ValidateInnings(int innings)
            => Guard.AgainstNegativeValue<InvalidMatchException>(
                innings,
                nameof(this.NumberOfInnings));

        private void ValidateOvers(int overs)
            => Guard.AgainstOutOfRange<InvalidMatchException>(
                overs,
                MinOvers,
                MaxOvers,
                nameof(this.Overs));

        private void ValidateIsScoreDefault()
        {
            if (this.Score! == default!)
            {
                throw new InvalidScoreException($"Set new {nameof(this.Score)}");
            }
        }

        #endregion
    }
}
/*
 * Statistic
 * - match date
 * - man of the match if the match ended
 * - totalMatchTime
 * - depending on how many innings are there record the time for each inning
 * - tossWinner
 * - tossDecision
 */

