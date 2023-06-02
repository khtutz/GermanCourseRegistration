using System.ComponentModel.DataAnnotations.Schema;

namespace GermanCourseRegistration.EntityModels;

public sealed class CourseMaterialOrderItem : Entity<Guid>
{
    public CourseMaterialOrderItem(
        Guid id, 
        Guid courseMaterialOrderId, 
        Guid courseMaterialId,
        int quantity)
        : base(id)
    {
        CourseMaterialOrderId = courseMaterialOrderId;
        CourseMaterialId = courseMaterialId;
        Quantity = quantity;
    }

    public CourseMaterialOrderItem() { }

    [ForeignKey("CourseMaterialOrder")]
    public Guid CourseMaterialOrderId { get; set; }

    [ForeignKey("CourseMaterial")]
    public Guid CourseMaterialId { get; set; }

    public int Quantity { get; set; }

    // Navigation properties
    public CourseMaterialOrder? CourseMaterialOrder { get; set; }

    public CourseMaterial? CourseMaterial { get; set; }
}
