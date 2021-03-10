namespace CricketTeams.Domain.Models
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;

    using static ModelConstants.Common;

    public class Achievement : ValueObject
    {
        internal Achievement(
            string name,
            string description,
            string imageUrl,
            AchievementType achievementType)
        {
            Validate(name, description, imageUrl);

            this.Name = name;
            this.Description = description;
            this.ImageUrl = imageUrl;

            AchievementType = achievementType;
        }

        private Achievement(
            string name,
            string description,
            string imageUrl) 
        {
            this.Name = name;
            this.Description = description;
            this.ImageUrl = imageUrl;

            this.AchievementType = default!;
        }

        public string Name { get; set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public AchievementType AchievementType { get; private set; }

        private void Validate(string name, string description, string imageUrl)
        {
            ValidateName(name);
            ValidateDescription(description);
            ValidateImageUrl(imageUrl);
        }

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidAchievementException>(
                name,
                ModelConstants.Achievement.MinNameLenght,
                ModelConstants.Achievement.MinNameLenght,
                nameof(this.Name));

        private void ValidateDescription(string description)
            => Guard.ForStringLength<InvalidAchievementException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));

        private void ValidateImageUrl(string imageUrl)
            => Guard.ForValidUrl<InvalidAchievementException>(
                imageUrl,
                nameof(this.ImageUrl));
    }
}