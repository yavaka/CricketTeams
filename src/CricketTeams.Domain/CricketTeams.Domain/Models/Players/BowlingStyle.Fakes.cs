namespace CricketTeams.Domain.Models.Players
{
    using FakeItEasy;
    using System;

    public class BowlingStyleFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(BowlingStyle);
        
        public object? Create(Type type)
            => new BowlingStyle(
                "Bouncer", 
                BowlingTypes.FastBowling, 
                "A delivery is known as a bouncer when a bowler intentionally makes the ball bounce nearly half way on the pitch and, as a result, the ball reaches the batsman at a shoulder or head level height.");
    }
}
