using Microsoft.AspNetCore.Mvc;
using MyBlog.Services;
using MyBlog.Models;

public class AboutController : Controller
{
    private readonly IAboutContentService _aboutContentService;

    public AboutController(IAboutContentService aboutContentService)
    {
        _aboutContentService = aboutContentService;
    }

    public IActionResult Index()
    {
        var aboutContent = _aboutContentService.GetAboutContent(); // Call service to retrieve content
        var model = new AboutModel
        {
            Title = "About My Blog",
            Content = "Welcome to my blog, where I share my experiences and insights on software development, programming, and coding. As a hobbyist, I bring a fresh and relatable perspective to these topics, and I aim to provide informative and educational content that helps my readers learn new skills, solve problems, and stay up-to-date with the latest trends and technologies in the field.",
            Author = "John Doe", // Example for additional property
            SocialMediaLink = "https://..." // Example for additional property
        };

        return View(model);
    }
}
