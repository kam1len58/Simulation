
namespace Simulation.Entities;

public abstract class Entity(string sign)
{
    public string Sign { get; protected set; } = sign;
}
