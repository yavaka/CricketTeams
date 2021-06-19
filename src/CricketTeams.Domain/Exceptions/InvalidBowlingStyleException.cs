namespace CricketTeams.Domain.Exceptions
{
    public class InvalidBowlingStyleException : BaseDomainException
    {
        public InvalidBowlingStyleException()
        {
        }

        public InvalidBowlingStyleException(string error) => this.Error = error;
    }
}
