using CSharpFunctionalExtensions;
using System.Linq.Expressions;

namespace Framework.Database.Repository
{
    public interface IRepository<TEntity> where TEntity : Entity<Guid>
    {
        Task<Guid> AddAsync(TEntity entity, CancellationToken ct);
        Task AddRangeAsync(IReadOnlyList<TEntity> entities, CancellationToken ct);
        Task<int> CountAsync(CancellationToken ct);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
        Task<bool> IsExistsAsync(Guid id, CancellationToken ct);
        ValueTask<TEntity?> FindAsync(Guid id, CancellationToken ct);
        Task<TEntity> FirstAsync(CancellationToken ct);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
        Task<TEntity?> FirstOrDefaultAsync(CancellationToken ct);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
        Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken ct);
        Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
        Task<long> LongCountAsync(CancellationToken ct);
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
        Task<IReadOnlyList<TResult>> Query<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query, CancellationToken ct);
        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(IReadOnlyList<TEntity> entities);
        Task<TEntity> SingleAsync(CancellationToken ct);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
        Task<TEntity?> SingleOrDefaultAsync(CancellationToken ct);
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
    }
}