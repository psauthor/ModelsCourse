using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JurisTempus.Data;
using Microsoft.EntityFrameworkCore;
using JurisTempus.ViewModels;

namespace JurisTempus.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly BillingContext _context;

    public HomeController(ILogger<HomeController> logger, BillingContext context)
    {
      _logger = logger;
      _context = context;
    }

    public IActionResult Index()
    {
      var result = _context.Clients.Include(c => c.Address).ToArray();
      return View(result);
    }

    [HttpGet("editor/{id:int}")]
    public async Task<IActionResult> ClientEditor(int id)
    {
      var result = await _context.Clients
        .Include(c => c.Address)
        .Where(c => c.Id == id)
        .FirstOrDefaultAsync();

      return View(result);
    }

    [HttpGet("timesheet")]
    public IActionResult Timesheet()
    {
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
}
