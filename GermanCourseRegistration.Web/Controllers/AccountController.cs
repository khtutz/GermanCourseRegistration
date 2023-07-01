using GermanCourseRegistration.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GermanCourseRegistration.Web.Controllers;

public class AccountController : Controller
{
	private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public AccountController(
		UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
		this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [HttpGet]
	public IActionResult Register()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Register(RegisterView registerView)
	{
		if (ModelState.IsValid)
		{
			// Create the user
			var identityUser = new IdentityUser
			{
				UserName = registerView.Username,
				Email = registerView.Email
			};

			var identityResult = await userManager.CreateAsync(
				identityUser, registerView.Password);

			// Assign the role to user
			if (identityResult.Succeeded)
			{
				var roleIdentityResult = await userManager.AddToRoleAsync(
					identityUser, "User");

				if (roleIdentityResult.Succeeded)
				{
					return RedirectToAction("Login");
				}
			}
		}

		TempData["ErrorMessage"] = "Failed to register.";
        return View();
	}

	[HttpGet]
	public IActionResult Login(string ReturnUrl) 
	{
		var model = new LoginView
		{
			ReturnedUrl = ReturnUrl
		};

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Login(LoginView model)
	{
        if (ModelState.IsValid)
		{
            var signInResult = await signInManager.PasswordSignInAsync(
                model.Username, model.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(model.ReturnedUrl))
                {
                    return Redirect(model.ReturnedUrl);
                }

                return RedirectToAction("Index", "Home");
            }
			else
			{
                TempData["ErrorMessage"] = "Invalid credentials.";
                return View();
            }
        }

        TempData["ErrorMessage"] = "Failed to log in.";
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
