using AirGroup_Blog.Models;
using AirGroup_Blog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirGroup_Blog.Helpers
{
    public class MenuHelper
    {
        public static void ShowMenu()
        {
            Console.WriteLine("===================================================");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Create Blog Post");
            Console.WriteLine("2. List Blog Posts");
            Console.WriteLine("3. View Blog Post by ID");
            Console.WriteLine("4. Search Blog Posts");
            Console.WriteLine("5. Exit");

            Console.Write("Option: ");
        }

        public static async Task ActivateCreateOperationAsync(BlogService blogService)
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            Console.Write("Enter content: ");
            string content = Console.ReadLine();

            Console.Write("Enter tags (comma separated): ");
            string tagsInput = Console.ReadLine();

            List<string> tags = new List<string>(tagsInput.Split(','));

            int lastId = await blogService.GetLastBlogsIdAsync();

            var newBlog = new Blog(++lastId, title, content, tags);

            await blogService.AddBlogAsync(newBlog);
        }

        public static async Task ActivateShowBlogsOperationAsync(BlogService blogService)
        {
            var blogs = await blogService.GetAllBlogsAsync();

            if (blogs.Any())
            {
                foreach (var blog in blogs)
                {
                    ShowBlogDetails(blog);
                }
            }
        }

        public static async Task ActivateShowBlogByIdAsync(BlogService blogService)
        {
            Console.Write("Enter ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var blog = await blogService.GetBlogByIdAsync(id);

                if (blog is not null)
                {
                    ShowBlogDetails(blog);
                }
                else
                {
                    Console.WriteLine("No blog was found");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        public static async Task ActivateSearchBlogAsync(BlogService blogService)
        {
            Console.Write("Enter search query: ");
            string query = Console.ReadLine();
            var searchedBlogs = await blogService.SearchBlogsAsync(query);

            if (searchedBlogs.Any())
            {
                foreach (var searchedBlog in searchedBlogs)
                {
                    ShowBlogDetails(searchedBlog);
                }
            }
            else
            {
                Console.WriteLine("No blog was found");
            }
        }

        private static void ShowBlogDetails(Blog blog)
        {
            Console.WriteLine($"Id: {blog.Id}");
            Console.WriteLine($"Title: {blog.Title}");
            Console.WriteLine($"Description: {blog.Description}");
            if (blog.Tags.Any())
            {
                Console.WriteLine($"Tags: {string.Join(",", blog.Tags)}");
            }
            else
            {
                Console.WriteLine("Tags: No tags specified");
            }
            Console.WriteLine("________________________________");
        }
    }
}
