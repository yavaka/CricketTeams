using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CricketTeams.Domain.Models.Players
{
    public class FieldingPositionFakes : IDummyFactory
    {
        public Priority Priority => Priority.Default;

        public bool CanCreate(Type type) => type == typeof(FieldingPosition);

        public object? Create(Type type)
            => new FieldingPosition(
                "Wicketkeeper", 
                "The keeper stands behind the stumps, although further back if the bowler is quicker.");
    }
}
