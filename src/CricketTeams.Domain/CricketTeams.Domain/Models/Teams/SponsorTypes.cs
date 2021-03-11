namespace CricketTeams.Domain.Models.Teams
{
    using CricketTeams.Domain.Common;

    public class SponsorTypes : Enumeration
    {
        public static readonly SponsorTypes Title = new SponsorTypes(1, nameof(Title));
        public static readonly SponsorTypes Platinum = new SponsorTypes(2, nameof(Platinum));
        public static readonly SponsorTypes Gold = new SponsorTypes(3, nameof(Gold));
        public static readonly SponsorTypes Bronze = new SponsorTypes(4, nameof(Bronze));
        public static readonly SponsorTypes Silver = new SponsorTypes(5, nameof(Silver));
        public static readonly SponsorTypes Other = new SponsorTypes(6, nameof(Other));

        private SponsorTypes(int value)
            : this(value, FromValue<SponsorTypes>(value).Name)
        {
        }

        private SponsorTypes(int value, string name)
            : base(value, name)
        {
        }
    }
}