namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Teams;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static CricketTeams.Domain.Models.ModelConstants.Common;

    internal class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            // Id
            builder
                .HasKey(t => t.Id);

            // Name
            builder
                .Property(t => t.Name)
                .HasMaxLength(MaxNameLength)
                .IsRequired();

            // Logo Url
            builder
                .Property(t => t.LogoUrl)
                .HasMaxLength(MaxUrlLength);

            // Players owner
            builder
                .OwnsOne(t => t.Players, p =>
                {
                    p.WithOwner();
                });

            // Coach one to one
            builder
                .HasOne(t => t.Coach)
                .WithOne()
                .HasForeignKey("CoachId")
                .OnDelete(DeleteBehavior.Restrict);

            // Stadium one to one
            builder
                .HasOne(t => t.Stadium)
                .WithOne()
                .HasForeignKey("StadiumId")
                .OnDelete(DeleteBehavior.Restrict);

            // History owner
            builder
                .OwnsOne(t => t.History, h => 
                {
                    h.WithOwner();
                });

            // Sponsors owner
            builder
                .OwnsMany(t => t.Sponsors, s => 
                {
                    s.WithOwner().HasForeignKey("OwnerId");

                    // Name
                    s.Property(n => n.Name)
                        .HasMaxLength(MaxNameLength)
                        .IsRequired();

                    // Website url
                    s.Property(wUrl => wUrl.WebsiteUrl)
                        .HasMaxLength(MaxUrlLength);

                    // Sponsor type
                    s.OwnsOne(st => st.SponsorType, sType => 
                    {
                        sType.WithOwner();

                        sType.Property(v => v!.Value);
                    });
                });

            //Achievements owner
            builder
                .OwnsMany(t => t.Achievements, a =>
                {
                    a.WithOwner().HasForeignKey("OwnerId");

                    // Name
                    a.Property(n => n.Name)
                     .IsRequired()
                     .HasMaxLength(MaxNameLength);

                    // Description
                    a.Property(n => n.Description)
                     .IsRequired()
                     .HasMaxLength(MaxDescriptionLength);

                    // Image url
                    a.Property(n => n.ImageUrl)
                     .HasMaxLength(MaxUrlLength);

                    //Achievement type owner
                    a.OwnsOne(at => at.AchievementType, at =>
                    {
                        at.WithOwner();

                        at.Property(v => v.Value);
                    });
                });
        }
    }
}
