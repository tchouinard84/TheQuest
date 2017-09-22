using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    public class Bat : Enemy
    {
        private const int ATTACK_DAMAGE = 2;
        private const int HIT_POINTS = 6;

        public Bat(Game game, Point location) : base(game, location, HIT_POINTS) { }

        public override void Move()
        {
            if (HitPoints < 1) { return; }

            Move(DetermineDirection());
            MaybeAttack();
        }

        private void MaybeAttack()
        {
            if (!NearPlayer()) { return; }
            game.HitPlayer(ATTACK_DAMAGE);
        }

        private Direction DetermineDirection()
        {
            var moveToward = game.random.Next(2);
            if (moveToward == 1) { return FindPlayerDirection(game.PlayerLocation); }
            return (Direction)game.random.Next(4);
        }
    }
}
