using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GermanCourseRegistration.EntityModels;

public sealed class CourseMaterialOrder : Entity<Guid>
{
    public const string OrderUnpaid = "Unpaid";
    public const string OrderPaid = "Paid";

    public CourseMaterialOrder(
        Guid id,
        Guid registrationId,
        string orderStatus,
        DateTime orderDate)
        : base(id)
    {
        RegistrationId = registrationId;
        OrderStatus = orderStatus;
        OrderDate = orderDate;
    }

    public CourseMaterialOrder() { }

    [ForeignKey("Registration")]
    public Guid RegistrationId { get; set; }

    [StringLength(25)]
    public string OrderStatus { get; set; }

    public DateTime OrderDate { get; set; }

    // Navigation properties
    public Registration? Registration { get; set; }

    public IEnumerable<CourseMaterialOrderItem>? CourseMaterialOrderItems { get; set; }
}
