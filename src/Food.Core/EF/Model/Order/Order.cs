namespace Food.Core.Model;

// Order Aggregate Root
public class Order : BaseEntity, IAggregate
{
    public int RestaurantId {get; set;}
    public DateTime CreationDate {get; set;} = DateTime.Now;
    public CustomerInfo CustomerInfo { get; set; } = null!;
    public OrderInfo OrderDetails { get; set; } = null!; // TODO Why 2 different names?
    public string DeliveryInstructions { get; set; } = null!;
    public OrderStatus Status { get; set;} = OrderStatus.Received;

    // TODO this quite naively always sets the time remaining as one hour into the future
    public TimeSpan TimeRemaining()
    {
        var delivery_time = CreationDate.AddHours(1);
        var remaining = delivery_time - DateTime.Now;
        if( remaining <= TimeSpan.Zero )
            return TimeSpan.Zero;
        return remaining;
    }
}