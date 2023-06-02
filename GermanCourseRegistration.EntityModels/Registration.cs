using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GermanCourseRegistration.EntityModels;

public sealed class Registration : Entity<Guid>
{
    public const string Unpaid = "Unpaid";
    public const string Completed = "Completed";

    public Registration(
        Guid id, 
        Guid studentId, 
        Guid courseOfferId, 
        string status,
        DateTime createdOn,
        DateTime lastModifiedOn)
        : base(id)
    {
        StudentId = studentId;
        CourseOfferId = courseOfferId;
        Status = status;
        CreatedOn = createdOn;
        LastModifiedOn = lastModifiedOn;
    }

    public Registration() { }

    [ForeignKey("Student")]
    public Guid StudentId { get; set; }

    [ForeignKey("CourseOffer")]
    public Guid CourseOfferId { get; set; }

    [ForeignKey("Payment")]
    public Guid? PaymentId { get; set; }

    [StringLength(25)]
    public string Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    // Navigation properties
    public Student? Student { get; set; }

    public CourseOffer? CourseOffer { get; set; }

    public Payment? Payment { get; set; }

    public CourseMaterialOrder? CourseMaterialOrder { get; set; }
}
