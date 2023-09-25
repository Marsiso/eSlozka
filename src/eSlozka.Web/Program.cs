using eSlozka.Domain.Models.Mappings;
using eSlozka.Web;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

services.AddSingleton<IConfiguration>(configuration);

services.AddControllers();
services.AddLocalization(options => options.ResourcesPath = "Resources");

services.AddRazorPages();
services.AddServerSideBlazor();

services.AddServerlessDatabase(configuration, environment);
services.AddAutoMapper(typeof(UserMappingConfiguration));
services.AddViewModels();
services.AddMudServices();

var application = builder.Build();

application.UseServerlessDatabaseAutoMigration();

application.UseLocalizationResources();

if (environment.IsDevelopment())
{
    application.UseDeveloperExceptionPage();
}
else
{
    application.UseExceptionHandler("/Error");
    application.UseHsts();
}

application.UseHttpsRedirection();

application.UseStaticFiles();

application.UseRouting();

application.MapControllers();
application.MapBlazorHub();
application.MapFallbackToPage("/_Host");

application.Run();