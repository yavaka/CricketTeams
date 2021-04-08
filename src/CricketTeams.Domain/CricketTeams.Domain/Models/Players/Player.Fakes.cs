namespace CricketTeams.Domain.Models.Players
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using CricketTeams.Domain.Common;
    using FakeItEasy;

    public class PlayerFakes
    {
        public class PlayerDummyFactory : IDummyFactory
        {
            public Priority Priority => Priority.Default;

            public bool CanCreate(Type type) => type == typeof(Player);

            public object? Create(Type type) => Data.GetPlayer(); 
        }

        public static class Data
        {
            public static IEnumerable<Player> GetPlayers(int start = 1, int numberOfPlayers = 10)
                => Enumerable
                    .Range(start, numberOfPlayers)
                    .Select(i => GetPlayer(i))
                    .ToList();

            public static Player GetPlayer(int id = 1)
            {
                var player = new Faker<Player>()
                    .CustomInstantiator(f => new Player(
                        f.Person.FirstName,
                        f.Person.LastName,
                        f.Random.Number(10, 99),
                        f.Address.Country(),
                        f.Image.PlaceImgUrl(),
                        BattingStyle.LeftHand,
                        A.Dummy<BowlingStyle>(),
                        A.Dummy<FieldingPosition>(),
                        new History()))
                    .Generate()
                    .SetId(id);

                return player;
            }
        }
    }
}
