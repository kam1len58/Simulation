
namespace Simulation.Entities;

internal class Herbivore(
    string sign,
    int speed,
    int hp
) : Creature(sign, speed, hp)
{
    protected override void Attack(Map map, Point point)
    {
        map.RemoveEntity(point);
        Hp = Math.Min(EntityDefaultParameters.DefaultHp, Hp + 10);
    }

    protected override Point? FindTarget(Map map)
    {
        var point = map.GetPoint(this);

        if (point is null)
        {
            return null;
        }

        return map.FindNearestGrass(point);
    }
}
