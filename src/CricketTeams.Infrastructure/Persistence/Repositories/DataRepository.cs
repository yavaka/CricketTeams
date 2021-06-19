namespace CricketTeams.Infrastructure.Persistence.Repositories
{
    using CricketTeams.Application.Contracts;
    using CricketTeams.Domain.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DataRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IAggregateRoot
    {
        private readonly CricketTeamsDbContext _data;

        public DataRepository(CricketTeamsDbContext data) => this._data = data;

        public IQueryable<TEntity> All() => this._data.Set<TEntity>();

        public Task<int> SaveChanges(CancellationToken cancellationToken = default)
            => this._data.SaveChangesAsync(cancellationToken);
    }
}
