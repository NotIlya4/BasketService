using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace IntegrationTests.Fixture;

[CollectionDefinition(nameof(AppFixture))]
public class AppFixture : ICollectionFixture<AppFixture>, IAsyncLifetime
{
    internal WebApplicationFactory<Program> WebApplicationFactory { get; private set; } = null!;
    public HttpClient Client { get; private set; } = null!;
    public IBasketRepository BasketRepository { get; private set; } = null!;
    private IDatabase Redis { get; set; } = null!;
    private IServiceScope Scope { get; set; } = null!;

    public async Task InitializeAsync()
    {
        WebApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("IntegrationTests");
        });
        Client = WebApplicationFactory.CreateClient();
        Scope = WebApplicationFactory.Services.CreateScope();
        
        Redis = Scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>().GetDatabase();
        BasketRepository = Scope.ServiceProvider.GetRequiredService<IBasketRepository>();
        await FlushDb();
    }

    public async Task FlushDb()
    {
        await Redis.ExecuteAsync("FLUSHDB");
    }

    public async Task DisposeAsync()
    {
        Scope.Dispose();
        await FlushDb();
    }
}