namespace CricketTeams.Domain.Models.Matches
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;

    using static ModelConstants.Common;

    public class Umpire : Entity<int>
    {
        internal Umpire(
            string firstName,
            string lastName,
            int age,
            int matchesAsMainReferee,
            int matchesAsSecondReferee)
        {
            Validate(firstName, lastName, age, matchesAsMainReferee, matchesAsSecondReferee);

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.MatchesAsMainReferee = matchesAsMainReferee;
            this.MatchesAsSecondReferee = matchesAsSecondReferee;
        }

        private Umpire(
            string firstName,
            string lastName,
            int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.MatchesAsMainReferee = default!;
            this.MatchesAsSecondReferee = default!;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }
        public int MatchesAsMainReferee { get; private set; }
        public int MatchesAsSecondReferee { get; private set; }

        #region Update & Increase methods

        public Umpire UpdateNames(string firstName, string lastName)
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

        public Umpire UpdateAge(int age)
        {
            if (this.Age != age)
            {
                ValidateAge(age);
                this.Age = age;
            }
            return this;
        }

        public Umpire IncreaseMatchAsMainReferee()
        {
            this.MatchesAsMainReferee++;
            return this;
        }

        public Umpire IncreaseMatchAsSecondReferee()
        {
            this.MatchesAsSecondReferee++;
            return this;
        }

        #endregion

        #region Vlidations

        private void Validate(
            string firstName,
            string lastName,
            int age,
            int matchesAsMainReferee,
            int matchesAsSecondReferee)
        {
            ValidateName(firstName, nameof(this.FirstName));
            ValidateName(lastName, nameof(this.LastName));
            ValidateAge(age);
            ValidateMatchesAsReferee(matchesAsMainReferee, nameof(matchesAsMainReferee));
            ValidateMatchesAsReferee(matchesAsSecondReferee, nameof(matchesAsSecondReferee));
        }

        private void ValidateName(string name, string propName)
            => Guard.ForStringLength<InvalidUmpireException>(
                name,
                MinNameLength,
                MaxNameLength,
                propName);

        private void ValidateAge(int age)
            => Guard.AgainstOutOfRange<InvalidUmpireException>(
                age,
                MinAge,
                MaxAge,
                nameof(this.Age));

        private void ValidateMatchesAsReferee(int matchesAsReferee, string propName)
            => Guard.AgainstNegativeValue<InvalidUmpireException>(
                matchesAsReferee,
                propName);

        #endregion
    }
}