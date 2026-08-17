using Simulation.Entities;

namespace Simulation.Actions;

public class MoveCreaturesAction(Map map, PathFinder pathFinder) : GameAction(map)
{
    public PathFinder PathFinder { get; } = pathFinder;

    public override void Execute()
    {
        foreach (Creature entity in Map.GetAllCreatures())
        {
            var point = Map.GetPoint(entity);

            if (point is null)
            {
                continue;
            }

            entity.MakeMove(Map, PathFinder);
        }
    }
}
