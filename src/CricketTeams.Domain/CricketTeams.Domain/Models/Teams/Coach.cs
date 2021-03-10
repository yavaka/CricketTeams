namespace CricketTeams.Domain.Models.Teams
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
            string avatarUrl,
            BattingStyle battingStyle,
            BowlingStyle bowlingStyle,
            History matchesAsCoach)
        {
            this.Validate(firstName, lastName, nickname, age, avatarUrl);
            this.ValidateBowlingStyle(bowlingStyle);

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Nickname = nickname;
            this.Age = age;
            this.Nationality = nationality;
            this.AvatarUrl = avatarUrl;
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
            string avatarUrl)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Nickname = nickname;
            this.Age = age;
            this.Nationality = nationality;
            this.AvatarUrl = avatarUrl;

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
        public string AvatarUrl { get; private set; }
        public BattingStyle? BattingStyle { get; private set; }
        public BowlingStyle? BowlingStyle { get; private set; }
        public History? MatchesAsCoach { get; private set; }
        public ICollection<Achievement> Achievements { get; private set; }

        #endregion

        #region Validations

        private void Validate(string firstName, string lastName, string nickname, int age, string avatarUrl)
        {
            this.ValidateName(firstName, nameof(this.FirstName));
            this.ValidateName(lastName, nameof(this.LastName));
            this.ValidateName(nickname, nameof(this.Nickname));
            this.ValidateAge(age);
            this.ValidateAvatarUrl(avatarUrl);
        }

        private void ValidateName(string name, string propName)
            => Guard.ForStringLength<InvalidCoachException>(
              name,
              MinNameLenght,
              MaxNameLenght,
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

        private void ValidateAvatarUrl(string avatarUrl)
           => Guard.ForValidUrl<InvalidCoachException>(
               avatarUrl,
               nameof(this.AvatarUrl));

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
