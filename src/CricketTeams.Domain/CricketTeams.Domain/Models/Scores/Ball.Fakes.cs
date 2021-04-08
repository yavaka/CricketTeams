using Bogus;
using CricketTeams.Domain.Common;
using CricketTeams.Domain.Models.Matches;
using CricketTeams.Domain.Models.Players;
using FakeItEasy;
using System.Collections.Generic;
using System.Linq;

namespace CricketTeams.Domain.Models.Scores
{
    public class BallFakes
    {
        public static class Data
        {
            /// <summary>
            /// Get balls without dismissed players
            /// </summary>
            public static IEnumerable<Ball> GetBalls(int count = 6)
                => Enumerable
                    .Range(1, count)
                    .Select(b => 
                        GetBallWithRuns(A.Dummy<Player>(), A.Dummy<Player>(), A.Dummy<Player>()))
                    .ToList();

            /// <summary>
            /// Get ball with runs only
            /// </summary>
            public static Ball GetBallWithRuns(
                Player bowler,
                Player striker,
                Player nonStriker)
                => new Faker<Ball>().CustomInstantiator(f => new Ball(
                    bowler,
                    striker,
                    nonStriker,
                    runs: f.Random.Number(0, 9)));

            /// <summary>
            /// Get ball with striker out
            /// Bowler catched the striker
            /// </summary>
            public static Ball GetBallWithStrikerDismiss(
                Player bowler,
                Player striker,
                Player nonStriker)
            {
                return new Ball(
                    bowler,
                    striker,
                    nonStriker,
                    bowler,
                    striker,
                    PlayerOutTypes.Catch);
            }

            public static Ball GetBallWithNonStrikerDismiss(
                Player bowler,
                Player striker,
                Player nonStriker)
            {
                return new Ball(
                    bowler,
                    striker,
                    nonStriker,
                    bowler,
                    nonStriker,
                    PlayerOutTypes.Catch);
            }

            public static Ball GetBallWithWideBall(
                Player bowler,
                Player striker,
                Player nonStriker)
                => new Faker<Ball>()
                    .CustomInstantiator(f => new Ball(
                        bowler,
                        striker,
                        nonStriker,
                        runs: f.Random.Number(0, 9),
                        noBall: false,
                        wideBall: true));

            public static Ball GetBallWithNoBall(
                Player bowler,
                Player striker,
                Player nonStriker)
                => new Faker<Ball>()
                    .CustomInstantiator(f => new Ball(
                        bowler,
                        striker,
                        nonStriker,
                        runs: f.Random.Number(0, 9),
                        noBall: true,
                        wideBall: false));
        }
    }
}
