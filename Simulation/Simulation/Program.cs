using Simulation.Menu;

namespace Simulation;

internal class Program
{
    private static Simulation? _simulation;

    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        (string Label, GameStatus Status)[] menuItems =
        [
            ("Новая игра", GameStatus.Start),
            ("Выйти", GameStatus.Exit),
        ];

        GameSettings.SetConsoleSettings();

        while (true)
        {
            GameStatus menuItem = ConsoleMenu.SelectFromMenu(menuItems);

            switch (menuItem)
            {
                case GameStatus.Start:
                    _simulation = SimulationSetup.GetConfiguration();
                    _simulation.StartSimulation();
                    break;
                case GameStatus.Exit:
                    Console.Clear();
                    Console.WriteLine("\nДо новых встреч!");
                    Console.ReadKey();
                    return;
            }
        }
    }
}
