namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;

    using static ModelConstants.Common;

    public class GymTrainer : ValueObject
    {
        internal GymTrainer(
            string firstName,
            string lastName,
            int age,
            string photoUrl)
        {
            Validate(firstName, lastName, age, photoUrl);

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.PhotoUrl = photoUrl;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName => this.FirstName + this.LastName;
        public int Age { get; private set; }
        public string PhotoUrl { get; private set; }

        private void Validate(string firstName, string lastName, int age, string photoUrl)
        {
            ValidateName(firstName, nameof(this.FirstName));
            ValidateName(lastName, nameof(this.LastName));
            ValidateAge(age);
            ValidatePhotoUrl(photoUrl);
        }

        private void ValidateName(string name, string propName)
            => Guard.ForStringLength<InvalidGymTrainerException>(
                name,
                MinNameLength,
                MaxNameLength,
                propName);

        private void ValidateAge(int age)
            => Guard.AgainstOutOfRange<InvalidGymTrainerException>(
                age,
                MinAge,
                MaxAge,
                nameof(this.Age));

        private void ValidatePhotoUrl(string photoUrl)
            => Guard.ForValidUrl<InvalidGymTrainerException>(
                photoUrl,
                nameof(this.PhotoUrl));
    }
}
