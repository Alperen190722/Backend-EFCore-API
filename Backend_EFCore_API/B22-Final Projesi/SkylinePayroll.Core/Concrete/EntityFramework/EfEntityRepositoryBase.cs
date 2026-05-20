using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkylinePayroll.Core.Abstract;
using SkylinePayroll.Core.Entities;
using SkylinePayroll.Core.Utilities.IoC;
using System.Linq.Expressions;
using System.Security.Claims;

namespace SkylinePayroll.Core.Concrete.EntityFramework;

public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext
{
    protected readonly TContext _context;
    public EfEntityRepositoryBase(TContext context)
    {
        _context = context;
    }
    public async Task AddAsync(TEntity entity)
    {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedDate = DateTime.Now;
                baseEntity.IsDeleted = false;
            }

            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var deletedEntity = _context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        return filter == null
            ? await _context.Set<TEntity>().ToListAsync()
            : await _context.Set<TEntity>().Where(filter).ToListAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var updatedEntity = _context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
    }
}
