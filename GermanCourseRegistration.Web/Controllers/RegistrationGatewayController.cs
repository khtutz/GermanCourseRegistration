using Microsoft.AspNetCore.Mvc;

namespace GermanCourseRegistration.Web.Controllers;

public class RegistrationGatewayController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
