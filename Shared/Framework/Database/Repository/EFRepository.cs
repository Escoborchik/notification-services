using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Framework.Database.Repository
{
    public abstract class EFRepository<TEntity, TDbContext>(DbContext Context) 
        : Repository<TEntity> 
        where TEntity : Entity<Guid> 
        where TDbContext : DbContext
    {

        protected DbSet<TEntity> Items => Context.Set<TEntity>();

        public async override Task<Guid> AddAsync(TEntity entity, CancellationToken ct)
        {
            await Items.AddAsync(entity, ct);
            return entity.Id;
        }

        public async override Task AddRangeAsync(IReadOnlyList<TEntity> entities, CancellationToken ct)
        {
            await Items.AddRangeAsync(entities, ct);
        }

        public override Task RemoveAsync(TEntity entity)
        {
            Items.Remove(entity);
            return Task.CompletedTask;
        }

        public override Task RemoveRangeAsync(IReadOnlyList<TEntity> entities)
        {
            Items.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public override async Task<bool> IsExistsAsync(Guid id, CancellationToken ct = default)
        {
            return await Items
                .AsNoTracking()
                .AnyAsync(o => o.Id == id, ct);
        }
        public override ValueTask<TEntity?> FindAsync(Guid id, CancellationToken ct)
        {
            return Items.FindAsync([id], ct);
        }

        public override async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken ct)
        {
            return await Items.ToListAsync(ct);
        }

        public override async Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await Items.Where(predicate).ToListAsync(ct);
        }

        public override async Task<TEntity> SingleAsync(CancellationToken ct)
        {
            return await Items.SingleAsync(ct);
        }

        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await Items.SingleAsync(predicate, ct);
        }

        public override Task<TEntity?> SingleOrDefaultAsync(CancellationToken ct)
        {
            return Items.SingleOrDefaultAsync(ct);
        }

        public override async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await Items.SingleOrDefaultAsync(predicate, ct);
        }

        public override async Task<TEntity> FirstAsync(CancellationToken ct)
        {
            return await Items.FirstAsync(ct);
        }

        public override async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await Items.FirstAsync(predicate, ct);
        }

        public override async Task<TEntity?> FirstOrDefaultAsync(CancellationToken ct)
        {
            return await Items.FirstOrDefaultAsync(ct);
        }

        public override async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await Items.FirstOrDefaultAsync(predicate, ct);
        }

        public override async Task<int> CountAsync(CancellationToken ct)
        {
            return await Items.CountAsync(ct);
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await Items.CountAsync(predicate, ct);
        }

        public override async Task<long> LongCountAsync(CancellationToken ct)
        {
            return await Items.LongCountAsync(ct);
        }

        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        {
            return await Items.LongCountAsync(predicate, ct);
        }

        public override async Task<IReadOnlyList<TResult>> Query<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> query, CancellationToken ct)
        {
            return await query(Items).ToListAsync(ct);
        }
    }
}
