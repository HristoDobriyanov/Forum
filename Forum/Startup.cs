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

            var categories = context.Categories
                .Include(c => c.Posts)
                .ThenInclude(p => p.Author);

            foreach (var cat in categories)
            {
                Console.WriteLine($"{cat.Name} {cat.Posts.Count}");

                foreach (var post in cat.Posts)
                {
                    Console.WriteLine($"--{post.Title}: {post.Content}");
                    Console.WriteLine($"--by {post.Author.Username}");


                    foreach (var reply in post.Replies)
                    {
                        Console.WriteLine($"-----{reply.Content} from {reply.Author.Username}");
                    }
                }

            }


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
                new Post("C# Problem", "Not good", categories[0], users[0]),
                new Post("Jupiter Notebook", "Some help needed", categories[2], users[1]),
                new Post("Support wanted", "Pls Help", categories[1], users[2]),
            };

            context.Posts.AddRange(posts);


            var replies = new[] {
                new Reply(content:"Stupid post", post: posts[0], author:users[1]),
                new Reply(content:"I can help you", post: posts[1], author:users[2]),
            };

            context.Replies.AddRange(replies);

            context.SaveChanges();
        }
    }
}
