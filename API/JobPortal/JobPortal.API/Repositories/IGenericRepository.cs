namespace JobPortal.API.Repositories
{
    public interface IGenericRepository<T> where T : class

    {
        Task<List<T>> GetAll();

        Task<T?> GetById(int id);

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);
        
    }
}