namespace Domain.DTOs;

public class PostCreationDto
{
    public int OwnerId { get; }
    public string Title { get; }
    
    public String Description { get; }

    public PostCreationDto(int ownerId, string title, string description)
    {
        OwnerId = ownerId;
        Title = title;
        Description = description;
    }
}