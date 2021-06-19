namespace CricketTeams.Domain.Exceptions
{
    public class InvalidOverException : BaseDomainException
    {
        public InvalidOverException()
        {
        }

        public InvalidOverException(string error) => this.Error = error;
    }
}
