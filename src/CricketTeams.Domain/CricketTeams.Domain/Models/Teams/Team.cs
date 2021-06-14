namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Coaches;
    using CricketTeams.Domain.Models.Players;
    using CricketTeams.Domain.Models.Stadiums;
    using System.Collections.Generic;

    using static ModelConstants.Team;

    public class Team : Entity<int>, IAggregateRoot
    {
        public Team(
            string name,
            string logoUrl,
            Players players,
            Stadium stadium,
            Coach coach,
            History history)
        {
            Validate(name, logoUrl);

            this.Name = name;
            this.LogoUrl = logoUrl;
            this.Players = players;
            this.Stadium = stadium;
            this.Coach = coach;
            this.History = history;

            this.Sponsors = new List<Sponsor>();
            this.Achievements = new List<Achievement>();
        }

        private Team(string name, string logoUrl)
        {
            this.Name = name;
            this.LogoUrl = logoUrl;

            this.Players = default!;
            this.Coach = default!;
            this.Stadium = default!;
            this.History = default!;
            this.Sponsors = default!;
            this.Achievements = default!;
        }

        #region Properties

        public string Name { get; private set; }
        public string LogoUrl { get; private set; }
        public Players Players { get; private set; }
        public Coach Coach { get; private set; }
        public Stadium? Stadium { get; private set; }
        public History? History { get; private set; }
        public ICollection<Sponsor> Sponsors { get; private set; }
        public ICollection<Achievement> Achievements { get; private set; }

        #endregion

        #region Add & Update methods

        public Team UpdateName(string name)
        {
            if (this.Name != name)
            {
                ValidateName(name);
                this.Name = name;
            }
            return this;
        }

        public Team UpdateLogoUrl(string logoUrl)
        {
            if (this.LogoUrl != logoUrl)
            {
                ValidateLogoUrl(logoUrl);
                this.LogoUrl = logoUrl;
            }
            return this;
        }

        public Team UpdatePlayers(Players players)
        {
            this.Players = players;

            return this;
        }

        public Team UpdateCoach(Coach coach)
        {
            this.Coach = coach;

            return this;
        }

        public Team UpdateHistory(History history)
        {
            this.History = history;

            return this;
        }

        public Team AddBatsman(Player player)
        {
            this.Players.AddBatsman(player);

            return this;
        }

        public Team AddAllRounder(Player allRounder)
        {
            this.Players.AddAllRounder(allRounder);

            return this;
        }

        public Team AddBowler(Player bowler)
        {
            this.Players.AddBowler(bowler);

            return this;
        }

        public Team UpdateCaptain(Player captain)
        {
            this.Players.UpdateCaptain(captain);

            return this;
        }

        public Team UpdateWicketKeeper(Player wicketKeeper)
        {
            this.Players.UpdateWicketKeeper(wicketKeeper);

            return this;
        }

        public Team UpdateTwelfth(Player twelfth)
        {
            this.Players.UpdateTwelfth(twelfth);

            return this;
        }

        public Team UpdateStadium(Stadium stadium)
        {
            this.Stadium = stadium;

            return this;
        }

        public Team AddSponsor(
            string name,
            string websiteUrl,
            SponsorTypes sponsorType)
        {
            var sponsor = new Sponsor(name, websiteUrl, sponsorType);

            this.Sponsors.Add(sponsor);

            return this;
        }

        public Team AddAchievement(
           string name,
           string description,
           string imageUrl,
           AchievementTypes achievementType)
        {
            var achievement = new Achievement(name, description, imageUrl, achievementType);

            this.Achievements.Add(achievement);

            return this;
        }

        #endregion

        #region Validations

        private void Validate(string name, string logoUrl)
        {
            ValidateName(name);
            ValidateLogoUrl(logoUrl);
        }

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidTeamException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

        private void ValidateLogoUrl(string logoUrl)
            => Guard.ForValidUrl<InvalidTeamException>(
                logoUrl,
                nameof(this.LogoUrl));

        #endregion
    }
}
