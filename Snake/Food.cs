using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Food : AnimatedObjects 
    {
        private int lastFoodTime; // last time food was eaten
        private const int foodDissapearTime = 10000; // food time

        public Food()
        {
            lastFoodTime = Environment.TickCount;
            symbol = "@";
            color = ConsoleColor.Yellow;
        }

        public Food(Position _position,
            string _symbol = "@",
            ConsoleColor _color = ConsoleColor.Yellow) : base(_position, _symbol, _color) { }

        public int LastFoodTime
        {
            get { return lastFoodTime; }
        }

        public int DisappearTime
        {
            get { return foodDissapearTime; }
        }


        /// <summary>
        /// Position food randomly
        /// </summary>
        /// <param name="snake"></param>
        /// <param name="ObstacleList"></param>
        /// <param name="rand"></param>
        public void UpdateFoodPosition(Snake snake, ObstacleList ObstacleList, Random rand)
        {
            do
            {
                this.Pos = new Position(rand.Next(0, Console.WindowHeight),
                    rand.Next(0, Console.WindowWidth));
            }
            while (snake.SnakeElements.Contains(this.Pos) || ObstacleList.Position.Contains(this.Pos));

            lastFoodTime = Environment.TickCount;
        }


    }

    
}
