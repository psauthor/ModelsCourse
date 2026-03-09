using JurisTempus.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Http.Results;

namespace JurisTempus.Apis;

public class ClientApis
{

  public static IEndpointRouteBuilder MapApi(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/clients");
    group.MapGet("", Get);
    group.MapPost("", Post);
    return app;
  }

  public static async Task<IResult> Get(BillingContext ctx, ILogger<ClientApis> logger)
  {
    var results = await ctx.Clients.Include(c => c.Address).ToListAsync();

    if (results.Any() == false)
    {
      logger.LogWarning("No clients found in database.");
      return NotFound();
    }

    return Ok(results);
  }

  public static async Task<IResult> Post(Client client, BillingContext ctx, ILogger<ClientApis> logger)
  {
    ctx.Clients.Add(client);
    await ctx.SaveChangesAsync();
    logger.LogInformation("Client with id {Id} created.", client.Id);
    return CreatedAtRoute("GetClient", new { id = client.Id }, client);
  }
}
