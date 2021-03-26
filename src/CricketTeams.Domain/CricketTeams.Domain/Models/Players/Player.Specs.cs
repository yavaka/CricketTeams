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
            act.Should()
                .Throw<InvalidPlayerException>()
                .WithMessage($"{nameof(this._player.FirstName)} must have between {MinNameLength} and {MaxNameLength} symbols.");
        }
    }
}
