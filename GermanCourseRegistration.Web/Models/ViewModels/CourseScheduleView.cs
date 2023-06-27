using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GermanCourseRegistration.Web.Models.ViewModels;

public class CourseScheduleView
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Class type is required.")]
    public string ClassType { get; set; }

    [Required(ErrorMessage = "Cost is required.")]
    public decimal Cost { get; set; }

    [Required(ErrorMessage = "Start date is required.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date is required.")]
    public DateTime EndDate { get; set; }

    // Navigation properties
    public CourseView? Course { get; set; }

    public TimetableView Timetable { get; set; } = new TimetableView();

    // UI display properties
    public IEnumerable<string> DaysOfWeek { get; set; }

    public IEnumerable<string> SelectDays { get; set; }

    // Drop down list properties
    public IEnumerable<SelectListItem>? AvailableClassTypes { get; set; }

    public IEnumerable<SelectListItem>? AvailableCourseLevels { get; set; }
}
