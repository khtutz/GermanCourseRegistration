using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GermanCourseRegistration.EntityModels;

public sealed class Timetable : Entity<Guid>
{
    public static readonly List<string> DaysOfWeek = new List<string>
    {
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday"
    };

    public Timetable(
        Guid id, 
        Guid courseOfferId,
        string dayName,
        int startTimeHour,
        int startTimeMinute,
        int endTimeHour,
        int endTimeMinute)
        : base(id)
    {
        CourseOfferId = courseOfferId;
        DayName = dayName;
        StartTimeHour = startTimeHour;
        StartTimeMinute = startTimeMinute;
        EndTimeHour = endTimeHour;
        EndTimeMinute = endTimeMinute;
    }

    public Timetable() { }

    [ForeignKey("CourseOffer")]
    public Guid CourseOfferId { get; set; }

    [StringLength(10)]
    public string DayName { get; set; }

    [Range(0, 24)]
    public int StartTimeHour { get; set; }

    [Range(0, 60)]
    public int StartTimeMinute { get; set; }

    [Range(0, 24)]
    public int EndTimeHour { get; set; }

    [Range(0, 60)]
    public int EndTimeMinute { get; set; }

    // Navigation properties
    public CourseOffer? CourseOffer { get; set; }

}
