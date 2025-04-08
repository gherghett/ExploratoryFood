namespace Food.Core.Model;

// Aggregate Root for deliverymen (Runners)
public class Runner : BaseEntity, IAggregate
{
    public int? ActiveOrderId {get; set;}
}