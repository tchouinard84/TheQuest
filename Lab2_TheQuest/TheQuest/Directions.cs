using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheQuest.Direction;

namespace TheQuest
{
    public class Directions
    {
        public static Direction Clockwise(Direction direction)
        {
            switch (direction) {
                case Up:
                    return Right;
                case Right:
                    return Down;
                case Down:
                    return Left;
            }
            return Up;
        }

        public static Direction CounterClockwise(Direction direction)
        {
            switch (direction)
            {
                case Up:
                    return Left;
                case Left:
                    return Down;
                case Down:
                    return Right;
            }
            return Up;
        }
    }
}
