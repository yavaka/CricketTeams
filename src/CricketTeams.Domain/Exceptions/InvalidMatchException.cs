namespace CricketTeams.Domain.Exceptions
{
    public class InvalidMatchException : BaseDomainException
    {
        public InvalidMatchException()
        {
        }

        public InvalidMatchException(string error) => this.Error = error;
    }
}
