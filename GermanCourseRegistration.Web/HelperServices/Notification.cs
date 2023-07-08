namespace GermanCourseRegistration.Web.HelperServices;

public static class Notification
{
    public static readonly Dictionary<short, string> ModalMessage = 
        new Dictionary<short, string>()
    {
        [0] = "ErrorMessage",
        [1] = "SuccessMessage"
    };
}
