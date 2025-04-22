using Domain.DTOs;
using Domain.DTOs.Menues;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MenuController(IMenuService menuService) : ControllerBase
{
     [HttpPost]
    public async Task<Response<GetMenuDto>> CreateAsync(CreateMenuDto request)
    {
        var user = await menuService.CreateAsync(request);
        return user;
    }
    [HttpGet("{Id:int}")]
    public async Task<Response<GetMenuDto>> GetMenurAsync(int Id)
    {
        var res = await menuService.GetMenuAsync(Id);
        return res;
    }
    [HttpGet]
    public async Task<Response<List<GetMenuDto>>> GetAllAsync(MenuFilters filters)
    {
        var res = await menuService.GetAllAsync(filters);
        return res;

    }
      [HttpPut("{Id:int}")]
    public async Task<Response<GetMenuDto>> UpDateAsync(int Id, UpdateMenuDto request)
    {
        var res = await menuService.UpDateAsync(Id, request);
        return res;

    }
    [HttpDelete("{Id:int}")]
    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var res = await menuService.DeleteAsync(Id);
        return res;

    }
}
