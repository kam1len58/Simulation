using Simulation.Entities;

namespace Simulation.Actions;

public class AddHerbivoreAction : GameAction
{
    public AddHerbivoreAction(Map map) 
        : base(map)
    {
    }

    public override void Execute()
    {
        TrySpawnEntities(Map.HerbivoreCount(), 
            coordinates => new Herbivore(coordinates, Signs.Herbivore[Random.Shared.Next(Signs.Herbivore.Length)], 
            EntityDefaultParameters.DefaultSpeed, EntityDefaultParameters.DefaultHp)
        );
    }
}
