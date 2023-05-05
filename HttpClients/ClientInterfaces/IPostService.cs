using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(int? postId, string? userName, int? userId, string? titleContains, string? description);

    Task UpdateAsync(PostUpdateDto dto);

    Task<PostBasicDto> GetByIdAsync(int id);

    Task DeleteAsync(int id);
}