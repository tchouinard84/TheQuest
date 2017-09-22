using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    public class Game
    {
        private int level = 0;
        private Player player;
        private Rectangle boundaries;
        internal Random random;

        public IEnumerable<Enemy> Enemies { get; private set; }
        public Weapon WeaponInRoom { get; private set; }
        public Point PlayerLocation { get { return player.Location; } }
        public int PlayerHitPoints { get { return player.HitPoints; } }
        public IEnumerable<Weapons> PlayerWeapons { get { return player.Weapons; } }
        public int Level { get { return level; } }
        public Rectangle Boundaries { get { return boundaries; } }

        public Game(Rectangle boundaries, Random random)
        {
            this.boundaries = boundaries;
            this.random = random;
            player = new Player(this, new Point(boundaries.Left + 10, boundaries.Top + 70));
        }

        public void Move(Direction direction)
        {
            player.Move(direction);
            foreach (var enemy in Enemies)
            {
                enemy.Move();
            }
        }
        public void Equip(Weapons weaponName)
        {
            player.Equip(weaponName);
        }
        public bool CheckPlayerInventory(Weapons weaponName)
        {
            return player.Weapons.Contains(weaponName);
        }
        public void HitPlayer(int maxDamage)
        {
            player.Hit(maxDamage);
        }
        public void IncreasePlayerHealth(int health)
        {
            player.IncreaseHealth(health);
        }
        public void Attack(Direction direction)
        {
            player.Attack(direction);
            foreach (var enemy in Enemies)
            {
                enemy.Move();
            }
        }

        public void NewLevel()
        {
            level++;
            switch (level)
            {
                case 1:
                    Enemies = new List<Enemy>()
                    {
                        new Bat(this, GetRandomLocation())
                    };
                    WeaponInRoom = new Sword(this, GetRandomLocation());
                    break;
            }
        }

        private Point GetRandomLocation()
        {
            return new Point(boundaries.Left + random.Next(boundaries.Right / 10 - boundaries.Left / 10) * 10,
                boundaries.Top + random.Next(boundaries.Bottom / 10 - boundaries.Top / 10) * 10);
        }
    }
}
