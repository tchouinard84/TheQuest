using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    public class Sword : Weapon
    {
        private const int ATTACK_RADIUS = CommonConstants.GRID_INTERVAL * 1;
        private const int ATTACK_DAMAGE = 3;
        public override Weapons Name { get { return Weapons.Sword; } }

        public Sword(Game game, Point location) : base(game, location) { }

        public override void Attack(Direction direction)
        {
            if (DamageEnemy(direction)) { return; }
            if (DamageEnemy(Directions.Clockwise(direction))) { return; }
            DamageEnemy(Directions.CounterClockwise(direction));
        }

        private bool DamageEnemy(Direction direction)
        {
            return DamageEnemy(direction, ATTACK_RADIUS, ATTACK_DAMAGE);
        }
    }
}
