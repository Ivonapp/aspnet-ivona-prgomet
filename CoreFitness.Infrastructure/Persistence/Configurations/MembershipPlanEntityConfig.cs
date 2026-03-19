using CoreFitness.Domain.MembershipPlans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Persistence.Configurations;

public class MembershipPlanEntityConfig : IEntityTypeConfiguration<MembershipPlanEntity>
{
    public void Configure(EntityTypeBuilder<MembershipPlanEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MembershipPlanType)
            .HasConversion<string>();

        builder.Property(x => x.Price)
            .HasPrecision(10, 2);

        builder.HasMany(x => x.Features)
            .WithOne()
            .HasForeignKey(x => x.MembershipPlanId)
            .OnDelete(DeleteBehavior.Cascade);      
    }
}
