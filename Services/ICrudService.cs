namespace BakeryOps.API.Services
{
    public interface ICrudService<T> where T : class
    {
        Task<T?> Create(T entity);
        Task<T?> GetById(Guid id);

        Task<List<T>> GetAll();
        Task<T?> Update(T entity);
        Task<bool> Delete(Guid id);
        Task<bool> Delete(int id);
    }
}