namespace CricketTeams.Domain.Models.Teams
{
    using Bogus;
    using FakeItEasy;
    using System;

    public class SponsorFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(Sponsor);

        public object? Create(Type type)
            => new Faker<Sponsor>()
                .CustomInstantiator(f => new Sponsor(
                    f.Name.FullName(),
                    f.Internet.Url(),
                    SponsorTypes.Gold))
            .Generate();
    }
}
