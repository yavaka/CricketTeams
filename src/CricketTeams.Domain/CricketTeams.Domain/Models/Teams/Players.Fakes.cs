namespace CricketTeams.Domain.Models.Teams
{
    using Bogus;
    using CricketTeams.Domain.Models.Players;
    using FakeItEasy;
    using System;
    using System.Linq;

    public class PlayersFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(Players);

        public object? Create(Type type) => Data.GetPlayers();

        public static class Data
        {
            public static Players GetPlayers()
            {
                var captain = A.Dummy<Player>();
                var wicketKeeper = A.Dummy<Player>();
                var twelftman = A.Dummy<Player>();
                var restOfPlayers = PlayerFakes.Data.GetPlayers();

                var players = new Faker<Players>()
                    .CustomInstantiator(f => new Players(
                        captain,
                        wicketKeeper,
                        twelftman)).Generate();

                players.AddAllRounder(captain);
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
