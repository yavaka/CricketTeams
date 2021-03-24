namespace CricketTeams.Domain.Models
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;

    using static ModelConstants.Common;
    using static ModelConstants.FieldingPosition;

    public class FieldingPosition : Entity<int>
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
            Guard.ForStringLength<InvalidPlayerException>(
                positionName,
                MinPositionName,
                MaxPositionName,
                nameof(this.PositionName));

            Guard.ForStringLength<InvalidPlayerException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));
        }

    }
}