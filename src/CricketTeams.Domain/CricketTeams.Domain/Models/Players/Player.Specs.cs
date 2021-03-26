namespace CricketTeams.Domain.Models.Players
{
    using CricketTeams.Domain.Exceptions;
    using FakeItEasy;
    using FluentAssertions;
    using System;
    using Xunit;

    using static CricketTeams.Domain.Models.Players.PlayerFakes;
    using static ModelConstants.Common;

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
    }
}
