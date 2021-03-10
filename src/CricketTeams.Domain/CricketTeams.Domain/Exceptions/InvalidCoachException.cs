namespace CricketTeams.Domain.Exceptions
{
    public class InvalidCoachException : BaseDomainException
    {
        public InvalidCoachException()
        {
        }

        public InvalidCoachException(string error) => this.Error = error;
    }
}
