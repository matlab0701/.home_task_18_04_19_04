using Domain.DTOs.Couriers;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CourierController(ICourierService courierService) : ControllerBase
{
     [HttpPost]
    public async Task<Response<GetCourierDto>> CreateAsync(CreateCourierDto request)
    {
        var user = await courierService.CreateAsync(request);
        return user;
    }
    [HttpGet("{Id:int}")]
    public async Task<Response<GetCourierDto>> GetUserAsync(int Id)
    {
        var res = await courierService.GetUserAsync(Id);
        return res;
    }
    [HttpGet]
    public async Task<Response<List<GetCourierDto>>> GetAllAsync(CourierFilters filters)
    {
        var res = await courierService.GetAllAsync(filters);
        return res;

    }
      [HttpPut("{Id:int}")]
    public async Task<Response<GetCourierDto>> UpDateAsync(int Id, UpdateCourierDto request)
    {
        var res = await courierService.UpDateAsync(Id, request);
        return res;

    }
    [HttpDelete("{Id:int}")]
    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var res = await courierService.DeleteAsync(Id);
        return res;

    }
}
