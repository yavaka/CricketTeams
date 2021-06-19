namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Exceptions;
    using FakeItEasy;
    using FluentAssertions;
    using System;
    using Xunit;

    public class UmpireSpecs
    {
        private Umpire _umpire;

        public UmpireSpecs()
            => this._umpire = A.Dummy<Umpire>();

        [Fact]
        public void UpdateNamesShouldSetFirstAndLastName()
        {
            var fName = "Test";
            var lName = "User";

            //Act
            this._umpire.UpdateNames(fName, lName);

            //Assert
            this._umpire.FirstName
                .Should()
                .Be(fName);

            this._umpire.LastName
                .Should()
                .Be(lName);
        }

        [Fact]
        public void InvalidNamesShouldThrowException()
        {
            //Act
            Action act = ()
                => this._umpire.UpdateNames("i", "First and last name are invalid should throw exception.");

            //Assert
            act.Should().Throw<InvalidUmpireException>();
        }

        [Fact]
        public void UpdateAgeShouldSetAge()
        {
            var age = 20;

            //Act
            this._umpire.UpdateAge(age);

            //Assert
            this._umpire.Age.Should().Be(age);
        }

        [Fact]
        public void InvalidAgeShouldThrowException()
        {
            //Act
            Action act = ()
                => this._umpire.UpdateAge(1000);

            //Assert
            act.Should().Throw<InvalidUmpireException>();
        }

        [Fact]
        public void IncreaseMatchesAsRefereeShouldIncreaseWithOne()
        {
            //Arrange
            var currentValueMain = this._umpire.MatchesAsMainReferee;
            var currentValueSecond = this._umpire.MatchesAsSecondReferee;

            //Act
            this._umpire.IncreaseMatchAsMainReferee();
            this._umpire.IncreaseMatchAsSecondReferee();

            //Assert
            this._umpire.MatchesAsMainReferee.Should().Be(currentValueMain + 1);
            this._umpire.MatchesAsSecondReferee.Should().Be(currentValueSecond + 1);
        }

        [Fact]
        public void InvalidMatchesAsRefereeShouldThrowException()
        {
            //Act
            Action act = ()
                => this._umpire = new Umpire(
                    "Invalid",
                    "Matches",
                    age: 10,
                    matchesAsMainReferee: -10,
                    matchesAsSecondReferee: 0);

            //Assert
            act.Should().Throw<InvalidUmpireException>();
        }
    }
}
