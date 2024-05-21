using System.Collections.Generic;

namespace AirGroup_Blog.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public Blog(int id, string title, string description, List<string> tags)
        {
            Id = id; 
            Title = title; 
            Description = description; 
            Tags = tags;
        }
    }
}
