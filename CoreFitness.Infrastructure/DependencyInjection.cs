using CoreFitness.Domain.MembershipPlans;
using CoreFitness.Infrastructure.Persistence;
using CoreFitness.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreFitness.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("LocalDb")));

            services.AddScoped<IMembershipPlanRepository, MembershipPlanRepository>();

            return services;
        }
    }
}
