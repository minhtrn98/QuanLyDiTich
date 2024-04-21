namespace QLDT.Domain.UnitOfWork;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query { get; }
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
