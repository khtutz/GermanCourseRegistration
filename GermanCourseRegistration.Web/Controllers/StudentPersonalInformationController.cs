using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.EntityModels.Enums;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using GermanCourseRegistration.Web.Services;

namespace GermanCourseRegistration.Web.Controllers;

public class StudentPersonalInformationController : Controller
{
    private readonly IStudentRepository studentRepository;
    private readonly UserManager<IdentityUser> userManager;

    public StudentPersonalInformationController(
        IStudentRepository studentRepository,
        UserManager<IdentityUser> userManager)
    {
        this.studentRepository = studentRepository;
        this.userManager = userManager;
    }

    [HttpGet]
	public async Task<IActionResult> Add()
	{
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        // Load the existing information if registered previously
        var student = await studentRepository.GetAsync(loginId);
        bool isExistingStudent = student != null;

        var model = isExistingStudent ? MapStudentToViewModel(student!) : new();

        // Load the selection data and properties
        var salutations = Enum.GetNames(typeof(Salutation));
		model.AvailableSalutations = salutations.Select(s => new SelectListItem
		{
			Text = s,
			Value = s
		});
        model.IsExistingStudent = isExistingStudent;

		return View(model);
	}

    private StudentView MapStudentToViewModel(Student student)
    {
        return new StudentView
        {
            Id = student.Id,
            Salutation = student.Salutation,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Birthday = student.Birthday,
            Gender = student.Gender,
            Mobile = student.Mobile,
            Email = student.Email,
            Address = student.Address,
            PostalCode = student.PostalCode
        };
    }

    [HttpPost]
    public async Task<IActionResult> Add(StudentView model)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);
        var student = MapViewModelToStudent(model, loginId);

        if (model.IsExistingStudent)
        {
            student.LastModifiedOn = DateTime.Now;
            await studentRepository.UpdateAsync(student);
        }
        else
        {
            student.CreatedOn = DateTime.Now;
            await studentRepository.AddAsync(student);
        }
        
        return RedirectToAction("Add", "CourseSelection");
    }

    private Student MapViewModelToStudent(StudentView model, Guid loginId)
    {
        return new Student
        {
            Id = loginId,
            Salutation = model.Salutation,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Birthday = model.Birthday,
            Gender = model.Gender,
            Mobile = model.Mobile,
            Email = model.Email,
            Address = model.Address,
            PostalCode = model.PostalCode
        };
    }
}
