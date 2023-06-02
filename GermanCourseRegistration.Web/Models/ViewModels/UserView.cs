namespace GermanCourseRegistration.Web.Models.ViewModels;

public class UserView
{
    public List<UserIndividualView> Users { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool AdminRoleChecked { get; set; }
}
