
namespace Simulation.Entities;

public abstract class Entity
{
    protected Entity(Coordinates coordinates, string sign)
    {
        Coordinates = coordinates;
        Sign = sign;
    }

    public Coordinates Coordinates { get; protected set; }
    public string Sign { get; protected set; }
}
