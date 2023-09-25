using eSlozka.Web;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

services.AddSingleton<IConfiguration>(configuration);

services.AddRazorPages();
services.AddServerSideBlazor();

services.AddServerlessDatabase(configuration, environment);

var application = builder.Build();

application.UseServerlessDatabaseAutoMigration();

if (!application.Environment.IsDevelopment()) application.UseHsts();

application.UseHttpsRedirection();

application.UseStaticFiles();

application.UseRouting();

application.MapBlazorHub();
application.MapFallbackToPage("/_Host");

application.Run();
