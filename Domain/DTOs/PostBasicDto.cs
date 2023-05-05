namespace Domain.DTOs;

public class PostBasicDto
{
    public int Id { get; }
    public string OwnerName { get; }
    public string Title { get; }
    public string Description { get;  }

    public PostBasicDto(int id, string ownerName, string title, string description)
    {
        Id = id;
        OwnerName = ownerName;
        Title = title;
        Description = description;
    }
}