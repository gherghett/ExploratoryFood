namespace Food.Core.Model;

public enum OrderStatus
{
    Received,
    Confirmed,
    CourierAccepted,
    Preparing,
    ReadyForPickup,
    InTransit,
    Delivered
}
