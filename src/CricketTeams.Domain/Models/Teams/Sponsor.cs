namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Exceptions;

    using static ModelConstants.Common;

    public class Sponsor : ValueObject
    {
        internal Sponsor(
            string name,
            string websiteUrl,
            SponsorTypes sponsorType)
        {
            Validate(name, websiteUrl);

            this.Name = name;
            this.WebsiteUrl = websiteUrl;
            this.SponsorType = sponsorType;
        }

        private Sponsor(string name, string websiteUrl) 
        {
            this.Name = name;
            this.WebsiteUrl = websiteUrl;

            this.SponsorType = default!;
        }

        public string Name { get; private set; }
        public string WebsiteUrl { get; private set; }
        public SponsorTypes SponsorType { get; private set; }

        private void Validate(string name, string websiteUrl)
        {
            ValidateName(name);
            ValidateWebsiteUrl(websiteUrl);
        }

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidSponsorException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

        private void ValidateWebsiteUrl(string websiteUrl)
            => Guard.ForValidUrl<InvalidSponsorException>(
                websiteUrl,
                nameof(this.WebsiteUrl));
    }
}