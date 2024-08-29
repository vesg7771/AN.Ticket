using AN.Ticket.WebUI.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddWebUI(builder.Configuration);

var app = builder.Build();

app.UseWebUI(builder.Environment);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
