using IGenServer.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

using var host = builder.Build();

// Future DB sync / migration / seed work goes here.
// Example:
// using var scope = host.Services.CreateScope();

await host.RunAsync();
