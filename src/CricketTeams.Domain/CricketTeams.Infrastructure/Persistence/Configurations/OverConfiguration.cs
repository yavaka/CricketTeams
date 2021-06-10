namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Scores;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OverConfiguration : IEntityTypeConfiguration<Over>
    {
        public void Configure(EntityTypeBuilder<Over> builder)
        {
            builder.HasKey("Id");

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

            // Balls
            builder
                .OwnsMany(o => o.Balls, b => 
                {
                    b.WithOwner().HasForeignKey("OwnerId");
                });
        }
    }
}
