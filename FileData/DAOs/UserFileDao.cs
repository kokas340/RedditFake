using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Shared.Dtos;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<User?> GetAsync(UserLoginDto loginUser)
    {
        User user = null;
        if (loginUser.UserName != null && loginUser.Password != null)
        {
             user  = context.Users.FirstOrDefault(u => 
                u.UserName.Contains(loginUser.UserName, StringComparison.OrdinalIgnoreCase) && u.Password.Contains(loginUser.Password, StringComparison.Ordinal));
        }
        return Task.FromResult(user);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Id == id
        );
        return Task.FromResult(existing);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return context.Users.ToList();
    }
}