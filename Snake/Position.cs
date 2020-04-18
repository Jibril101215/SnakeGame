using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public struct Position
    {
        public int row;
        public int col;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        /// <summary>
        /// Overloaded operator "==" that compares 2 Position for equality
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool operator ==(Position c1, Position c2)
        {
            return c1.Equals(c2);
        }


        /// <summary>
        /// Overloaded operator "!=" that compares 2 Position for equality
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool operator !=(Position c1, Position c2)
        {
            return !c1.Equals(c2);
        }
    }

    
}
