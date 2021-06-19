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

            // History owner
            builder
                .OwnsOne(t => t.History, h =>
                {
                    h.WithOwner();

                    h.Property(w => w!.TotalWins);
                    h.Property(l => l!.TotalLoses);
                });

            // Sponsors owner
            builder
                .OwnsMany(t => t.Sponsors, s =>
                {
                    s.WithOwner().HasForeignKey("FK_Sponsor_Owned");

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
        }
    }
}
