﻿namespace CricketTeams.Domain.Models.Players
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using System.Collections.Generic;
    using System.Linq;
    using static ModelConstants.Common;

    public class Player : Entity<int>, IAggregateRoot
    {
        private static readonly IEnumerable<BowlingStyle> AllowedBowlingStyles
            = new BowlingStyleData().GetData().Cast<BowlingStyle>();

        private static readonly IEnumerable<FieldingPosition> AllowedFieldingPositions
             = new FieldingPositionData().GetData().Cast<FieldingPosition>();

        public Player(
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
        }

        #region Properties
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => $"{this.FirstName} {this.LastName}";
        public int Age { get; private set; }
        public string Nationality { get; private set; }
        public string PhotoUrl { get; private set; }
        public History History { get; private set; }
        public BattingStyle? BattingStyle { get; private set; }
        public BowlingStyle? BowlingStyle { get; private set; }
        public FieldingPosition? FieldingPosition { get; private set; }

        #endregion

        #region Add & Update methods

        public Player UpdateNames(string firstName, string lastName)
        {
            if (this.FirstName != firstName)
            {
                ValidateName(firstName, nameof(this.FirstName));
                this.FirstName = firstName;
            }

            if (this.LastName != lastName)
            {
                ValidateName(lastName, nameof(this.LastName));
                this.LastName = lastName;
            }
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

        #endregion

        #region Validations
        private void Validate(
            string firstName,
            string lastName,
            int age,
            string nationality,
            string photoUrl)
        {
            ValidateName(firstName, nameof(this.FirstName));
            ValidateName(lastName, nameof(this.LastName));
            ValidateAge(age);
            //this.ValidateNationality(nationality);
            ValidatePhotoUrl(photoUrl);
        }

        private void ValidateName(string name, string propName)
            => Guard.ForStringLength<InvalidPlayerException>(
                name,
                MinNameLength,
                MaxNameLength,
                propName);

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
