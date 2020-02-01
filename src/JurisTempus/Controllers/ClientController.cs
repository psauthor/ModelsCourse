using JurisTempus.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurisTempus.Controllers
{
  [Route("api/clients")]
  public class ClientController : ControllerBase
  {
    private readonly BillingContext _ctx;
    private readonly ILogger<ClientController> _logger;

    public ClientController(BillingContext ctx, ILogger<ClientController> logger)
    {
      _ctx = ctx;
      _logger = logger;
    }

    public async Task<IActionResult> Get()
    {
      return Ok(await _ctx.Clients.Include(c => c.Address).ToListAsync());
    }
  }
}
