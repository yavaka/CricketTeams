using Bogus;
using CricketTeams.Domain.Models.Matches;
using CricketTeams.Domain.Models.Players;
using System.Collections.Generic;

namespace CricketTeams.Domain.Models.Scores
{
    public class BallFakes
    {
        public static class Data
        {
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
                    runs: f.Random.Number(0,9),
                    six: false,
                    four: false,
                    wideBall: false,
                    noBall: false));

            /// <summary>
            /// Get ball with striker out
            /// </summary>
            public static Ball GetBallWithStrikerDismiss(
                Player bowler,
                Player striker,
                Player nonStriker)
                => new Ball(
                    bowler,
                    striker,
                    nonStriker,
                    runs: 0,
                    six: false,
                    four: false,
                    wideBall: false,
                    noBall: false,
                    new KeyValuePair<KeyValuePair<Player, PlayerOutTypes>, Player>(
                        new KeyValuePair<Player, PlayerOutTypes>(bowler,PlayerOutTypes.Wicket),
                        striker));
        }
    }
}
