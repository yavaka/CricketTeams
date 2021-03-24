namespace CricketTeams.Domain.Exceptions
{
    public class InvalidFieldingPositionException : BaseDomainException
    {
        public InvalidFieldingPositionException()
        {
        }

        public InvalidFieldingPositionException(string error) => this.Error = error;
    }
}
