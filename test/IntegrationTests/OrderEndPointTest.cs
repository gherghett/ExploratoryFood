using Microsoft.AspNetCore.Mvc.Testing;
using Food.Api;
using System.Net;

namespace IntegrationTests;

public class OrderEndPointTest : IClassFixture<ApiFactory>
{
    private readonly ApiFactory _factory;

    public OrderEndPointTest(ApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetOrders_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/orders");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}