namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Scores;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BallConfiguration : IEntityTypeConfiguration<Ball>
    {
        public void Configure(EntityTypeBuilder<Ball> builder)
        {
            // Bowler
            builder
                .Property(o => o.Bowler)
                .IsRequired();

            // Striker
            builder
                .Property(o => o.Striker)
                .IsRequired();

            // Non striker
            builder
                .Property(o => o.NonStriker)
                .IsRequired();
        }
    }
}
