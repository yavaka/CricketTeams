namespace CricketTeams.Domain.Exceptions
{
    public class InvalidGymTrainerException : BaseDomainException
    {
        public InvalidGymTrainerException()
        {
        }

        public InvalidGymTrainerException(string error) => this.Error = error;
    }
}
