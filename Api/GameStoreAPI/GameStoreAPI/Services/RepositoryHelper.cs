using GameStoreAPi.Data;
using GameStoreAPi.Modals.SKU;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPi.Services
{
    public class RepositoryHelper<T> : IRepositoryHelper<T> where T : DomainObject
    {
        internal AppDBContext context;
        internal DbSet<T> dbSet;

        public RepositoryHelper(AppDBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> Query()
        {
            return dbSet.AsQueryable().AsNoTracking();
        }

        public T Create(T Entity)
        {
            Entity.CreatedDate = DateTime.Now;
            dbSet.Add(Entity);
            context.SaveChanges();
            return Entity;
        }

        public void Create(IEnumerable<T> Entities)
        {
            foreach(var Entity in Entities)
            {
                Entity.CreatedDate = DateTime.Now;
            }
            dbSet.AddRange(Entities);
            context.SaveChanges();
        }

        public T Update(T Entity)
        {
            Entity.UpdatedDate = DateTime.Now;
            dbSet.Attach(Entity);
            context.Entry(Entity).State = EntityState.Modified;
            context.SaveChanges();
            return Entity;
        }

        public void Update(IEnumerable<T> Entities)
        {
            foreach(var Entity in Entities)
            {
                Entity.UpdatedDate = DateTime.Now;
                dbSet.Attach(Entity);
                context.Entry(Entity).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(T Entity)
        {
            if (context.Entry(Entity).State == EntityState.Detached)
            {
                dbSet.Attach(Entity);
            }
            dbSet.Remove(Entity);
            context.SaveChanges();
        }

        public void Delete(IEnumerable<T> Entities)
        {
            foreach(var Entity in Entities)
            {
                if (context.Entry(Entity).State == EntityState.Detached)
                {
                    dbSet.Attach(Entity);
                }
                dbSet.Remove(Entity);
            }
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            T entity = dbSet.Find(id);
            Delete(entity);
        }

        private bool disposed = false;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
