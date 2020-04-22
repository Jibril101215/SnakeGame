using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Media;
using System.IO;






namespace Snake
{


    class Program
    {
        
        static void Main(string[] args)
        {

            


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


            // INITIALISE USER POINTS
            int userPoints = 0;

            //INITIALISE SOUNDS
            string path = Path.Combine(Directory.GetCurrentDirectory(), "repeat.wav");
            SoundPlayer sound = new SoundPlayer(path);
            sound.PlayLooping();


           

            // PROGAM STARTS HERE
            while (userPoints<500)
            {


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

                    

                    string path2 = Path.Combine(Directory.GetCurrentDirectory(), "aww.wav");
                    SoundPlayer sound2 = new SoundPlayer(path2);
                    sound2.Play();
                    
                    Console.WriteLine(s1);
                    
                  
                    userPoints = Math.Max(userPoints, 0);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, ((Console.WindowHeight) / 2));
                    Console.WriteLine(s2, userPoints);
                    Console.SetCursorPosition((Console.WindowWidth - s2.Length) / 2, ((Console.WindowHeight+2) / 2));
                    Console.WriteLine("Press Enter to exit game");
                    Console.ReadLine();
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

            // STORES PLAYER'S DATA IN "UserData.txt"
            try
            {
                string path1 = Path.Combine(Directory.GetCurrentDirectory(), "userData.txt");
                StreamWriter user;
                if (!File.Exists(path1))
                {
                    user = File.CreateText(path1);
                }
                else
                {
                    user = File.AppendText(path1);
                }
                user.WriteLine("SCORE: " + userPoints + "\tDATE/TIME: " + DateTime.Now); // Player score and current datetime
                user.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine("Exception: " + err.Message);
            }

            string path3 = Path.Combine(Directory.GetCurrentDirectory(), "yay.wav");
            SoundPlayer sound3 = new SoundPlayer(path3);
            sound3.Play();

            string s3 = "You won! Your score is {0} \n";
            Console.SetCursorPosition((Console.WindowWidth - s3.Length) / 2, 0);

            Console.Write(s3, userPoints);
            Console.ReadLine();
            
        }
    }
}
