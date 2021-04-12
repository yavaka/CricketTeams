namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Players;
    using CricketTeams.Domain.Models.Teams;
    using FakeItEasy;
    using FluentAssertions;
    using System;
    using System.Linq;
    using Xunit;

    public class InningSpecs
    {
        private Inning _inning;
        private Team _teamA;
        private Team _teamB;
        private Player _bowler;
        private Player _striker;
        private Player _nonStriker;

        public InningSpecs()
        {
            this._teamA = A.Dummy<Team>();
            this._teamB = A.Dummy<Team>();

            this._inning = InningFakes.Data.GetInning(this._teamA, this._teamB);

            this._striker = this._teamA.Players.Batsmen.First();
            this._nonStriker = this._teamA.Players.Batsmen.Last();
            this._bowler = this._teamB.Players.Bowlers.Last();
        }

        [Fact]
        public void UpdateCurrentOverShouldSetOver()
        {
            //Act
            this._inning.UpdateCurrentOver(this._bowler, this._striker, this._nonStriker);

            //Assert
            this._inning.CurrentOver!.Bowler
                .Should()
                .Be(this._bowler);

            this._inning.CurrentOver!.Striker
                .Should()
                .Be(this._striker);

            this._inning.CurrentOver!.NonStriker
                .Should()
                .Be(this._nonStriker);
        }

        [Fact]
        public void UpdateCurrentOverTwiceShouldStoreTheFirstOverIntoOvers()
        {
            //Arrange
            this._inning.UpdateCurrentOver(this._bowler, this._striker, this._nonStriker);
            var expectedOver = new Over(this._bowler, this._striker, this._nonStriker);

            for (int i = 0; i < 6; i++)
            {
                this._inning.CurrentOver!.UpdateCurrentBallWithRuns(i);
                expectedOver.UpdateCurrentBallWithRuns(i);
            }

            //Act
            var newBowler = this._teamB.Players.Bowlers.First();

            this._inning.UpdateCurrentOver(newBowler, this._striker, this._nonStriker);

            //Assert
            var lastOver = this._inning.Overs.Last();

            this._inning.TotalRuns
                .Should()
                .Be(expectedOver.TotalRuns);

            lastOver.Striker
                .Should()
                .Be(expectedOver.Striker);

            lastOver.NonStriker
                .Should()
                .Be(expectedOver.NonStriker);

            lastOver.Bowler.Should()
                .Be(expectedOver.Bowler);

            lastOver.IsOverEnd
                .Should()
                .Be(expectedOver.IsOverEnd);
        }

        [Fact]
        public void UpdateCurrentOverWithNonExistingBowlerShouldThrowException()
        {
            //Act
            Action act = ()
                => this._inning.UpdateCurrentOver(
                    PlayerFakes.Data.GetPlayer(50),
                    this._striker,
                    this._nonStriker);

            //Assert
            act.Should().Throw<InvalidInningException>();
        }

        [Fact]
        public void SameBowlerTwoOversInRowShouldThrowException()
        {
            //Arrange
            this._inning.UpdateCurrentOver(this._bowler, this._striker, this._nonStriker);

            for (int i = 0; i < 6; i++)
            {
                this._inning.CurrentOver!.UpdateCurrentBallWithRuns(i);
            }

            //Act
            Action act = ()
                => this._inning.UpdateCurrentOver(this._bowler, this._striker, this._nonStriker);

            //Assert
            act.Should().Throw<InvalidInningException>();
        }

        [Fact]
        public void UpdateCurrentOverWithNonExistingBatsmanShouldThrowException()
        {
            //Act
            Action act = ()
                   => this._inning.UpdateCurrentOver(
                       this._bowler,
                       PlayerFakes.Data.GetPlayer(50),
                       this._nonStriker);

            //Assert
            act.Should().Throw<InvalidInningException>();
        }

        [Fact]
        public void UpdateBallWhenOverNotSetShouldThrowException() 
        {
            //Act
            Action act = ()
                => this._inning.UpdateCurrentBallWithRuns(7);

            //Assert
            act.Should().Throw<InvalidInningException>();
        }

        [Fact]
        public void EndingCurrentOverShouldAddDismissedBatsmen() 
        {
            //Arrange                                         id = 1
            this._inning.UpdateCurrentOver(this._bowler, this._striker, this._nonStriker);

            for (int i = 2; i <= 7; i++)
            {
                var newBatsmen = this._inning.BattingTeam.Players.Batsmen
                    .First(id => id.Id == i);

                this._inning.UpdateCurrentBallWithDismissedBatsman(
                    true,
                    newBatsmen,
                    this._bowler,
                    this._inning.BattingTeam.Players.Batsmen.FirstOrDefault(id =>id.Id == i - 1)!,
                    PlayerOutTypes.Wicket);
            }
            
            //Act
            this._inning.UpdateCurrentOverWithBatsman(
                this._inning.BowlingTeam.Players.Bowlers.First(), 
                this._inning.BattingTeam.Players.Batsmen.First(i =>i.Id == 8));

            //Assert
            this._inning.TotalBatsmenOut.Count
                .Should()
                .Be(6);
        }

        [Fact]
        public void InvalidOversPerInningShouldThrowException() 
        {
            //Act 
            Action act = ()
                => new Inning(this._teamA, this._teamB, 5);

            //Assert
            act.Should().Throw<InvalidInningException>();
        }

        [Fact]
        public void UpdateCurrentOverBeforeOverEndShouldThrowException() 
        {
            //Arrange
            this._inning.UpdateCurrentOver(this._bowler, this._striker, this._nonStriker);

            //Act
            Action act = ()
                => this._inning.UpdateCurrentOver(this._bowler, this._striker, this._nonStriker);

            //Assert
            act.Should().Throw<InvalidInningException>();
        }

        [Fact]
        public void UpdateCurrentOverWithBatsmanWhoIsOutShouldThrowException() 
        {
            //Arrange                                         id = 1
            this._inning.UpdateCurrentOver(this._bowler, this._striker, this._nonStriker);

            for (int i = 2; i <= 7; i++)
            {
                var newBatsmen = this._inning.BattingTeam.Players.Batsmen
                    .First(id => id.Id == i);

                this._inning.UpdateCurrentBallWithDismissedBatsman(
                    true,
                    newBatsmen,
                    this._bowler,
                    this._inning.BattingTeam.Players.Batsmen.FirstOrDefault(id => id.Id == i - 1)!,
                    PlayerOutTypes.Wicket);
            }

            //Act
            Action act = ()
                => this._inning.UpdateCurrentOverWithBatsman(
                    this._inning.BowlingTeam.Players.Bowlers.First(),
                    this._inning.BattingTeam.Players.Batsmen.First());

            //Assert
            act.Should().Throw<InvalidInningException>();
        }

        [Fact]
        public void EndInningWhenAllBatsmenDismissedShouldEndInning() 
        {
            //Arrange                                         id = 1
            this._inning.UpdateCurrentOver(this._bowler, this._striker, this._nonStriker);

            for (int i = 2; i <= 7; i++)
            {
                var newBatsmen = this._inning.BattingTeam.Players.Batsmen
                    .First(id => id.Id == i);

                this._inning.UpdateCurrentBallWithDismissedBatsman(
                    true,
                    newBatsmen,
                    this._bowler,
                    this._inning.BattingTeam.Players.Batsmen.FirstOrDefault(id => id.Id == i - 1)!,
                    PlayerOutTypes.Wicket);
            }
            //Over ended with 6 dismissed batsmen

            this._inning.UpdateCurrentOverWithBatsman(
                this._inning.BowlingTeam.Players.Bowlers.First(),
                this._inning.BattingTeam.Players.Batsmen.First(i => i.Id == 8));

            for (int i = 8; i <= 11; i++)
            {
                var newBatsmen = this._inning.BattingTeam.Players.Batsmen
                    .First(id => id.Id == i);

                this._inning.UpdateCurrentBallWithDismissedBatsman(
                    true,
                    newBatsmen,
                    this._bowler,
                    this._inning.BattingTeam.Players.Batsmen.FirstOrDefault(id => id.Id == i - 1)!,
                    PlayerOutTypes.Wicket);
            }

            //Assert
            this._inning.IsInningEnd.Should().BeTrue();
        }

        //[Fact]
        //public void UpdateBallWith()
        //{
        //    //Arrange

        //    //Act

        //    //Assert
        //}

        //[Fact]
        //public void TestNameException()
        //{
        //    //Act

        //    //Assert
        //}

        //[Fact]
        //public void TestName() 
        //{
        //    //Arrange

        //    //Act

        //    //Assert
        //}
    }
}
