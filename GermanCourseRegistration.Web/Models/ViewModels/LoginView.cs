using System.ComponentModel.DataAnnotations;

namespace GermanCourseRegistration.Web.Models.ViewModels;

public class LoginView
{
	[Required]
	public string Username { get; set; } = null!;

	[Required]
	[MinLength(6, ErrorMessage = "Password has to be at least 6 characters")]
	public string Password { get; set; } = null!;

	public string? ReturnedUrl { get; set; }
}
