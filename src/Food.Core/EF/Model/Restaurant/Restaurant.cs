
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Food.Core.Model;

// Restaurant entity (Aggregate Root)
public class Restaurant : BaseEntity, IAggregate
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string ContactInfo { get; set; } = null!;
    public string ImageUrl {get; set;} = null!;

    [JsonConverter(typeof(OpenHoursJsonConverter))]
    public OpenHours OpenHours { get; set; } = null!;

    public bool IsOpenForOrders() =>
        OpenHours.IsOpenForOrders();
}