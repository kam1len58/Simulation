using Simulation.Entities;

namespace Simulation.Actions;

internal class AddGrassAction(Map map) : GameAction(map)
{
    public override void Execute()
    {
        TrySpawnEntities(
            Map.GrassCount(),
            coordinates => new Grass(Signs.Grass[Random.Shared.Next(Signs.Grass.Length)])
        );
    }
}
