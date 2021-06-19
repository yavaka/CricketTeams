namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Scores;
    using CricketTeams.Domain.Models.Stadiums;
    using CricketTeams.Domain.Models.Teams;

    using static ModelConstants.Match;

    public class Match : Entity<int>, IAggregateRoot
    {
        public Match(
            Team teamA,
            Team teamB,
            int numberOfInnings,
            int oversPerInning,
            Umpire firstUmpire,
            Umpire secondUmpire,
            Stadium stadium)
        {
            Validate(teamA.Id, teamB.Id, firstUmpire.Id, secondUmpire.Id, numberOfInnings, oversPerInning);

            this.TeamA = teamA;
            this.TeamB = teamB;
            this.NumberOfInnings = numberOfInnings;
            this.OversPerInning = oversPerInning;
            this.FirstUmpire = firstUmpire;
            this.SecondUmpire = secondUmpire;
            this.Stadium = stadium;
        }

        private Match(
            int numberOfInnings,
            int oversPerInning)
        {
            this.OversPerInning = numberOfInnings;
            this.NumberOfInnings = oversPerInning;

            this.TeamA = default!;
            this.TeamB = default!;
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
        public int OversPerInning { get; private set; } = DefaultOvers;
        public bool InProgress { get; private set; } = false;
        public bool IsMatchEnded { get; private set; } = false;
        public Umpire? FirstUmpire { get; private set; }
        public Umpire? SecondUmpire { get; private set; }
        public Score? Score { get; private set; }
        public Statistic? Statistic { get; private set; }
        public Stadium? Stadium { get; private set; }

        #endregion

        #region Add & Update methods

        public Match StartMatch(int tossWinnerTeamId, TossDecisions tossDecision)
        {
            ValidateIsMatchEnd();
            ValidateIsMatchInProgress();

            ValidateTeamId(tossWinnerTeamId);

            this.Statistic = new Statistic(tossWinnerTeamId, tossDecision);

            this.Score = new Score(tossWinnerTeamId, tossDecision, this.TeamA, this.TeamB, this.OversPerInning, this.NumberOfInnings);

            this.InProgress = true;

            return this;
        }

        public Match UpdateFirstUmpire(Umpire umpire)
        {
            if (this.SecondUmpire is not null && umpire == this.SecondUmpire)
            {
                throw new InvalidMatchException($"Umpire same as second umpire.");
            }
            this.FirstUmpire = umpire;

            return this;
        }

        public Match UpdateSecondUmpire(Umpire umpire)
        {
            if (this.FirstUmpire is not null && umpire == this.FirstUmpire)
            {
                throw new InvalidMatchException($"Umpire same as first umpire.");
            }
            this.SecondUmpire = umpire;

            return this;
        }

        public Match UpdateStadium(Stadium stadium)
        {
            if (this.InProgress)
            {
                throw new InvalidMatchException($"Match already in progress. Stadium cannot be changed.");
            }
            this.Stadium = stadium;

            return this;
        }

        public Match EndMatch()
        {
            ValidateIsScoreDefault();
            ValidateIsMatchEnd();

            this.IsMatchEnded = true;
            this.InProgress = false;

            return this;
        }

        #endregion

        #region Validations

        private void Validate(int teamAId, int teamBId, int firstUmpireId, int secondUmpireId, int innings, int overs)
        {
            if (teamAId == teamBId)
            {
                throw new InvalidMatchException($"Cannot add same teams to compete agains each other.");
            }

            if (firstUmpireId == secondUmpireId)
            {
                throw new InvalidMatchException($"Invalid umpire.");
            }

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
                nameof(this.OversPerInning));

        private void ValidateIsScoreDefault()
        {
            if (this.Score is null)
            {
                throw new InvalidScoreException($"Set new {nameof(this.Score)}");
            }
        }

        private void ValidateIsMatchInProgress()
        {
            if (this.InProgress)
            {
                throw new InvalidMatchException("Match already in progress.");
            }
        }

        private void ValidateTeamId(int tossWinnerTeamId)
        {
            if (tossWinnerTeamId != this.TeamA.Id && tossWinnerTeamId != this.TeamB.Id)
            {
                throw new InvalidMatchException($"Toss winner id: {tossWinnerTeamId} is invalid.");
            }
        }

        private void ValidateIsMatchEnd()
        {
            if (this.Score is not null && this.Score.IsMatchEnd is true)
            {
                throw new InvalidMatchException($"Match ended.");
            }
        }

        #endregion
    }
}
