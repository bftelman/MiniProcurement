using Microsoft.Extensions.Options;
using MiniProcurement.Extensions;
using MiniProcurement.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<AuthorizationMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mini Procurement application api");
        c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
        app.MapGet("/swagger-ui/SwaggerDark.css", async (CancellationToken cancellationToken) =>
{
    var css = await File.ReadAllBytesAsync("SwaggerDark.css", cancellationToken);
    return Results.File(css, "text/css");
}).ExcludeFromDescription();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
