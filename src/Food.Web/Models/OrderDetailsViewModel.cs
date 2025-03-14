using System;

using Food.Core.Model;


namespace Food.Web.Models;

public class OrderDetailsViewModel
{
    public int OrderId { get; set; }
    public DateTime CreationDate { get; set; }
    public OrderStatus Status { get; set; }
    public string DeliveryInstructions { get; set; } = string.Empty;

    // Customer Information
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerAddress { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;

    // Order Details
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; } = string.Empty;
    public decimal MenuItemPrice { get; set; }
    public string ExtraInstructions { get; set; } = string.Empty;
    public string MenuItemImageUrl { get; set; } = string.Empty;

    // Restaurant Information
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
    public string RestaurantAddress { get; set; } = string.Empty;
    public string RestaurantContact { get; set; } = string.Empty;
    public string RestaurantImageUrl { get; set; } = string.Empty;

    public static OrderDetailsViewModel FromOrder(Order order, Restaurant restaurant, MenuItem menuItem)
    {
        return new OrderDetailsViewModel
        {
            OrderId = order.Id,
            CreationDate = order.CreationDate,
            Status = order.Status,
            DeliveryInstructions = order.DeliveryInstructions,

            CustomerName = order.CustomerInfo.Name,
            CustomerAddress = order.CustomerInfo.Address,
            CustomerPhone = order.CustomerInfo.PhoneNumber,

            MenuItemId = order.OrderDetails.MenuItemId,
            MenuItemName = order.OrderDetails.MenuItemName,
            MenuItemPrice = order.OrderDetails.Price,
            ExtraInstructions = order.OrderDetails.ExtraInstructions,
            MenuItemImageUrl = menuItem.ImageUrl,

            RestaurantId = restaurant.Id,
            RestaurantName = restaurant.Name,
            RestaurantAddress = restaurant.Address,
            RestaurantContact = restaurant.ContactInfo,
            RestaurantImageUrl = restaurant.ImageUrl
        };
    }
}
