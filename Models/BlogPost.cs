using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using MyBlog.Models;
public class BlogPost
{
	[Key]
	public int Id { get; set; }
	[Required]
	public string Title { get; set; }
	[Required]
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; }
	public int CategoryId { get; set; }
	public Category Category { get; set; }
	public ICollection<Tag> Tags { get; set; }
	public string ImageUrl { get; set; }
	[StringLength(500)]
	public string Summary { get; set; }
}
