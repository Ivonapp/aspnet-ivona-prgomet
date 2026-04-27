using CoreFitness.Domain.MembershipPlans;

namespace CoreFitness.Domain.MembershipPlans;

    public record MembershipPlanDto
    (
    Guid Id,
    MembershipPlanType MembershipPlanType,
    string Title,
    string Description,
    List<string> Features,
    decimal Price,
    string Monthly,
    int MonthlyClasses,
    int FreeTrial
    );
