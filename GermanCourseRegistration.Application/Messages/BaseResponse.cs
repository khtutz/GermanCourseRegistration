namespace GermanCourseRegistration.Application.Messages;

public record BaseResponse
{
    public bool IsTransactionSuccess { get; internal protected set; }

    public string Message { get; internal protected set; } = "Something went wrong!";
}
