using IITUforum.Data;
using IITUforum.Data.Models;
using IITUforum.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITUforum.Tests
{
    [TestFixture]
    public class PostServiceTests
    {
        [Test]
        public async Task Create_Post_Creates_New_Post_Via_Context()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);

                var post = new Post
                {
                    Title = "writing functional javascript",
                    Content = "some post content"
                };

                await postService.Add(post);
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                Assert.AreEqual(1, ctx.Posts.CountAsync().Result);
                Assert.AreEqual("writing functional javascript", ctx.Posts.SingleAsync().Result.Title);
            }
        }

        [TestCase("second post", 223)]
        [TestCase("first post", 1986)]
        [TestCase("third post", 12)]
        public void Get_Post_By_Id_Returns_Correct_Post(string postTitle, int postID)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post 
                { 
                    Id = 1986, 
                    Title = "first post" 
                });

                ctx.Posts.Add(new Post 
                { 
                    Id = 223, 
                    Title = "second post" 
                });

                ctx.Posts.Add(new Post 
                { 
                    Id = 12, 
                    Title = "third post" 
                });
                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetById(postID).Title;
                Assert.AreEqual(result, postTitle);
            }
        }


        [Test]
        public void Get_All_Posts_Returns_All_Posts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Posts.Add(new Post 
                { 
                    Id = 21341, 
                    Title = "first post" 
                });
                ctx.Posts.Add(new Post 
                { 
                    Id = 8144, 
                    Title = "second post" 
                });
                ctx.Posts.Add(new Post 
                { 
                    Id = 1245, 
                    Title = "third post" 
                });

                ctx.SaveChanges();
            }

            using (var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetAll().Count();
                Assert.AreEqual(3, result);
            }
        }


    }
}
