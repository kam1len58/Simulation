using Simulation.Entities;

namespace Simulation;

public static class NeighborChecker
{
    public static readonly Coordinates Down = new Coordinates(0, 1);
    public static readonly Coordinates Up = new Coordinates(0, -1);
    public static readonly Coordinates Right = new Coordinates(1, 0);
    public static readonly Coordinates Left = new Coordinates(-1, 0);
}
