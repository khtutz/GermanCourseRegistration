using GermanCourseRegistration.Application.Messages.CourseMaterialMessages;
using GermanCourseRegistration.Application.Services;
using GermanCourseRegistration.Web.HelperServices;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Models.ViewModels;
using GermanCourseRegistration.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace GermanCourseRegistration.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminCourseMaterialController : Controller
{
    private readonly IAdminCourseMaterialService adminCourseMaterialService;
    private readonly UserManager<IdentityUser> userManager;

    public AdminCourseMaterialController(
        IAdminCourseMaterialService adminCourseMaterialService,
        UserManager<IdentityUser> userManager)
    {
        this.adminCourseMaterialService = adminCourseMaterialService;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var courseMaterialsResponse = await adminCourseMaterialService.GetAllAsync();

        var viewModels = CourseMaterialMapping.MapToViewModels(courseMaterialsResponse);

        return View(viewModels);
    }

    [HttpGet]
    public IActionResult Add()
    {
        var viewModel = new CourseMaterialView();

        // Load the categories to select in UI
        LoadCategories(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CourseMaterialView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        var request = CourseMaterialMapping.MapToAddRequest(viewModel, loginId, DateTime.Now);

        AddCourseMaterialResponse response = await adminCourseMaterialService.AddAsync(request);

        short key = Convert.ToInt16(response.IsTransactionSuccess);
        TempData[Notification.ModalMessage[key]] = response.Message;

        return RedirectToAction("List");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        GetCourseMaterialByIdResponse response = await adminCourseMaterialService
            .GetByIdAsync(new GetCourseMaterialByIdRequest(id));

        if (response.CourseMaterial == null)
        {
            TempData[Notification.ModalMessage[0]] = response.Message;
            return RedirectToAction("List");
        }

        var viewModel = CourseMaterialMapping.MapToViewModel(response);

        // Load the categories to select in UI
        LoadCategories(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CourseMaterialView viewModel)
    {
        Guid loginId = await UserAccountService.GetCurrentUserId(userManager, User);

        UpdateCourseMaterialRequest request = CourseMaterialMapping
            .MapToUpdateRequest(viewModel, loginId, DateTime.Now);

        UpdateCourseMaterialResponse response = 
            await adminCourseMaterialService.UpdateAsync(request);

        short key = Convert.ToInt16(response.IsTransactionSuccess);
        TempData[Notification.ModalMessage[key]] = response.Message;

        return RedirectToAction("List");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        DeleteCourseMaterialResponse response = await adminCourseMaterialService
            .DeleteAsync(new DeleteCourseMaterialRequest(id));

        short key = Convert.ToInt16(response.IsTransactionSuccess);
        TempData[Notification.ModalMessage[key]] = response.Message;

        return RedirectToAction("List");
    }

    private void LoadCategories(CourseMaterialView viewModel)
    {
        var categories = adminCourseMaterialService.GetCourseMaterialCategories();
        viewModel.AvailableCategories = categories.Select(c => new SelectListItem
        {
            Text = c,
            Value = c
        });
    }
}
