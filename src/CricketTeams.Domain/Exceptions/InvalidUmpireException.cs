namespace CricketTeams.Domain.Exceptions
{
    class InvalidUmpireException : BaseDomainException
    {
        public InvalidUmpireException()
        {
        }

        public InvalidUmpireException(string error) => this.Error = error;
    }
}
