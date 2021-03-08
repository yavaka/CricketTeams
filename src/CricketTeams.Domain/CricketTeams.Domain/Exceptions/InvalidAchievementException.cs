namespace CricketTeams.Domain.Exceptions
{
    class InvalidAchievementException : BaseDomainException
    {
        public InvalidAchievementException()
        {
        }

        public InvalidAchievementException(string error) => this.Error = error;
    {
    }
}
