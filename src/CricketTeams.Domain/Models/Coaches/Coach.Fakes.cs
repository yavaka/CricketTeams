namespace CricketTeams.Domain.Models.Coaches
{
    using Bogus;
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Players;
    using FakeItEasy;
    using System;

    public class CoachFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(Coach);

        public object? Create(Type type) => Data.GetCoach();

        public static class Data
        {
            public static Coach GetCoach(int id = 1)
                => new Faker<Coach>()
                    .CustomInstantiator(f => new Coach(
                        f.Person.FirstName,
                        f.Person.LastName,
                        f.Person.UserName,
                        f.Random.Number(10, 80),
                        f.Address.Country(),
                        f.Image.PlaceImgUrl(),
                        BattingStyle.RightHand,
                        A.Dummy<BowlingStyle>()))
                    .Generate()
                    .SetId(id);
        }
    }
}
