namespace GermanCourseRegistration.Web.Models.ViewModels;

public class CourseRegistrationView
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }

    public Guid? PaymentId { get; set; }

    public string Status { get; set; } = "Unpaid";

    public Guid SelectedScheduleId { get; set; }

    public IEnumerable<Guid>? SelectedMaterialIds { get; set; }

    public IEnumerable<CourseScheduleView> CourseSchedules { get; set; } = 
        new List<CourseScheduleView>();

    public IEnumerable<CourseMaterialView> CourseMaterials { get; set; } = 
        new List<CourseMaterialView>();
}
