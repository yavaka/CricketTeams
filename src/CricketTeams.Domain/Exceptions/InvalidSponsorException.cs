namespace CricketTeams.Domain.Exceptions
{
    public class InvalidSponsorException : BaseDomainException
    {
        public InvalidSponsorException()
        {
        }

        public InvalidSponsorException(string error) => this.Error = error;
    }
}
