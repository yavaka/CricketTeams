namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Stadiums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static CricketTeams.Domain.Models.ModelConstants.Common;
    using static CricketTeams.Domain.Models.ModelConstants.Stadium;

    internal class StadiumConfiguration : IEntityTypeConfiguration<Stadium>
    {
        public void Configure(EntityTypeBuilder<Stadium> builder)
        {
            // Id
            builder.HasKey(i => i.Id);

            // Name
            builder.Property(n => n.Name)
                .HasMaxLength(MaxNameLength)
                .IsRequired();

            // Capacity
            builder.Property(c => c.Capacity)
                .HasMaxLength(MaxCapacityLength)
                .IsRequired();

            // Website url
            builder.Property(wUrl => wUrl.WebsiteUrl)
                .HasMaxLength(MaxUrlLength);
        }
    }
}
