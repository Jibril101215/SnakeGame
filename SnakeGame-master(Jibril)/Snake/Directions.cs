using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// An enumuration for Snake's direction
    /// RIGHT, LEFT, DOWN, UP
    /// </summary>
    public enum Arrow
    {
        right,
        left,
        down,
        up
    }

    public struct Directions
    {
        
        private static readonly Position[] Direction = {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
        };

        private Arrow direction;

        public Directions(Arrow _direction)
        {
            direction = _direction;
        }

        /// <summary>
        /// Returns the Position of the Snake's current direction
        /// </summary>
        public Position CurrentDirection 
        {
            get { return Direction[(int)direction]; }
        }

        /// <summary>
        /// Returns the enumeration value of Arrow
        /// </summary>
        public Arrow Arrow
        {
            get { return direction; }
        }

        /// <summary>
        /// Update's current direction of the snake
        /// </summary>
        public void ChangeDirection()
        {
            ConsoleKeyInfo userInput = Console.ReadKey();
            if (userInput.Key == ConsoleKey.LeftArrow)
            {
                if (direction != Arrow.right) direction = Arrow.left;
            }
            if (userInput.Key == ConsoleKey.RightArrow)
            {
                if (direction != Arrow.left) direction = Arrow.right;
            }
            if (userInput.Key == ConsoleKey.UpArrow)
            {
                if (direction != Arrow.down) direction = Arrow.up;
            }
            if (userInput.Key == ConsoleKey.DownArrow)
            {
                if (direction != Arrow.up) direction = Arrow.down;
            }
        }
    }
}
