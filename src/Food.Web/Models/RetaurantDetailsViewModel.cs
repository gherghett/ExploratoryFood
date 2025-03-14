using System;

namespace Food.Web.Models;

public class RestaurantDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ContactInfo {get; set; } = string.Empty;
}
