using Forum.Data;
using Forum.Data.Models;
using System;

namespace Forum
{
    public class Startup
    {
        public static void Main(string[] args)
        {

            var context = new ForumDbContext();

            var user = new[]
            {
                new User(username:"Gosho", password:"123"),
                new User(username:"Ivan", password:"2223"),
                new User(username:"Pesho", password:"444"),
                new User(username:"Ginka", password:"111"),
                new User(username:"Canka", password:"321"),


            };


                        
        }
    }
}
