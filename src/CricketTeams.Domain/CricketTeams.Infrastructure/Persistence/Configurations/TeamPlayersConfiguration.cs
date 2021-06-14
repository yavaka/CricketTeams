namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Players;
    using CricketTeams.Domain.Models.Teams;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TeamPlayersConfiguration : IEntityTypeConfiguration<Players>
    {
        public void Configure(EntityTypeBuilder<Players> builder)
        {
            builder
                .HasKey(i => i.Id);

            // Captain one to one
            builder
                .HasOne(c => c.Captain)
                .WithOne()
                .HasForeignKey<Player>(i => i.Id)
                .HasConstraintName("FK_Captain_Id")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Wicketkeeper one to many
            builder
                .HasOne(w => w.WicketKeeper)
                .WithOne()
                .HasForeignKey<Player>(i => i.Id)
                .HasConstraintName("FK_WicketKeeper_Id")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Twelft man one to many
            builder
                .HasOne(t => t.Twelfth)
                .WithOne()
                .HasForeignKey<Player>(i => i.Id)
                .HasConstraintName("FK_Twelfth_Id")
                .OnDelete(DeleteBehavior.Restrict);

            // Batsmen many to one
            builder
                .HasMany(b => b.Batsmen)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            // Bowlers many to one
            builder
                .HasMany(b => b.Bowlers)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            // All rounders many to one
            builder
                .HasMany(a => a.AllRounders)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            // All players not mapped
            builder.Ignore(ap => ap.AllPlayers);
        }
    }
}
