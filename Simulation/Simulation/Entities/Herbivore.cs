
namespace Simulation.Entities;

internal class Herbivore : Creature
{
    public Herbivore(Coordinates coordinates, string sign, int speed, int hp) 
        : base(coordinates, sign, speed, hp)
    { 
    }

    protected override void Attack(Coordinates coordinates, Map map)
    {
        map.RemoveEntity(coordinates);
        Hp = Math.Min(EntityDefaultParameters.DefaultHp, Hp + 10);
    }

    protected override Coordinates? FindTarget(Map map) => map.FindNearestGrass(Coordinates);
}
