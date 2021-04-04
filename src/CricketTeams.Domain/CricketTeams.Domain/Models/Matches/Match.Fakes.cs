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
                var teamA = A.Dummy<Team>();
                var teamB = A.Dummy<Team>();

                var match = new Faker<Match>()
                    .CustomInstantiator(f => new Match(
                        teamA,
                        teamB,
                        default!,
                        default!,
                        A.Dummy<Umpire>(),
                        A.Dummy<Umpire>(),
                        A.Dummy<Score>(),
                        new Statistic(
                            f.Date.Recent(),
                            teamB.Players.AllPlayers
                                .Select(i =>i.Id)
                                .FirstOrDefault(i => i == f.Random.Number(1,11)),
                            teamB.Id,
                            f.Random.Number(100, 250),
                            teamA.Id,
                            f.Random.Number(50, 99),
                            teamA.Id,
                            TossDecisions.Batting),
                        A.Dummy<Stadium>()))
                    .Generate()
                    .SetId(id);

                return match;
            }
        }
    }
}
