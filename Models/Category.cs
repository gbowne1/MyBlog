using System;
using System.Collections.Generic;

public class Category
{
	public int Id { get; set; }
	public string Name { get; set; }
	public ICollection<BlogPost> BlogPosts { get; set; }
}
