using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TheQuest.CommonConstants;

namespace TheQuest
{
    public abstract class Mover
    {
        protected Game game;
        protected Point location;
        public Point Location { get { return location; } }

        public Mover(Game game, Point location)
        {
            this.game = game;
            this.location = location;
        }

        public bool Nearby(Point locationToCheck, int distance)
        {
            return Nearby(location, locationToCheck, distance);
        }

        public bool Nearby(Point thisLocation, Point thatLocation, int distance)
        {
            var horizontalDistanceApart = Math.Abs(thisLocation.X - thatLocation.X);
            var verticalDistanceApart = Math.Abs(thisLocation.Y - thatLocation.Y);

            if (horizontalDistanceApart >= distance) { return false; }
            return verticalDistanceApart < distance;
        }

        public virtual void Move(Direction direction)
        {
            location = Move(direction, location);
        }

        public virtual Point Move(Direction direction, Point target)
        {
            var newLocation = target;
            switch (direction)
            {
                case Direction.Up:
                    if (newLocation.Y - GRID_INTERVAL >= game.Boundaries.Top) { newLocation.Y -= GRID_INTERVAL; }
                    break;
                case Direction.Down:
                    if (newLocation.Y + GRID_INTERVAL <= game.Boundaries.Bottom) { newLocation.Y += GRID_INTERVAL; }
                    break;
                case Direction.Left:
                    if (newLocation.X - GRID_INTERVAL >= game.Boundaries.Left) { newLocation.X -= GRID_INTERVAL; }
                    break;
                case Direction.Right:
                    if (newLocation.X + GRID_INTERVAL <= game.Boundaries.Right) { newLocation.X += GRID_INTERVAL; }
                    break;
                default:
                    break;
            }
            return newLocation;
        }
    }
}
