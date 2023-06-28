using AutoMapper;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GermanCourseRegistration.Web.Controllers;

public class StudentPersonalInformationController : Controller
{
    private readonly IStudentService studentService;
    private readonly UserManager<IdentityUser> userManager;
    private readonly IMapper mapper;

    public StudentPersonalInformationController(
        IStudentService studentService,
        UserManager<IdentityUser> userManager,
        IMapper mapper)
    {
        this.studentService = studentService;
        this.userManager = userManager;
        this.mapper = mapper;
    }

    [HttpGet]
	public async Task<IActionResult> Add()
	{
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        // Load the existing information if registered previously
        StudentResult studentResult = await studentService.GetByIdAsync(loginId);

        bool isExistingStudent = studentResult.Student != null;

        var viewModel = isExistingStudent 
            ? mapper.Map<StudentView>(studentResult) 
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
            await studentService.AddAsync(
                viewModel.Salutation,
                viewModel.FirstName,
                viewModel.LastName,
                viewModel.Birthday,
                viewModel.Gender,
                viewModel.Mobile,
                viewModel.Email,
                viewModel.Address,
                viewModel.PostalCode,
                DateTime.Now);
        }
        else
        {
           await studentService.UpdateAsync(
                loginId,
                viewModel.Salutation,
                viewModel.FirstName,
                viewModel.LastName,
                viewModel.Birthday,
                viewModel.Gender,
                viewModel.Mobile,
                viewModel.Email,
                viewModel.Address,
                viewModel.PostalCode,
                DateTime.Now);
        }

        return RedirectToAction("Add", "CourseSelection");
    }
}
