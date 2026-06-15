
namespace Simulation;

class Program
{
    private static Simulation? _simulation;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        (string, GameStatus)[] menuItems = [
        ("Новая игра", GameStatus.Start),
        ("Выйти", GameStatus.Exit),
        ];

        GameSettings.SetConsoleSettings();
        while (true)
        {
            GameStatus menuItem = Menu.SelectFromMenu(menuItems);
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
