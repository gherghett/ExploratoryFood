namespace Food.Core.Model;

// MenuItem entity  Aggregate Root
public class MenuItem : BaseEntity, IAggregate
{
    public string Name { get; set; } = null!;
    public int RestaurantId { get; set; }

    // public Restaurant Restaurant { get; set; } = null!; //We dont need this
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
}