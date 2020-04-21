using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;


namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            int negativePoints = 0; // POINTS
            Random randomNumbersGenerator = new Random(); // RANDOM NUMBER
            Console.BufferHeight = Console.WindowHeight;


            // INITIALISE & DISPLAY 5 OBSTACLES
            // added to ObstacleList list
            ObstacleList ObstacleList = new ObstacleList();
            ObstacleList.AddObstacle(new Obstacle(new Position(12, 12)));
            ObstacleList.AddObstacle(new Obstacle(new Position(14, 20)));
            ObstacleList.AddObstacle(new Obstacle(new Position(7, 7)));
            ObstacleList.AddObstacle(new Obstacle(new Position(19, 19)));
            ObstacleList.AddObstacle(new Obstacle(new Position(6, 9)));
            foreach (Obstacle ob in ObstacleList.Obstacles) { ob.Display(); } // Dislay Obstacles


            // INITIALISE SNAKE ELEMENTS
            Snake snake = new Snake();
            for (int i = 0; i <= 5; i++)
            {
                snake.AddElement(new Position(0, i));
                snake.Display();
            }

            // INITIALISE SNAKE DIRECTION
            Directions direction = new Directions(Arrow.right);

            // INITIALISE FOOD POSITION
            Food food = new Food(); ;
            food.UpdateFoodPosition(snake, ObstacleList, randomNumbersGenerator);
            food.Display();


            // PROGAM STARTS HERE
            while (true)
            {
                negativePoints++;

                // Update Snake's current direction when a key is pressed
                if (Console.KeyAvailable) direction.ChangeDirection();
                
                // Update Snake's position
                Position snakeNewHead = snake.UpdatePosition(direction.CurrentDirection);
                
                //  GAME OVER 
                if (snake.SnakeElements.Contains(snakeNewHead) || ObstacleList.Position.Contains(snakeNewHead))
                {
                    string s1 = "Game over!";
                    string s2 = "Your points are: {0}";
                    Console.SetCursorPosition((Console.WindowWidth - s1.Length) / 2, (Console.WindowHeight -2) /2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    Console.WriteLine("Game over!");
                    int userPoints = (snake.CountElements() - 6) * 100 - negativePoints;
                    //if (userPoints < 0) userPoints = 0;
                    userPoints = Math.Max(userPoints, 0);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, ((Console.WindowHeight) / 2));
                    Console.WriteLine("Your points are: {0}", userPoints);
                    return;
                }

                snake.Display();

                // Update Snake's Head
                snake.AddElement(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Gray;
                if (direction.Arrow == Arrow.right) Console.Write(">");
                if (direction.Arrow == Arrow.left) Console.Write("<");
                if (direction.Arrow == Arrow.up) Console.Write("^");
                if (direction.Arrow== Arrow.down) Console.Write("v");

                // WHEN FOOD IS EATEN
                if (snakeNewHead == food.Pos)
                {
                    // Reposition Food after eaten
                    food.UpdateFoodPosition(snake, ObstacleList, randomNumbersGenerator);
                    
                    // Randomly place new obstacle
                    ObstacleList.PositionNewObstacle(snake, food, randomNumbersGenerator);
                }

                // WHEN FOOD IS NOT EATEN
                else
                {
                    // moving...
                    Position last = snake.SnakeElements.Dequeue();
                    Console.SetCursorPosition(last.col, last.row);
                    Console.Write(" ");

                    // REPOSITION FOOD IF NOT EATEN
                    if (Environment.TickCount - food.LastFoodTime >= food.DisappearTime)
                    {
                        negativePoints = negativePoints + 50; 
                        Console.SetCursorPosition(food.Col, food.Row);
                        Console.Write(" ");
                        food.UpdateFoodPosition(snake, ObstacleList, randomNumbersGenerator);
                    }
                }

                food.Display();
                snake.SleepTime -= 0.01; // Increase Snake's speed
                Thread.Sleep((int)snake.SleepTime); // Update Program's speed
            }
        }
    }
}
