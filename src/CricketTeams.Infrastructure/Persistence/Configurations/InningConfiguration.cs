namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Scores;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static CricketTeams.Domain.Models.ModelConstants.Match;

    public class InningConfiguration : IEntityTypeConfiguration<Inning>
    {
        public void Configure(EntityTypeBuilder<Inning> builder)
        {
            builder
                .HasKey(i => i.Id);

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

            // Total batsmen out many to one
            builder
                .HasMany(i => i.BatsmenOut)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("_batsmenOut");

            // Overs many to one
            builder
                .HasMany(i => i.Overs)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("_overs");

            // Ignore current over prop
            builder
                .Ignore("CurrentOver");
        }
    }
}
