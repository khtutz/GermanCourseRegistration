using GermanCourseRegistration.Application.Messages.PaymentMessages;

namespace GermanCourseRegistration.Web.Mappings;

public static class PaymentMapping
{
    public static AddPaymentRequest MapToAddRequest(
        string paymentMethod, decimal amount, string paymentStatus)
    {
        var request = new AddPaymentRequest(
            paymentMethod,
            amount,
            paymentStatus);

        return request;
    }
}
