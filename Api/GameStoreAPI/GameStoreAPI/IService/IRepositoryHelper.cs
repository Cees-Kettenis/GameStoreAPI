namespace GameStoreAPi.Services
{
    public interface IRepositoryHelper<T> : IDisposable
    {
        IQueryable<T> Query();
        T Create(T Entity);
        void Create(IEnumerable<T> Entites);
        T Update(T Entity);
        void Update(IEnumerable<T> Entites);
        void Delete(T Entity);
        void Delete(IEnumerable<T> Entities);
        void Delete(Guid id);
    }
}
