using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public abstract class AnimatedObjects
    {
        /// <summary>
        /// Object's position
        /// </summary>
        protected Position position;

        /// <summary>
        /// Object's Symbol
        /// </summary>
        protected string symbol;

        /// <summary>
        /// Object's color
        /// </summary>
        protected ConsoleColor color;

        public AnimatedObjects() { }

        public AnimatedObjects(Position _position, string _symbol, ConsoleColor _color)
        {
            position = _position;
            symbol = _symbol;
            color = _color;
        }

        public Position Pos {
            get { return position;  }
            set { position = value;  } 
        }

        /// <summary>
        /// Returns the Column value of the AnimatedObject's Position
        /// </summary>
        public int Col
        {
            get { return position.col; }
        }


        /// <summary>
        /// Returns the Row value of the AnimatedObject's Position
        /// </summary>
        public int Row
        {
            get { return position.row; }
        }

        public string Symbol { 
            get { return symbol; }
            set { symbol = value; } 
        }
        public ConsoleColor Color {
            get { return color; }
            set { color = value; } 
        }

        /// <summary>
        /// Displays the objects property on screen.
        /// </summary>
        public virtual void Display()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(position.col, position.row);
            Console.Write(symbol);
        }
    }
}
