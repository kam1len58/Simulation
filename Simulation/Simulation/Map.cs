using Simulation.Entities;

namespace Simulation;

public class Map
{
    private List<Creature> _allCreatures = [];
    private Dictionary<Coordinates, Entity> _world = [];
    private Dictionary<Coordinates, Grass> _grasses = [];
    private Dictionary<Coordinates, Herbivore> _herbivores = [];
    private Dictionary<Coordinates, Predator> _predators = [];
    private Dictionary<Coordinates, Rock> _rocks = [];
    private Dictionary<Coordinates, Tree> _trees = [];
    private readonly List<Coordinates> _potentialNeighbors = [NeighborChecker.Up, NeighborChecker.Down, NeighborChecker.Right, NeighborChecker.Left];
   
    public Map(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int Width { get; }
    public int Height { get; }

    public void AddEntity(Coordinates coordinates, Entity entity)
    {
        if (coordinates.X >= 0 && coordinates.Y >= 0 && coordinates.X<Width && coordinates.Y<Height)
        {
            if(!_world.ContainsKey(coordinates))
                _world.Add(coordinates, entity);

            if(entity is Grass grass)
                _grasses[coordinates]=grass;

            if (entity is Rock rock)
                _rocks[coordinates] = rock;

            if (entity is Tree tree)
                _trees[coordinates] = tree;

            if(entity is Herbivore herbivore)
            {
                _herbivores[coordinates] = herbivore;
                _allCreatures.Add(herbivore);
            }
                
            if(entity is Predator predator)
            {
                _predators[coordinates] = predator;
                _allCreatures.Add(predator);
            }
        }
    }

    public void RemoveEntity(Coordinates coordinates)
    {
        if(_world.TryGetValue(coordinates, out var entity))
        {
            _world.Remove(coordinates);

            if (entity is Grass grass)
                _grasses.Remove(coordinates);

            if (entity is Rock rock)
                _rocks.Remove(coordinates);

            if (entity is Tree tree)
                _trees.Remove(coordinates);

            if (entity is Herbivore herbivore)
            {
                _herbivores.Remove(coordinates);
                _allCreatures.Remove(herbivore);
            }

            if(entity is Predator predator)
            {
                _predators.Remove(coordinates);
                _allCreatures.Remove(predator);
            }
        }
        
    }

    public bool IsOccupied(Coordinates coordinates)
    {
        if(coordinates.X<0 || coordinates.Y<0 || coordinates.X>=Width || coordinates.Y>=Height || 
            _grasses.ContainsKey(coordinates) || _rocks.ContainsKey(coordinates) || _trees.ContainsKey(coordinates))
            return true;

        return _world.ContainsKey(coordinates);
    }

    public List<Coordinates> GetNeighbors(Coordinates coordinates)
    {
        var neighbors = new List<Coordinates>();

        foreach(var direction  in _potentialNeighbors)
        {
            var neighbor = new Coordinates(direction.X+coordinates.X, direction.Y+coordinates.Y);
            if(neighbor.X>=0 && neighbor.Y>=0 && neighbor.X<Width && neighbor.Y<Height)
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    public Coordinates? FindNearestGrass(Coordinates coordinates) => FindNearest(_grasses, coordinates);

    public Coordinates? FindNearestHerbivore(Coordinates coordinates) => FindNearest(_herbivores, coordinates);

    public Entity? GetEntity(Coordinates coordinates)
    {
        if (_world.TryGetValue(coordinates, out var entity))
            return entity;

        return null;
    }

    public List<Creature> GetAllCreatures() => _allCreatures.ToList();

    public int GrassCount() => _grasses.Count;

    public int HerbivoreCount() => _herbivores.Count;

    public int PredatorCount() => _predators.Count;

    public int RockCount() => _rocks.Count;

    public int TreeCount() => _trees.Count;

    public int GetTotalEntityCount() => _grasses.Count + _herbivores.Count + _predators.Count + _rocks.Count + _trees.Count;

    public Dictionary<Coordinates, Entity> GetMap() => _world.ToDictionary();

    private Coordinates? FindNearest<T>(Dictionary<Coordinates, T> entities, Coordinates coordinates)
    {
        if (entities.Count == 0)
            return null;

        Coordinates? nearest = null;
        int minDistanceToObject = int.MaxValue;
        foreach (var entity in entities)
        {
            var distance = Math.Abs(entity.Key.X - coordinates.X) + Math.Abs(entity.Key.Y - coordinates.Y);
            if (distance < minDistanceToObject)
            {
                minDistanceToObject = distance;
                nearest = entity.Key;
            }
        }

        return nearest;
    }
}
