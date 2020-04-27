using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class User
    {
        public string name;
        public int score;

        public User(string _name)
        {
            name = _name;
            score = 0;
        }

        public User(string _name, int _score)
        {
            name = _name;
            score = _score;
        }
    }
}
