namespace CricketTeams.Domain.Models.Players
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;

    using static ModelConstants.Common;
    using static ModelConstants.FieldingPosition;

    public class FieldingPosition : ValueObject
    {
        internal FieldingPosition(string positionName, string description)
        {
            this.Validate(positionName, description);

            this.PositionName = positionName;
            this.Description = description;
        }

        public string PositionName { get; private set; }
        public string Description { get; private set; }

        private void Validate(string positionName, string description)
        {
            Guard.ForStringLength<InvalidFieldingPositionException>(
                positionName,
                MinPositionName,
                MaxPositionName,
                nameof(this.PositionName));

            Guard.ForStringLength<InvalidFieldingPositionException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));
        }

    }
}