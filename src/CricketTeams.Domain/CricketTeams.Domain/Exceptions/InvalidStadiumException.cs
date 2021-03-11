namespace CricketTeams.Domain.Exceptions
{
    public class InvalidStadiumException : BaseDomainException
    {
        public InvalidStadiumException()
        {
        }

        public InvalidStadiumException(string error) => this.Error = error;
    }
}
