using GermanCourseRegistration.Application.Messages.CourseMessages;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.HelperServices;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GermanCourseRegistration.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCourseController : Controller
{
    private readonly IAdminCourseService adminCourseService;
    private readonly UserManager<IdentityUser> userManager;

    public AdminCourseController(
        IAdminCourseService adminCourseService,
        UserManager<IdentityUser> userManager)
    {
        this.adminCourseService = adminCourseService;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var response = await adminCourseService.GetAllAsync();

        var viewModels = CourseMapping.MapToViewModels(response);

        return View(viewModels);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        var request = CourseMapping.MapToAddRequest(viewModel, loginId, DateTime.Now);

        var response = await adminCourseService.AddAsync(request);

        short key = Convert.ToInt16(response.IsTransactionSuccess);
        TempData[Notification.ModalMessage[key]] = response.Message;

        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var response = await adminCourseService.GetByIdAsync(new GetCourseByIdRequest(id));

        if (response.Course == null)
        {
            TempData[Notification.ModalMessage[0]] = response.Message;
            return RedirectToAction("List");
        }

        var viewModel = CourseMapping.MapToViewModel(response);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        var request = CourseMapping.MapToUpdateRequest(
            viewModel, loginId, DateTime.Now);

        var response = await adminCourseService.UpdateAsync(request);

        short key = Convert.ToInt16(response.IsTransactionSuccess);
        TempData[Notification.ModalMessage[key]] = response.Message;

        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await adminCourseService.DeleteAsync(
            new DeleteCourseRequest(id));

        short key = Convert.ToInt16(response.IsTransactionSuccess);
        TempData[Notification.ModalMessage[key]] = response.Message;

        return RedirectToAction("List");
    }
}
