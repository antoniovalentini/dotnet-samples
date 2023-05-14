using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetConfiguration.ConsoleApp;

public class HostingSample
{
    public async Task Do(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args).Build();

        // Ask the service provider for the configuration abstraction.
        IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

        // Get values from the config given their key and their target type.
        int keyOneValue = config.GetValue<int>("Settings:KeyOne");
        bool keyTwoValue = config.GetValue<bool>("Settings:KeyTwo");
        string? keyThreeNestedValue = config.GetValue<string>("Settings:KeyThree:Message");
        string? keyFour = config.GetValue<string>("Settings:KeyFour");

        // Write the values to the console.
        Console.WriteLine($"KeyOne = {keyOneValue}");
        Console.WriteLine($"KeyTwo = {keyTwoValue}");
        Console.WriteLine($"KeyThree:Message = {keyThreeNestedValue}");
        Console.WriteLine($"KeyFour = {keyFour}");

        // Application code which might rely on the config could start here.

        await host.RunAsync();

        // This will output the following:
        // KeyOne = 1
        // KeyTwo = True
        // KeyThree:Message = Oh, that's nice...
        // KeyFour = I'm the real KeyFour
    }
}
