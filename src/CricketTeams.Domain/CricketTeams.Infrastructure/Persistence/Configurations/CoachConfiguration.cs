namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Coaches;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static CricketTeams.Domain.Models.ModelConstants.Common;
    using static CricketTeams.Domain.Models.ModelConstants.BowlingStyle;

    internal class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            // Id
            builder.HasKey(i => i.Id);

            // First name
            builder.Property(fn => fn.FirstName)
                .HasMaxLength(MaxNameLength)
                .IsRequired();

            // Last name
            builder.Property(ln => ln.LastName)
                .HasMaxLength(MaxNameLength)
                .IsRequired();

            // Nickname
            builder.Property(n => n.Nickname)
                .HasMaxLength(MaxNameLength)
                .IsRequired();

            // Age
            builder.Property(a => a.Age)
                .HasMaxLength(MaxAge);

            // Photo url
            builder.Property(p => p.PhotoUrl)
                .HasMaxLength(MaxUrlLength);

            //Batting style owner
            builder.OwnsOne(c => c.BattingStyle, c =>
            {
                c.WithOwner();

                // Value
                c.Property(bs => bs!.Value);
            });

            //Bowling style owner
            builder.OwnsOne(bs => bs.BowlingStyle, bs =>
            {
                bs.WithOwner();

                // Style name
                bs.Property(bowling => bowling!.StyleName)
                 .IsRequired()
                 .HasMaxLength(MaxStyleNameLength);

                // Description
                bs.Property(bowling => bowling!.Description)
                 .IsRequired()
                 .HasMaxLength(MaxDescriptionLength);

                // Bowling type owner
                bs.OwnsOne(bowlingType => bowlingType!.BowlingType, bt =>
                {
                    bt.WithOwner();

                    // Value
                    bt.Property(bt => bt.Value)
                      .IsRequired();
                });
            });

            //Achievement owner
            builder.OwnsMany(a => a.Achievements, a =>
            {
                a.WithOwner().HasForeignKey("OwnerId");

                // Name
                a.Property(n => n.Name)
                 .IsRequired()
                 .HasMaxLength(MaxNameLength);

                // Description
                a.Property(d => d.Description)
                 .IsRequired()
                 .HasMaxLength(MaxDescriptionLength);

                // Image url
                a.Property(iUrl => iUrl.ImageUrl)
                 .HasMaxLength(MaxUrlLength);

                //Achievement type owner
                a.OwnsOne(at => at.AchievementType, at =>
                {
                    at.WithOwner();

                    // Value
                    at.Property(v => v.Value);
                });
            });
        }
    }
}
