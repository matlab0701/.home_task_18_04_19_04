using Domain.DTOs.Restauronts;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;



[ApiController]
[Route("api/[controller]")]
public class RestaurantController(IRestaurontService restaurontService) : ControllerBase
{
     [HttpPost]
    public async Task<Response<GetRestaurantDto>> CreateAsync(CreateRestaurantDto request)
    {
        var user = await restaurontService.CreateAsync(request);
        return user;
    }
    [HttpGet("{Id:int}")]
    public async Task<Response<GetRestaurantDto>> GetRestaurontAsync(int Id)
    {
        var res = await restaurontService.GetRestaurontAsync(Id);
        return res;
    }
    [HttpGet]
    public async Task<Response<List<GetRestaurantDto>>> GetAllAsync(RestaurontFilters filters)
    {
        var res = await restaurontService.GetAllAsync(filters);
        return res;

    }
      [HttpPut("{Id:int}")]
    public async Task<Response<GetRestaurantDto>> UpDateAsync(int Id, UpdateRestaurantDto request)
    {
        var res = await restaurontService.UpDateAsync(Id, request);
        return res;

    }
    [HttpDelete("{Id:int}")]
    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var res = await restaurontService.DeleteAsync(Id);
        return res;

    }
}
