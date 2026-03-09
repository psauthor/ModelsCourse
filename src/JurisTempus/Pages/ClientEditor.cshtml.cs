
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JurisTempus.Pages;

[BindProperties]
public class ClientEditor(BillingContext context) : PageModel
{

  public required Client Client { get; set; }


  public async Task OnPostAsync()
  {
    context.Clients.Add(Client);
    await context.SaveChangesAsync();
  }
}
