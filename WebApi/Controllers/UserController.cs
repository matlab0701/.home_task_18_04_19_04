using Domain.DTOs.Users;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) :ControllerBase
{
     [HttpPost]
    public async Task<Response<GetUserDto>> CreateAsync(CreateUserDto request)
    {
        var user = await userService.CreateAsync(request);
        return user;
    }
    [HttpGet("{Id:int}")]
    public async Task<Response<GetUserDto>> GetUserAsync(int Id)
    {
        var res = await userService.GetUserAsync(Id);
        return res;
    }
    [HttpGet]
    public async Task<Response<List<GetUserDto>>> GetAllAsync(UserFilters filters)
    {
        var res = await userService.GetAllAsync(filters);
        return res;

    }
      [HttpPut("{Id:int}")]
    public async Task<Response<GetUserDto>> UpDateAsync(int Id, UpdateUserDto request)
    {
        var res = await userService.UpDateAsync(Id, request);
        return res;

    }
    [HttpDelete("{Id:int}")]
    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var res = await userService.DeleteAsync(Id);
        return res;

    }
}
