using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class Category
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; }
	public ICollection<BlogPost> BlogPosts { get; set; }
}
