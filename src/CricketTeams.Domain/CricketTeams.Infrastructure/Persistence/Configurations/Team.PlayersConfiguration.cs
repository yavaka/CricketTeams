namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Teams;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TeamPlayersConfiguration : IEntityTypeConfiguration<Players>
    {
        public void Configure(EntityTypeBuilder<Players> builder)
        {
            // Captain one to one
            builder
                .HasOne(c => c.Captain)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Wicketkeeper one to one
            builder
                .HasOne(w => w.WicketKeeper)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Twelft man one to one
            builder
                .HasOne(t => t.Twelfth)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            // Batsmen many to one
            builder
                .HasMany(b => b.Batsmen)
                .WithOne()
                .HasForeignKey("Team.BatsmanId")
                .OnDelete(DeleteBehavior.Restrict);

            // Bowlers many to one
            builder
                .HasMany(b => b.Bowlers)
                .WithOne()
                .HasForeignKey("Team.BowlerId")
                .OnDelete(DeleteBehavior.Restrict);

            // All rounders many to one
            builder
                .HasMany(a => a.AllRounders)
                .WithOne()
                .HasForeignKey("Team.AllRounderId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

