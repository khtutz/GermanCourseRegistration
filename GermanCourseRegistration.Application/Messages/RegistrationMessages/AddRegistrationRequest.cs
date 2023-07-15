namespace GermanCourseRegistration.Application.Messages.RegistrationMessages;

public record AddRegistrationRequest(
    Guid Id,
    Guid StudentId,
    Guid CourseOfferId,
    string Status,
    DateTime CreatedOn);

public record AddOrderRequest
{
    public Guid Id { get; init; }
    public Guid RegistrationId { get; init; }
    public string OrderStatus { get; init; } = null!;
    public DateTime OrderDate { get; init; }
}

public record AddOrderItemsRequest(List<AddOrderItemRequest> OrderItems);

public record AddOrderItemRequest(
    Guid OrderId,
    Guid MaterialId,
    int Quantity);