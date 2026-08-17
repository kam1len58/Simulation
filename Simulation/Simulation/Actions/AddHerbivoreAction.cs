using Simulation.Entities;

namespace Simulation.Actions;

public class AddHerbivoreAction(Map map) : GameAction(map)
{
    public override void Execute()
    {
        TrySpawnEntities(
            Map.HerbivoreCount(),
            coordinates => new Herbivore(Signs.Herbivore[Random.Shared.Next(Signs.Herbivore.Length)],
            EntityDefaultParameters.DefaultSpeed, EntityDefaultParameters.DefaultHp)
        );
    }
}
