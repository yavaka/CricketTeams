namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Coaches;
    using CricketTeams.Domain.Models.Players;
    using CricketTeams.Domain.Models.Stadiums;
    using FakeItEasy;
    using FluentAssertions;
    using System;
    using Xunit;

    public class TeamSpecs
    {
        private Team _team;

        public TeamSpecs()
        {
            this._team = A.Dummy<Team>();
        }

        [Fact]
        public void UpdateNameShouldSetName()
        {
            //Act
            this._team.UpdateName("valid name");

            //Assert
            this._team.Name.Should().Be("valid name");
        }

        [Fact]
        public void InvalidNameShouldThrowException()
        {
            //Act
            Action act = ()
                => this._team.UpdateName("invalid name that is more than allowed length");

            //Assert
            act.Should().Throw<InvalidTeamException>();
        }

        [Fact]
        public void UpdateLogoUrlShouldSetLogoUrl()
        {
            //Act
            this._team.UpdateLogoUrl("http://valid.url");

            //Assert
            this._team.LogoUrl.Should().Be("http://valid.url");
        }

        [Fact]
        public void InvalidLogoUrlShouldThrowException()
        {
            //Act
            Action act = () => this._team.UpdateLogoUrl("invalid.url");

            //Assert
            act.Should().Throw<InvalidTeamException>();
        }

        [Fact]
        public void UpdatePlayersShouldSetPlayers()
        {
            //Arrange
            var players = A.Dummy<Players>();

            //Act
            this._team.UpdatePlayers(players);

            //Assert
            this._team.Players.Should().Be(players);
        }

        [Fact]
        public void AddBatsmanShouldAddBatsman()
        {
            //Arrange
            var batsman = A.Dummy<Player>();

            //Act
            this._team.AddBatsman(batsman);

            //Assert
            this._team.Players.Batsmen.Should().Contain(batsman);
        }

        [Fact]
        public void AddBatsmanTwiceShouldThrowException()
        {
            //Arrange
            var batsman = A.Dummy<Player>();

            //Act
            this._team.AddBatsman(batsman);

            Action act = ()
                => this._team.AddBatsman(batsman);

            //Assert
            act.Should().Throw<InvalidTeamPlayersException>();
        }

        [Fact]
        public void AddBowlerShouldAddBowler()
        {
            //Arrange
            var bowler = A.Dummy<Player>();

            //Act
            this._team.AddBowler(bowler);

            //Assert
            this._team.Players.Bowlers.Should().Contain(bowler);
        }

        [Fact]
        public void AddBowlerTwiceShouldThrowException()
        {
            //Arrange
            var bowler = A.Dummy<Player>();

            //Act
            this._team.AddBowler(bowler);

            Action act = ()
                => this._team.AddBowler(bowler);

            //Assert
            act.Should().Throw<InvalidTeamPlayersException>();
        }

        [Fact]
        public void AddAllRounderShouldAddAllRounder()
        {
            //Arrange
            var allRounder = A.Dummy<Player>();

            //Act
            this._team.AddAllRounder(allRounder);

            //Assert
            this._team.Players.AllRounders.Should().Contain(allRounder);
        }

        [Fact]
        public void AddAllRounderTwiceShouldThrowException()
        {
            //Arrange
            var allRounder = A.Dummy<Player>();

            //Act
            this._team.AddAllRounder(allRounder);

            Action act = ()
                => this._team.AddAllRounder(allRounder);

            //Assert
            act.Should().Throw<InvalidTeamPlayersException>();
        }

        [Fact]
        public void AddCaptainShouldAddCaptain()
        {
            //Arrange
            var captain = A.Dummy<Player>();

            //Act
            this._team.UpdateCaptain(captain);

            //Assert
            this._team.Players.Captain.Should().Be(captain);
        }

        [Fact]
        public void AddWicketkeeperShouldAddWicketkeeper()
        {
            //Arrange
            var wicketkeeper = A.Dummy<Player>();

            //Act
            this._team.UpdateWicketKeeper(wicketkeeper);

            //Assert
            this._team.Players.WicketKeeper.Should().Be(wicketkeeper);
        }

        [Fact]
        public void AddTwelftShouldAddTwelft()
        {
            //Arrange
            var twelftMan = A.Dummy<Player>();

            //Act
            this._team.UpdateTwelfth(twelftMan);

            //Assert
            this._team.Players.Twelfth.Should().Be(twelftMan);
        }

        [Fact]
        public void UpdateStadiumShouldSetStadium()
        {
            //Arrange
            var stadium = A.Dummy<Stadium>();

            //Act
            this._team.UpdateStadium(stadium);

            //Assert
            this._team.Stadium.Should().Be(stadium);
        }

        [Fact]
        public void UpdateCoachShouldSetCoach()
        {
            //Arrange
            var coach = A.Dummy<Coach>();

            //Act
            this._team.UpdateCoach(coach);

            //Assert
            this._team.Coach.Should().Be(coach);
        }

        [Fact]
        public void UpdateHistoryShouldSetHistory()
        {
            //Arrange
            var history = new History(1, 0);

            //Act
            this._team.UpdateHistory(history);

            //Assert
            this._team.History.Should().Be(history);
        }

        [Fact]
        public void AddSponsorShouldAddSponsor()
        {
            //Arrange
            var sponsor = A.Dummy<Sponsor>();

            //Act
            this._team.AddSponsor(
                sponsor.Name,
                sponsor.WebsiteUrl,
                sponsor.SponsorType);

            //Assert
            this._team.Sponsors.Should().Contain(sponsor);
        }

        [Fact]
        public void AddSponsorWithInvalidNameShouldThrowException()
        {
            //Act
            Action act = ()
                => this._team.AddSponsor(
                    "i",
                    "http://invalid.name",
                    SponsorTypes.Bronze);

            //Assert
            act.Should().Throw<InvalidSponsorException>();
        }

        [Fact]
        public void AddSponsorWithInvalidWebsiteUrlShouldThrowException()
        {
            //Act
            Action act = ()
                => this._team.AddSponsor(
                    "invalid website url",
                    "invalid.url",
                    SponsorTypes.Bronze);

            //Assert
            act.Should().Throw<InvalidSponsorException>();
        }

        [Fact]
        public void AddAchievementShouldAddAchievement()
        {
            //Arrange
            var achievement = A.Dummy<Achievement>();

            //Act
            this._team.Achievements.Add(achievement);

            //Assert
            this._team.Achievements.Should().Contain(achievement);
        }

        [Fact]
        public void AddAchievementWithInvalidNameShouldThrowException()
        {
            //Act
            Action act = ()
                => this._team.AddAchievement(
                    "invalid name should throw exception",
                    "invalid name",
                    "http://invalid.name",
                    AchievementTypes.Trophy);

            //Assert
            act.Should().Throw<InvalidAchievementException>();
        }

        [Fact]
        public void AddAchievementWithInvalidDescriptionShouldThrowException()
        {
            //Act
            Action act = ()
                => this._team.AddAchievement(
                    "invalid description",
                    "inv",
                    "http://invalid.description",
                    AchievementTypes.Trophy);

            //Assert
            act.Should().Throw<InvalidAchievementException>();
        }

        [Fact]
        public void AddAchievementWithInvalidImageUrlShouldThrowException()
        {
            //Act
            Action act = ()
                => this._team.AddAchievement(
                    "invalid image url",
                    "invalid image url should throw exception",
                    "invalid.url",
                    AchievementTypes.Trophy);

            //Assert
            act.Should().Throw<InvalidAchievementException>();
        }
    }
}