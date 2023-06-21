namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface IRepository<T, TEntityKey>
{
    Task<T?> GetByIdAsync(TEntityKey id);

    Task<IEnumerable<T>> GetAllAsync();

    Task<bool> AddAsync(T entity);

    Task<T?> UpdateAsync(T entity, TEntityKey id);

    Task<T?> DeleteAsync(TEntityKey id);
}
