using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    public abstract class Weapon : Mover
    {
        public bool PickedUp { get; private set; }
        public abstract Weapons Name { get; }

        public Weapon(Game game, Point location) : base(game, location)
        {
            PickedUp = false;
        }

        public abstract void Attack(Direction direction);

        public void PickUpWeapon()
        {
            PickedUp = true;
        }

        protected bool DamageEnemy(Direction direction, int radius, int damage)
        {
            var target = game.PlayerLocation;
            for (var i = 0; i <= radius/10; i++)
            {
                foreach (var enemy in game.Enemies)
                {
                    if (enemy.Dead) { continue; }
                    if (!Nearby(target, enemy.Location, 5)) { continue; }
                    return Hit(enemy, damage);
                }
                target = Move(direction, target);
            }
            return false;
        }

        private static bool Hit(Enemy enemy, int damage)
        {
            enemy.Hit(damage);
            return true;
        }
    }
}
