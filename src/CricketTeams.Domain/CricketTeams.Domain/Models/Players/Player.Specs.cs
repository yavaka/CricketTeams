namespace CricketTeams.Domain.Models.Players
{
    using CricketTeams.Domain.Exceptions;
    using FakeItEasy;
    using FluentAssertions;
    using System;
    using Xunit;

    public class PlayerSpecs
    {
        private Player _player;

        public PlayerSpecs()
        {
            this._player = A.Dummy<Player>();
        }

        [Fact]
        public void UpdateNamesShouldSetNames()
        {
            //Act
            this._player!.UpdateNames("Valid", "Name");

            //Assert
            this._player.FirstName
                .Should()
                .BeEquivalentTo("Valid");
            
            this._player.LastName
                .Should()
                .BeEquivalentTo("Name");
            
            this._player.FullName
                .Should()
                .BeEquivalentTo("Valid Name");
        }

        [Fact]
        public void InvalidFirstNameShouldThrowException() 
        {
            //Act
            Action act = () 
                => this._player!.UpdateNames("i", "Invalid name");

            //Assert
            act.Should().Throw<InvalidPlayerException>();
        }

        [Fact]
        public void UpdateAgeShouldSetAge()
        {
            //Act
            this._player!.UpdateAge(23);

            //Assert
            this._player!.Age
                .Should()
                .Be(23);
        }

        [Fact]
        public void InvalidAgeShouldThrowException()
        {
            //Act
            Action act = ()
                => this._player!.UpdateAge(-10);

            //Assert
            act.Should().Throw<InvalidPlayerException>();
        }

        [Fact]
        public void UpdatePhotoUrlShouldSetUrl()
        {
            //Act
            this._player!.UpdatePhotoUrl("https://valid.url");

            //Assert
            this._player!.PhotoUrl
                .Should()
                .Be("https://valid.url");
        }

        [Fact]
        public void InvalidPhotoUrlShouldThrowException()
        {
            //Act
            Action act = ()
                => this._player!.UpdatePhotoUrl("invalid.url");

            //Assert
            act.Should().Throw<InvalidPlayerException>();
        }

        //TODO: Unit tests of nationality

        [Fact]
        public void UpdateBowlingStyleShouldSetBowlingStyle() 
        {
            //Arrange
            var bowlingStyle = A.Dummy<BowlingStyle>();
            
            //Act
            this._player!.UpdateBowlingStyle(bowlingStyle);

            //Assert
            this._player!.BowlingStyle
                .Should()
                .BeEquivalentTo(bowlingStyle);
        }

        [Fact]
        public void InvalidBowlingStyleNameShouldThrowException() 
        {
            //Arrange
            var bowlingStyle = new BowlingStyle(
                "Invalid bowling", 
                BowlingTypes.FastBowling,
                "Some invalid bowling style");

            //Act
            Action act = ()
               => this._player!.UpdateBowlingStyle(bowlingStyle);

            //Assert
            act.Should().Throw<InvalidPlayerException>();
        }

        [Fact]
        public void UpdateFieldingPositionShouldSetBowlingStyle()
        {
            //Arrange
            var fieldingPosition = A.Dummy<FieldingPosition>();

            //Act
            this._player!.UpdateFieldingPosition(fieldingPosition);

            //Assert
            this._player!.FieldingPosition
                .Should()
                .BeEquivalentTo(fieldingPosition);
        }

        [Fact]
        public void InvalidFieldingPositionNameShouldThrowException()
        {
            //Arrange
            var fieldingPosition = new FieldingPosition(
                "Invalid position",
                "Some invalid fieliding position");

            //Act
            Action act = ()
               => this._player!.UpdateFieldingPosition(fieldingPosition);

            //Assert
            act.Should().Throw<InvalidPlayerException>();
        }

        [Fact]
        public void AddAchievementShouldAddAchievementAtAchievements() 
        {
            //Arrange
            var achievement = A.Dummy<Achievement>();

            //Act
            this._player!.AddAchievement(
                achievement.Name, 
                achievement.Description, 
                achievement.ImageUrl, 
                achievement.AchievementType);

            //Assert
            this._player!.Achievements
                .Should()
                .Contain(achievement);
        }

        [Fact]
        public void AddAchievementWithInvalidNameShouldThrowException() 
        {
            //Act
            Action act = () 
                => this._player!.AddAchievement(
                    "I",
                    "Invalid name test",
                    "http://invalid.name",
                    AchievementTypes.Medal);

            //Assert
            act.Should().Throw<InvalidAchievementException>();
        }

        [Fact]
        public void AddAchievementWithInvalidDescriptionShouldThrowException() 
        {
            //Act
            Action act = ()
                => this._player!.AddAchievement(
                    "Invalid description",
                    "Invalid",
                    "http://invalid.description",
                    AchievementTypes.Medal);

            //Assert
            act.Should().Throw<InvalidAchievementException>();
        }

        [Fact]
        public void AddAchievementWithInvalidImageUrlShouldThrowException()
        {
            //Act
            Action act = ()
                => this._player!.AddAchievement(
                    "Invalid image url",
                    "Invalid image url should throw exception",
                    "invalid.url",
                    AchievementTypes.Medal);

            //Assert
            act.Should().Throw<InvalidAchievementException>();
        }
    }
}
