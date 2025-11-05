using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmsWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CompanyPortalDbContext _context;

    public HomeController(ILogger<HomeController> logger, CompanyPortalDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new EmployeeProfileViewModel
        {
            Items = await _context.EmployeeProfiles
                .ToListAsync()
        };

        return View(viewModel);
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
