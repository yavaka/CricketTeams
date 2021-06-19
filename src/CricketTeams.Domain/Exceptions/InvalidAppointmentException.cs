namespace CricketTeams.Domain.Exceptions
{
    public class InvalidAppointmentException : BaseDomainException
    {
        public InvalidAppointmentException()
        {
        }

        public InvalidAppointmentException(string error) => this.Error = error;
    }
}
