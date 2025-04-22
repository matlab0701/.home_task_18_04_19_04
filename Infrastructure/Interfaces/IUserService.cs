using Domain.DTOs.Users;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<List<GetUserDto>>> GetAllAsync(UserFilters filter);
    Task<Response<GetUserDto>> CreateAsync(CreateUserDto request);
    Task<Response<GetUserDto>> GetUserAsync(int Id);
    Task<Response<GetUserDto>> UpDateAsync(int Id, UpdateUserDto request);
    Task<Response<string>> DeleteAsync(int Id);
}
