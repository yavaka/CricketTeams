namespace CricketTeams.Domain.Models.Teams
{
    using Bogus;
    using CricketTeams.Domain.Common;
    using CricketTeams.Domain.Models.Coaches;
    using CricketTeams.Domain.Models.Stadiums;
    using FakeItEasy;
    using System;

    public class TeamFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(Team);

        public object? Create(Type type) => Data.GetTeam();
        
        public static class Data
        {
            public static Team GetTeam(int id = 1)
            {
                var team = new Faker<Team>()
                    .CustomInstantiator(f => new Team(
                        f.Internet.UserName(),
                        f.Image.PlaceImgUrl(),
                        A.Dummy<TeamPlayers>(),
                        A.Dummy<Stadium>(),
                        A.Dummy<Coach>(),
                        new History(1, 0)))
                    .Generate()
                    .SetId(id);

                return team;
            }
        }
    }

}
