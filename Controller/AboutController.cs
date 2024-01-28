using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

public class AboutController : Controller
{
	public IActionResult Index()
	{
		var model = new AboutModel
		{
			Title = "About My Blog",
			Content = "Welcome to my blog, where I share my experiences and insights on software development, programming, and coding. As a hobbyist, I bring a fresh and relatable perspective to these topics, and I aim to provide informative and educational content that helps my readers learn new skills, solve problems, and stay up-to-date with the latest trends and technologies in the field."
		};

		return View(model);
	}
}