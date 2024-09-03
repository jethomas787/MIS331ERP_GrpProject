using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MIS331ERP_GrpProject.Pages;

public class Login : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public Login(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    public string Username { get; set; }
    public string Password { get; set; }
    public string ErrorMessage { get; set; }
    
    public async Task<IActionResult>OnPostAsync()
    {
        var result= await _signInManager.PasswordSignInAsync(Username, Password, false, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Home"); //access to all pages
            }
            return RedirectToAction("Index", "Home");//restric to default page
        }
        ErrorMessage = "Invalid username or password";
        return Page();
        }
    }

