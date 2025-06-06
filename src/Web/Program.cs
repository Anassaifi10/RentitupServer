using Microsoft.AspNetCore.Http.Connections;
using RentalApp.Application.Common;
using RentalApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddKeyVaultIfConfigured();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CORS");
app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});


app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/api"));
app.MapHub<RentalHub>("/Rentituphub", o => o.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling);

app.MapEndpoints();

app.Run();

public partial class Program { }
