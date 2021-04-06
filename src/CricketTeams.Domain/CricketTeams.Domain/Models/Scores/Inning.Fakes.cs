namespace CricketTeams.Domain.Models.Scores
{
    using Bogus;
    using CricketTeams.Domain.Models.Teams;
    using System.Linq;

    public class InningFakes
    {
        public static class Data
        {
            public static Inning GetInning(Team teamA, Team teamB)
                => new Faker<Inning>()
                    .CustomInstantiator(f => new Inning(
                        teamA,
                        teamB,
                        OverFakes.Data.GetOver(
                                bowler: 
                                    teamB.Players.Bowlers.Last(),
                                striker: 
                                    teamA.Players.Batsmen.First(),
                                nonStriker: 
                                    teamA.Players.Batsmen.First(i => i.Id == 2))))
                    .Generate();
        }
    }
}
