using Simulation.Entities;

namespace Simulation.Actions;

public abstract class GameAction(Map map)
{
    public Map Map { get; protected set; } = map;

    public abstract void Execute();

    protected void TrySpawnEntities(int currentCount, Func<Point, Entity> createEntity)
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
                Point point = new Point(x, y);

                if (!Map.IsOccupied(point))
                {
                    Map.AddEntity(createEntity(point), point);
                    spawnedCount++;
                }

                spawnAttempts++;
            }
        }
    }
}
