namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Teams;
    using FakeItEasy;
    using FluentAssertions;
    using System;
    using System.Linq;
    using Xunit;

    public class ScoreSpecs
    {
        private Score _score;

        public ScoreSpecs()
        {
            this._score = A.Dummy<Score>();
        }

        [Fact]
        public void UpdateCurrentInningShouldSetInning()
        {
            //Act
            this._score.UpdateCurrentInning();

            var expectedInning = new Inning(
                        this._score.TeamA,
                        this._score.TeamB,
                        20);
            //Arrange
            this._score.CurrentInning!.BattingTeam.Id
                .Should()
                .Be(expectedInning.BattingTeam.Id);

            this._score.CurrentInning!.BowlingTeam.Id
                .Should()
                .Be(expectedInning.BowlingTeam.Id);
        }

        [Fact]
        public void CreateScoreWithSameTeamsShouldThrowException()
        {
            //Arrange
            var team = A.Dummy<Team>();

            //Act
            Action act = ()
                => new Score(1, TossDecisions.Batting,team, team, 5, 2);

            //Assert
            act.Should().Throw<InvalidScoreException>();
        }

        [Fact]
        public void UpdateInningBeforeEndShouldThrowException()
        {
            //Arrange
            this._score.UpdateCurrentInning();

            //Act
            Action act = ()
                => this._score.UpdateCurrentInning();

            //Assert
            act.Should().Throw<InvalidScoreException>();
        }

        [Fact]
        public void UpdateInningShouldAddLastInningAndSetRunsForTeamA()
        {
            //Arrange
            this._score = ScoreFakes.Data.GetScoreWithEndedInning();

            //Act
            this._score.UpdateCurrentInning();

            //Assert
            this._score.Innings.First().BowlingTeam.Id
                .Should()
                .Be(this._score.TeamB.Id);

            this._score.Innings.First().BattingTeam.Id
                .Should()
                .Be(this._score.TeamA.Id);

            this._score.TotalScoreTeamA
                .Should()
                .Be(this._score.Innings.First().TotalRuns);
        }

        [Fact]
        public void EndMatchShouldSetScoreOfTeamAAndBAndContainTwoInnings()
        {
            //Arrange
            this._score = ScoreFakes.Data.GetScoreWithEndedInning();

            this._score.UpdateCurrentInning();

            var striker = this._score.TeamB.Players.Batsmen.First(i => i.Id == 1);
            var nonStriker = this._score.TeamB.Players.Batsmen.First(i => i.Id == 2);
            var bowler = this._score.TeamA.Players.Bowlers.First();
            var secondBowler = this._score.TeamA.Players.Bowlers.Last();

            //Act
            for (int over = 0; over < 5; over++)
            {
                this._score.UpdateCurrentOver(
                    over % 2 == 0 ? bowler : secondBowler,
                    striker,
                    nonStriker);

                for (int ball = 0; ball < 6; ball++)
                {
                    this._score.UpdateCurrentBallWithRuns(ball);
                }
            }

            //Assert
            this._score.IsMatchEnd.Should().BeTrue();

            this._score.TotalScoreTeamA
                .Should()
                .Be(this._score.Innings.First().TotalRuns);

            this._score.TotalScoreTeamB
                .Should()
                .Be(this._score.Innings.Last().TotalRuns);

            this._score.Innings.Count
                .Should()
                .Be(2);
        }
    }
}
