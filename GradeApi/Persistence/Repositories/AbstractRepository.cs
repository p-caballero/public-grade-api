namespace GradeApi.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public abstract class AbstractRepository<TEntity>
        where TEntity : class, new()
    {
        protected GradeDbContext DbContext { get; }

        protected AbstractRepository(GradeDbContext context)
        {
            DbContext = context;
        }

        public IQueryable<TEntity> GetAll(bool trackChanges = true)
        {
            return trackChanges
                ? DbContext.Set<TEntity>()
                : DbContext.Set<TEntity>().AsNoTracking();
        }

        public void Add(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Detached;
            DbContext.Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Detached;
            DbContext.Update(entity);
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }
    }
}
