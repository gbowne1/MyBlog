using System.Collections.Generic;
using MyBlog.Models;

namespace MyBlog.Services
{
    public interface IBlogPostService
    {
        IEnumerable<BlogPost> GetBlogPosts();
        BlogPost? GetBlogPostById(int id);
        void CreateBlogPost(BlogPost blogPost);
        void UpdateBlogPost(BlogPost blogPost);
        void DeleteBlogPost(int id);
    }
}
