namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Scores;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class OverConfiguration : IEntityTypeConfiguration<Over>
    {
        public void Configure(EntityTypeBuilder<Over> builder)
        {
            // Bowler one to many
            builder
                .HasOne(o => o.Bowler)
                .WithMany()
                .HasForeignKey("BowlerId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Striker one to many
            builder
               .HasOne(o => o.Striker)
               .WithMany()
               .HasForeignKey("StrikerId")
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            // Non striker one to many
            builder
               .HasOne(o => o.NonStriker)
               .WithMany()
               .HasForeignKey("NonStrikerId")
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            // Total batsmen out many to one
            builder
                .HasMany(o => o.BatsmenOut)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("_batsmenOut");

            // Balls many to one
            builder
                .HasMany(o => o.Balls)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("_balls");

            // Ignore current ball prop
            builder
                .Ignore("CurrentBall");
        }
    }
}
