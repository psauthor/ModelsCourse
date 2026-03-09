
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace JurisTempus.Pages;

public class IndexPage(BillingContext context) : PageModel
{
  public List<Client> Clients { get; set; } = new();

  public async Task OnGetAsync()
  {
    var results = await context.Clients
      .Include(c => c.Address)
      .Include(c => c.Cases)
      .OrderBy(c => c.Name)
      .ToListAsync();

    Clients.AddRange(results);
  }
}
