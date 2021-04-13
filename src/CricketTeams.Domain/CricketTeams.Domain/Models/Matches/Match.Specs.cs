namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Stadiums;
    using FakeItEasy;
    using FluentAssertions;
    using System;
    using Xunit;

    public class MatchSpecs
    {
        private Match _match;

        public MatchSpecs() => this._match = A.Dummy<Match>();

        [Fact]
        public void StartMatchShouldSetInProgressAndStatistic()
        {
            //Act
            this._match.StartMatch(1, TossDecisions.Batting);

            //Assert
            this._match.InProgress
                .Should()
                .BeTrue();

            this._match.Statistic!.TossWinnerTeamId
                .Should()
                .Be(1);

            this._match.Statistic!.TossDecision
                .Should()
                .Be(TossDecisions.Batting);
        }

        [Fact]
        public void StartMatchTwiceShouldThrowException()
        {
            //Arrange
            this._match.StartMatch(1, TossDecisions.Batting);

            //Act
            Action act = ()
                => this._match.StartMatch(1, TossDecisions.Batting);

            //Assert
            act.Should().Throw<InvalidMatchException>();
        }

        [Fact]
        public void InvalidTossWinnerIdShouldThrowException() 
        {
            //Act
            Action act = ()
               => this._match.StartMatch(10, TossDecisions.Batting);

            //Assert
            act.Should().Throw<InvalidMatchException>();
        }

        [Fact]
        public void UpdateFirstAndSecondUmpiresShouldSetUmpires() 
        {
            //Arrange
            var firstUmpire = UmpireFakes.Data.GetUmpire(3);
            var secondUmpire = UmpireFakes.Data.GetUmpire(4);

            //Act
            this._match.UpdateFirstUmpire(firstUmpire);
            this._match.UpdateSecondUmpire(secondUmpire);

            //Assert
            this._match.FirstUmpire!.Id
                .Should()
                .Be(3);
            
            this._match.SecondUmpire!.Id
                .Should()
                .Be(4);
        }

        [Fact]
        public void UpdateFirstUmpireWithSecondUmpireShouldThrowException() 
        {
            //Act
            Action act = ()
               => this._match.UpdateFirstUmpire(this._match.SecondUmpire!);

            //Assert
            act.Should().Throw<InvalidMatchException>();
        }

        [Fact]
        public void UpdateStadiumShouldSetStadium() 
        {
            //Arrange
            var stadium = A.Dummy<Stadium>();

            //Act
            this._match.UpdateStadium(stadium);

            //Assert
            this._match.Stadium.Should().Be(stadium);
        }

        [Fact]
        public void UpdateStadiumWhileMatchInProgressShouldThrowException() 
        {
            //Arrange
            var stadium = A.Dummy<Stadium>();

            this._match.StartMatch(1, TossDecisions.Batting);

            //Act
            Action act = ()
               => this._match.UpdateStadium(stadium);

            //Assert
            act.Should().Throw<InvalidMatchException>();
        }
    }
}
