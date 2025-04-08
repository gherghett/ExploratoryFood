using System;
using Food.Core.Model;

namespace Food.Web.Models;

public class NewOrder
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public string? ExtraInstructions {get; set;}

    public int RestaurantId {get; set;}
    public int MenuItemId {get; set;}
    public int Quantity {get; set;}
    public string? DeliveryInstructions {get; set;}

    public Pricing ExpectedPricing { get; set;} = null!;
}
