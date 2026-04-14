using CoreFitness.Application;
using CoreFitness.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using CoreFitness.Infrastructure.Persistence;
using CoreFitness.Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// **Vi har Dependency Injection i denna filen**
builder.Services.AddControllersWithViews();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration); 
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb")));


var app = builder.Build();
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await MembershipPlanSeeder.SeedAsync(context);







app.UseHsts();


app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection(); //denna delen redirectar om man råkar skriva http, så kommer den automatiskt redirecta till https, vilket är mer säkert
app.UseRouting(); // Denna delen är för att vi ska kunna hantera routing funktionaliteten 

app.UseAuthorization();

app.MapStaticAssets(); //wwwroot katalogen - css filer, bootstrap, js filer etc. Den hanterar statista filer såsom bilder men INTE bilder vi laddat ner

app.MapControllerRoute(
    name: "default", //Routing, Var ska den gå när den först laddar sidan? 
    pattern: "{controller=Home}/{action=Index}/{id?}") //Den går till en controller som heter HOME, och en actionsom heter INDEX. alltså Home/Index
    .WithStaticAssets(); // Använder wwwroot för att läsa in alla filer


app.Run(); // Kör igång hela webservern
