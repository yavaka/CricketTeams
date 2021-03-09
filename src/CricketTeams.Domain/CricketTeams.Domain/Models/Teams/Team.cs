namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using System.Collections.Generic;

    using static ModelConstants.Team;

    public class Team : Entity<int>, IAggregateRoot
    {
        internal Team(
            string name,
            string logoUrl,
            Player captain,
            Players players,
            Stadium stadium,
            Coach coach,
            History history,
            Therapist therapist,
            GymTrainer gymTrainer)
        {
            Validate(name, logoUrl);

            this.Name = name;
            this.LogoUrl = logoUrl;
            this.Captain = captain;
            this.Players = players;
            this.Stadium = stadium;
            this.Coach = coach;
            this.History = history;
            this.Therapist = therapist;
            this.GymTrainer = gymTrainer;

            this.Achievements = new List<Achievement>();
        }

        private Team(
            string name,
            string logoUrl)
        {
            this.Name = name;
            this.LogoUrl = logoUrl;

            this.Captain = default!;
            this.Players = default!;
            this.Stadium = default!;
            this.Coach = default!;
            this.History = default!;
            this.Therapist = default!;
            this.GymTrainer = default!;
            this.Achievements = default!;
        }

        #region Properties
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public Player? Captain { get; set; }
        public Players? Players { get; set; }
        public Stadium? Stadium { get; set; }
        public Coach? Coach { get; set; }
        public History? History { get; set; }
        public Therapist? Therapist { get; set; }
        public GymTrainer? GymTrainer { get; set; }
        public ICollection<Achievement> Achievements { get; set; }
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

        public Team UpdateCaptain(Player captain)
        {
            this.Captain = captain;

            return this;
        }

        public Team UpdatePlayers(Players players)
        {
            this.Players = players;

            return this;
        }

        public Team UpdateStadium(Stadium stadium)
        {
            this.Stadium = stadium;

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

        public Team UpdateTherapist(Therapist therapist)
        {
            this.Therapist = therapist;

            return this;
        }

        public Team UpdateGymTrainer(GymTrainer gymTrainer)
        {
            this.GymTrainer = gymTrainer;

            return this;
        }

        //public Team AddAchievement()
        //{
            
        //}

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
                MinNameLenght,
                MaxNameLenght,
                nameof(this.Name));

        private void ValidateLogoUrl(string logoUrl)
            => Guard.ForValidUrl<InvalidTeamException>(
                logoUrl,
                nameof(this.LogoUrl));

        #endregion
    }
}
