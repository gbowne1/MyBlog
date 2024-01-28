using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models;

namespace MyBlog.Models {
public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
