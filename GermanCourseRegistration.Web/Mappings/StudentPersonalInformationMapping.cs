using GermanCourseRegistration.Application.Messages.StudentMessages;
using GermanCourseRegistration.Web.Models.ViewModels;

namespace GermanCourseRegistration.Web.Mappings;

public static class StudentPersonalInformationMapping
{
    public static StudentView MapToViewModel(GetStudentByIdResponse response)
    {
        if (response == null)
        {
            throw new ArgumentNullException(
                nameof(response),
                "The response object cannot be null.");
        }

        if (response.Student == null)
        {
            throw new ArgumentNullException(
                nameof(response.Student),
                "The Student property cannot be null.");
        }

        var viewModel = new StudentView()
        {
            Id = response.Student.Id,
            Salutation = response.Student.Salutation,
            FirstName = response.Student.FirstName,
            LastName = response.Student.LastName,
            Birthday = response.Student.Birthday,
            Gender = response.Student.Gender,
            Mobile = response.Student.Mobile,
            Email = response.Student.Email,
            Address = response.Student.Address,
            PostalCode = response.Student.PostalCode
        };

        return viewModel;
    }

    public static AddStudentRequest MapToAddRequest(
        StudentView viewModel, Guid studentId, DateTime createdOn)
    {
        var request = new AddStudentRequest(
            studentId,
            viewModel.Salutation,
            viewModel.FirstName,
            viewModel.LastName,
            viewModel.Birthday,
            viewModel.Gender,
            viewModel.Mobile,
            viewModel.Email,
            viewModel.Address,
            viewModel.PostalCode,
            createdOn
        );

        return request;
    }

    public static UpdateStudentRequest MapToUpdateRequest(
        StudentView viewModel, Guid studentId, DateTime lastModifiedOn)
    {
        var request = new UpdateStudentRequest(
            studentId,
            viewModel.Salutation,
            viewModel.FirstName,
            viewModel.LastName,
            viewModel.Birthday,
            viewModel.Gender,
            viewModel.Mobile,
            viewModel.Email,
            viewModel.Address,
            viewModel.PostalCode,
            lastModifiedOn
        );

        return request;
    }
}
