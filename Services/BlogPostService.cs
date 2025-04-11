using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogPostService(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public IEnumerable<BlogPost> GetBlogPosts()
        {
            return _blogDbContext.BlogPosts.ToList();
        }

        public BlogPost? GetBlogPostById(int id)
        {
            return _blogDbContext.BlogPosts.FirstOrDefault(bp => bp.Id == id);
        }

        public void CreateBlogPost(BlogPost blogPost)
        {
            _blogDbContext.BlogPosts.Add(blogPost);
            _blogDbContext.SaveChanges();
        }

        public void UpdateBlogPost(BlogPost blogPost)
        {
            _blogDbContext.BlogPosts.Update(blogPost);
            _blogDbContext.SaveChanges();
        }

        public void DeleteBlogPost(int id)
        {
            var blogPost = _blogDbContext.BlogPosts.FirstOrDefault(bp => bp.Id == id);
            if (blogPost != null)
            {
                _blogDbContext.BlogPosts.Remove(blogPost);
                _blogDbContext.SaveChanges();
            }
        }
    }
}
