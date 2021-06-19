namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Players;
    using CricketTeams.Domain.Models.Teams;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class TeamPlayersConfiguration : IEntityTypeConfiguration<TeamPlayers>
    {
        public void Configure(EntityTypeBuilder<TeamPlayers> builder)
        {
            builder
                .HasKey(i => i.Id);

            // Captain one to one
            builder
                .HasOne(c => c.Captain)
                .WithOne()
                .HasForeignKey<Player>("CaptainId")
                .HasConstraintName("FK_CaptainId")
                .OnDelete(DeleteBehavior.Restrict);

            // Wicketkeeper one to many
            builder
                .HasOne(w => w.WicketKeeper)
                .WithOne()
                .HasForeignKey<Player>("WicketKeeperId")
                .HasConstraintName("FK_WicketKeeperId")
                .OnDelete(DeleteBehavior.Restrict);

            // Batsmen many to one
            builder
                .HasMany(b => b.Batsmen)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("_batsmen");

            // Bowlers many to one
            builder
                .HasMany(b => b.Bowlers)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("_bowlers");

            // All rounders many to one
            builder
                .HasMany(a => a.AllRounders)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("_allRounders");

            // All players not mapped
            builder.Ignore(ap => ap.AllPlayers);
        }
    }
}
