namespace CoreFitness.Domain.MembershipPlans;


public class MembershipPlanEntity
{
            public Guid Id { get; set; }
            public MembershipPlanType MembershipPlanType { get; set; }
            public string Title { get; set; } = null!;
            public string Description { get; set; } = null!;
            public ICollection<MembershipPlanFeatureEntity> Features { get; set; } = [];
            public decimal Price { get; set; }
            public string Monthly { get; set; }
            public int MonthlyClasses { get; set; }
            public int FreeTrial { get; set; }


            //Nedan ser jag på senare
            public int SortOrder { get; set; }

}