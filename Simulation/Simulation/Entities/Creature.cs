
namespace Simulation.Entities;

public abstract class Creature : Entity
{
    protected Creature(Coordinates coordinates, string sign, int speed, int hp)
        : base(coordinates, sign)
    {
        Speed = speed;
        Hp = hp;
    }

    public int Speed { get; protected set; }
    public int Hp { get; protected set; }

    public void TakeDamage(int attackPower) => Hp -= attackPower;

    public void MakeMove(Map map, PathFinder pathFinder)
    {
        var target = FindTarget(map);

        if (target != null)
        {
            var path = pathFinder.FindPath(Coordinates, target, map);
            if (path.Count > 1)
                MoveTo(path[0], map);
            else if (path.Count == 1)
            {
                Attack(target, map);
                MoveTo(target, map);
            }
            else
                MoveNext(map);
        }
        else
            MoveNext(map);
    }

    protected void MoveTo(Coordinates newCoordinates, Map map)
    {
        if (map.IsOccupied(newCoordinates))
            return;

        map.RemoveEntity(Coordinates);
        map.AddEntity(newCoordinates, this);
        Coordinates = newCoordinates;
    }

    protected void MoveNext(Map map)
    {
        var freeNeighbors = new List<Coordinates>();
        foreach (var neighbor in map.GetNeighbors(Coordinates))
        {
            if (!map.IsOccupied(neighbor))
                freeNeighbors.Add(neighbor);
        }

        if (freeNeighbors.Count == 0)
            return;

        var nextStep = freeNeighbors[Random.Shared.Next(freeNeighbors.Count)];
        MoveTo(nextStep, map);
    }

    protected abstract void Attack(Coordinates coordinates, Map map);

    protected abstract Coordinates? FindTarget(Map map);
}
