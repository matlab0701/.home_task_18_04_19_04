using Domain.DTOs.OrderDetails;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;



[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController(IOrderDetailService orderDetailService) :ControllerBase
{
     [HttpPost]
    public async Task<Response<GetOrderDetailDto>> CreateAsync(CreateOrderDetailDto request)
    {
        var user = await orderDetailService.CreateAsync(request);
        return user;
    }
    [HttpGet("{Id:int}")]
    public async Task<Response<GetOrderDetailDto>> GetOrderDetailAsync(int Id)
    {
        var res = await orderDetailService.GetOrderDetailAsync(Id);
        return res;
    }
    [HttpGet]
    public async Task<Response<List<GetOrderDetailDto>>> GetAllAsync(OrderDetailFilters filters)
    {
        var res = await orderDetailService.GetAllAsync(filters);
        return res;

    }
      [HttpPut("{Id:int}")]
    public async Task<Response<GetOrderDetailDto>> UpDateAsync(int Id, UpdateOrderDetailDto request)
    {
        var res = await orderDetailService.UpDateAsync(Id, request);
        return res;

    }
    [HttpDelete("{Id:int}")]
    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var res = await orderDetailService.DeleteAsync(Id);
        return res;

    }
}
