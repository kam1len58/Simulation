using Simulation.Entities;

namespace Simulation;

public class Renderer
{
    public void Render(Map map, int stepsCount, bool isSimulationStopped)
    {
        Console.Clear();

        for (int i = 0; i < map.Height; i++)
        {
            for (int j = 0; j < map.Width; j++)
            {
                Point point = new Point(j, i);

                var entity = map.GetEntity(point);

                if (entity is null)
                {
                    Console.Write($"{Signs.Grounds[Random.Shared.Next(Signs.Grounds.Length)]} ");
                }
                else
                {
                    Console.Write($"{entity.Sign} ");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("─────────────────────────");
        Console.WriteLine("Выберите операцию:");

        if (isSimulationStopped)
        {
            Console.WriteLine("Enter - возобновить игру");
        }
        else
        {
            Console.WriteLine("P - поставить игру на паузу");
        }

        Console.WriteLine("Esc - выход в главное меню");
    }
}
