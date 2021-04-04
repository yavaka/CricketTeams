namespace CricketTeams.Domain.Models.Scores
{
    using Bogus;
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Teams;
    using FakeItEasy;
    using System;
    using System.Linq;

    public class ScoreFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(Score);

        public object? Create(Type type) => Data.GetScore();

        public static class Data
        {
            public static Score GetScore(int id = 1)
            {
                var teamA = A.Dummy<Team>();
                var teamB = A.Dummy<Team>();

                var score = new Faker<Score>()
                    .CustomInstantiator(f => new Score(
                        oversPerInning: 20,
                        numberOfInnings: 2,
                        InningFakes.Data.GetInning(
                            teamA,
                            teamB)))
                    .Generate()
                    .SetId(id);

                return score;
            }
        }
    }
}
