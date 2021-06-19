namespace CricketTeams.Infrastructure
{
    using CricketTeams.Infrastructure.Persistence;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<CricketTeamsDbContext>(opt => opt
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b =>b.MigrationsAssembly(typeof(CricketTeamsDbContext).Assembly.FullName)));
    }
}
