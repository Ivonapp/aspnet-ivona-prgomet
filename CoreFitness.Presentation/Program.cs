using CoreFitness.Application;
using CoreFitness.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using CoreFitness.Infrastructure.Persistence;
using CoreFitness.Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using CoreFitness.Infrastructure.Services;
using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


// **Vi har Dependency Injection i denna filen**
builder.Services.AddControllersWithViews();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration); 
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb")));

builder.Services.AddIdentity<AppUser, IdentityRole>(x => // IdentityRole är en inbyggd klass i ASP.NET Core Identity-biblioteket.
{
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = true; // Detta är en inbyggd säkerhetsspärr. Det gör det omöjligt för två olika konton att registreras med samma e-postadress.
    x.SignIn.RequireConfirmedEmail = false; // denna skickar ut ett mail där man bekräftar användaren när den är TRUE
})
    .AddEntityFrameworkStores<ApplicationDbContext>(); // Den talar om för Identity att alla användare, lösenordshashar och roller ska sparas i den databas som sköts av ApplicationDbContext

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<FileService>();

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
