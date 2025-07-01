using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetEntityWithSpec(ISpecification<T> spec); 
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);


        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T?> GetByIdAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> SaveAllAsync();
        bool Exists(int id);
    }
}
