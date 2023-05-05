namespace Domain.DTOs;

public class SearchPostParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    public int? PostId { get;}
    public string? DescriptionContains { get;}
    public string? TitleContains { get;}

    public SearchPostParametersDto(int? postId, string? username, int? userId, string? titleContains, string? descriptionContains)
    {
        PostId = postId;
        Username = username;
        UserId = userId;
        DescriptionContains = descriptionContains;
        TitleContains = titleContains;
    }
}