
namespace Simulation.Entities;

internal class Predator(
    string sign,
    int speed,
    int hp,
    int attackPower
) : Creature(sign, speed, hp)
{
    public int AttackPower { get; protected set; } = attackPower;

    protected override Point? FindTarget(Map map)
    {
        var point = map.GetPoint(this);

        if (point is null)
        {
            return null;
        }

        return map.FindNearestHerbivore(point);
    }

    protected override void Attack(Map map, Point point)
    {
        var entity = map.GetEntity(point);

        if (entity is Herbivore herbivore)
        {
            herbivore.TakeDamage(AttackPower);

            if (herbivore.Hp <= 0)
            {
                map.RemoveEntity(point);
                Hp = Math.Min(Hp + 10, EntityDefaultParameters.DefaultHp);
            }
        }
    }
}
