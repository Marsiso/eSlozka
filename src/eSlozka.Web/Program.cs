var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var application = builder.Build();

if (!application.Environment.IsDevelopment()) application.UseHsts();

application.UseHttpsRedirection();

application.UseStaticFiles();

application.UseRouting();

application.MapBlazorHub();
application.MapFallbackToPage("/_Host");

application.Run();