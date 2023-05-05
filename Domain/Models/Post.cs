namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public User Owner { get; private set;}
    public string Title { get; private set; }

    public string Description { get; private set; }

    public Post(User owner, string title, string description)
    {
        Owner = owner;
        Title = title;
        Description = description;
    }
    private Post(){}
}