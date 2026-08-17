
namespace Simulation;

public static class GameSettings
{
    public const int TurnDelay = 500;
    public static void SetConsoleSettings()
    {
        if (OperatingSystem.IsWindows())
        {
            Console.SetWindowSize(70, 30);
            Console.SetBufferSize(70, 30);
        }

        Console.CursorVisible = false;
        Console.CancelKeyPress += (sender, args) =>
        {
            args.Cancel = true;
        };
    }
}
