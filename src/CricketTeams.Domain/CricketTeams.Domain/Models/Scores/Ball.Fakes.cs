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
                        GetBall(A.Dummy<Player>(), A.Dummy<Player>(), A.Dummy<Player>()))
                    .ToList();

            /// <summary>
            /// Get ball with runs only
            /// </summary>
            public static Ball GetBall(
                Player bowler,
                Player striker,
                Player nonStriker)
                => new Faker<Ball>().CustomInstantiator(f => new Ball(
                    bowler,
                    striker,
                    nonStriker,
                    runs: f.Random.Number(0, 9),
                    six: false,
                    four: false,
                    wideBall: false,
                    noBall: false));

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
                    runs: 0,
                    six: false,
                    four: false,
                    wideBall: false,
                    noBall: false,
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
                    runs: 0,
                    six: false,
                    four: false,
                    wideBall: false,
                    noBall: false,
                    bowler,
                    nonStriker,
                    PlayerOutTypes.Catch);
            }
        }
    }
}
