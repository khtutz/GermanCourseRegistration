using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GermanCourseRegistration.EntityModels;

public sealed class CourseOffer : Entity<Guid>
{
    public const string OnlineClass = "Online";
    public const string InPersonClass = "In Person";

    public CourseOffer(
        Guid id,
        Guid courseId,
        string name,
        string classType,
        decimal cost,
        DateTime startDate,
        DateTime endDate,
        Guid createdBy,
        DateTime createdOn,
        Guid lastModifiedBy,
        DateTime lastModifiedOn)
        : base(id)
    {
        CourseId = courseId;
        Name = name;
        ClassType = classType;
        Cost = cost;
        StartDate = startDate;
        EndDate = endDate;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
        LastModifiedBy = lastModifiedBy;
        LastModifiedOn = lastModifiedOn;
    }

    public CourseOffer() { }

    [ForeignKey("Course")]
    public Guid CourseId { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(20)]
    public string ClassType { get; set; }

    [Column(TypeName = "money")]
    public decimal Cost { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    // Navigation Properties
    public Course? Course { get; set; }

    public IEnumerable<Timetable>? Timetables { get; set; }

    public IEnumerable<Registration>? Registrations { get; set; }
}
