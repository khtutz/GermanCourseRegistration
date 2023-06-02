using System.ComponentModel.DataAnnotations;

namespace GermanCourseRegistration.EntityModels;

public sealed class Course : Entity<Guid>
{
	public Course(
        Guid id, 
        string level, 
        int part,
        string description,
        Guid createdBy,
        DateTime createdOn,
        Guid lastModifiedBy,
        DateTime lastModifiedOn) : base(id)
    {
        Level = level;
        Part = part;
        Description = description;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
        LastModifiedBy = lastModifiedBy;
        LastModifiedOn = lastModifiedOn;
    }

    public Course() { }

    [StringLength(2)]
    public string Level { get; set; }

	public int Part { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    // Navigation property
    public IEnumerable<CourseOffer>? CourseOffers { get; set; }
}
