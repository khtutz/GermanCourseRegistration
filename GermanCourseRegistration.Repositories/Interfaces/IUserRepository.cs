using Microsoft.AspNetCore.Identity;

namespace GermanCourseRegistration.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<IdentityUser>> GetAll();
}
