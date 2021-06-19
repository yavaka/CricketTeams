namespace CricketTeams.Domain.Models.Teams
{
    using Bogus;
    using CricketTeams.Domain.Models.Players;
    using FakeItEasy;
    using System;
    using System.Linq;

    public class TeamPlayersFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(TeamPlayers);

        public object? Create(Type type) => Data.GetPlayers();

        public static class Data
        {
            public static TeamPlayers GetPlayers()
            {
                var restOfPlayers = PlayerFakes.Data.GetPlayers(3,9);
                var captain = PlayerFakes.Data.GetPlayer(1);
                var wicketKeeper = PlayerFakes.Data.GetPlayer(2);
                var twelftman = PlayerFakes.Data.GetPlayer(12);

                var players = new Faker<TeamPlayers>()
                    .CustomInstantiator(f => new TeamPlayers(
                        captain,
                        wicketKeeper,
                        twelftman)).Generate();

                players.AddAllRounder(captain);
                players.AddBatsman(captain);
                players.AddBatsman(wicketKeeper);

                foreach (var batsman in restOfPlayers)
                {
                    players.AddBatsman(batsman);
                }

                foreach (var bowler in restOfPlayers.Take(5))
                {
                    players.AddBowler(bowler);
                }

                return players;
            }
        }
    }
}
