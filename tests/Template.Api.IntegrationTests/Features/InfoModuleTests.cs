using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using static Template.Api.Features.Info.InfoEndpoints;

namespace Template.Api.IntegrationTests.Features;

public class InfoModuleTests(IntegrationTestClassFixture factory) : IClassFixture<IntegrationTestClassFixture>
{
    private readonly WebApplicationFactory<Program> factory = factory;

    [Fact]
    public async Task GetVersion_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = factory.CreateClient();
        // Act
        var response = await client.GetAsync("/info/version");
        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetVersion_ReturnsExpectedMediaType()
    {
        // Arrange
        var client = factory.CreateClient();
        // Act
        var response = await client.GetAsync("/info");
        // Assert
        response.Content.Headers.ContentType?.MediaType.ShouldBe("application/json");
    }

    [Fact]
    public async Task GetInfo_ReturnsExpectedResponse()
    {
        // Arrange
        var client = factory.CreateClient();
        // Act
        var response = await client.GetAsync("/info");
        // Assert
        var forecast = await response.Content.ReadFromJsonAsync<Info[]>();
        forecast.ShouldNotBeNull();
        forecast.ShouldNotBeEmpty();
    }
}
