using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GermanCourseRegistration.EntityModels;

public sealed class Payment : Entity<Guid>
{
    public const string PaymentSuccess = "Success";
    public const string PaymentFailed = "Failed";

    public Payment(
        Guid id, 
        string paymentMethod,
        decimal amount,
        string paymentStatus)
        : base(id)
    {
        PaymentMethod = paymentMethod;
        Amount = amount;
        PaymentStatus = paymentStatus;
    }

    public Payment() { }

    [StringLength(25)]
    public string PaymentMethod { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [StringLength(25)]
    public string PaymentStatus { get; set; }

    // Navigation properties
    public Registration? Registration { get; set; }
}
