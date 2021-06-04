namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Scores;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static CricketTeams.Domain.Models.ModelConstants.Match;

    internal class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            // Toss decision owner
            builder
                .OwnsOne(s => s.TossDecision, td =>
                {
                    td.WithOwner();

                    td.Property(v => v.Value);
                });

            // Team A One to many
            builder
                .HasOne(s => s.TeamA)
                .WithMany()
                .HasForeignKey("TeamAId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Team B One to many
            builder
                .HasOne(s => s.TeamB)
                .WithMany()
                .HasForeignKey("TeamBId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Number of innings
            builder
                .Property(s => s.NumberOfInnings)
                .HasMaxLength(MaxInnings)
                .IsRequired();

            // Overs per inning
            builder
                .Property(s => s.OversPerInning)
                .HasMaxLength(MaxOvers)
                .IsRequired();

            // Innings owner
            builder
                .OwnsMany(s => s.Innings, i => 
                {
                    i.WithOwner().HasForeignKey("OwnerId");
                });
        }
    }
}
