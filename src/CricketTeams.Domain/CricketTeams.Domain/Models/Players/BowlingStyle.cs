namespace CricketTeams.Domain.Models.Players
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;
    using static ModelConstants.BowlingStyle;
    using static ModelConstants.Common;

    public class BowlingStyle : ValueObject
    {
        internal BowlingStyle(string styleName, BowlingType bowlingType, string description)
        {
            this.Validate(styleName, description);

            this.StyleName = styleName;
            this.Description = description;
            this.BowlingType = bowlingType;
        }

        private BowlingStyle(string styleName, string description)
        {
            this.StyleName = styleName;
            this.Description = description;

            this.BowlingType = default!;
        }

        public string StyleName { get; private set; }
        public BowlingType BowlingType { get; private set; }
        public string Description { get; private set; }

        private void Validate(string styleName, string description)
        {
            Guard.ForStringLength<InvalidPlayerException>(
                styleName,
                MinStyleNameLength,
                MaxStyleNameLength,
                nameof(this.StyleName));

            Guard.ForStringLength<InvalidPlayerException>(
                description,
                MinDescriptionLength,
                MaxDescriptionLength,
                nameof(this.Description));
        }
    }
}