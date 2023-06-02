using System.ComponentModel.DataAnnotations;

namespace GermanCourseRegistration.Web.Models.ViewModels;

public class TimetableView
{
    [Required(ErrorMessage = "Day of the week is required.")]
    public string DayName { get; set; }

    [Required(ErrorMessage = "End time in hour is required.")]
    public int StartTimeHour { get; set; }

    [Required(ErrorMessage = "Start time in minute is required.")]
    public int StartTimeMinute { get; set; }

    [Required(ErrorMessage = "End time in hour is required.")]
    public int EndTimeHour { get; set; }

    [Required(ErrorMessage = "End time in minute is required.")]
    public int EndTimeMinute { get; set; }
}
