using CoreFitness.Domain.MembershipPlans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Persistence.Configurations;

public class MembershipPlanFeatureEntityConfig : IEntityTypeConfiguration<MembershipPlanFeatureEntity>
{
    public void Configure(EntityTypeBuilder<MembershipPlanFeatureEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
