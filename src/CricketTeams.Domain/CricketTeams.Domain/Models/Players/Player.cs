namespace CricketTeams.Domain.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Players;

    using static ModelConstants.Common;

    public class Player : Entity<int>, IAggregateRoot
    {
        private static readonly IEnumerable<BowlingStyle> AllowedBowlingStyles
            = new BowlingStyleData().GetData().Cast<BowlingStyle>();

        private static readonly IEnumerable<FieldingPosition> AllowedFieldingPositions
             = new FieldingPositionData().GetData().Cast<FieldingPosition>();

        internal Player(
            string firstName,
            string lastName,
            int age,
            string nationality,
            string photoUrl,
            BattingStyle battingStyle,
            BowlingStyle bowlingStyle,
            FieldingPosition fieldingPosition,
            History history)
        {
            this.Validate(firstName, lastName, age, nationality, photoUrl);
            this.ValidateBowlingStyle(bowlingStyle);
            this.ValidateFieldingPosition(fieldingPosition);

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Nationality = nationality;
            this.PhotoUrl = photoUrl;
            this.BattingStyle = battingStyle;
            this.BowlingStyle = bowlingStyle;
            this.FieldingPosition = fieldingPosition;
            this.History = history;

            this.Achievements = new List<Achievement>();
            this.Appointments = new List<Appointment>();
        }

        private Player(
            string firstName,
            string lastName,
            int age,
            string nationality,
            string photoUrl)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Nationality = nationality;
            this.PhotoUrl = photoUrl;

            this.BattingStyle = default!;
            this.BowlingStyle = default!;
            this.FieldingPosition = default!;
            this.History = default!;
            this.Achievements = default!;
            this.Appointments = default!;
        }

        #region Properties
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => this.FirstName + this.LastName;
        public int Age { get; private set; }
        public string Nationality { get; private set; }
        public string PhotoUrl { get; private set; }
        public BattingStyle? BattingStyle { get; private set; }
        public BowlingStyle? BowlingStyle { get; private set; }
        public FieldingPosition? FieldingPosition { get; private set; }
        /// <summary>
        /// The history prop should be completed after matches model
        /// </summary>
        public History? History { get; private set; }
        public ICollection<Achievement> Achievements { get; private set; }
        public ICollection<Appointment> Appointments { get; private set; }
        //public IEnumerable<Match> UpcomingMatches { get; private set; }
        #endregion

        #region Add & Update methods
        
        public Player UpdateNames(string firstName, string lastName)
        {
            ValidateNames(firstName, lastName);
            this.FirstName = firstName;
            this.LastName = lastName;

            return this;
        }

        public Player UpdateAge(int age)
        {
            ValidateAge(age);
            this.Age = age;

            return this;
        }

        public Player UpdateNationality(string nationality)
        {
            //ValidateNationality(nationality);
            this.Nationality = nationality;

            return this;
        }

        public Player UpdatePhotoUrl(string photoUrl)
        {
            ValidatePhotoUrl(photoUrl);
            this.PhotoUrl = photoUrl;

            return this;
        }

        public Player UpdateBattingStyle(BattingStyle battingStyle)
        {
            if (this.BattingStyle != battingStyle)
            {
                this.BattingStyle = battingStyle;
            }
            return this;
        }

        public Player UpdateBowlingStyle(BowlingStyle bowlingStyle) 
        {
            ValidateBowlingStyle(bowlingStyle);
            this.BowlingStyle = bowlingStyle;

            return this;
        }

        public Player UpdateFieldingPosition(FieldingPosition fieldingPosition) 
        {
            ValidateFieldingPosition(fieldingPosition);
            this.FieldingPosition = fieldingPosition;

            return this;
        }

        public Player UpdateHistory(History history) 
        {
            this.History = history;

            return this;
        }

        public Player AddAchievement(
            string name,
            string description,
            string imageUrl,
            AchievementType achievementType)
        {
            var achievement = new Achievement(name, description, imageUrl, achievementType);

            this.Achievements.Add(achievement);

            return this;
        }

        public Player BookAppointment(
            string description,
            string service,
            string appointmentWith,
            DateTime startDate,
            DateTime endDate) 
        {
            var appointment = new Appointment(description, service, appointmentWith, startDate, endDate);

            this.Appointments.Add(appointment);

            return this;
        }

        #endregion

        #region Validations
        private void Validate(
            string firstName,
            string lastName,
            int age,
            string nationality,
            string photoUrl)
        {
            this.ValidateNames(firstName, lastName);
            this.ValidateAge(age);
            //this.ValidateNationality(nationality);
            this.ValidatePhotoUrl(photoUrl);
        }

        private void ValidateNames(string firstName, string lastName)
        {
            Guard.ForStringLength<InvalidPlayerException>(
              firstName,
              MinNameLength,
              MaxNameLength,
              nameof(this.FirstName));

            Guard.ForStringLength<InvalidPlayerException>(
              lastName,
              MinNameLength,
              MaxNameLength,
              nameof(this.LastName));
        }

        private void ValidateAge(int age)
        => Guard.AgainstOutOfRange<InvalidPlayerException>(
            age,
            MinAge,
            MaxAge,
            nameof(this.Age));

        //private void ValidateNationality(string nationality)
        //{
        //    // It should check an external db with all countries around the world
        //    throw new NotImplementedException();
        //}

        private void ValidatePhotoUrl(string photoUrl)
           => Guard.ForValidUrl<InvalidPlayerException>(
               photoUrl,
               nameof(this.PhotoUrl));

        private void ValidateBowlingStyle(BowlingStyle bowlingStyle)
        {
            var styleName = bowlingStyle?.StyleName;

            if (AllowedBowlingStyles.Any(s => s.StyleName == styleName))
            {
                return;
            }

            var allowedBowlingStyleNames = string.Join(
                " ,",
                AllowedBowlingStyles.Select(s => s.StyleName));

            throw new InvalidPlayerException($"{styleName} is not valid bowling style. Allowed values are: {allowedBowlingStyleNames}");
        }

        private void ValidateFieldingPosition(FieldingPosition fieldingPosition)
        {
            var positionName = fieldingPosition?.PositionName;

            if (AllowedFieldingPositions.Any(p => p.PositionName == positionName))
            {
                return;
            }

            var allowedPositionNames = string.Join(
                " ,",
                AllowedFieldingPositions.Select(p => p.PositionName));

            throw new InvalidPlayerException($"{positionName} is not valid fielding position. Allowed values are: {allowedPositionNames}");
        }
        #endregion
    }
}
