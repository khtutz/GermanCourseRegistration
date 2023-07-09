namespace GermanCourseRegistration.Application.Messages.PaymentMessages;

public record AddPaymentRequest(
    string PaymentMethod, 
    decimal Amount, 
    string PaymentStatus);
