using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TheQuest
{
    public class Player : Mover
    {
        private Weapon equippedWeapon;
        private List<Weapon> inventory = new List<Weapon>();

        public int HitPoints { get; private set; }

        public IEnumerable<Weapons> Weapons
        {
            get
            {
                return inventory.Select(weapon => weapon.Name).ToList();
            }
        }

        public Player(Game game, Point location) : base(game, location)
        {
            HitPoints = 10;
        }

        public void Hit(int maxDamage)
        {
            HitPoints -= game.random.Next(1, maxDamage);
        }

        public void IncreaseHealth(int health)
        {
            HitPoints += game.random.Next(1, health);
        }

        public void Equip(Weapons weaponName)
        {
            foreach (var weapon in inventory)
            {
                if (weapon.Name == weaponName) { equippedWeapon = weapon; }
            }
        }

        public override void Move(Direction direction)
        {
            base.Move(direction);
            MaybePickUpWeaponInRoom();
        }

        private void MaybePickUpWeaponInRoom()
        {
            var weapon = game.WeaponInRoom;
            if (weapon.PickedUp) { return; }
            if (!Nearby(weapon.Location, CommonConstants.GRID_INTERVAL / 2)) { return; }

            inventory.Add(weapon);
            weapon.PickUpWeapon();
            MaybeEquipWeapon(weapon);
        }

        private void MaybeEquipWeapon(Weapon weapon)
        {
            if (inventory.Count != 1) { return; }
            Equip(weapon.Name);
        }

        public void Attack(Direction direction)
        {
            if (equippedWeapon == null) { return; }
            equippedWeapon.Attack(direction);
        }
    }
}
