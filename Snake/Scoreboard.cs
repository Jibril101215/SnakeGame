using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace Snake
{
    class Scoreboard
    {
        public List<User> users;
        public string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "userData.txt"); // Directory of data file
        

        public Scoreboard()
        {
            users = new List<User>();
            if (File.Exists(dataPath))
            {
                string[] lines = File.ReadAllLines(dataPath);
                foreach (string line in lines)
                {
                    string[] newLine = line.Split(' ');
                    users.Add(new User(newLine[0], Int32.Parse(newLine[1])));
                }
               
            } else
            {
                StreamWriter score = File.CreateText(dataPath);
                score.Close();
            }
        }


        public void updateScoreboard(User currUser)
        {
            if (users.Any())
            {
                foreach (User user in users)
                {
                    if (user.name == currUser.name)
                    {
                        user.score = currUser.score;
                        return;
                    }
                }
            }
            users.Add(currUser);
        }

        public void updateFile()
        {
            StreamWriter scores;
            scores = File.CreateText(dataPath);
            foreach (User user in users)
            {
                scores.WriteLine(user.name+' '+user.score);
            }

            scores.Close();
        }

        //Console.SetCursorPosition((Console.WindowWidth / 4) * 3, i);
    }
}
