namespace Domain.DTOs;

public class PostUpdateDto
{
    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    public PostUpdateDto(int id)
    {
        Id = id;
    }
}