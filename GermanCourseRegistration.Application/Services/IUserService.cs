using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface IUserService
{
    Task<IEnumerable<UserResult>> GetAllAsync();

    Task<bool> AddAsync(
        string userName,
        string email,
        string password,
        bool adminRoleChecked);

    Task<bool> DeleteAsync(Guid id);
}
