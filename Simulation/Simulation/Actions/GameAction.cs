using Simulation.Entities;

namespace Simulation.Actions;

public abstract class GameAction
{
    protected GameAction(Map map)
    {
        Map = map;
    }

    public Map Map { get; protected set; }

    public abstract void Execute();

    protected void TrySpawnEntities(int currentCount, Func<Coordinates, Entity> createEntity)
    {
        if (currentCount < EntityDefaultParameters.MinEntityCount)
        {
            int toSpawn = EntityDefaultParameters.MinEntityCount - currentCount;
            int spawnedCount = 0;
            int spawnAttempts = 0;
            int maxSpawnAttempts = EntityDefaultParameters.MinEntityCount * toSpawn;
            while (spawnedCount != toSpawn && spawnAttempts < maxSpawnAttempts)
            {
                int x = Random.Shared.Next(Map.Width);
                int y = Random.Shared.Next(Map.Height);
                Coordinates coordinates = new Coordinates(x, y);
                if (!Map.IsOccupied(coordinates))
                {
                    Map.AddEntity(coordinates, createEntity(coordinates));
                    spawnedCount++;
                }
                spawnAttempts++;
            }
        }
    }
}
