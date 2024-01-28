using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using MyBlog.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string Bio { get; set; }
    public string Location { get; set; }
    public string Website { get; set; }
    public ICollection<BlogPost> Posts { get; set; }
}
