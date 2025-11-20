using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models
{
    // Inherit from the base IdentityUser class
    public class ApplicationUser : IdentityUser
    {
        // TODO Add the custom properties here:

        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;

        // A property that tracks when the user created their account
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;

        // TODO also add navigation properties for relationships, e.g.:
        // public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}
