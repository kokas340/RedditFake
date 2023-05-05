using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;

namespace EfcDataAccess.DAOs;

public class UserEfcDao:IUserDao
{
    private readonly RedditContext context;

    public UserEfcDao(RedditContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.UserName.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }
   
    public Task<User?> GetAsync(UserLoginDto searchParameters)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        return user;
    }

    public Task<List<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}