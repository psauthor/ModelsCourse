using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Http.Results;

namespace JurisTempus.Apis;

public class CaseApis
{
  public static IEndpointRouteBuilder MapApi(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/cases");
    group.MapGet("", GetCases);
    group.MapPost("", PostCase);
    return app;
  }

  public static async Task<IResult> GetCases(BillingContext ctx, ILogger<CaseApis> logger)
  {
    var results = await ctx.Cases.Include(c => c.Client).ToListAsync();
    if (results.Any() == false)
    {
      logger.LogWarning("No cases found in database.");
      return NotFound();
    }
    return Ok(results);
  }

  public static async Task<IResult> PostCase(Case newEntity, BillingContext ctx, ILogger<CaseApis> logger)
  {
    var client = await ctx.Clients.FindAsync(newEntity.Client?.Id);
    if (client == null) 
    {
      logger.LogWarning("Client with id {Id} not found.", newEntity.Client?.Id);
      return NotFound($"Client with id {newEntity.Client?.Id} not found.");
    }
    newEntity.Client = client;
    ctx.Cases.Add(newEntity);
    await ctx.SaveChangesAsync();
    logger.LogInformation("Case with id {Id} created.", newEntity.Id);
    return CreatedAtRoute("GetCase", new { id = newEntity.Id }, newEntity);
  }
}
