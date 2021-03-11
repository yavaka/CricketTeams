namespace CricketTeams.Domain.Models.Coaches
{
    using System.Linq;
    using System.Collections.Generic;
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using CricketTeams.Domain.Models.Players;

    using static ModelConstants.Common;

    public class Coach : Entity<int>
    {
        private static readonly IEnumerable<BowlingStyle> AllowedBowlingStyles
            = new BowlingStyleData().GetData().Cast<BowlingStyle>();

        internal Coach(
            string firstName,
            string lastName,
            string nickname,
            int age,
            string nationality,
            string photoUrl,
            BattingStyle battingStyle,
            BowlingStyle bowlingStyle,
            History matchesAsCoach)
        {
            this.Validate(firstName, lastName, nickname, age, photoUrl);
            this.ValidateBowlingStyle(bowlingStyle);

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Nickname = nickname;
            this.Age = age;
            this.Nationality = nationality;
            this.PhotoUrl = photoUrl;
            this.BattingStyle = battingStyle;
            this.BowlingStyle = bowlingStyle;
            this.MatchesAsCoach = matchesAsCoach;

            this.Achievements = new List<Achievement>();
        }

        private Coach(
            string firstName,
            string lastName,
            string nickname,
            int age,
            string nationality,
            string photoUrl)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Nickname = nickname;
            this.Age = age;
            this.Nationality = nationality;
            this.PhotoUrl = photoUrl;

            this.BattingStyle = default!;
            this.BowlingStyle = default!;
            this.MatchesAsCoach = default!;
            this.Achievements = default!;
        }

        #region Properties

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => this.FirstName + this.LastName;
        public string Nickname { get; private set; }
        public int Age { get; private set; }
        public string Nationality { get; private set; }
        public string PhotoUrl { get; private set; }
        public BattingStyle? BattingStyle { get; private set; }
        public BowlingStyle? BowlingStyle { get; private set; }
        public History? MatchesAsCoach { get; private set; }
        public ICollection<Achievement> Achievements { get; private set; }

        #endregion

        #region Update & Add methods

        public Coach UpdateNames(string firstName, string lastName)
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

        public Coach UpdateNickname(string nickname)
        {
            if (this.Nickname != nickname)
            {
                ValidateName(nickname, nameof(this.Nickname));
                this.Nickname = nickname;
            }
            return this;
        }

        public Coach UpdateAge(int age)
        {
            if (this.Age != age)
            {
                ValidateAge(age);
                this.Age = age;
            }
            return this;
        }

        public Coach UpdateNationality(string nationality)
        {
            if (this.Nationality != nationality)
            {
                this.Nationality = nationality;
            }
            return this;
        }

        public Coach UpdatePhotoUrl(string photoUrl)
        {
            if (this.PhotoUrl != photoUrl)
            {
                ValidatePhotoUrl(photoUrl);
                this.PhotoUrl = photoUrl;
            }
            return this;
        }

        public Coach UpdateBattingStyle(BattingStyle battingStyle)
        {
            if (this.BattingStyle != battingStyle)
            {
                this.BattingStyle = battingStyle;
            }
            return this;
        }

        public Coach UpdateBowlingStyle(BowlingStyle bowlingStyle)
        {
            ValidateBowlingStyle(bowlingStyle);

            this.BowlingStyle = bowlingStyle;

            return this;
        }

        public Coach UpdateMatchesAsCoach(History matchesAsCoach)
        {
            this.MatchesAsCoach = matchesAsCoach;

            return this;
        }

        public Coach AddAchievement(
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

        private void Validate(string firstName, string lastName, string nickname, int age, string photoUrl)
        {
            this.ValidateName(firstName, nameof(this.FirstName));
            this.ValidateName(lastName, nameof(this.LastName));
            this.ValidateName(nickname, nameof(this.Nickname));
            this.ValidateAge(age);
            this.ValidatePhotoUrl(photoUrl);
        }

        private void ValidateName(string name, string propName)
            => Guard.ForStringLength<InvalidCoachException>(
              name,
              MinNameLength,
              MaxNameLength,
              propName);

        private void ValidateAge(int age)
            => Guard.AgainstOutOfRange<InvalidCoachException>(
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
           => Guard.ForValidUrl<InvalidCoachException>(
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

            throw new InvalidCoachException($"{styleName} is not valid bowling style. Allowed values are: {allowedBowlingStyleNames}");
        }

        #endregion
    }
}
