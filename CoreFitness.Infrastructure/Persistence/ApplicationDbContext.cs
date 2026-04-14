using CoreFitness.Domain.MembershipPlans;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CoreFitness.Domain.Entities;

namespace CoreFitness.Infrastructure.Persistence;

// medans DbContext = En tom motor. Du måste bygga allt (tabeller för användare, inloggning, lösenord) helt själv från noll.
// IdentityDbContext: Samma motor, men den kommer "förtrimmat" med allt som rör säkerhet och användare.
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)  // <- ": IdentityDbContext<AppUser>" Denna rad skapar alla tabeller som behövs för att registrera användare, logga in dem och lagra deras namn/e-post.
{
    public DbSet<MembershipPlanEntity> MembershipPlans => Set<MembershipPlanEntity>();
    public DbSet<MembershipPlanFeatureEntity> MembershipPlanFeatures => Set<MembershipPlanFeatureEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
