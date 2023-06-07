using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.EntityModels.Enums;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using GermanCourseRegistration.Web.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        Student? student = null;

        try
        {
            student = await studentRepository.GetByIdAsync(loginId);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);

            // Show error notification
            return View(new StudentView());
        }

        bool isExistingStudent = student != null;

        var model = isExistingStudent ? MapStudentToViewModel(student!) : new StudentView();

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

    [HttpPost]
    public async Task<IActionResult> Add(StudentView model)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);
        var student = MapViewModelToStudent(model, loginId);

        if (model.IsExistingStudent)
        {
            student.LastModifiedOn = DateTime.Now;

            Student? updatedStudent = null;

            try
            {
                updatedStudent = await studentRepository.UpdateAsync(student);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
                WriteLine(ex.StackTrace);
            }

            if (updatedStudent == null)
            {
                // Show error notification
                return View(model);
            }
        }
        else
        {
            student.CreatedOn = DateTime.Now;

            bool isAdded = false;

            try
            {
                isAdded = await studentRepository.AddAsync(student);
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
                WriteLine(ex.StackTrace);
            }

            if (!isAdded)
            {
                // Show error notification
                return View(model);
            }
        }
        
        return RedirectToAction("Add", "CourseSelection");
    }

    //
    // Mapping Methods
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
