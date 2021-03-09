namespace CricketTeams.Domain.Exceptions
{
    class InvalidAppointmentException : BaseDomainException
    {
        public InvalidAppointmentException()
        {
        }

        public InvalidAppointmentException(string error) => this.Error = error;
    }
}
