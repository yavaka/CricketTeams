namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Players;
    using FakeItEasy;
    using FluentAssertions;
    using System;
    using System.Linq;
    using Xunit;

    public class OverSpecs
    {
        private Over _over;
        private Player _bowler;
        private Player _striker;
        private Player _nonStriker;

        public OverSpecs()
        {
            this._bowler = PlayerFakes.Data.GetPlayer(1);
            this._striker = PlayerFakes.Data.GetPlayer(2);
            this._nonStriker = PlayerFakes.Data.GetPlayer(3);

            this._over = OverFakes.Data.GetOver(
                this._bowler,
                this._striker,
                this._nonStriker);
        }

        [Fact]
        public void UpdateBallShouldSetBallAndIncreaseTotalRuns()
        {
            //Arrange
            var ball = BallFakes.Data.GetBallWithRuns(this._bowler, this._striker, this._nonStriker);

            //Act
            this._over.UpdateCurrentBallWithRuns(ball.Runs);

            //Assert
            this._over.CurrentBall
                .Should()
                .Be(ball);

            this._over.TotalRuns
                .Should()
                .Be(ball.Runs);
        }

        [Fact]
        public void UpdateBallWithOddRunsShouldSwapBatsmenPositions()
        {
            //Act
            this._over.UpdateCurrentBallWithRuns(3);

            //Assert
            this._over.Striker
                .Should()
                .BeEquivalentTo(this._nonStriker);

            this._over.NonStriker
                .Should()
                .BeEquivalentTo(this._striker);
        }

        [Fact]
        public void UpdateCurrentBallWithSixShouldAddSixRunsToTotalRuns()
        {
            //Act
            this._over.UpdateCurrentBallWithSix();

            //Assert
            this._over.TotalRuns.Should().Be(6);
        }

        [Fact]
        public void UpdateCurrentBallWithFourShouldAddFourRunsToTotalRuns()
        {
            //Act
            this._over.UpdateCurrentBallWithFour();

            //Assert
            this._over.TotalRuns.Should().Be(4);
        }

        [Fact]
        public void EndOverShouldEndOverAndHaveSixBalls()
        {
            //Arrange
            var balls = BallFakes.Data.GetBalls();

            //Act
            foreach (var ball in balls)
            {
                this._over.UpdateCurrentBallWithRuns(ball.Runs);
            }

            //Assert
            this._over.Balls.Count()
                .Should()
                .Be(6);

            this._over.IsOverEnd
                .Should()
                .BeTrue();
        }

        [Fact]
        public void UpdateCurrentBallWithNoBallShouldSetNoBall()
        {
            //Arrange
            var ball = BallFakes.Data.GetBallWithNoBall(this._bowler, this._striker, this._nonStriker);

            //Act
            this._over.UpdateCurrentBallWithNoBall(ball.Runs);

            //Assert
            this._over.CurrentBall!.NoBall.Should().BeTrue();
        }

        [Fact]
        public void UpdateCurrentBallWithWideBallShouldIncreaseExtraBallsAndTotalRuns()
        {
            //Arrange
            var ball = BallFakes.Data
                .GetBallWithWideBall(this._bowler, this._striker, this._nonStriker);

            //Act
            this._over.UpdateCurrentBallWithWideBall(ball.Runs);

            //Assert
            this._over.ExtraBalls.Should().Be(1);
            this._over.TotalRuns.Should().Be(ball.Runs + 1);
        }

        [Fact]
        public void UpdateCurrentBallWithDismissedStrikerShouldUpdateStrikerAndSetBall()
        {
            //Arrange
            var ball = BallFakes.Data
                .GetBallWithStrikerDismiss(this._bowler, this._striker, this._nonStriker);

            var newStrike = A.Dummy<Player>();

            //Act
            this._over.UpdateCurrentBallWithDismissedBatsman(
                true,
                newStrike,
                ball.BowlingTeamPlayer!,
                ball.DismissedBatsman!,
                ball.OutType!);

            var expectedBall = new Ball(
                ball.Bowler,
                newStrike,
                ball.NonStriker,
                ball.BowlingTeamPlayer!,
                ball.DismissedBatsman!,
                ball.OutType!);

            //Assert
            this._over.BatsmenOut
                .Should()
                .Contain(this._striker);

            this._over.Striker
                .Should()
                .BeEquivalentTo(newStrike);

            this._over.CurrentBall
                .Should()
                .BeEquivalentTo(expectedBall);
        }

        [Fact]
        public void UpdateCurrentBallWithDismissedNonStrikerShouldUpdateNonStrikerAndSetBall()
        {
            //Arrange
            var ball = BallFakes.Data
                .GetBallWithNonStrikerDismiss(this._bowler, this._striker, this._nonStriker);

            var newStrike = A.Dummy<Player>();

            //Act
            this._over.UpdateCurrentBallWithDismissedBatsman(
                false,
                newStrike,
                ball.BowlingTeamPlayer!,
                ball.DismissedBatsman!,
                ball.OutType!);

            var expectedBall = new Ball(
                ball.Bowler,
                ball.Striker,
                newStrike,
                ball.BowlingTeamPlayer!,
                ball.DismissedBatsman!,
                ball.OutType!);

            //Assert
            this._over.BatsmenOut
                .Last()
                .Should()
                .BeEquivalentTo(this._nonStriker);

            this._over.NonStriker
                .Should()
                .BeEquivalentTo(newStrike);

            this._over.CurrentBall
                .Should()
                .BeEquivalentTo(expectedBall);
        }

        [Fact]
        public void UpdateBallWithInvalidDismissedStrikerShouldThrowException()
        {
            //Arrange
            var ball = BallFakes.Data
                .GetBallWithStrikerDismiss(this._bowler, this._striker, this._nonStriker);

            //Act
            Action act = ()
                => this._over.UpdateCurrentBallWithDismissedBatsman(
                    true,
                    newBatsman: A.Dummy<Player>(),
                    ball.BowlingTeamPlayer!,
                    dismissedBatsman: A.Dummy<Player>(),
                    ball.OutType!);

            //Assert
            act.Should().Throw<InvalidOverException>();
        }

        [Fact]
        public void UpdateBallWithInvalidDismissedNonStrikerShouldThrowException()
        {
            //Arrange
            var ball = BallFakes.Data
                .GetBallWithStrikerDismiss(this._bowler, this._striker, this._nonStriker);

            //Act
            Action act = ()
                => this._over.UpdateCurrentBallWithDismissedBatsman(
                    false,
                    newBatsman: A.Dummy<Player>(),
                    ball.BowlingTeamPlayer!,
                    dismissedBatsman: A.Dummy<Player>(),
                    ball.OutType!);

            //Assert
            act.Should().Throw<InvalidOverException>();
        }

        [Fact]
        public void CreateBallWithSixAndFourShouldThrowException()
        {
            //Act
            Action act = ()
                => new Ball(this._bowler, this._striker, this._nonStriker, true, true);
            //Assert
            act.Should().Throw<InvalidBallException>();
        }

        [Fact]
        public void CreateBallWithNoBallAndWideBallShouldThrowException()
        {
            //Act
            Action act = ()
                => new Ball(this._bowler, this._striker, this._nonStriker, 0, true, true);
            //Assert
            act.Should().Throw<InvalidBallException>();
        }

        [Fact]
        public void CreateBallWithNegativeRunsShouldThrowException()
        {
            //Act
            Action act = ()
                => new Ball(this._bowler, this._striker, this._nonStriker, -1);
            //Assert
            act.Should().Throw<InvalidBallException>();
        }

        [Fact]
        public void UpdateBastmanWhoIsAlreadyOutShouldThrowException() 
        {
            //Arrange
            var ball = BallFakes.Data.GetBallWithStrikerDismiss(this._bowler,this._striker,this._nonStriker);
            this._over.UpdateCurrentBallWithDismissedBatsman(
                true,
                ball.Striker,
                ball.BowlingTeamPlayer!,
                ball.DismissedBatsman!,
                ball.OutType!);

            //Act
            Action act = ()
                => this._over.UpdateCurrentBallWithDismissedBatsman(
                    true, 
                    ball.Striker, 
                    ball.BowlingTeamPlayer!, 
                    ball.DismissedBatsman!, 
                    ball.OutType!);

            //Assert
            act.Should().Throw<InvalidOverException>();
        }
    }
}
