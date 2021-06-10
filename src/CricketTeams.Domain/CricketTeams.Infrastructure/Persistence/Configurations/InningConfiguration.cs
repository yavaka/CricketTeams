namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Scores;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static CricketTeams.Domain.Models.ModelConstants.Match;

    internal class InningConfiguration : IEntityTypeConfiguration<Inning>
    {
        public void Configure(EntityTypeBuilder<Inning> builder)
        {
            // Batting team one to many
            builder
                .HasOne(i => i.BattingTeam)
                .WithMany()
                .HasForeignKey("BattingTeamId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Bowling team one to many
            builder
                .HasOne(i => i.BowlingTeam)
                .WithMany()
                .HasForeignKey("BowlingTeamId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Overs per inning
            builder
                .Property(i => i.OversPerInning)
                .HasMaxLength(MaxOvers)
                .IsRequired();

            // Overs owner
            builder
                .OwnsMany(i => i.Overs, o => 
                {
                    o.WithOwner().HasForeignKey("OwnerId");
                });
        }
    }
}
