namespace DataAccessLayer
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> Table { get; }
        IEnumerable<T> TableNoTracking { get; }
        T GetById(object id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
