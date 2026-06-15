
namespace Simulation.Actions;

public class MoveCreaturesAction : GameAction
{
    public MoveCreaturesAction(Map map, PathFinder pathFinder) 
        : base(map)
    {
        PathFinder = pathFinder;
    }

    public PathFinder PathFinder { get; }

    public override void Execute()
    {
        foreach(var entity in Map.GetAllCreatures())
        {
            entity.MakeMove(Map, PathFinder);
        }
    }
}
