using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MJ.Models;
using MJ.Application.Services;

namespace MJ.Controllers;

[CustomAuthorization]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // public IActionResult Index()
    // {
    //     return View();
    // }
    public IActionResult Dashboard()
    {
        // if (Request.Cookies.ContainsKey("AuthToken"))
        // {
        //     var token = Request.Cookies["AuthToken"];
        //     if (Helper.ValidateJwtToken(token))
        //     {
        //         return RedirectToAction("Dashboard", "Home");
        //     }
        // }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
