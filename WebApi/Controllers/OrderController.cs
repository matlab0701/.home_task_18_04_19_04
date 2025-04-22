using Domain.DTOs.Orders;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;



[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService):ControllerBase
{
     [HttpPost]
    public async Task<Response<GetOrderDto>> CreateAsync(CreateOrderDto request)
    {
        var user = await orderService.CreateAsync(request);
        return user;
    }
    [HttpGet("{Id:int}")]
    public async Task<Response<GetOrderDto>> GetOrderAsync(int Id)
    {
        var res = await orderService.GetOrderAsync(Id);
        return res;
    }
    [HttpGet]
    public async Task<Response<List<GetOrderDto>>> GetAllAsync(OrderFilters filters)
    {
        var res = await orderService.GetAllAsync(filters);
        return res;

    }
      [HttpPut("{Id:int}")]
    public async Task<Response<GetOrderDto>> UpDateAsync(int Id, UpdateOrderDto request)
    {
        var res = await orderService.UpDateAsync(Id, request);
        return res;

    }
    [HttpDelete("{Id:int}")]
    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var res = await orderService.DeleteAsync(Id);
        return res;

    }
}
