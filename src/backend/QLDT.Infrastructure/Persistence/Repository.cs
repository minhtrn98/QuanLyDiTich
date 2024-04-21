using Microsoft.EntityFrameworkCore;

namespace QLDT.Infrastructure.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(DbSet<TEntity> dbSet)
    {
        _dbSet = dbSet;
    }

    public IQueryable<TEntity> Query => _dbSet;

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}
