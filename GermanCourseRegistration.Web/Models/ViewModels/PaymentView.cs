namespace GermanCourseRegistration.Web.Models.ViewModels;

public class PaymentView
{
    public Guid RegistrationId { get; set; }

    public string PaymentMethod { get; set; }

    public decimal Amount { get; set; }
}
