namespace GermanCourseRegistration.Application.Messages;

public record BaseResponse
{
    public bool IsTransactionSuccess { get; internal set; } = true;

    public string Message { get; internal set; } = "Something went wrong!";
}
