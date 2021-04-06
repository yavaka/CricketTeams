﻿namespace CricketTeams.Domain.Models.Scores
{
    using Bogus;
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
                => new Faker<Over>()
                    .CustomInstantiator(f => new Over(
                        bowler,
                        striker,
                        nonStriker,
                        BallFakes.Data.GetBall(bowler,striker,nonStriker)))
                    .Generate();
        }
    }
}