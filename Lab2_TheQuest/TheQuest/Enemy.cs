using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheQuest.Direction;

namespace TheQuest
{
    public abstract class Enemy : Mover
    {
        private const int NEAR_PLAYER_DISTANCE = (int)(CommonConstants.GRID_INTERVAL * 2.5);

        public int HitPoints { get; private set; }
        public bool Dead => HitPoints <= 0;

        public Enemy(Game game, Point location, int hitPoints) : base(game, location)
        {
            HitPoints = hitPoints;
        }
        public abstract void Move();

        public void Hit(int maxDamage)
        {
            HitPoints -= game.random.Next(1, maxDamage);
        }

        protected bool NearPlayer()
        {
            return (Nearby(game.PlayerLocation, NEAR_PLAYER_DISTANCE));
        }

        protected Direction FindPlayerDirection(Point playerLocation)
        {
            if (playerLocation.X > location.X + 10) { return Right; }
            if (playerLocation.X < location.X - 10) { return Left; }
            if (playerLocation.Y < location.Y - 10) { return Up; }
            return Down;
        }
    }
}
