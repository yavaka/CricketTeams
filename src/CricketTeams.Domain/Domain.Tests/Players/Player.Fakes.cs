namespace Domain.Tests.Players
{
    using CricketTeams.Domain.Models.Players;
    using FakeItEasy;
    using System;
    public class PlayerFakes
    {
        public class PlayerDummyFactory : IDummyFactory
        {
            public Priority Priority => throw new NotImplementedException();

            public bool CanCreate(Type type) => true;

            public object? Create(Type type)
                => new Player(
                    "Test", "User", 23, "UK", "https://valid.test",
                    BattingStyle.LeftHand,
                    new BowlingStyle("Leg cutter", BowlingType.FastBowling, "The goal of using the leg cutter is not necessarily deceiving the batsman with the speed at which the ball is bowled, but also the change in direction after the ball bounces on the pitch.");
        }
    }
}
