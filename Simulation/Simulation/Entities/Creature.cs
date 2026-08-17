
namespace Simulation.Entities;

public abstract class Creature(string sign, int speed, int hp) : Entity(sign)
{
    public int Speed { get; protected set; } = speed;
    public int Hp { get; protected set; } = hp;

    public void TakeDamage(int attackPower) => Hp -= attackPower;

    public void MakeMove(Map map, PathFinder pathFinder)
    {
        var point = map.GetPoint(this);

        if (point is null)
        {
            return;
        }

        var target = FindTarget(map);

        if (target is null)
        {
            MoveNext(map);
            return;
        }

        var path = pathFinder.FindPath(map, point, target);

        if (path.Count == 0)
        {
            MoveNext(map);
            return;
        }

        if (path.Count > 1)
        {
            for (int i = 0; i < Speed && i < path.Count - 1; i++)
            {
                var moveTo = MoveTo(map, path[i]);

                if (!moveTo)
                {
                    break;
                }

                if (i == path.Count - 2)
                {
                    Attack(map, target);
                }
            }
        }
        else if (path.Count == 1)
        {
            Attack(map, target);
            MoveTo(map, target);
        }
    }

    protected bool MoveTo(Map map, Point coordinate)
    {
        var point = map.GetPoint(this);

        if (point is null)
        {
            return false;
        }

        if (map.IsOccupied(coordinate))
        {
            return false;
        }

        map.RemoveEntity(point);
        map.AddEntity(this, coordinate);
        point = coordinate;
        return true;
    }

    protected void MoveNext(Map map)
    {
        var point = map.GetPoint(this);

        if (point is null)
        {
            return;
        }

        var freeNeighbors = new List<Point>();
        foreach (var neighbor in map.GetNeighbors(point))
        {
            if (!map.IsOccupied(neighbor))
            {
                freeNeighbors.Add(neighbor);
            }
        }

        if (freeNeighbors.Count == 0)
        {
            return;
        }

        var nextStep = freeNeighbors[Random.Shared.Next(freeNeighbors.Count)];
        MoveTo(map, nextStep);
    }

    protected abstract void Attack(Map map, Point point);

    protected abstract Point? FindTarget(Map map);
}
