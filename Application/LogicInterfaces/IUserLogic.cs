using Domain.DTOs;
using Domain.Models;
using Shared.Dtos;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserLoginDto dto);
    public Task<User?> GetAsync(UserLoginDto user);

    public Task<List<User>> GetAllAsync();
}