using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Infrastructure;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    private readonly ObiManagementDbContext db;
    private DbSet<T> dbSet;

    public Repository(ObiManagementDbContext context)
    {
        db = context;
        dbSet = db.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }
    public async Task<T> GetByIdAsync(object id)
    {
        return await dbSet.FindAsync(id);
    }
    public async Task InsertAsync(T obj)
    {
        obj.IsValid = true;
        await dbSet.AddAsync(obj);
    }
    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
    {
        return this.dbSet.Where(predicate);
    }
    public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await this.dbSet.Where(predicate).ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await this.dbSet.AnyAsync(predicate);
    }
    public async Task SaveAsync()
    {
        await db.SaveChangesAsync();
    }
    public async Task DeleteAsync(object id)
    {
        T getObjById = await dbSet.FindAsync(id);
        dbSet.Remove(getObjById);
    }
    public Task UpdateAsync(T obj)
    {
        obj.IsValid = true;
        db.Entry(obj).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IIncludableQueryable<T, object> query = null;

        if (includes.Length > 0)
        {
            query = dbSet.Include(includes[0]);
        }
        for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
        {
            query = query.Include(includes[queryIndex]);
        }
        if (query is null)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        return await query.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IIncludableQueryable<T, object> query = null;

        if (includes.Length > 0)
        {
            query = dbSet.Include(includes[0]);
        }
        for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
        {
            query = query.Include(includes[queryIndex]);
        }
        if (query is null)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }
        return await query.FirstOrDefaultAsync(predicate);
    }
}