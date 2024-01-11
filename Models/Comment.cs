using System;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string AuthorName { get; set; }
    public string AuthorEmail { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public int BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }
}
