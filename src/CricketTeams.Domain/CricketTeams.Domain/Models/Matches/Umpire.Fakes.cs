namespace CricketTeams.Domain.Models.Matches
{
    using Bogus;
    using FakeItEasy;
    using System;
    using Domain.Common;

    public class UmpireFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(Umpire);

        public object? Create(Type type) => Data.GetUmpire();

        public static class Data
        {
            public static Umpire GetUmpire(int id = 1)
                => new Faker<Umpire>()
                    .CustomInstantiator(f => new Umpire(
                        f.Person.FirstName,
                        f.Person.LastName,
                        f.Random.Number(10, 99),
                        f.Random.Number(10,50),
                        f.Random.Number(10, 50)))
                    .Generate()
                    .SetId(id);
        }
    }
}
