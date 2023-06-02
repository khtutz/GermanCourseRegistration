namespace GermanCourseRegistration.Web.Models.ViewModels;

public class CourseScheduleForStudentView
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ClassType { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public CourseView? Course { get; set; }
}
