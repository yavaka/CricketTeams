namespace CricketTeams.Domain.Models.Matches
{
    using Bogus;
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Scores;
    using CricketTeams.Domain.Models.Stadiums;
    using CricketTeams.Domain.Models.Teams;
    using FakeItEasy;
    using System;
    using System.Linq;

    public class MatchFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(Match);

        public object? Create(Type type) => Data.GetMatch();

        public static class Data
        {
            public static Match GetMatch(int id = 1)
            {
                var teamA = A.Dummy<Team>().SetId(1);
                var teamB = A.Dummy<Team>().SetId(2);

                var match = new Faker<Match>()
                    .CustomInstantiator(f => new Match(
                        teamA,
                        teamB,
                        2,
                        5,
                        A.Dummy<Umpire>(),
                        A.Dummy<Umpire>().SetId(2),
                        A.Dummy<Stadium>()))
                    .Generate()
                    .SetId(id);

                return match;
            }
        }
    }
}
