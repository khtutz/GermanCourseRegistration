using System.ComponentModel.DataAnnotations;

namespace GermanCourseRegistration.Web.Models.ViewModels;

public class CourseView
{
	public Guid Id { get; set; }

	[Required(ErrorMessage = "Level is required.")]
	public string Level { get; set; }

	[Required(ErrorMessage = "Part is required.")]
	public int Part { get; set; }

	[Required(ErrorMessage = "Mode is required.")]
	public string Mode { get; set; }

	public string? Description { get; set; }
}
