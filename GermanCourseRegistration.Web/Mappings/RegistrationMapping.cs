using GermanCourseRegistration.Application.Messages.RegistrationMessages;

namespace GermanCourseRegistration.Web.Mappings;

public static class RegistrationMapping
{
    public static AddRegistrationRequest MapToRegistrationAddRequest(
        Guid id,
        Guid studentId,
        Guid courseOfferId,
        string status,
        DateTime createdOn)
    {
        var request = new AddRegistrationRequest(
            id,
            studentId,
            courseOfferId,
            status,
            createdOn);

        return request;
    }

    public static AddOrderRequest MapToOrderRequest(
        Guid id,
        Guid registrationId,
        string orderStatus,
        DateTime orderDate)
    {
        var request = new AddOrderRequest()
        {
            Id = id,
            RegistrationId = registrationId,
            OrderStatus = orderStatus,
            OrderDate = orderDate
        };

        return request;
    }

    public static AddOrderItemRequest MapToOrderItemRequest(
        Guid orderId,
        Guid materialId,
        int quantity)
    {
        var request = new AddOrderItemRequest(orderId, materialId, quantity);

        return request;
    }
}
