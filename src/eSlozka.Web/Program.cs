using eSlozka.Application.Authentication;
using eSlozka.Application.Mappings;
using eSlozka.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Localization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

services.AddSingleton<IConfiguration>(configuration);

services.AddControllers();
services.AddLocalization(options => options.ResourcesPath = "Resources");
services.AddTransient<IStringLocalizer, StringLocalizer<App>>();

services.AddAuthenticationCore();
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddScoped<ProtectedSessionStorage>();
services.AddScoped<AuthenticationStateProvider, RevalidatingAuthenticationStateProvider>();

services.AddHttpContextAccessor();
services.AddHttpClient();

services.AddServerlessDatabase(configuration, environment);
services.AddAutoMapper(typeof(UserMappingConfiguration));
services.AddViewModels();
services.AddMudServices();
services.AddValidations();
services.AddCqrs();
services.AddUtilities(configuration);

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

application.UseAuthentication();
application.UseAuthorization();

application.MapControllers();
application.MapBlazorHub();
application.MapFallbackToPage("/_Host");

application.Run();