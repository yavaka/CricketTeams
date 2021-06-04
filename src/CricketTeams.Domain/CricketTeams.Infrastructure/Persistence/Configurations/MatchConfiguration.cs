namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Matches;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static CricketTeams.Domain.Models.ModelConstants.Match;

    internal class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            // Id
            builder.HasKey("Id");

            // Team A One to many
            builder
                .HasOne(m => m.TeamA)
                .WithMany()
                .HasForeignKey("TeamAId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Team B One to many
            builder
                .HasOne(m => m.TeamB)
                .WithMany()
                .HasForeignKey("TeamBId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Number of innings
            builder
                .Property(m => m.NumberOfInnings)
                .HasMaxLength(MaxInnings)
                .IsRequired();

            // Overs per inning
            builder
                .Property(m => m.OversPerInning)
                .HasMaxLength(MaxOvers)
                .IsRequired();

            // First umpire one to many
            builder
                .HasOne(m => m.FirstUmpire)
                .WithMany()
                .HasForeignKey("FirstUmpireId")
                .OnDelete(DeleteBehavior.Restrict);

            // Second umpire one to many
            builder
                .HasOne(m => m.SecondUmpire)
                .WithMany()
                .HasForeignKey("SecondUmpireId")
                .OnDelete(DeleteBehavior.Restrict);

            // Score one to many
            builder
                .HasOne(m => m.Score)
                .WithMany()
                .HasForeignKey("ScoreId")
                .OnDelete(DeleteBehavior.Restrict);

            // Stadium one to many
            builder
                .HasOne(m => m.Stadium)
                .WithMany()
                .HasForeignKey("StadiumId")
                .OnDelete(DeleteBehavior.Restrict);

            // Statistic owner
            builder
                .OwnsOne(m => m.Statistic, s =>
                {
                    s.WithOwner();

                    s.Property(d => d!.Date)
                        .IsRequired();

                    s.OwnsOne(td => td!.TossDecision, tDecision =>
                    {
                        tDecision.WithOwner();

                        tDecision.Property(v => v!.Value);
                    });
                });
        }
    }
}
