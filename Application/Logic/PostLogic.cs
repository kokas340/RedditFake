using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        //Get User that will be owner of post
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
            throw new Exception($"User with id {dto.OwnerId} was not found.");

        //Store Post in a variable
        Post post = new Post(user, dto.Title, dto.Description);

        //Validate if Post meets the parameters
        ValidatePost(post);

        //Create Post
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        //Get Post by Search Parameters
        return postDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(PostUpdateDto dto)
    {
        
        //Try to get post by ID
        Post? existing = await postDao.GetByIdAsync(dto.Id);

        //If post does not exists
        if (existing == null)
        {
            throw new Exception($"Post with ID {dto.Id} not found!");
        }

        
        User? user = null;
        //Check if post has Owner
        if (dto.OwnerId != null)
        {
            //Get Owner of Post
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        //Same User has the original Owner
        User userToUse = user ?? existing.Owner;
        //Check if title changed
        string titleToUse = dto.Title ?? existing.Title;
        //Check if description changed
        string descriptionToUse = dto.Description ?? existing.Description;
        
        //Store the new Updated Post in a variable
        Post updated = new (userToUse, titleToUse, descriptionToUse)
        {
            Id = existing.Id,
        };

        //Validate the Updated Post
        ValidatePost(updated);

        //Update the Post
        await postDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        //Get Post by ID
        Post? post = await postDao.GetByIdAsync(id);
        //Check if Post with provided ID exists
        if (post == null)
            throw new Exception($"Post with ID {id} was not found!");
        
        //Delete Post
        await postDao.DeleteAsync(id);
    }

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        //Get Post by ID
        Post? post = await postDao.GetByIdAsync(id);
        //Check if Post with provided ID exists
        if (post == null)
            throw new Exception($"Post with id {id} not found");

        //Return the DTO Post
        return new PostBasicDto(post.Id, post.Owner.UserName, post.Title, post.Description);
    }

    private void ValidatePost(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title) && string.IsNullOrEmpty(dto.Description))
            throw new Exception("Title and description cannot be empty.");
    }
}