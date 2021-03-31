namespace CricketTeams.Domain.Exceptions
{
    public class InvalidTeamPlayersException : BaseDomainException
    {
        public InvalidTeamPlayersException()
        {
        }

        public InvalidTeamPlayersException(string error) => this.Error = error;
    }
}
