namespace CricketTeams.Domain.Models.Players
{
    using CricketTeams.Domain.Common;
    using System;
    using System.Collections.Generic;

    internal class FieldingPositionData : IInitialData
    {
        public Type EntityType => typeof(FieldingPosition);

        public IEnumerable<object> GetData()
        => new List<FieldingPosition>
        {
            new FieldingPosition("Wicketkeeper","The keeper stands behind the stumps, although further back if the bowler is quicker."),
            new FieldingPosition("Slips","Positioned on the off-side behind the wicket, first slip is alongside the wicketkeeper, with second slip, third slip etc following in the same direction."),
            new FieldingPosition("Gully","The gully is just behind square of the wicket on the off side. Quick reactions is key to this position."),
            new FieldingPosition("Point", "Located on square of the wicket on the off-side and have the responsibility of stopping forceful shots played off the back foot like the square cut."),
            new FieldingPosition("Cover", "The main aim in the covers is to stop the runs coming from the batsman driving off the front or back foot."),
            new FieldingPosition("Third man", "The third man is positioned behind the wicketkeeper on the off-side."),
            new FieldingPosition("Fine log", "The position is on the leg side at around 45 degrees to the wicket."),
            new FieldingPosition("Mid wicket", "Position on the leg side, between square leg and mid-on."),
            new FieldingPosition("Mid off", "Positioned about 25-30 yards from the batsman. Normally, it is captains position."),
            new FieldingPosition("Square leg", "This position is square of the wicket on the leg side."),
        };
    }
}
