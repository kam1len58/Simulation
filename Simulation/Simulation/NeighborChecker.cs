using Simulation.Entities;

namespace Simulation;

public static class NeighborChecker
{
    public static readonly Point Down = new Point(0, 1);
    public static readonly Point Up = new Point(0, -1);
    public static readonly Point Right = new Point(1, 0);
    public static readonly Point Left = new Point(-1, 0);
}
