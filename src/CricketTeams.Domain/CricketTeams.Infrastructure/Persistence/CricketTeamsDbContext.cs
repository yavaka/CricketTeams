namespace CricketTeams.Infrastructure.Persistence
{
    using CricketTeams.Domain.Models.Coaches;
    using CricketTeams.Domain.Models.Matches;
    using CricketTeams.Domain.Models.Players;
    using CricketTeams.Domain.Models.Scores;
    using CricketTeams.Domain.Models.Stadiums;
    using CricketTeams.Domain.Models.Teams;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics;
    using System.Reflection;

    internal class CricketTeamsDbContext : DbContext
    {
        public CricketTeamsDbContext(DbContextOptions<CricketTeamsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; } = default!;
        public DbSet<Team> Teams { get; set; } = default!;
        public DbSet<TeamPlayers> TeamPlayers { get; set; } = default!;
        public DbSet<Coach> Coaches { get; set; } = default!;
        public DbSet<Stadium> Stadiums { get; set; } = default!;
        public DbSet<Match> Matches { get; set; } = default!;
        public DbSet<Umpire> Umpires { get; set; } = default!;
        public DbSet<Score> ScoresFromMatches { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            Debugger.Launch();

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
