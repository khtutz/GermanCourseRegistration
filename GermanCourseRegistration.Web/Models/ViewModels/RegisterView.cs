using System.ComponentModel.DataAnnotations;

namespace GermanCourseRegistration.Web.Models.ViewModels;

public class RegisterView
{
	[Required]
	public string Username { get; set; } = null!;

	[Required]
	[EmailAddress]
	public string Email { get; set; } = null!;

	[Required]
	[MinLength(6, ErrorMessage = "Password has to be at least 6 characters")]
	public string Password { get; set; } = null!;
}
