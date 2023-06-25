using Microsoft.AspNetCore.Identity;

namespace GermanCourseRegistration.Application.ServiceResults;

public record UserResult(IdentityUser? User);
