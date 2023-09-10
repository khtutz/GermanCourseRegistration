using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.DataContext;

public class GermanCourseAuthDbContext : IdentityDbContext
{
    public GermanCourseAuthDbContext(DbContextOptions<GermanCourseAuthDbContext> options)
        : base(options)
    {
        
    }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

        // Seed roles: SuperAdmin, Admin, and User
        var superAdminRoleId = "5722c8d0-39f5-40ea-aeca-03fb0ea1a8de";
        var adminRoleId = "f65d73d0-7b75-4b2b-a05d-dde3e322aab6";
        var userRoleId = "123dbdc8-5581-432b-b630-a4f3eed66c50";

		var roles = new List<IdentityRole>
		{
			new IdentityRole
			{
				Name = "SuperAdmin",
				NormalizedName = "SuperAdmin",
				Id = superAdminRoleId,
				ConcurrencyStamp = superAdminRoleId
			},
			new IdentityRole
			{
				Name = "Admin",
				NormalizedName = "Admin",
				Id = adminRoleId,
				ConcurrencyStamp = adminRoleId
			},
			new IdentityRole
			{
				Name = "User",
				NormalizedName = "User",
				Id = userRoleId,
				ConcurrencyStamp = userRoleId
			}
		};

		builder.Entity<IdentityRole>().HasData(roles);

		// Seed SuperAdminUser
		var superAdminId = "cb8c6732-40c9-45d3-af52-1bda250cb9db";
		var superAdminUser = new IdentityUser
		{
			UserName = "superadmin@deutchinstitut.com",
			Email = "superadmin@deutchinstitut.com",
			NormalizedEmail = "superadmin@deutchinstitut.com".ToUpper(),
			NormalizedUserName = "superadmin@deutchinstitut.com".ToUpper(),
			Id = superAdminId
		};

		const string password = "";

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
			.HashPassword(superAdminUser, password);

		builder.Entity<IdentityUser>().HasData(superAdminUser);

		// Add all roles to SuperAdminUser
		var superAdminRoles = new List<IdentityUserRole<string>>
		{
			new IdentityUserRole<string>()
			{
				RoleId = superAdminRoleId,
				UserId = superAdminId
			},
			new IdentityUserRole<string>()
			{
				RoleId = adminRoleId,
				UserId = superAdminId
			},	
			new IdentityUserRole<string>()
			{
				RoleId = userRoleId,
				UserId = superAdminId
			}
		};

		builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
	}
}
