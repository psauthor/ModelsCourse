using System.Configuration;
using JurisTempus.Apis;
using JurisTempus.Data;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddRazorPages()
  .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<BillingContext>(cfg =>
  {
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("JurisDb"));
  });

builder.Services.Configure<JsonOptions>(options =>
{
  options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
  options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

//builder.Services.AddValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages();

// Register the Minimal APIs
ClientApis.MapApi(app);
TimeBillsApis.MapApi(app);
CaseApis.MapApi(app);

app.Run();
