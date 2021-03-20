namespace CricketTeams.Domain.Exceptions
{
    public class InvalidBallException : BaseDomainException
    {
        public InvalidBallException()
        {
        }

        public InvalidBallException(string error) => this.Error = error;
    }
}
