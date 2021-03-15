namespace CricketTeams.Domain.Exceptions
{
    public class InvalidScoreException : BaseDomainException
    {
        public InvalidScoreException()
        {
        }

        public InvalidScoreException(string error) => this.Error = error;
    }
}
