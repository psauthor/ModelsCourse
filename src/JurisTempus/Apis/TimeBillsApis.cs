using JurisTempus.Data;
using JurisTempus.Data.Entities;

using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Http.Results;

namespace JurisTempus.Apis;

public class TimeBillsApis
{

  public static IEndpointRouteBuilder MapApi(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/timebills");
    group.MapGet("", GetTimeBills);
    group.MapGet("{id:int}", GetTimeBill)
      .WithName("GetTimeBill");
    group.MapPost("", PostTimeBill);
    return app;
  }

  public static async Task<IResult> GetTimeBills(ILogger<TimeBillsApis> logger,
    BillingContext ctx)
  {
    var result = await ctx.TimeBills
      .Include(t => t.Case)
      .Include(t => t.Employee)
      .ToArrayAsync();

    if (result.Any() == false)
    {
      logger.LogWarning("No timebills found");
      return NotFound();
    }

    return Ok(result);
  }

  public static async Task<IResult> GetTimeBill(int id, ILogger<TimeBillsApis> logger,
    BillingContext ctx)
  {
    var result = await ctx.TimeBills
      .Include(t => t.Case)
      .Include(t => t.Employee)
      .Where(t => t.Id == id)
      .FirstOrDefaultAsync();

    if (result is null)
    {
      logger.LogWarning("Timebill with id {Id} not found", id);
      return NotFound();
    }

    return Ok(result);
  }

  public static async Task<IResult> PostTimeBill(TimeBill bill, ILogger<TimeBillsApis> logger,
    BillingContext ctx)
  {
    var theCase = await ctx.Cases
      .Where(c => c.Id == bill.Case.Id)
      .FirstAsync();

    var theEmployee = await ctx.Employees
      .Where(e => e.Id == bill.Employee.Id)
      .FirstAsync();

    bill.Case = theCase;
    bill.Employee = theEmployee;

    ctx.Add(bill);

    if (await ctx.SaveChangesAsync() > 0)
    {
      return CreatedAtRoute("GetTimeBill", new { id = bill.Id }, bill);
    }

    return BadRequest("Failed to save new timebill");
  }
}
