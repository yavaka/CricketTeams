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
            private static Team teamA = A.Dummy<Team>().SetId(1);
            private static Team teamB = A.Dummy<Team>().SetId(2);

            public static Score GetScore(int id = 1)
            {
                var score = new Faker<Score>()
                    .CustomInstantiator(f => new Score(
                        teamA,
                        teamB,
                        oversPerInning: 5,
                        numberOfInnings: 2))
                    .Generate()
                    .SetId(id);

                return score;
            }

            public static Score GetScoreWithEndedInning(int id = 1)
            {
                var score = GetScore(id);

                var striker = teamA.Players.Batsmen.First(i => i.Id == 1);
                var nonStriker = teamA.Players.Batsmen.First(i => i.Id == 2);
                var bowler = teamB.Players.Bowlers.First();
                var secondBowler = teamB.Players.Bowlers.Last();

                score.UpdateCurrentInning(teamA, teamB);

                for (int over = 0; over < 5; over++)
                {
                    score.UpdateCurrentOver(
                        over % 2 == 0 ? bowler : secondBowler, 
                        striker, 
                        nonStriker);

                    for (int ball = 0; ball < 6; ball++)
                    {
                        score.UpdateCurrentBallWithRuns(ball);
                    }
                }

                return score;
            }
        }
    }
}
