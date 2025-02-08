using Microsoft.EntityFrameworkCore;
using TaskFlow.Extensions;
using TaskFlow.Infrastracture.Connections;
using TestWebBackEndDeveloper.Extensions.ExtensionsLogs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
LogExtension.InitializeLogger();

var loggerSerialLog = LogExtension.GetLogger();

loggerSerialLog.Information("Logging initialized.");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
    });
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration!");
}

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
