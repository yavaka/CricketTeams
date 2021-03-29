namespace CricketTeams.Domain.Models.Stadiums
{
    using Bogus;
    using CricketTeams.Domain.Common;
    using FakeItEasy;
    using System;

    public class StadiumFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(Stadium);

        public object? Create(Type type) => Data.GetStadium();

        public static class Data
        {
            public static Stadium GetStadium(int id = 1)
                => new Faker<Stadium>()
                    .CustomInstantiator(f => new Stadium(
                        f.Name.FindName(),
                        f.Address.FullAddress(),
                        f.Random.Number(100, 100000),
                        f.Internet.Url(),
                        f.Company.CompanyName()))
                    .Generate()
                    .SetId(id);
        }

    }
}
