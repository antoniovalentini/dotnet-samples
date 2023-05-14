using DotNetConfiguration.ConsoleApp;

// Set KeyFour as environment variable
Environment.SetEnvironmentVariable("Settings__KeyFour", "I'm the real KeyFour");

// new BasicSample().Do();
await new HostingSample().Do(args);

public class Settings
{
    public int KeyOne { get; init; }
    public bool KeyTwo { get; init; }
    public SettingsKey KeyThree { get; init; }
    public string KeyFour { get; init; }
}

public class SettingsKey
{
    public string Message { get; init; }
}
