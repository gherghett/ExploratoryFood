public class OrderException : Exception
{
    // Default constructor
    public OrderException() : base("An error occurred with the order.") { }

    // Constructor with custom message
    public OrderException(string message) : base(message) { }

    // Constructor with custom message and inner exception
    public OrderException(string message, Exception innerException) : base(message, innerException) { }
}