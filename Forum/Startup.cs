using Forum.Data;
using Forum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Forum
{
    public class Startup
    {
        public static void Main(string[] args)
        {

            var context = new ForumDbContext();
            ResetDatabase(context);


        }

        private static void ResetDatabase(ForumDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            Seed(context);
        }

        private static void Seed(ForumDbContext context)
        {
            var users = new[]
            {
                new User(username:"Gosho", password:"123"),
                new User(username:"Ivan", password:"2223"),
                new User(username:"Pesho", password:"444"),
                new User(username:"Ginka", password:"111"),
                new User(username:"Canka", password:"321"),
            };

            context.Users.AddRange(users);



            var categories = new[] {
                new Category("C#"),
                new Category("Support"),
                new Category("Python"),
                new Category("EF Core")
            };


            context.Categories.AddRange(categories);

            var posts = new[] {
                new Post(),
                new Post(),
                new Post(),
                new Post(),
            };




            context.SaveChanges();
        }
    }
}
