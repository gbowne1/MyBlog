using System.Collections.Generic;
using MyBlog.Models;

namespace MyBlog.Services // Assuming your services namespace is MyBlog.Services
{
    public interface IBlogPostService
    {
        // Define methods (functions) for blog post functionalities

        IEnumerable<BlogPost> GetBlogPosts(); // Retrieve all blog posts

        // Add other methods as needed, for example:
        BlogPost GetBlogPostById(int id); // Retrieve a specific blog post by ID
        void CreateBlogPost(BlogPost blogPost); // Create a new blog post
        void UpdateBlogPost(BlogPost blogPost); // Update an existing blog post
        void DeleteBlogPost(int id); // Delete a blog post by ID
    }
}
