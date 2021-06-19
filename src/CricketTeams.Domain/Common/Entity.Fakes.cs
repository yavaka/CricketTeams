namespace CricketTeams.Domain.Common
{
    public class EntityFakes
    {
        public class MockEntity : Entity<int> 
        {
            internal MockEntity(string name)
            {
                this.Name = name;
            }

            public string Name { get; private set; }
        }
    }
}
