using Itmo.ObjectOrientedProgramming.Lab5.Application;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.API;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationServices("admin123");
        builder.Services.AddInfrastructureRepositories();
        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}