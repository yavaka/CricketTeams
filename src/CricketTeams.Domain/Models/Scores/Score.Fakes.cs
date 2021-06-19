namespace CricketTeams.Domain.Models.Scores
{
    using Bogus;
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Players;
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
            private static Player striker = teamA.Players.Batsmen.First(i => i.Id == 1);
            private static Player nonStriker = teamA.Players.Batsmen.First(i => i.Id == 2);
            private static Player bowler = teamB.Players.Bowlers.First();
            private static Player secondBowler = teamB.Players.Bowlers.Last();

            public static Score GetScore(int id = 1)
                => new Faker<Score>()
                    .CustomInstantiator(f => new Score(
                        tossWinnerTeamId: 1,
                        TossDecisions.Batting,
                        teamA,
                        teamB,
                        oversPerInning: 5,
                        numberOfInnings: 2))
                    .Generate()
                    .SetId(id);

            public static Score GetScoreWithEndedInning(int id = 1)
            {
                var score = GetScore(id);

                score.UpdateCurrentInning();

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

            public static Score GetScoreWithEndedMatch(int id = 1) 
            {
                var score = GetScoreWithEndedInning(id);

                score.UpdateCurrentInning();

                for (int over = 0; over < 5; over++)
                {
                    score.UpdateCurrentOver(
                        over % 2 == 0 ? secondBowler : bowler,
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
