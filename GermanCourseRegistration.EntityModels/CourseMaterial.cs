using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GermanCourseRegistration.EntityModels;
public sealed class CourseMaterial : Entity<Guid>
{
    public const string PhysicalCopyCategory = "Physical Copy";
    public const string DigitalCopyCategory = "Digital Copy";
    public const string AudioCopyCategory = "Audio Copy";

    public CourseMaterial(
        Guid id,
        string name,
        string description,
        string category,
        decimal price,
        Guid createdBy,
        DateTime createdOn,
        Guid lastModifiedBy,
        DateTime lastModifiedOn)
        : base(id)
    {
        Name = name;
        Description = description;
        Category = category;
        Price = price;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
        LastModifiedBy = lastModifiedBy;
        LastModifiedOn = lastModifiedOn;
    }

    public CourseMaterial() { }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string Category { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    // Navigation property
    public IEnumerable<CourseMaterialOrderItem>? CourseMaterialOrderItems { get; set; }
}
