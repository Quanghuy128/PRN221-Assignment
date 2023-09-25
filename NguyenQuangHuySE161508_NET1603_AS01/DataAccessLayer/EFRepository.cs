using BusinessObjects.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class EfRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FucarRentingManagementContext _dbContext;
        private DbSet<T> _entities;

        public EfRepository(FucarRentingManagementContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected virtual DbSet<T> Entities => _entities ?? (_entities = _dbContext.Set<T>());

        public IEnumerable<T> Table => Entities.ToList();

        public IEnumerable<T> TableNoTracking => Entities.AsNoTracking().ToList();

        public bool Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Entities.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public T GetById(object id) => Entities.Find(id);

        public bool Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                Entities.Add(entity);
                return _dbContext.SaveChanges() > 0;
            }
            finally
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _dbContext.Entry(entity).State = EntityState.Modified;
                Entities.Update(entity);
                return _dbContext.SaveChanges() > 0;
            }
            finally
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
        }
    }
}
