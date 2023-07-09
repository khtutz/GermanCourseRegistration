using GermanCourseRegistration.Application.Messages.StudentMessages;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.HelperServices;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GermanCourseRegistration.Web.Controllers;

public class StudentPersonalInformationController : Controller
{
    private readonly IStudentService studentService;
    private readonly UserManager<IdentityUser> userManager;

    public StudentPersonalInformationController(
        IStudentService studentService,
        UserManager<IdentityUser> userManager)
    {
        this.studentService = studentService;
        this.userManager = userManager;
    }

    [HttpGet]
	public async Task<IActionResult> Add()
	{
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        var response = await studentService.GetByIdAsync(new GetStudentByIdRequest(loginId));

        bool isExistingStudent = response == null || response.Student == null ? false : true;

        var viewModel = isExistingStudent
            ? StudentPersonalInformationMapping.MapToViewModel(response!)
            : new StudentView();

        // Load the drop down list items, and properties
        var salutations = studentService.GetSalutations();
        viewModel.AvailableSalutations = salutations.Select(s => new SelectListItem
        {
            Text = s,
            Value = s
        });
        viewModel.IsExistingStudent = isExistingStudent;

        return View(viewModel);
	}

    [HttpPost]
    public async Task<IActionResult> Add(StudentView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        if (!viewModel.IsExistingStudent)
        {
            var request = StudentPersonalInformationMapping.MapToAddRequest(
                viewModel, loginId, DateTime.Now);

            await studentService.AddAsync(request);
        }
        else
        {
            var request = StudentPersonalInformationMapping.MapToUpdateRequest(
                viewModel, loginId, DateTime.Now);

            await studentService.UpdateAsync(request);
        }

        return RedirectToAction("Add", "CourseSelection");
    }
}
