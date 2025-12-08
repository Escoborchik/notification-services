using CSharpFunctionalExtensions;
using System.Linq.Expressions;

namespace Framework.Database.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<Guid>
{
    public abstract Task<Guid> AddAsync(TEntity entity, CancellationToken ct);

    public abstract Task AddRangeAsync(IReadOnlyList<TEntity> entities, CancellationToken ct);

    public abstract Task RemoveAsync(TEntity entity);

    public abstract Task RemoveRangeAsync(IReadOnlyList<TEntity> entities);

    public abstract Task<bool> IsExistsAsync(Guid id, CancellationToken ct);

    public abstract ValueTask<TEntity?> FindAsync(Guid id, CancellationToken ct);

    public abstract Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken ct);

    public abstract Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

    public abstract Task<TEntity> SingleAsync(CancellationToken ct);

    public abstract Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

    public abstract Task<TEntity?> SingleOrDefaultAsync(CancellationToken ct);

    public abstract Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

    public abstract Task<TEntity> FirstAsync(CancellationToken ct);

    public abstract Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

    public abstract Task<TEntity?> FirstOrDefaultAsync(CancellationToken ct);

    public abstract Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

    public abstract Task<int> CountAsync(CancellationToken ct);

    public abstract Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

    public abstract Task<long> LongCountAsync(CancellationToken ct);

    public abstract Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

    public abstract Task<IReadOnlyList<TResult>> Query<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query, CancellationToken ct);
}