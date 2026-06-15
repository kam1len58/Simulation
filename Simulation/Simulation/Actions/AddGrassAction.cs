using Simulation.Entities;

namespace Simulation.Actions;

internal class AddGrassAction : GameAction
{
    public AddGrassAction(Map map) 
        : base(map) 
    {
    }

    public override void Execute()
    {
        TrySpawnEntities(Map.GrassCount(),
            coordinates => new Grass(coordinates, Signs.Grass[Random.Shared.Next(Signs.Grass.Length)]));
    }
}
