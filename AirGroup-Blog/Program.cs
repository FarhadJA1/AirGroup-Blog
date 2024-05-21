using AirGroup_Blog.Helpers;
using AirGroup_Blog.Services;
using System;
using System.Threading.Tasks;

namespace AirGroup_Blog
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            BlogService blogService = new BlogService();
            bool exit = false;

            while (!exit)
            {
                MenuHelper.ShowMenu();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await MenuHelper.ActivateCreateOperationAsync(blogService);                        
                        break;
                    case "2":
                        await MenuHelper.ActivateShowBlogsOperationAsync(blogService);
                        break;
                    case "3":
                        await MenuHelper.ActivateShowBlogByIdAsync(blogService);
                        break;
                    case "4":
                        await MenuHelper.ActivateSearchBlogAsync(blogService);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

        }
    }
}
