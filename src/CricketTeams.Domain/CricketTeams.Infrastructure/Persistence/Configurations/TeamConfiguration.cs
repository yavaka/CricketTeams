namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Teams;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static CricketTeams.Domain.Models.ModelConstants.Common;
    using static CricketTeams.Domain.Models.ModelConstants.BowlingStyle;

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

            // Coach owner
            builder
                .OwnsOne(t => t.Coach, c =>
                {
                    c.HasKey(i => i.Id);

                    // First name
                    c.Property(fn => fn.FirstName)
                        .HasMaxLength(MaxNameLength)
                        .IsRequired();

                    // Last name
                    c.Property(ln => ln.LastName)
                        .HasMaxLength(MaxNameLength)
                        .IsRequired();

                    // Nickname
                    c.Property(n => n.Nickname)
                        .HasMaxLength(MaxNameLength)
                        .IsRequired();

                    // Age
                    c.Property(a => a.Age)
                        .HasMaxLength(MaxAge);

                    // Photo url
                    c.Property(p => p.PhotoUrl)
                        .HasMaxLength(MaxUrlLength);

                    //Batting style owner
                    c.OwnsOne(c => c.BattingStyle, c =>
                    {
                        c.WithOwner();

                        c.Property(bs => bs!.Value);
                    });

                    //Bowling style owner
                    c.OwnsOne(bs => bs.BowlingStyle, bs =>
                    {
                        bs.WithOwner();

                        bs.Property(bowling => bowling!.StyleName)
                         .IsRequired()
                         .HasMaxLength(MaxStyleNameLength);

                        bs.Property(bowling => bowling!.Description)
                         .IsRequired()
                         .HasMaxLength(MaxDescriptionLength);

                        // Bowling type owner
                        bs.OwnsOne(bowlingType => bowlingType!.BowlingType, bt =>
                        {
                            bt.WithOwner();

                            bt.Property(bt => bt.Value)
                              .IsRequired();
                        });
                    });

                    //Achievement owner
                    c.OwnsMany(a => a.Achievements, a =>
                    {
                        a.WithOwner().HasForeignKey("OwnerId");

                        a.HasKey("Id");

                        a.Property(n => n.Name)
                         .IsRequired()
                         .HasMaxLength(MaxNameLength);

                        a.Property(d => d.Description)
                         .IsRequired()
                         .HasMaxLength(MaxDescriptionLength);

                        a.Property(iUrl => iUrl.ImageUrl)
                         .HasMaxLength(MaxUrlLength);

                        //Achievement type owner
                        a.OwnsOne(at => at.AchievementType, at =>
                        {
                            at.WithOwner();

                            at.Property(v => v.Value);
                        });
                    });
                });

            // Stadium owner
            builder
                .OwnsOne(t => t.Stadium, s =>
                {
                    s.WithOwner();

                    // Id
                    s.HasKey(i => i!.Id);

                    // Name
                    s.Property(n => n!.Name)
                        .IsRequired();

                    // Capacity
                    s.Property(c => c!.Capacity)
                        .IsRequired();

                    // Website url
                    s.Property(wUrl => wUrl!.WebsiteUrl)
                        .HasMaxLength(MaxUrlLength);
                });

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
