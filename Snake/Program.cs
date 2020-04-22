using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            int userPoints = 0; // POINTS
            Random randomNumbersGenerator = new Random(); // RANDOM NUMBER
            Console.BufferHeight = Console.WindowHeight;


            // INITIALISE & DISPLAY 5 OBSTACLES
            // added to ObstacleList list
            ObstacleList ObstacleList = new ObstacleList();
            for(int i = 0; i < 5; i++) 
            {
                ObstacleList.AddObstacle(new Obstacle(new Position(randomNumbersGenerator.Next(5, Console.WindowHeight), randomNumbersGenerator.Next(5, Console.WindowWidth))));
            }
            foreach (Obstacle ob in ObstacleList.Obstacles) { ob.Display(); } // Dislay Obstacles


            // INITIALISE SNAKE ELEMENTS
            Snake snake = new Snake();

            // INITIALISE SNAKE DIRECTION
            Directions direction = new Directions(Arrow.right);

            // INITIALISE FOOD POSITION
            Food food = new Food(); ;
            food.UpdateFoodPosition(snake, ObstacleList, randomNumbersGenerator);
            food.Display();


            // PROGAM STARTS HERE
            while (true)
            {
                //negativePoints++;

                // Update Snake's current direction when a key is pressed
                if (Console.KeyAvailable) direction.ChangeDirection();
                
                // Update Snake's position
                Position snakeNewHead = snake.UpdatePosition(direction.CurrentDirection);
                
                //  GAME OVER 
                if (snake.SnakeElements.Contains(snakeNewHead) || ObstacleList.Position.Contains(snakeNewHead))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    //userPoints = (snake.CountElements() - 6) * 100 - negativePoints;
                    Console.WriteLine("Game over!");
                    //if (userPoints < 0) userPoints = 0;
                    userPoints = Math.Max(userPoints, 0);
                    Console.WriteLine("Your points are: {0}", userPoints);
                    Console.WriteLine("Press Enter to exit game");// new update: pause the game and end the game by pressing Enter.
                    Console.ReadLine();
                    
					
					// STORES PLAYER'S DATA IN "UserData.txt"
                    try
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "userData.txt");
                        StreamWriter user;
                        if (!File.Exists(path))
                        {
                            user = File.CreateText(path);
                        } else
                        {
                            user = File.AppendText(path);
                        }
                        user.WriteLine("SCORE: " + userPoints + "\tDATE/TIME: " + DateTime.Now); // Player score and current datetime
                        user.Close();
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine("Exception: " + err.Message);
                    }
					
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
                    userPoints += 100;
                   
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
                        //negativePoints = 50;
                        Console.SetCursorPosition(food.Col, food.Row);
                        Console.Write(" ");
                        food.UpdateFoodPosition(snake, ObstacleList, randomNumbersGenerator);
                        userPoints -= 50;

                    }
                }

                food.Display();
                snake.SleepTime -= 0.01; // Increase Snake's speed
                Thread.Sleep((int)snake.SleepTime); // Update Program's speed 
                userPoints = Math.Max(userPoints, 0);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Your points are:    ");
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Your points are: {0}", userPoints);
            }
        }
    }
}
