using Simulation.Entities;

namespace Simulation;

public class Map(int width, int height, PathFinder pathFinder)
{
    private readonly Dictionary<Point, Entity> _world = [];
    private readonly List<Point> _potentialNeighbors =
    [
        NeighborChecker.Up,
        NeighborChecker.Down,
        NeighborChecker.Right,
        NeighborChecker.Left
    ];

    public int Width { get; } = width;
    public int Height { get; } = height;

    public void AddEntity(Entity entity, Point point)
    {
        if (point.X >= 0 && point.Y >= 0 && point.X < Width && point.Y < Height)
        {
            if (!_world.ContainsKey(point))
            {
                _world.Add(point, entity);
            }
        }
    }

    public void RemoveEntity(Point point) => _world.Remove(point);

    public bool IsOccupied(Point point)
    {
        if (point.X < 0 || point.Y < 0 || point.X >= Width || point.Y >= Height || _world.ContainsKey(point))
        {
            return true;
        }

        return false;
    }

    public List<Point> GetNeighbors(Point point)
    {
        var neighbors = new List<Point>();

        foreach (Point direction in _potentialNeighbors)
        {
            var neighbor = new Point(direction.X + point.X, direction.Y + point.Y);
            if (neighbor.X >= 0 && neighbor.Y >= 0 && neighbor.X < Width && neighbor.Y < Height)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    public Point? FindNearestGrass(Point point) => FindNearest<Grass>(point, _world);

    public Point? FindNearestHerbivore(Point point) => FindNearest<Herbivore>(point, _world);

    public Entity? GetEntity(Point point)
    {
        _world.TryGetValue(point, out Entity? entity);
        return entity;
    }

    public Point? GetPoint(Entity entity) => _world.FirstOrDefault(k => k.Value == entity).Key;

    public List<Creature> GetAllCreatures() => _world.Values.OfType<Creature>().ToList();

    public int GrassCount() => _world.Values.OfType<Grass>().Count();

    public int HerbivoreCount() => _world.Values.OfType<Herbivore>().Count();

    public int PredatorCount() => _world.Values.OfType<Predator>().Count();

    public int RockCount() => _world.Values.OfType<Rock>().Count();

    public int TreeCount() => _world.Values.OfType<Tree>().Count();

    public int GetTotalEntityCount() => GrassCount() + HerbivoreCount() + PredatorCount() + RockCount() + TreeCount();

    public Dictionary<Point, Entity> GetMap() => _world.ToDictionary();

    private Point? FindNearest<T>(Point start, Dictionary<Point, Entity> entities)
    {
        if (entities.Count == 0)
        {
            return null;
        }

        Point? nearest = null;
        int minDistanceToObject = Width * Height;

        foreach (KeyValuePair<Point, Entity> entity in entities)
        {
            if (entity.Value is T)
            {
                var path = pathFinder.FindPath(this, start, entity.Key);

                if (path.Count == 0)
                {
                    continue;
                }

                var distance = path.Count;

                if (distance < minDistanceToObject)
                {
                    minDistanceToObject = distance;
                    nearest = entity.Key;
                }
            }
        }

        return nearest;
    }
}
