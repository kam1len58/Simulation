using Simulation.Entities;

namespace Simulation.Actions;

public class InitMapAction : GameAction
{
    public InitMapAction(Map map, int grassCount, int herbivoreCount, int predatorCount, int rockCount, int treeCount)
        : base(map)
    {
        GrassCount = grassCount;
        HerbivoreCount = herbivoreCount;
        PredatorCount = predatorCount;
        RockCount = rockCount;
        TreeCount = treeCount;
    }

    public int GrassCount { get; }
    public int HerbivoreCount { get; }
    public int PredatorCount { get; }
    public int RockCount { get; }
    public int TreeCount { get; }

    public void GenerateEntity(int entityCount, Func<Coordinates, Entity> createEntity)
    {
        int objectCount = 0;
        int generateAttempts = 0;
        int maxGenerateAttempts = 5 * entityCount;
        while (objectCount < entityCount && generateAttempts < maxGenerateAttempts)
        {
            int x = Random.Shared.Next(Map.Width);
            int y = Random.Shared.Next(Map.Height);
            Coordinates coordinates = new Coordinates(x, y);
            if (!Map.IsOccupied(coordinates))
            {
                Map.AddEntity(coordinates, createEntity(coordinates));
                objectCount++;
            }
            generateAttempts++;
        }
    }

    public override void Execute()
    {
        GenerateEntity(GrassCount,
            (coordinates) => new Grass(coordinates, Signs.Grass[Random.Shared.Next(Signs.Grass.Length)]));

        GenerateEntity(HerbivoreCount,
            (coordinates) => new Herbivore(coordinates, Signs.Herbivore[Random.Shared.Next(Signs.Herbivore.Length)],
            EntityDefaultParameters.DefaultSpeed, EntityDefaultParameters.DefaultHp));

        GenerateEntity(PredatorCount,
            (coordinates) => new Predator(coordinates, Signs.Predator[Random.Shared.Next(Signs.Predator.Length)],
            EntityDefaultParameters.DefaultSpeed, EntityDefaultParameters.DefaultHp, EntityDefaultParameters.DefaultAttackPower));

        GenerateEntity(RockCount,
            (coordinates) => new Rock(coordinates, Signs.Rock[Random.Shared.Next(Signs.Rock.Length)]));

        GenerateEntity(TreeCount,
            (coordinates) => new Tree(coordinates, Signs.Tree[Random.Shared.Next(Signs.Tree.Length)]));
    }
}
