using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class Tag
{
	public int Id { get; set; }
	public string Name { get; set; }
	public ICollection<BlogPost> BlogPosts { get; set; }
}
