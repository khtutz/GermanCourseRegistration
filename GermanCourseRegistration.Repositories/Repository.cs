using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories;

public class Repository<T, TEntityKey>
    : IRepository<T, TEntityKey> where T : class
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public Repository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(TEntityKey id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<bool> AddAsync(T entity)
    {
        await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<T?> UpdateAsync(T entity, TEntityKey id)
    {
        var existingEntity = await dbContext.Set<T>().FindAsync(id);

        if (existingEntity == null)
        {
            return null;
        }

        dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        await dbContext.SaveChangesAsync();

        return existingEntity;
    }

    public async Task<T?> DeleteAsync(TEntityKey id)
    {
        var existingEntity = await dbContext.Set<T>().FindAsync(id);

        if (existingEntity == null)
        {
            return null;
        }

        dbContext.Set<T>().Remove(existingEntity);
        await dbContext.SaveChangesAsync();

        return existingEntity;
    }
}
