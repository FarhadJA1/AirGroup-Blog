using AirGroup_Blog.Models;
using AirGroup_Blog.Resources;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirGroup_Blog.Services
{
    public class BlogService
    {
        public async Task AddBlogAsync(Blog blog)
        {
            var settings = new Settings();

            if (File.Exists(settings.FilePath))
            {
                var text = await File.ReadAllTextAsync(settings.FilePath);

                var blogs = JsonSerializer.Deserialize<List<Blog>>(text)!;

                blogs.Add(blog);

                var jsonData = JsonSerializer.Serialize(blogs);

                await File.WriteAllTextAsync(settings.FilePath, jsonData);
            }
            else
            {
                var blogs = new List<Blog>() { blog };

                using var dataSw = new StreamWriter(settings.FilePath, true);

                var jsonData = JsonSerializer.Serialize(blogs);

                dataSw.WriteLine(jsonData);
            }
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            var settings = new Settings();

            if (File.Exists(settings.FilePath))
            {
                var text = await File.ReadAllTextAsync(settings.FilePath);

                var blogs = JsonSerializer.Deserialize<List<Blog>>(text)!;

                return blogs;
            }
            else
            {
                return new List<Blog>(0);
            }
        }

        public async Task<List<Blog>> SearchBlogsAsync(string searchText)
        {
            var settings = new Settings();

            if (File.Exists(settings.FilePath))
            {
                var text = await File.ReadAllTextAsync(settings.FilePath);

                var blogs = JsonSerializer.Deserialize<List<Blog>>(text)!;

                return blogs
                    .Where(m => m.Title.ToLower().StartsWith(searchText.ToLower()) || 
                                m.Description.ToLower().StartsWith(searchText.ToLower()) ||
                                m.Tags.Any(x => x.ToLower().StartsWith(searchText.ToLower())))
                    .ToList();
            }
            else
            {
                return new List<Blog>(0);
            }            
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            var settings = new Settings();

            if (File.Exists(settings.FilePath))
            {
                var text = await File.ReadAllTextAsync(settings.FilePath);

                var blogs = JsonSerializer.Deserialize<List<Blog>>(text)!;

                return blogs.FirstOrDefault(m => m.Id == id);                    
            }
            else
            {
                return null;
            }
        }

        public async Task<int> GetLastBlogsIdAsync()
        {
            var settings = new Settings();

            if (File.Exists(settings.FilePath))
            {
                var text = await File.ReadAllTextAsync(settings.FilePath);

                var blogs = JsonSerializer.Deserialize<List<Blog>>(text)!;

                var lastBlog = blogs.OrderByDescending(m => m.Id).FirstOrDefault();

                return lastBlog.Id;
            }
            else
            {
                return 0;
            }
        }
    }
}
