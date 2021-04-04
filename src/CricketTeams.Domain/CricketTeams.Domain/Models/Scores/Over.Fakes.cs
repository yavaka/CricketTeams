namespace CricketTeams.Domain.Models.Scores
{
    using Bogus;
    using CricketTeams.Domain.Models.Players;
    using FakeItEasy;
    using System.Collections.Generic;
    using System.Linq;

    public class OverFakes
    {
        public static class Data
        {
            public static IEnumerable<Over> GetOvers(int count = 20)
                => Enumerable
                    .Range(1, count)
                    .Select(i => GetOver())
                    .ToList();

            public static Over GetOver()
            {
                var bowler = A.Dummy<Player>();
                var striker = A.Dummy<Player>();
                var nonStriker = A.Dummy<Player>();

                var over = new Faker<Over>()
                    .CustomInstantiator(f => new Over(
                        bowler,
                        striker,
                        nonStriker,
                        new Ball(
                            bowler,
                            striker,
                            nonStriker,
                            f.Random.Number(0, 150),
                            true,
                            false,
                            false,
                            false)))
                    .Generate();

                return over;
            }
        }
    }
}
