namespace CricketTeams.Domain.Models.Scores
{
    using CricketTeams.Domain.Models.Players;
    using System.Collections.Generic;
    using System.Linq;

    public class OverFakes
    {
        public static class Data
        {
            public static IEnumerable<Over> GetOvers(
                Player bowler,
                Player striker,
                Player nonStriker,
                int count = 20)
                => Enumerable
                    .Range(1, count)
                    .Select(i => GetOver(bowler, striker, nonStriker))
                    .ToList();

            public static Over GetOver(
                Player bowler,
                Player striker,
                Player nonStriker)
                => new Over(bowler, striker, nonStriker);

            public static Over GetEndedOver(
                Player bowler,
                Player striker,
                Player nonStriker) 
            {
                var balls = BallFakes.Data.GetBalls(6);

                var over = new Over(bowler, striker, nonStriker);

                foreach (var ball in balls)
                {
                    over.UpdateCurrentBallWithRuns(ball.Runs);
                }
                return over;
            }
        }
    }
}
