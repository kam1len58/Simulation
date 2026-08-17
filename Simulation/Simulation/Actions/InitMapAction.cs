using Simulation.Entities;

namespace Simulation.Actions;

public class InitMapAction(
    Map map,
    int grassCount,
    int herbivoreCount,
    int predatorCount,
    int rockCount,
    int treeCount
) : GameAction(map)
{
    public int GrassCount { get; } = grassCount;
    public int HerbivoreCount { get; } = herbivoreCount;
    public int PredatorCount { get; } = predatorCount;
    public int RockCount { get; } = rockCount;
    public int TreeCount { get; } = treeCount;

    public void GenerateEntity(int entityCount, Func<Point, Entity> createEntity)
    {
        int objectCount = 0;
        int generateAttempts = 0;
        int maxGenerateAttempts = 5 * entityCount;

        while (objectCount < entityCount && generateAttempts < maxGenerateAttempts)
        {
            int x = Random.Shared.Next(Map.Width);
            int y = Random.Shared.Next(Map.Height);
            Point point = new Point(x, y);

            if (!Map.IsOccupied(point))
            {
                Map.AddEntity(createEntity(point), point);
                objectCount++;
            }

            generateAttempts++;
        }
    }

    public override void Execute()
    {
        GenerateEntity(
            GrassCount,
            (point) => new Grass(Signs.Grass[Random.Shared.Next(Signs.Grass.Length)])
        );

        GenerateEntity(
            HerbivoreCount,
            (point) => new Herbivore(
                Signs.Herbivore[Random.Shared.Next(Signs.Herbivore.Length)],
                EntityDefaultParameters.DefaultSpeed,
                EntityDefaultParameters.DefaultHp
            )
        );

        GenerateEntity(
            PredatorCount,
            (point) => new Predator(
                Signs.Predator[Random.Shared.Next(Signs.Predator.Length)],
                EntityDefaultParameters.DefaultSpeed,
                EntityDefaultParameters.DefaultHp,
                EntityDefaultParameters.DefaultAttackPower
            )
        );

        GenerateEntity(
            RockCount,
            (point) => new Rock(Signs.Rock[Random.Shared.Next(Signs.Rock.Length)])
        );

        GenerateEntity(
            TreeCount,
            (point) => new Tree(Signs.Tree[Random.Shared.Next(Signs.Tree.Length)])
        );
    }
}
