namespace Food.Core.Model;


// Value Object for Order Info
public class OrderInfo
{
    //TODO why is there no quantity here?
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; } = null!;
    // public int RestaurantId {get; set;}
    public Pricing Price {get; set;} = null!;
    public string ExtraInstructions { get; set; } = null!;
}