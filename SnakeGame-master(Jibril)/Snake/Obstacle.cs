using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Obstacle : AnimatedObjects
    {
        public Obstacle() 
        {
            symbol = "=";
            color = ConsoleColor.Cyan;
        }

        public Obstacle(Position _position,
            string _symbol = "=",
            ConsoleColor _color = ConsoleColor.Cyan) : base(_position, _symbol, _color) { }

        
    }
}
