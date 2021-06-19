namespace CricketTeams.Domain.Exceptions
{
    public class InvalidInningException : BaseDomainException
    {
        public InvalidInningException()
        {
        }

        public InvalidInningException(string error) => this.Error = error;
    }
}
