namespace CricketTeams.Domain.Models.Teams
{
    using Bogus;
    using CricketTeams.Domain.Models.Players;
    using FakeItEasy;
    using System;

    public class AchievementFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(Achievement);

        public object? Create(Type type) => Data.GetAchievement();

        public static class Data
        {
            public static Achievement GetAchievement()
                => new Faker<Achievement>()
                    .CustomInstantiator(f => new Achievement(
                        f.Name.FindName(),
                        f.Lorem.Sentence(),
                        f.Image.PlaceImgUrl(),
                        AchievementTypes.Trophy))
                    .Generate();
        }
    }
}
