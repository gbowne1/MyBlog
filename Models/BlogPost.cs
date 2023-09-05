using System;
using System.Collections.Generic;
public class BlogPost
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; }
	public int CategoryId { get; set; }
	public Category Category { get; set; }
	public ICollection<Tag> Tags { get; set; }
}
