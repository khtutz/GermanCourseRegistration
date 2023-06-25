using Microsoft.AspNetCore.Identity;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<IdentityUser>> GetAllAsync();
}
