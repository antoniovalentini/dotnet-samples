using Microsoft.Extensions.Configuration;

namespace DotNetConfiguration.ConsoleApp;

public class BasicSample
{
    public void Do()
    {
        // Build a config object, using env vars and JSON providers.
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        // Get values from the config given their key and their target type.
        Settings settings = config.GetRequiredSection("Settings").Get<Settings>()
                            ?? throw new Exception($"Unable to bind '{nameof(Settings)}' object.");

        // Write the values to the console.
        Console.WriteLine($"KeyOne = {settings.KeyOne}");
        Console.WriteLine($"KeyTwo = {settings.KeyTwo}");
        Console.WriteLine($"KeyThree:Message = {settings.KeyThree.Message}");
        Console.WriteLine($"KeyFour = {settings.KeyFour}");

        // This will output the following:
        // KeyOne = 1
        // KeyTwo = True
        // KeyThree:Message = Oh, that's nice...
        // KeyFour = I'm the real KeyFour
    }
}
