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
        var list = await _context.Employees.ToListAsync();
        _logger.LogInformation("Fetched {Count} employees from the database.", list.Count);
        
        var _departmentList = await _context.Departments.ToListAsync();
        _logger.LogInformation("Fetched {Count} departments from the database.", _departmentList.Count);
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
