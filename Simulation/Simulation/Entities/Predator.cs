
namespace Simulation.Entities;

internal class Predator : Creature
{
    public Predator(Coordinates coordinates, string sign, int speed, int hp, int attackPower)
        : base(coordinates, sign, speed, hp)
    {
        AttackPower = attackPower;
    }

    public int AttackPower { get; protected set; }

    protected override Coordinates? FindTarget(Map map) => map.FindNearestHerbivore(Coordinates);

    protected override void Attack(Coordinates coordinates, Map map)
    {
        var entity = map.GetEntity(coordinates);
        if (entity is Herbivore herbivore)
        {
            herbivore.TakeDamage(AttackPower);

            if (herbivore.Hp <= 0)
            {
                map.RemoveEntity(coordinates);
                Hp = Math.Min(Hp + 10, EntityDefaultParameters.DefaultHp);
            }
        }
    }
}
