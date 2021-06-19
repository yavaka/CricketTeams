namespace CricketTeams.Domain.Exceptions
{
    public class InvalidAchievementException : BaseDomainException
    {
        public InvalidAchievementException()
        {
        }

        public InvalidAchievementException(string error) => this.Error = error;
    }
}
