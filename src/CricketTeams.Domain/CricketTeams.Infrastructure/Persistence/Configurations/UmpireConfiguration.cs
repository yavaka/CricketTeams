namespace CricketTeams.Infrastructure.Persistence.Configurations
{
    using CricketTeams.Domain.Models.Matches;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static CricketTeams.Domain.Models.ModelConstants.Common;

    internal class UmpireConfiguration : IEntityTypeConfiguration<Umpire>
    {
        public void Configure(EntityTypeBuilder<Umpire> builder)
        {
            // Id
            builder.HasKey("Id");

            // First name
            builder
                .Property(u => u.FirstName)
                .HasMaxLength(MaxNameLength)
                .IsRequired();

            // Last name
            builder
                .Property(u => u.LastName)
                .HasMaxLength(MaxNameLength)
                .IsRequired();

            // Age
            builder
                .Property(u => u.Age)
                .HasMaxLength(MaxAge)
                .IsRequired();
        }
    }
}
