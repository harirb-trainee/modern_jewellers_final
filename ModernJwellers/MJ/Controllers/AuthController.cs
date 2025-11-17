namespace MJ.Controllers;
using Microsoft.AspNetCore.Mvc;
using MJ.Application.Services;
using MJ.Domain.ViewModels;


public class AuthController : Controller
{
    private readonly MJDbContext _context;

    public AuthController(MJDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // UserVM model = new UserVM();
        // if (Request.Cookies.ContainsKey("AuthToken"))
        // {
        //     var token = Request.Cookies["AuthToken"];
        //     if (Helper.ValidateJwtToken(token))
        //     {
        //         return RedirectToAction("Index", "Home");
        //     }
        // }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserVM model)
    {

        var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
        if (user == null)
        {
            return Json(new { success = false, message = "Invalid email or password." });
        }
        var token = Helper.GenerateJwtToken(user);
        Response.Cookies.Append("AuthToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
            // Expires = DateTimeOffset.UtcNow.AddHours(5)
        });


        return Json(new { success = true, message = "Login Succesful." });
    }

    [HttpGet]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        // HttpContext.Session.Clear();
        // Redirect to the login page or home page
        return RedirectToAction("Index", "Auth");
    }
}

