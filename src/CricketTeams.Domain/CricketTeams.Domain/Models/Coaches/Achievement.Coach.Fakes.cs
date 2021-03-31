namespace CricketTeams.Domain.Models.Coaches
{
    using Bogus;
    using CricketTeams.Domain.Models.Players;
    using FakeItEasy;
    using System;

    public class AchievementCoachFakes : IDummyFactory
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
                        AchievementTypes.Medal))
                    .Generate();
        }
    }
}
