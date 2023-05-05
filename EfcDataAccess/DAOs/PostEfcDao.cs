using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao:IPostDao
{
    private readonly RedditContext context;

    public PostEfcDao(RedditContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post todo)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(todo);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Post todo)
    {
        context.Posts.Update(todo);
        await context.SaveChangesAsync();
    }

    public async Task<Post?> GetByIdAsync(int todoId)
    {
        Post? found = await context.Posts
            .Include(todo => todo.Owner)
            .SingleOrDefaultAsync(todo => todo.Id == todoId);
        return found;
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {id} not found");
        }

        context.Posts.Remove(existing);
        await context.SaveChangesAsync();
    }
}