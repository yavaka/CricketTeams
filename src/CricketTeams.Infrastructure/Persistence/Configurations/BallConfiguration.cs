namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Scores;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class BallConfiguration : IEntityTypeConfiguration<Ball>
    {
        public void Configure(EntityTypeBuilder<Ball> builder)
        {
            // Bowler
            builder
                .HasOne(b => b.Bowler)
                .WithMany()
                .HasForeignKey("BowlerId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Striker
            builder
               .HasOne(s => s.Striker)
               .WithMany()
               .HasForeignKey("StrikerId")
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            // Non striker
            builder
               .HasOne(ns => ns.NonStriker)
               .WithMany()
               .HasForeignKey("NonStrikerId")
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            // Out type owner
            builder.OwnsOne(ot => ot.OutType, outType =>
            {
                outType.WithOwner();

                outType.Property(v => v!.Value);
            });
        }
    }
}