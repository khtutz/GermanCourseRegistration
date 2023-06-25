using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Web.Models.ViewModels;

namespace GermanCourseRegistration.Web.Mappings;

public static class MapperProfiles
{
    public static List<UserIndividualView> MapUserResultsToUserIndividualViews(
        IEnumerable<UserResult> users)
    {
        var userIndividualViews = new List<UserIndividualView>();

        foreach (var user in users)
        {
            if (user.User != null)
            {
                userIndividualViews.Add(new UserIndividualView
                {
                    Id = Guid.Parse(user.User.Id),
                    Username = user.User.UserName!,
                    EmailAddress = user.User.Email!
                });
            }
        }

        return userIndividualViews;
    }
}
