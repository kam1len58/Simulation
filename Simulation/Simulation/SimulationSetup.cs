using Simulation.Entities;

namespace Simulation;

public static class SimulationSetup
{
    private static PathFinder _pathFinder = new PathFinder();
    private static Renderer _renderer = new Renderer();
    private static Map? _map;

    public static Simulation GetConfiguration()
    {
        Console.Clear();
        int height;
        int width;
        do
        {
            Console.Clear();
            do
            {
                Console.Clear();
                Console.WriteLine("Введите длину карты:");
            }
            while (!int.TryParse(Console.ReadLine(), out height));

            do
            {
                Console.Clear();
                Console.WriteLine("Введите ширину карты:");
            }
            while (!int.TryParse(Console.ReadLine(), out width));
        }
        while (height <= 0 || width <= 0 || (_map != null && height * width < _map.GetTotalEntityCount()));
        
        _map = new Map(width, height);
        int grassCount = EntityDefaultParameters.MinEntityCount;
        int herbivoreCount = EntityDefaultParameters.MinEntityCount;
        int predatorCount = EntityDefaultParameters.MinEntityCount;
        int rockCount = EntityDefaultParameters.MinEntityCount;
        int treeCount = EntityDefaultParameters.MinEntityCount;
        var simulation = new Simulation(_map, _pathFinder, _renderer, grassCount, herbivoreCount, predatorCount, rockCount, treeCount);
        return simulation;
    }
}
