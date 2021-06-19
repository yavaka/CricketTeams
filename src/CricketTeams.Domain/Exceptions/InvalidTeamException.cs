namespace CricketTeams.Domain.Exceptions
{
    public class InvalidTeamException : BaseDomainException
    {
        public InvalidTeamException()
        {
        }

        public InvalidTeamException(string error) => this.Error = error;
    }
}
