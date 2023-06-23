using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GermanCourseAuthDbContext authDbContext;

    public UserRepository(GermanCourseAuthDbContext authDbContext)
    {
        this.authDbContext = authDbContext;
    }

    public async Task<IEnumerable<IdentityUser>> GetAll()
    {
        // Return all users except super admin
        var users = await authDbContext.Users.ToListAsync();
        var superAdminUser = await authDbContext.Users
            .FirstOrDefaultAsync(x => x.Email == "superadmin@deutchinstitut.com");

        if (superAdminUser != null)
        {
            users.Remove(superAdminUser);
        }

        return users;
    }
}
